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
        private IReportSaveManager _reportSaveManager;
        private ISaveValues _saveValues;
        private string _userName;
        private bool _isReport;

        [Inject]
        public void Inject(IDialogSaveManager dialogSaveManager, IReportSaveManager reportSaveManager, ISaveValues saveValues)
        {
            _dialogSaveManager = dialogSaveManager;
            _reportSaveManager = reportSaveManager;
            _saveValues = saveValues;
        }

        public void Initialize(string dialogName, bool isReport, bool canDelete)
        {
            _isReport = isReport;
            _dialogName = dialogName;
            _elementText.SetText(dialogName.Replace("StreamingAssets/", string.Empty));
            _playDialogButton.onClick.AddListener(OnPlayClick);
            _editDialogButton.onClick.AddListener(OnEditClick);
            _deleteDialogButton.onClick.AddListener(OnDeleteClick);

            if (isReport)
            {
                _editDialogButton.gameObject.SetActive(false);
            }

            if (!canDelete)
            {
                _deleteDialogButton.gameObject.SetActive(false);
            }
        }

        private void OnPlayClick()
        {
            _saveValues.OpenedScenarioName = _dialogName;

            if (_isReport)
            {
                SceneManager.LoadScene(Scenes.ReportViewScene.GetName());
                return;
            }
            
            var playerScene = _dialogSaveManager
                .LoadDialog(_saveValues.OpenedScenarioName).ScenarioModel.PlayerSceneId;
            SceneManager.LoadScene(playerScene.GetName());
        }
        
        private void OnEditClick()
        {
            _saveValues.OpenedScenarioName = _dialogName;
            SceneManager.LoadScene(Scenes.MakerScene.GetName());
        }
        
        private void OnDeleteClick()
        {
            if (!_isReport)
            {
                _dialogSaveManager.DeleteDialog(_dialogName);
            }
            else
            {
                _reportSaveManager.DeleteReport(_dialogName);
            }

            Destroy(gameObject);
        }
    }
}