using System;
using System.Collections.Generic;
using DialogCommon.Manager;
using DialogCommon.Model;
using DialogCommon.Utils;
using DialogMaker.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace DialogMaker.UI.Panel
{
    public class MainSidePanel : DialogCommon.Manager.Panel
    {
        [SerializeField] private TMP_InputField _saveNameField;
        [SerializeField] private Button _createNewSceneButton;
        [SerializeField] private Button _saveScenarioButton;
        [SerializeField] private Button _returnToMainMenuButton;

        private IDialogSaveManager _dialogSaveManager;
        private IMakerManager _makerManager;
        private ISaveValues _saveValues;

        [Inject]
        public void Inject(IDialogSaveManager dialogSaveManager, IMakerManager makerManager, ISaveValues saveValues)
        {
            _dialogSaveManager = dialogSaveManager;
            _makerManager = makerManager;
            _saveValues = saveValues;
        }

        private void Start()
        {
            _createNewSceneButton.onClick.AddListener(OnCreateNewSceneClick);
            _saveScenarioButton.onClick.AddListener(OnSaveScenarioClick);
            _returnToMainMenuButton.onClick.AddListener(OnReturnToMainMenuClick);

            _saveNameField.text = _saveValues.OpenedScenarioName;
        }

        private void OnCreateNewSceneClick()
        {
            var node = _makerManager.SpawnNode(new DialogSceneModel
            {
                Id = Random.Range(int.MinValue, int.MaxValue),
                Answers = new List<AnswerModel>()
            });
            
            _makerManager.SelectNode(node);
        }

        private void OnSaveScenarioClick()
        {
            _dialogSaveManager.SaveDialog(_saveNameField.text, _makerManager.SerializeScenario(), _makerManager.SerializeMetadata());
        }

        private void OnReturnToMainMenuClick()
        {
            SceneManager.LoadScene(Scenes.MainMenu.GetName());
        }
    }
}