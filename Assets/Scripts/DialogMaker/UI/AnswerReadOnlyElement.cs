using DialogMaker.Manager;
using DialogMaker.UI.Panel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DialogMaker.UI
{
    public class AnswerReadOnlyElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TMP_InputField _mainTextField;
        [SerializeField] private TextMeshProUGUI _connectedText;
        
        private IMakerManager _makerManager;

        [Inject]
        public void Inject(IMakerManager makerManager)
        {
            _makerManager = makerManager;
        }
        
        public void Initialize(IDialogConnector connector)
        {
            SetIndex(connector.Index);
            _mainTextField.text = connector.Text;
            SetConnection(connector.TransitionSceneId);
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        }
        
        public void SetIndex(int index)
        {
            _titleText.text = $"Answer {index}";
        }
        
        private void SetConnection(int sceneId)
        {
            _connectedText.text = sceneId != 0 ? _makerManager.FindNode(sceneId).Text : $"Not connected";
        }
    }
}