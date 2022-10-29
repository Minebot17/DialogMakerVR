using DialogCommon.Manager;
using DialogCommon.Utils;
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
        [SerializeField] private GameObject _dialogElementPrefab;

        private DiContainer _container;
        private IPanelManager _panelManager;
        private IDialogSaveManager _dialogSaveManager;
        private ISaveValues _saveValues;

        [Inject]
        private void Inject(
            DiContainer container, 
            IPanelManager panelManager, 
            IDialogSaveManager dialogSaveManager,
            ISaveValues saveValues)
        {
            _container = container;
            _panelManager = panelManager;
            _dialogSaveManager = dialogSaveManager;
            _saveValues = saveValues;
        }

        private void Start()
        {
            _createNewDialogButton.onClick.AddListener(OnCreateNewDialogClick);
            _backButton.onClick.AddListener(OnBackClick);
        }

        public override void Open()
        {
            base.Open();

            for (int i = 0; i < _dialogElementsParent.childCount; i++)
            {
                Destroy(_dialogElementsParent.GetChild(i).gameObject);
            }

            foreach (string savedDialogName in _dialogSaveManager.SavedDialogNames)
            {
                _container
                    .InstantiatePrefabForComponent<DialogsPanelElement>(_dialogElementPrefab, _dialogElementsParent)
                    .Initialize(savedDialogName);
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
    }
}