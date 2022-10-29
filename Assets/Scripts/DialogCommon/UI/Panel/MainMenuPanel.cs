using DialogCommon.Manager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DialogCommon.UI.Panel
{
    public class MainMenuPanel : Manager.Panel
    {
        [SerializeField] private Button _openDialogsButton;
        [SerializeField] private Button _exitButton;

        private IPanelManager _panelManager;

        [Inject]
        private void Inject(IPanelManager panelManager)
        {
            _panelManager = panelManager;
        }

        private void Start()
        {
            _openDialogsButton.onClick.AddListener(OnOpenDialogsClick);
            _exitButton.onClick.AddListener(OnExitClick);
        }

        private void OnOpenDialogsClick()
        {
            _panelManager.ClosePanel<MainMenuPanel>();
            _panelManager.OpenPanel<DialogsPanel>();
        }

        private void OnExitClick()
        {
            Application.Quit();
        }
    }
}