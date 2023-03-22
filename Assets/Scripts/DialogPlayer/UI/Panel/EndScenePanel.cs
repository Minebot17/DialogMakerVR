using System;
using System.Collections;
using DialogCommon.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace DialogPlayer.UI.Panel
{
    public class EndScenePanel : DialogCommon.Manager.Panel
    {
        [SerializeField] private Button _returnToMenuButton;
        [SerializeField] private GameObject[] _objectsToEnable;
        [SerializeField] private GameObject[] _objectsToDisable;

        private void Awake()
        {
            _returnToMenuButton.onClick.AddListener(OnReturnToMenuClick);
        }

        private void OnReturnToMenuClick()
        {
            StartCoroutine(ReturnToMainMenuCoroutine());
        }

        private IEnumerator ReturnToMainMenuCoroutine()
        {
            foreach (var obj in _objectsToDisable)
            {
                obj.SetActive(false);
            }
            
            foreach (var obj in _objectsToEnable)
            {
                obj.SetActive(true);
            }
            
            yield return new WaitForSeconds(0.1f);
            SceneManager.LoadScene(Scenes.MainMenu.GetName());
        }
    }
}