using DialogCommon.Manager;
using DialogCommon.Utils;
using DialogPlayer.UI.Panel;
using UnityEngine;
using Zenject;

namespace DialogPlayer.Manager
{
    public class PlayerManager : IPlayerManager, IInitializable
    {
        private readonly SaveFileDm _scenarioFile;
        private readonly IPanelManager _panelManager;

        public PlayerManager(SaveFileDm scenarioFile, IPanelManager panelManager) 
        {
            _scenarioFile = scenarioFile;
            _panelManager = panelManager;
        }
        
        public void Initialize()
        {
            if (_scenarioFile == null)
            {
                Debug.LogError("Nothing scenario to load");
                return;
            }
            
            _panelManager.OpenPanel<ScenePanel>().StartScenario(_scenarioFile);
        }
    }
}