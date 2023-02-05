using System;
using DialogCommon.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DialogPlayer.UI.Panel
{
    public class EndScenePanel : DialogCommon.Manager.Panel
    {
        [SerializeField] private Button _returnToMenuButton;

        private void Awake()
        {
            _returnToMenuButton.onClick.AddListener(OnReturnToMenuClick);
        }

        private void OnReturnToMenuClick()
        {
            SceneManager.LoadScene(Scenes.MainMenu.GetName());
        }
    }
}