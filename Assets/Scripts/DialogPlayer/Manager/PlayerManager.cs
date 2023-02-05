using DialogCommon.Manager;
using DialogCommon.Model;
using DialogPlayer.UI.Panel;
using UnityEngine;
using Zenject;

namespace DialogPlayer.Manager
{
    public class PlayerManager : IPlayerManager, IInitializable
    {
        private readonly ScenarioModel _scenarioModel;
        private readonly IPanelManager _panelManager;

        public PlayerManager(ScenarioModel scenarioModel, IPanelManager panelManager) 
        {
            _scenarioModel = scenarioModel;
            _panelManager = panelManager;
        }
        
        public void Initialize()
        {
            if (_scenarioModel == null)
            {
                Debug.LogError("Nothing scenario to load");
                return;
            }
            
            _panelManager.OpenPanel<ScenePanel>().StartScenario(_scenarioModel);
        }
    }
}