using DialogCommon.Model;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace DialogMaker.Manager
{
    public class DialogConnector : MonoBehaviour, IDialogConnector
    {
        [SerializeField] private TextMeshProUGUI _iconText;
        [SerializeField] private GameObject _answerLinePrefab;

        private DiContainer _container;
        private IMakerManager _makerManager;
        private Transform _editorCanvas;
        private Camera _camera;

        private IDialogSceneNode _parent;
        private IDialogSceneNode _answerLineNode;
        private LineRenderer _answerLine;
        private bool _answerLineNotSetted;
    
        public int TransitionSceneId { get; private set; }
        public string Text { get; set; }
        public int Index { get; private set; }
        
        [Inject]
        public void Inject(
            DiContainer container, 
            [Inject(Id = "EditorCanvas")] Transform editorCanvas, 
            IMakerManager makerManager,
            Camera camera
        ) {
            _container = container;
            _editorCanvas = editorCanvas;
            _makerManager = makerManager;
            _camera = camera;
        }

        public void Initialize(IDialogSceneNode parent, AnswerModel answerModel)
        {
            _parent = parent;
            UpdateIndex();
            _iconText.text = Index.ToString();
            TransitionSceneId = answerModel.ToDialogSceneId;
            Text = answerModel.MainText;
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent as RectTransform);
        }

        private void Update()
        {
            if (!_answerLineNotSetted)
            {
                return;
            }

            var worldPosition = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _answerLine.SetPositions(new []{ transform.position, new Vector3(worldPosition.x, worldPosition.y, 0) });
        }

        private void OnDestroy()
        {
            if (_answerLineNode as MonoBehaviour)
            {
                _answerLineNode.OnPositionChanged -= UpdateAnswerLinePositions;
            }

            if (_parent as MonoBehaviour)
            {
                _parent.OnPositionChanged -= UpdateAnswerLinePositions;
            }
        }

        public void UpdateIndex()
        {
            Index = _parent.Connectors.FindIndex(x => x.Item1 == gameObject) + 1;
            _iconText.text = Index.ToString();
        }

        public void SpawnNewAnswerLine()
        {
            if (_answerLine != null)
            {
                Destroy(_answerLine.gameObject);
            }
            
            SpawnAnswerLine();
            _answerLineNotSetted = true;
            _makerManager.ActiveAnswerLine = _answerLine;
            _makerManager.ActiveAnswerConnector = this;
        }

        public void SetAnswerLineNode(IDialogSceneNode sceneNode)
        {
            if (_answerLine == null)
            {
                SpawnAnswerLine();
            }

            _answerLineNode = sceneNode;
            UpdateAnswerLinePositions();
            _answerLineNode.OnPositionChanged += UpdateAnswerLinePositions;
            _parent.OnPositionChanged += UpdateAnswerLinePositions;
            TransitionSceneId = sceneNode.Id;

            if (_makerManager.ActiveAnswerLine == _answerLine)
            {
                _makerManager.ActiveAnswerLine = null;
                _makerManager.ActiveAnswerConnector = null;
            }
            
            _answerLineNotSetted = false;
        }

        public AnswerModel Serialize()
        {
            return new AnswerModel
            {
                MainText = Text,
                ToDialogSceneId = TransitionSceneId
            };
        }

        private void SpawnAnswerLine()
        {
            _answerLine = _container.InstantiatePrefabForComponent<LineRenderer>(_answerLinePrefab, _editorCanvas);
            _answerLine.transform.SetSiblingIndex(1);
        }

        private void UpdateAnswerLinePositions()
        {
            _answerLine.SetPositions(new []{ transform.position, (_answerLineNode as MonoBehaviour).transform.position });
        }
    }
}