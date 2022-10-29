using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogCommon.UI
{
    public class DialogsPanelElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _elementText;
        [SerializeField] private Button _playDialogButton;
        [SerializeField] private Button _editDialogButton;
        [SerializeField] private Button _deleteDialogButton;
        
        public void Initialize(string dialogName)
        {
            _elementText.SetText(dialogName);
            _playDialogButton.onClick.AddListener(OnPlayClick);
            _editDialogButton.onClick.AddListener(OnEditClick);
            _deleteDialogButton.onClick.AddListener(OnDeleteClick);
        }

        private void OnPlayClick()
        {
            // TODO open vr scene
        }
        
        private void OnEditClick()
        {
            // TODO open maker scene
        }
        
        private void OnDeleteClick()
        {
            // TODO remote from list
        }
    }
}