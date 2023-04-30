using System;
using System.Collections.Generic;

namespace DialogCommon.Model
{
    [Serializable]
    public class ReportModel
    {
        public int Version { get; set; }
        public string UserName { get; set; }
        public ScenarioModel ScenarioModel { get; set; }
        
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