using System.Linq;
using DialogCommon.Model;
using DialogCommon.Utils;
using DialogPlayer.UI;
using TMPro;
using Zenject;

namespace DialogPlayer.Manager
{
    public class PlayerManager : IPlayerManager, IInitializable
    {
        private readonly IAnswersContainer _answersContainer;
        private readonly ScenarioModel _scenarioModel;
        private readonly TMP_Text _patientText;
        
        public PlayerManager(
            IAnswersContainer answersContainer, 
            ScenarioModel scenarioModel,
            [Inject(Id = InjectId.PatientText)] TMP_Text patientText
        ) {
            _answersContainer = answersContainer;
            _scenarioModel = scenarioModel;
            _patientText = patientText;
        }
        
        public void Initialize()
        {
            var defaultDialog = _scenarioModel.Scenes.FirstOrDefault(scene => scene.Id == _scenarioModel.StartSceneId);
            if (defaultDialog == null)
            {
                // TODO error scene
                return;
            }
            
            StartDialog(defaultDialog);
        }

        private void StartDialog(DialogSceneModel dialogSceneModel)
        {
            _patientText.text = dialogSceneModel.MainText;
            _answersContainer.Setup(dialogSceneModel.Answers, OnSelectedAnswer);
        }

        private void OnSelectedAnswer(AnswerModel answerModel)
        {
            if (answerModel.ToDialogSceneId == 0)
            {
                // TODO end scene
                return;
            }

            var newDialog = _scenarioModel.Scenes.FirstOrDefault(scene => scene.Id == answerModel.ToDialogSceneId);
            if (newDialog == null)
            {
                // TODO error scene
                return;
            }
            
            StartDialog(newDialog);
        }
    }
}