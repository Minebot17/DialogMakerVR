using System;
using System.Collections.Generic;
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
        [SerializeField] private Button _removeNodeButton;
        [SerializeField] private GameObject _answerEditElement;
        [SerializeField] private Transform _answerEditElementsParent;

        private DiContainer _container;
        private IMakerManager _makerManager;
        private IPanelManager _panelManager;
            
        private readonly List<AnswerEditElement> _answerElements = new();
        
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
            _removeNodeButton.onClick.AddListener(OnRemoveNodeClick);
            _makerManager.OnNodeSelected += OnNodeSelected;
        }
        
        public void OnRemovedAnswerElement(AnswerEditElement answerEditElement)
        {
            _answerElements.Remove(answerEditElement);
            _selectedNode.RemoveConnector(answerEditElement.Connector);
            
            foreach (AnswerEditElement element in _answerElements) {
                element.SetIndex(element.Connector.Index);
            }
        }

        public void OnMoveAnswer(AnswerEditElement answerEditElement, bool isUp)
        {
            _selectedNode.MoveAnswer(answerEditElement.Connector, isUp);
            
            for (int i = 0; i < _selectedNode.Connectors.Count; i++)
            {
                _answerElements[i].Initialize(this, _selectedNode.Connectors[i].Item2);
            }
        }

        private void OnAddAnswerClick()
        {
            SpawnAnswerEditElement(_selectedNode.CreateNewConnector());
        }

        private void OnRemoveNodeClick()
        {
            _selectedNode.RemoveNode();
            _selectedNode = null;
            _panelManager.ClosePanel<EditSceneSidePanel>();
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
            
            _answerElements.Clear();
            foreach (Transform answerEditElement in _answerEditElementsParent) {
                Destroy(answerEditElement.gameObject);
            }

            foreach (var tuple in sceneNode.Connectors)
            {
                SpawnAnswerEditElement(tuple.Item2);
            }

            _removeNodeButton.gameObject.SetActive(_makerManager.DefaultScene != sceneNode);
        }

        private void SpawnAnswerEditElement(IDialogConnector connector)
        {
            var editElement = _container.InstantiatePrefabForComponent<AnswerEditElement>(_answerEditElement, _answerEditElementsParent);
            editElement.Initialize(this, connector);
            _answerElements.Add(editElement);
        }
    }
}