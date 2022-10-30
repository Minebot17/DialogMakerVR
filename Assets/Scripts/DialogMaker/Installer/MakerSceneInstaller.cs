using DialogCommon.Manager;
using UnityEngine;
using Zenject;

namespace DialogMaker.Installer
{
    public class MakerSceneInstaller : MonoInstaller<MakerSceneInstaller>
    {
        [SerializeField] private PanelContainer _panelContainer;
        [SerializeField] private Transform _editorCanvas;
        
        public override void InstallBindings()
        {
            Container.Bind<IPanelContainer>().FromInstance(_panelContainer).AsSingle();
            Container.Bind<IPanelManager>().To<PanelManager>().AsSingle();
            Container.Bind<Transform>().WithId("EditorCanvas").FromInstance(_editorCanvas).AsSingle();
        }
    }
}