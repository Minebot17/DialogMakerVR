using System;
using DialogCommon.Manager;
using DialogMaker.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DialogMaker.UI.Panel
{
    public class EditSceneSidePanel : DialogCommon.Manager.Panel
    {
        [SerializeField] private TMP_InputField _mainTextField;
        [SerializeField] private Button _addAnswerButton;
        [SerializeField] private GameObject _answerEditElement;
        [SerializeField] private Transform _answerEditElementsParent;

        private DiContainer _container;
        private IMakerManager _makerManager;
        private IPanelManager _panelManager;
            
        private IDialogSceneNode _selectedNode;

        [Inject]
        public void Inject(DiContainer container, IMakerManager makerManager, IPanelManager panelManager)
        {
            _container = container;
            _makerManager = makerManager;
            _panelManager = panelManager;
        }
        
        private void Awake()
        {
            _mainTextField.onValueChanged.AddListener(OnMainTextChanged);
            _addAnswerButton.onClick.AddListener(OnAddAnswerClick);
            _makerManager.OnNodeSelected += OnNodeSelected;
        }

        private void OnAddAnswerClick()
        {
            // TODO add answer (spawn connector, add new answerEditElement)
        }

        private void OnMainTextChanged(string newValue)
        {
            _selectedNode.Text = newValue;
        }

        private void OnNodeSelected(IDialogSceneNode sceneNode)
        {
            _panelManager.OpenPanel<EditSceneSidePanel>();
            _selectedNode = sceneNode;
            _mainTextField.text = sceneNode.Text;
            
            foreach (Transform answerEditElement in _answerEditElementsParent) {
                Destroy(answerEditElement.gameObject);
            }

            foreach (var tuple in sceneNode.Connectors)
            {
                SpawnAnswerEditElement(tuple.Item2);
            }
        }

        private void SpawnAnswerEditElement(IDialogConnector connector)
        {
            // TODO spawn edit element
        }
    }
}