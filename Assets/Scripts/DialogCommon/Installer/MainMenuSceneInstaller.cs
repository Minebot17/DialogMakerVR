using DialogCommon.Manager;
using UnityEngine;
using Zenject;

namespace DialogCommon.Installer
{
    public class MainMenuSceneInstaller : MonoInstaller<MainMenuSceneInstaller>
    {
        [SerializeField] private PanelContainer _panelContainer;
        
        public override void InstallBindings()
        {
            Container.Bind<IPanelContainer>().FromInstance(_panelContainer).AsSingle();
            Container.Bind<IPanelManager>().To<PanelManager>().AsSingle();
            Container.Bind<ISaveManager>().To<SaveManager>().AsSingle();
        }
    }
}
