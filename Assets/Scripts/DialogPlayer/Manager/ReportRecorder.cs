using System;
using System.Collections.Generic;
using DialogCommon.Manager;
using DialogCommon.Model;
using DialogCommon.Utils;

namespace DialogPlayer.Manager
{
    public class ReportRecorder : IReportRecorder
    {
        private readonly ISaveValues _saveValues;
        
        private ReportModel _report;
        private DateTime _currentDialogStartTime;

        public ReportRecorder(ISaveValues saveValues)
        {
            _saveValues = saveValues;
        }

        public void StartRecord(SaveFileDm file)
        {
            _currentDialogStartTime = DateTime.Now;
            _report = new ReportModel
            {
                Version = 1,
                StartTime = DateTime.Now,
                ScenarioFile = file,
                DialogRecords = new List<DialogRecord>(),
                DialogsTime = new Dictionary<int, TimeSpan>(),
                UserName = _saveValues.UserName
            };
        }

        public void RecordAnswer(int id, int answerIndex)
        {
            _report.DialogRecords.Add(new DialogRecord { AnswerIndex = answerIndex, DialogId = id });
            _report.DialogsTime[id] = DateTime.Now - _currentDialogStartTime;
            _currentDialogStartTime = DateTime.Now;
        }

        public ReportModel EndRecord()
        {
            _report.TotalTime = DateTime.Now - _report.StartTime;
            return _report;
        }
    }
}