using System.Linq;
using DialogCommon.Manager;
using DialogCommon.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace DialogCommon.UI.Panel
{
    public class DialogsPanel : Manager.Panel
    {
        [SerializeField] private Button _createNewDialogButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Transform _dialogElementsParent;
        [SerializeField] private Transform _dialogElementsReportsParent;
        [SerializeField] private GameObject _dialogElementPrefab;
        [SerializeField] private GameObject _scrollHeaderPrefab;
        [SerializeField] private TMP_InputField _userName;

        private DiContainer _container;
        private IPanelManager _panelManager;
        private IDialogSaveManager _dialogSaveManager;
        private IReportSaveManager _reportSaveManager;
        private ISaveValues _saveValues;

        [Inject]
        private void Inject(
            DiContainer container, 
            IPanelManager panelManager, 
            IDialogSaveManager dialogSaveManager,
            IReportSaveManager reportSaveManager,
            ISaveValues saveValues
        ) {
            _container = container;
            _panelManager = panelManager;
            _dialogSaveManager = dialogSaveManager;
            _reportSaveManager = reportSaveManager;
            _saveValues = saveValues;
        }

        private void Start()
        {
            _createNewDialogButton.onClick.AddListener(OnCreateNewDialogClick);
            _backButton.onClick.AddListener(OnBackClick);
            _userName.onValueChanged.AddListener(OnUserNameChanged);
            Open();
        }

        public override void Open()
        {
            base.Open();

            for (int i = 0; i < _dialogElementsParent.childCount; i++)
            {
                Destroy(_dialogElementsParent.GetChild(i).gameObject);
            }
            
            for (int i = 0; i < _dialogElementsParent.childCount; i++)
            {
                Destroy(_dialogElementsReportsParent.GetChild(i).gameObject);
            }

            var buildinScenarios = _dialogSaveManager.SavedDialogNames.Where(s => s.Contains("StreamingAssets")).ToList();
            var customScenarios = _dialogSaveManager.SavedDialogNames.Where(s => !s.Contains("StreamingAssets")).ToList();

            if (customScenarios.Count != 0)
            {
                _container.InstantiatePrefabForComponent<TMP_Text>(_scrollHeaderPrefab, _dialogElementsParent).text = "Custom scenarios";
                
                foreach (string savedDialogName in customScenarios)
                {
                    _container
                        .InstantiatePrefabForComponent<DialogsPanelElement>(_dialogElementPrefab, _dialogElementsParent)
                        .Initialize(savedDialogName, false, true);
                }
            }

            _container.InstantiatePrefabForComponent<TMP_Text>(_scrollHeaderPrefab, _dialogElementsParent).text = "Default scenarios";
            foreach (string savedDialogName in buildinScenarios)
            {
                _container
                    .InstantiatePrefabForComponent<DialogsPanelElement>(_dialogElementPrefab, _dialogElementsParent)
                    .Initialize(savedDialogName, false, false);
            }

            foreach (var savedReportName in _reportSaveManager.SavedReportNames)
            {
                _container
                    .InstantiatePrefabForComponent<DialogsPanelElement>(_dialogElementPrefab, _dialogElementsReportsParent)
                    .Initialize(savedReportName, true, true);
            }
        }

        private void OnCreateNewDialogClick()
        {
            _saveValues.OpenedScenarioName = string.Empty;
            SceneManager.LoadScene(Scenes.MakerScene.GetName());
        }

        private void OnBackClick()
        {
            _panelManager.ClosePanel<DialogsPanel>();
            _panelManager.OpenPanel<MainMenuPanel>();
        }

        private void OnUserNameChanged(string value)
        {
            _saveValues.UserName = value;
        }
    }
}