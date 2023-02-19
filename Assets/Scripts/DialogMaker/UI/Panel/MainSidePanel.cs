using System;
using System.Collections.Generic;
using DialogCommon.Manager;
using DialogCommon.Model;
using DialogCommon.Model.Metadata;
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
        [SerializeField] private TMP_Dropdown _sceneNameDropdown;
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
            _sceneNameDropdown.value = (int) _makerManager.PlayerSceneId;
            _sceneNameDropdown.onValueChanged.AddListener(OnPlayerSceneChanged);
        }

        private void OnPlayerSceneChanged(int value)
        {
            _makerManager.PlayerSceneId = (PlayerScenes) value;
        }

        private void OnCreateNewSceneClick()
        {
            var newSceneModel = new DialogSceneModel
            {
                Id = Random.Range(int.MinValue, int.MaxValue),
                Answers = new List<AnswerModel>()
            };

            var newSceneMetadataModel = new DialogSceneMetadataModel
            {
                Id = newSceneModel.Id,
                NodePositionX = 0,
                NodePositionY = 0
            };
            
            var node = _makerManager.SpawnNode(newSceneModel, newSceneMetadataModel);
            
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