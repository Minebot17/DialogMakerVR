using DialogCommon.Manager;
using UnityEngine;
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
        private ISaveManager _saveManager;

        [Inject]
        private void Inject(DiContainer container, IPanelManager panelManager, ISaveManager saveManager)
        {
            _container = container;
            _panelManager = panelManager;
            _saveManager = saveManager;
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

            foreach (string savedDialogName in _saveManager.SavedDialogNames)
            {
                _container
                    .InstantiatePrefabForComponent<DialogsPanelElement>(_dialogElementPrefab, _dialogElementsParent)
                    .Initialize(savedDialogName);
            }
        }

        private void OnCreateNewDialogClick()
        {
            _panelManager.ClosePanel<DialogsPanel>();
            _panelManager.OpenPanel<MainMenuPanel>();
        }

        private void OnBackClick()
        {
            Application.Quit();
        }
    }
}