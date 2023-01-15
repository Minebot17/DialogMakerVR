using DialogCommon.Manager;
using DialogCommon.Model;
using DialogCommon.Utils;
using DialogPlayer.Manager;
using DialogPlayer.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace DialogPlayer.Installer
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        [SerializeField] private TMP_Text _patientText;
        [SerializeField] private AnswersContainer _answersContainer;
        
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
            Container.Bind<IPlayerManager>().To<PlayerManager>().AsSingle();

            var file = _saveManager.LoadDialog(_saveValues.OpenedScenarioName);
            Container.Bind<ScenarioModel>().FromInstance(file.ScenarioModel).AsSingle();
            Container.Bind<IAnswersContainer>().FromInstance(_answersContainer).AsSingle();

            Container.BindInterfacesTo<PlayerManager>().AsSingle();

            Container.Bind<TMP_Text>().WithId(InjectId.PatientText).FromInstance(_patientText).AsSingle();
        }
    }
}