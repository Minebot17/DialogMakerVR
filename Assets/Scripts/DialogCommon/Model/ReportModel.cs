using System;
using System.Collections.Generic;
using DialogCommon.Utils;

namespace DialogCommon.Model
{
    [Serializable]
    public class ReportModel
    {
        public int Version { get; set; }
        public string UserName { get; set; }
        public SaveFileDm ScenarioFile { get; set; }

        public DateTime StartTime { get; set; }
        public TimeSpan TotalTime { get; set; }
        public Dictionary<int, TimeSpan> DialogsTime { get; set; }
        public List<DialogRecord> DialogRecords { get; set; }
    }

    [Serializable]
    public class DialogRecord
    {
        public int DialogId { get; set; }
        public int AnswerIndex { get; set; }
    }
}