using System.Linq;
using DialogCommon.Manager;
using DialogCommon.Model;
using TMPro;
using UnityEngine;
using Zenject;

namespace DialogPlayer.UI.Panel
{
    public class ScenePanel : DialogCommon.Manager.Panel
    {
        [SerializeField] private AnswersContainer _answersContainer;
        [SerializeField] private TMP_Text _patientText;
        
        private ScenarioModel _scenarioModel;
        private IPanelManager _panelManager;

        [Inject]
        private void Construct(IPanelManager panelManager)
        {
            _panelManager = panelManager;
        }
        
        public void StartScenario(ScenarioModel scenarioModel)
        {
            _scenarioModel = scenarioModel;
            
            var defaultDialog = _scenarioModel.Scenes.FirstOrDefault(scene => scene.Id == _scenarioModel.StartSceneId);
            if (defaultDialog == null)
            {
                _panelManager.ClosePanel<ScenePanel>();
                _panelManager.OpenPanel<ErrorScenePanel>();
                return;
            }
            
            StartDialog(defaultDialog);
        }
        
        private void StartDialog(DialogSceneModel dialogSceneModel)
        {
            if (dialogSceneModel.Answers.Count == 0)
            {
                dialogSceneModel.Answers.Add(new AnswerModel { MainText = "До свидания", ToDialogSceneId = 0});
            }
            
            _patientText.text = dialogSceneModel.MainText;
            _answersContainer.Setup(dialogSceneModel.Answers, OnSelectedAnswer);
        }
        
        private void OnSelectedAnswer(AnswerModel answerModel)
        {
            if (answerModel.ToDialogSceneId == 0)
            {
                _panelManager.ClosePanel<ScenePanel>();
                _panelManager.OpenPanel<EndScenePanel>();
                return;
            }

            var newDialog = _scenarioModel.Scenes.FirstOrDefault(scene => scene.Id == answerModel.ToDialogSceneId);
            if (newDialog == null)
            {
                _panelManager.ClosePanel<ScenePanel>();
                _panelManager.OpenPanel<ErrorScenePanel>();
                return;
            }
            
            StartDialog(newDialog);
        }
    }
}