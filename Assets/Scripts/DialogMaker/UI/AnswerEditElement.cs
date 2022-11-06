using System;
using DialogMaker.Manager;
using DialogMaker.UI.Panel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DialogMaker.UI
{
    public class AnswerEditElement : MonoBehaviour
    {
        [SerializeField] private Button _removeButton;
        [SerializeField] private Button _moveUpButton;
        [SerializeField] private Button _moveDownButton;
        [SerializeField] private Button _connectButton;

        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TMP_InputField _mainTextField;
        [SerializeField] private TextMeshProUGUI _connectedText;

        private IMakerManager _makerManager;
        
        private EditSceneSidePanel _editPanel;
        private IDialogConnector _connector;

        public IDialogConnector Connector => _connector;

        [Inject]
        public void Inject(IMakerManager makerManager)
        {
            _makerManager = makerManager;
        }

        private void Start()
        {
            _removeButton.onClick.AddListener(OnRemoveClick);
            _moveUpButton.onClick.AddListener(OnMoveUpClick);
            _moveDownButton.onClick.AddListener(OnMoveDownClick);
            _connectButton.onClick.AddListener(OnConnectClick);
            _mainTextField.onValueChanged.AddListener(OnMainTextChanged);
        }

        public void Initialize(EditSceneSidePanel editPanel, IDialogConnector connector)
        {
            _editPanel = editPanel;
            _connector = connector;
            
            SetIndex(connector.Index);
            _mainTextField.text = connector.Text;
            SetConnection(connector.TransitionSceneId);
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        }
        
        public void SetIndex(int index)
        {
            _titleText.text = $"Answer {index}";
        }

        private void OnMainTextChanged(string newText)
        {
            _connector.Text = newText;
        }

        private void OnRemoveClick()
        {
            _editPanel.OnRemovedAnswerElement(this);
            Destroy(gameObject);
        }
        
        private void OnMoveUpClick()
        {
            // TODO
        }
        
        private void OnMoveDownClick()
        {
            // TODO
        }
        
        private void OnConnectClick()
        {
            // TODO
        }

        private void SetConnection(int sceneId)
        {
            _connectedText.text = sceneId != 0 ? _makerManager.FindNode(sceneId).Text : $"Not connected";
        }
    }
}