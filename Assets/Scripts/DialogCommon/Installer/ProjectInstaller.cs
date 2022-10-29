using DialogCommon.Manager;
using Zenject;

namespace DialogCommon.Installer
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ISaveValues>().To<SaveValues>().AsSingle();
            Container.Bind<IDialogSaveManager>().To<DialogSaveManager>().AsSingle();
        }
    }

}