using System.Collections.Generic;
using System.Linq;
using DialogCommon.Model;
using DialogCommon.Model.Metadata;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace DialogMaker.Manager
{
    public class DialogSceneNode : MonoBehaviour, IDialogSceneNode, IPointerClickHandler, IPointerMoveHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private TextMeshProUGUI _nodeText;
        [SerializeField] private Transform _connectorsParent;
        [SerializeField] private GameObject _connectorPrefab;
        [SerializeField] private TextMeshProUGUI _defaultNodeText;
        [SerializeField] private Image _backgroundImage;
        
        private DiContainer _container;
        private IMakerManager _makerManager;
        private Camera _camera;
        
        private bool _isMouseDown;
        private bool _ignoreNextClick;
        private Vector2 _mouseDownRelativePosition;

        public int Id { get; private set; }
        public string Text
        {
            get => _nodeText.text;
            set => _nodeText.text = value;
        }

        public List<(GameObject, IDialogConnector)> Connectors { get; } = new();

        [Inject]
        public void Inject(DiContainer container, IMakerManager makerManager, Camera camera)
        {
            _container = container;
            _makerManager = makerManager;
            _camera = camera;
        }

        public void Initialize(DialogSceneModel sceneModel, DialogSceneMetadataModel metadata, bool isDefaultNode)
        {
            Id = sceneModel.Id;
            _nodeText.text = sceneModel.MainText;
            transform.position = new Vector3(metadata.NodePositionX, metadata.NodePositionY, 0);
            
            // spawn connectors
            foreach (var answerModel in sceneModel.Answers)
            {
                SpawnConnector(answerModel);
            }
            
            _defaultNodeText.gameObject.SetActive(isDefaultNode);
        }

        public void OnSelected(bool isSelected)
        {
            _backgroundImage.color = isSelected ? new Color(1, 0.8f, 0.8f) : Color.white;
        }

        public IDialogConnector CreateNewConnector()
        {
            return SpawnConnector(new AnswerModel { MainText = string.Empty, ToDialogSceneId = 0 });
        }

        public void RemoveConnector(IDialogConnector connector)
        {
            var index = Connectors.FindIndex(c => c.Item2 == connector);
            Destroy(Connectors[index].Item1);
            Connectors.RemoveAt(index);

            foreach (var tuple in Connectors)
            {
                tuple.Item2.UpdateIndex();
            }
        }

        public void MoveAnswer(IDialogConnector connector, bool toUp)
        {
            var connectorIndex = Connectors.FindIndex(c => c.Item2 == connector);

            if (connectorIndex == 0 && toUp || connectorIndex == (Connectors.Count - 1) && !toUp)
            {
                return;
            }

            var delta = toUp ? -1 : 1;
            Connectors[connectorIndex].Item1.transform.SetSiblingIndex(connectorIndex + delta);
            (Connectors[connectorIndex], Connectors[connectorIndex + delta]) 
                = (Connectors[connectorIndex + delta], Connectors[connectorIndex]);
            
            
            foreach (var tuple in Connectors)
            {
                tuple.Item2.UpdateIndex();
            }
        }

        public DialogSceneModel SerializeScene()
        {
            return new DialogSceneModel
            {
                Id = Id,
                MainText = Text,
                Answers = Connectors.Select(x => x.Item2.Serialize()).ToList()
            };
        }

        public DialogSceneMetadataModel SerializeMetadata()
        {
            return new DialogSceneMetadataModel
            {
                Id = Id,
                NodePositionX = transform.position.x,
                NodePositionY = transform.position.y
            };
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }
            
            if (_ignoreNextClick)
            {
                _ignoreNextClick = false;
                return;
            }
            
            _makerManager.SelectNode(this);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (!_isMouseDown)
            {
                return;
            }
            
            transform.SetAsLastSibling();
            _ignoreNextClick = true;
            var newPosition = _camera.ScreenToWorldPoint(eventData.position);
            transform.position = new Vector3(
                newPosition.x - _mouseDownRelativePosition.x, 
                newPosition.y - _mouseDownRelativePosition.y, 
                transform.position.z);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }
            
            _isMouseDown = true;
            var worldPosition = _camera.ScreenToWorldPoint(eventData.position);
            _mouseDownRelativePosition = new Vector3(worldPosition.x, worldPosition.y, 0) - transform.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }
            
            _isMouseDown = false;
        }

        private IDialogConnector SpawnConnector(AnswerModel answerModel)
        {
            var connector = _container.InstantiatePrefab(_connectorPrefab, _connectorsParent);
            var connectorComponent = connector.GetComponent<IDialogConnector>();
            Connectors.Add((connector, connectorComponent));
            connectorComponent.Initialize(this, answerModel);
            return connectorComponent;
        }
    }
}