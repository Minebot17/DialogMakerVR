using System;
using DialogCommon.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DialogMaker.UI.Panel
{
    public class MainSidePanel : DialogCommon.Manager.Panel
    {
        [SerializeField] private TMP_InputField _saveNameField;
        [SerializeField] private Button _createNewSceneButton;
        [SerializeField] private Button _saveScenarioButton;
        [SerializeField] private Button _returnToMainMenuButton;

        private void Start()
        {
            _createNewSceneButton.onClick.AddListener(OnCreateNewSceneClick);
            _createNewSceneButton.onClick.AddListener(OnSaveScenarioClick);
            _createNewSceneButton.onClick.AddListener(OnReturnToMainMenuClick);
        }

        private void OnCreateNewSceneClick()
        {
            // TODO spawn new scenario and select it
        }

        private void OnSaveScenarioClick()
        {
            // TODO serialize and save
        }

        private void OnReturnToMainMenuClick()
        {
            SceneManager.LoadScene(Scenes.MainMenu.GetName());
        }
    }
}