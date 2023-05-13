using System.Collections;
using DialogCommon.Manager;
using DialogCommon.Model;
using DialogCommon.Model.Metadata;
using DialogCommon.Utils;
using DialogPlayer.Manager;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ReportRecorderTests
    {
        private IReportRecorder _recorder;
        private ISaveValues _saveValues;
        private string _prevUserName;

        [SetUp]
        public void Setup()
        {
            _saveValues = new SaveValues();
            _recorder = new ReportRecorder(_saveValues);
            _prevUserName = _saveValues.UserName;
        }

        [UnityTest]
        [Explicit, Category("integration")]
        public IEnumerator ZeroAnswersRecord()
        {
            var userName = "Test1";
            var scenario = new SaveFileDm(new ScenarioModel { Name = "Test" }, new ScenarioMetadataModel());
            _saveValues.UserName = userName;
            _recorder.StartRecord(scenario);
            yield return new WaitForSeconds(0.1f);
            var report = _recorder.EndRecord();
            
            Assert.AreEqual(0.1d, report.TotalTime.TotalSeconds, 0.01d);
            Assert.Zero(report.DialogRecords.Count);
            Assert.Zero(report.DialogsTime.Count);
            Assert.AreEqual(userName, report.UserName);
            Assert.AreSame(scenario, report.ScenarioFile);
        }
        
        [UnityTest]
        [Explicit, Category("integration")]
        public IEnumerator OneAnswerRecord()
        {
            var userName = "Test2";
            var scenario = new SaveFileDm(new ScenarioModel { Name = "Test" }, new ScenarioMetadataModel());
            _saveValues.UserName = userName;
            _recorder.StartRecord(scenario);
            yield return new WaitForSeconds(0.1f);
            _recorder.RecordAnswer(2, 2);
            yield return new WaitForSeconds(0.1f);
            var report = _recorder.EndRecord();
            
            Assert.AreEqual(0.2d, report.TotalTime.TotalSeconds, 0.01d);
            Assert.AreEqual(1, report.DialogRecords.Count);
            Assert.AreEqual(2, report.DialogRecords[0].DialogId);
            Assert.AreEqual(2, report.DialogRecords[0].AnswerIndex);
            Assert.AreEqual(1, report.DialogsTime.Count);
            Assert.AreEqual(0.1d, report.DialogsTime[2].TotalSeconds, 0.01d);
            Assert.AreEqual(userName, report.UserName);
            Assert.AreSame(scenario, report.ScenarioFile);
        }
        
        [UnityTest]
        [Explicit, Category("integration")]
        public IEnumerator ManyAnswersRecord()
        {
            var userName = "Test3";
            var scenario = new SaveFileDm(new ScenarioModel { Name = "Test" }, new ScenarioMetadataModel());
            _saveValues.UserName = userName;
            _recorder.StartRecord(scenario);
            yield return new WaitForSeconds(0.1f);
            _recorder.RecordAnswer(2, 2);
            yield return new WaitForSeconds(0.1f);
            _recorder.RecordAnswer(3, 3);
            yield return new WaitForSeconds(0.1f);
            _recorder.RecordAnswer(4, 4);
            yield return new WaitForSeconds(0.1f);
            var report = _recorder.EndRecord();
            
            Assert.AreEqual(0.4d, report.TotalTime.TotalSeconds, 0.01d);
            Assert.AreEqual(3, report.DialogRecords.Count);
            Assert.AreEqual(2, report.DialogRecords[0].DialogId);
            Assert.AreEqual(2, report.DialogRecords[0].AnswerIndex);
            Assert.AreEqual(3, report.DialogRecords[1].DialogId);
            Assert.AreEqual(3, report.DialogRecords[1].AnswerIndex);
            Assert.AreEqual(4, report.DialogRecords[2].DialogId);
            Assert.AreEqual(4, report.DialogRecords[2].AnswerIndex);
            Assert.AreEqual(3, report.DialogsTime.Count);
            Assert.AreEqual(0.1d, report.DialogsTime[2].TotalSeconds, 0.01d);
            Assert.AreEqual(0.1d, report.DialogsTime[3].TotalSeconds, 0.01d);
            Assert.AreEqual(0.1d, report.DialogsTime[4].TotalSeconds, 0.01d);
            Assert.AreEqual(userName, report.UserName);
            Assert.AreSame(scenario, report.ScenarioFile);
        }

        [TearDown]
        public void TearDown()
        {
            _saveValues.UserName = _prevUserName;
        }
    }
}