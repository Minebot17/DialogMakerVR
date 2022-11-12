using DialogCommon.Manager;
using DialogMaker.Manager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace DialogMaker.Installer
{
    public class MakerSceneInstaller : MonoInstaller<MakerSceneInstaller>
    {
        [SerializeField] private PanelContainer _panelContainer;
        [SerializeField] private Transform _editorCanvas;
        [SerializeField] private CanvasScaler _canvasScaler;
        
        public override void InstallBindings()
        {
            var inputActions = new DefaultInputActions();
            inputActions.UI.Enable();
            
            Container.Bind<IPanelContainer>().FromInstance(_panelContainer).AsSingle();
            Container.Bind<IPanelManager>().To<PanelManager>().AsSingle();
            Container.BindInterfacesTo<MakerManager>().AsSingle();
            Container.Bind<Transform>().WithId("EditorCanvas").FromInstance(_editorCanvas).AsSingle();
            Container.Bind<Camera>().FromInstance(Camera.main).AsSingle();
            Container.Bind<DefaultInputActions>().FromInstance(inputActions).AsSingle();
            Container.Bind<CanvasScaler>().FromInstance(_canvasScaler).AsSingle();
        }
    }
}