using DialogCommon.Manager;
using DialogMaker.Manager;
using TMPro;
using UnityEngine;
using Zenject;

namespace DialogMaker.UI.Panel
{
    public class ReadOnlySceneSidePanel : DialogCommon.Manager.Panel
    {
        [SerializeField] private TMP_InputField _mainTextField;
        [SerializeField] private GameObject _answerReadOnlyElement;
        [SerializeField] private Transform _answerReadOnlyElementsParent;
        [SerializeField] private TMP_Text _dialogTimeText;
        
        private DiContainer _container;
        private IMakerManager _makerManager;
        private IPanelManager _panelManager;
        
        [Inject]
        public void Inject(DiContainer container, IMakerManager makerManager, IPanelManager panelManager)
        {
            _container = container;
            _makerManager = makerManager;
            _panelManager = panelManager;
        }
        
        private void Awake()
        {
            _makerManager.OnNodeSelected += OnNodeSelected;
        }
        
        private void OnNodeSelected(IDialogSceneNode sceneNode)
        {
            _panelManager.OpenPanel<ReadOnlySceneSidePanel>();
            _mainTextField.text = sceneNode.Text;
            
            foreach (Transform answerEditElement in _answerReadOnlyElementsParent) {
                Destroy(answerEditElement.gameObject);
            }

            foreach (var tuple in sceneNode.Connectors)
            {
                SpawnAnswerReadOnlyElement(tuple.Item2);
            }

            var spendTimeExists = _makerManager.ReportModel.DialogsTime.ContainsKey(sceneNode.Id);
            _dialogTimeText.gameObject.SetActive(spendTimeExists);
            
            if (spendTimeExists)
            {
                var span = _makerManager.ReportModel.DialogsTime[sceneNode.Id];
                _dialogTimeText.text = $"Dialog time: {span.Hours:D2}:{span.Minutes:D2}:{span.Seconds:D2}";
            }
        }

        private void SpawnAnswerReadOnlyElement(IDialogConnector connector)
        {
            var readOnlyElement = _container.InstantiatePrefabForComponent<AnswerReadOnlyElement>(_answerReadOnlyElement, _answerReadOnlyElementsParent);
            readOnlyElement.Initialize(connector);
        }
    }
}