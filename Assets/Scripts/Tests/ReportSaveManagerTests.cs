using DialogCommon.Manager;
using DialogCommon.Model;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests
{
    public class ReportSaveManagerTests
    {
        private IReportSaveManager _reportSaveManager;
        
        [SetUp]
        public void Setup()
        {
            _reportSaveManager = new ReportSaveManager();
        }
        
        [Test]
        public void DialogLoadingTest()
        {
            if (!_reportSaveManager.SavedReportNames.Contains("Test"))
            {
                SaveTest1Dialog();
            }
            
            var report = _reportSaveManager.LoadReport("Test");
            Assert.AreEqual(LoadedJson, JsonConvert.SerializeObject(report));
        }

        [Test]
        public void DialogSavingTest()
        {
            if (_reportSaveManager.SavedReportNames.Contains("Test2"))
            {
                _reportSaveManager.DeleteReport("Test2");
            }
            
            SaveTest2Dialog();
            Assert.IsTrue(_reportSaveManager.SavedReportNames.Contains("Test2"));
        }

        [Test]
        public void DialogDeletingTest()
        {
            if (!_reportSaveManager.SavedReportNames.Contains("Test2"))
            {
                SaveTest2Dialog();
            }
            
            _reportSaveManager.DeleteReport("Test2");
            Assert.IsFalse(_reportSaveManager.SavedReportNames.Contains("Test2"));
        }

        [TearDown]
        public void TearDown()
        {
            _reportSaveManager.DeleteReport("Test");
            _reportSaveManager.DeleteReport("Test2");
        }
        
        private void SaveTest1Dialog()
        {
            var report = JsonConvert.DeserializeObject<ReportModel>(LoadedJson);
            _reportSaveManager.SaveReport("Test", report);
        }

        private void SaveTest2Dialog()
        {
            _reportSaveManager.SaveReport("Test2", new ReportModel());
        }

        private static string LoadedJson = @"{""Version"":1,""UserName"":""Minebot"",""ScenarioFile"":{""ScenarioModel"":{""Name"":null,""StartSceneId"":-70137928,""Scenes"":[{""Id"":-70137928,""MainText"":""Hello!"",""Answers"":[{""ToDialogSceneId"":-1987145538,""MainText"":""ssssss""},{""ToDialogSceneId"":1210342434,""MainText"":""123321""},{""ToDialogSceneId"":-2087859536,""MainText"":""asd 1""}]},{""Id"":-1987145538,""MainText"":""Good bye!"",""Answers"":[{""ToDialogSceneId"":0,""MainText"":""Bye""}]},{""Id"":1210342434,""MainText"":""Wtf?"",""Answers"":[{""ToDialogSceneId"":-1987145538,""MainText"":""Yes""},{""ToDialogSceneId"":-2087859536,""MainText"":""No""}]},{""Id"":-2087859536,""MainText"":""This is the end"",""Answers"":[{""ToDialogSceneId"":0,""MainText"":""Bye""}]}],""PlayerSceneId"":0},""ScenarioMetadataModel"":{""SceneMetadates"":[{""Id"":-70137928,""NodePositionX"":-0.680680037,""NodePositionY"":3.68368244},{""Id"":-1987145538,""NodePositionX"":-2.77277374,""NodePositionY"":-1.85185242},{""Id"":1210342434,""NodePositionX"":-0.590589762,""NodePositionY"":0.42042017},{""Id"":-2087859536,""NodePositionX"":1.60160172,""NodePositionY"":-1.77177155}]}},""StartTime"":""2023-05-07T18:54:14.2307113+03:00"",""TotalTime"":""00:00:02.3384978"",""DialogsTime"":{""-70137928"":""00:00:01.2146370"",""1210342434"":""00:00:00.5124711"",""-2087859536"":""00:00:00.6113897""},""DialogRecords"":[{""DialogId"":-70137928,""AnswerIndex"":1},{""DialogId"":1210342434,""AnswerIndex"":1},{""DialogId"":-2087859536,""AnswerIndex"":0}]}";
    }
}