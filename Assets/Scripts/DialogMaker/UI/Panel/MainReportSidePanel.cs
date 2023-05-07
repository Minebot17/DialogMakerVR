using System;
using System.Globalization;
using DialogCommon.Manager;
using DialogCommon.Utils;
using DialogMaker.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace DialogMaker.UI.Panel
{
    public class MainReportSidePanel : DialogCommon.Manager.Panel
    {
        [SerializeField] private TMP_Text _startTimeText;
        [SerializeField] private TMP_Text _totalTimeText;
        [SerializeField] private TMP_Text _userNameText;
        [SerializeField] private TMP_Text _reportNameText;
        [SerializeField] private Button _returnToMainMenuButton;
        
        private ISaveValues _saveValues;
        private IMakerManager _makerManager;
        
        [Inject]
        public void Inject(ISaveValues saveValues, IMakerManager makerManager)
        {
            _saveValues = saveValues;
            _makerManager = makerManager;
        }

        private void Start()
        {
            SetStartTime(_makerManager.ReportModel.StartTime);
            SetTotalTime(_makerManager.ReportModel.TotalTime);
            SetUserName(_makerManager.ReportModel.UserName);
            SetReportName(_saveValues.OpenedScenarioName);
            _returnToMainMenuButton.onClick.AddListener(OnReturnToMainMenuClick);
        }

        private void SetStartTime(DateTime date)
        {
            _startTimeText.text = $"Start time: {date.ToString(CultureInfo.CurrentCulture)}";
        }

        private void SetTotalTime(TimeSpan span)
        {
            _totalTimeText.text = $"Total time: {span.Hours:D2}:{span.Minutes:D2}:{span.Seconds:D2}";
        }

        private void SetUserName(string userName)
        {
            _userNameText.text = $"User: {userName}";
        }

        private void SetReportName(string reportName)
        {
            _reportNameText.text = $"Report: {reportName}";
        }
        
        private void OnReturnToMainMenuClick()
        {
            SceneManager.LoadScene(Scenes.MainMenu.GetName());
        }
    }
}