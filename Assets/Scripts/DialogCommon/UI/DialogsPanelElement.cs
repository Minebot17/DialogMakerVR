using DialogCommon.Manager;
using DialogCommon.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace DialogCommon.UI
{
    public class DialogsPanelElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _elementText;
        [SerializeField] private Button _playDialogButton;
        [SerializeField] private Button _editDialogButton;
        [SerializeField] private Button _deleteDialogButton;

        private string _dialogName;
        private IDialogSaveManager _dialogSaveManager;
        private ISaveValues _saveValues;

        [Inject]
        public void Inject(IDialogSaveManager dialogSaveManager, ISaveValues saveValues)
        {
            _dialogSaveManager = dialogSaveManager;
            _saveValues = saveValues;
        }

        public void Initialize(string dialogName)
        {
            _dialogName = dialogName;
            _elementText.SetText(dialogName);
            _playDialogButton.onClick.AddListener(OnPlayClick);
            _editDialogButton.onClick.AddListener(OnEditClick);
            _deleteDialogButton.onClick.AddListener(OnDeleteClick);
        }

        private void OnPlayClick()
        {
            _saveValues.OpenedScenarioName = _dialogName;
            SceneManager.LoadScene(Scenes.PlayerScene.GetName());
        }
        
        private void OnEditClick()
        {
            _saveValues.OpenedScenarioName = _dialogName;
            SceneManager.LoadScene(Scenes.MakerScene.GetName());
        }
        
        private void OnDeleteClick()
        {
            _dialogSaveManager.DeleteDialog(_dialogName);
            Destroy(gameObject);
        }
    }
}