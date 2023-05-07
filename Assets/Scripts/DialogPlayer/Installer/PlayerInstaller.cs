using DialogCommon.Manager;
using DialogCommon.Model;
using DialogCommon.Utils;
using DialogPlayer.Manager;
using UnityEngine;
using Zenject;

namespace DialogPlayer.Installer
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        [SerializeField] private PanelContainer _panelContainer;
        
        private IDialogSaveManager _saveManager;
        private ISaveValues _saveValues;
        
        [Inject]
        public void Construct(IDialogSaveManager saveManager, ISaveValues saveValues)
        {
            _saveManager = saveManager;
            _saveValues = saveValues;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<IPanelManager>().To<PanelManager>().AsSingle();

            var file = _saveManager.LoadDialog(_saveValues.OpenedScenarioName);
            Container.Bind<SaveFileDm>().FromInstance(file).AsSingle();
            Container.Bind<IPanelContainer>().FromInstance(_panelContainer).AsSingle();

            Container.Bind<IReportRecorder>().To<ReportRecorder>().AsSingle();
            Container.BindInterfacesTo<PlayerManager>().AsSingle();
        }
    }
}