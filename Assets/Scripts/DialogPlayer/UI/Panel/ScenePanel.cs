using System;
using System.Linq;
using DialogCommon.Manager;
using DialogCommon.Model;
using DialogPlayer.Manager;
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
        private IReportRecorder _reportRecorder;
        private IReportSaveManager _reportSaveManager;
        private ISaveValues _saveValues;

        private DialogSceneModel _currentDialog;

        [Inject]
        private void Construct(
            IPanelManager panelManager, 
            IReportRecorder reportRecorder, 
            IReportSaveManager reportSaveManager,
            ISaveValues saveValues)
        {
            _panelManager = panelManager;
            _reportSaveManager = reportSaveManager;
            _saveValues = saveValues;
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
            _reportRecorder.StartRecord(scenarioModel);
        }
        
        private void StartDialog(DialogSceneModel dialogSceneModel)
        {
            if (dialogSceneModel.Answers.Count == 0)
            {
                dialogSceneModel.Answers.Add(new AnswerModel { MainText = "До свидания", ToDialogSceneId = 0});
            }
            
            _patientText.text = dialogSceneModel.MainText;
            _answersContainer.Setup(dialogSceneModel.Answers, OnSelectedAnswer);
            _currentDialog = dialogSceneModel;
        }
        
        private void OnSelectedAnswer(int index, AnswerModel answerModel)
        {
            if (answerModel.ToDialogSceneId == 0)
            {
                _panelManager.ClosePanel<ScenePanel>();
                _panelManager.OpenPanel<EndScenePanel>();
                
                _reportSaveManager.SaveReport($"{_saveValues.OpenedScenarioName}_{_saveValues.UserName}_{DateTime.Now:hh:mm:ss}", _reportRecorder.EndRecord());
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
            _reportRecorder.RecordAnswer(_currentDialog.Id, index);
        }
    }
}