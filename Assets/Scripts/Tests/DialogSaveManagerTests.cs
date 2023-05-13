using DialogCommon.Manager;
using DialogCommon.Model;
using DialogCommon.Model.Metadata;
using DialogCommon.Utils;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests
{
    public class DialogSaveManagerTests
    {
        private IDialogSaveManager _dialogSaveManager;
        
        [SetUp]
        public void Setup()
        {
            _dialogSaveManager = new DialogSaveManager();
        }
        
        [Test]
        public void DialogLoadingTest()
        {
            if (!_dialogSaveManager.SavedDialogNames.Contains("Test"))
            {
                SaveTest1Dialog();
            }
            
            var saveFile = _dialogSaveManager.LoadDialog("Test");
            Assert.AreEqual(LoadedJson, JsonConvert.SerializeObject(saveFile));
        }

        [Test]
        public void DialogSavingTest()
        {
            if (_dialogSaveManager.SavedDialogNames.Contains("Test2"))
            {
                _dialogSaveManager.DeleteDialog("Test2");
            }
            
            SaveTest2Dialog();
            Assert.IsTrue(_dialogSaveManager.SavedDialogNames.Contains("Test2"));
        }

        [Test]
        public void DialogDeletingTest()
        {
            if (!_dialogSaveManager.SavedDialogNames.Contains("Test2"))
            {
                SaveTest2Dialog();
            }
            
            _dialogSaveManager.DeleteDialog("Test2");
            Assert.IsFalse(_dialogSaveManager.SavedDialogNames.Contains("Test2"));
        }

        [TearDown]
        public void TearDown()
        {
            _dialogSaveManager.DeleteDialog("Test");
            _dialogSaveManager.DeleteDialog("Test2");
        }
        
        private void SaveTest1Dialog()
        {
            var saveFile = JsonConvert.DeserializeObject<SaveFileDm>(LoadedJson);
            _dialogSaveManager.SaveDialog("Test", saveFile.ScenarioModel, saveFile.ScenarioMetadataModel);
        }

        private void SaveTest2Dialog()
        {
            _dialogSaveManager.SaveDialog("Test2", new ScenarioModel { Name = "Test2" }, new ScenarioMetadataModel());
        }

        private static string LoadedJson = @"{""ScenarioModel"":{""Name"":""Test"",""StartSceneId"":-2129811963,""Scenes"":[{""Id"":-2129811963,""MainText"":""Hello"",""Answers"":[{""ToDialogSceneId"":1859521555,""MainText"":""Hello""}]},{""Id"":1859521555,""MainText"":""Bye"",""Answers"":[{""ToDialogSceneId"":0,""MainText"":""Bye""}]}],""PlayerSceneId"":0},""ScenarioMetadataModel"":{""SceneMetadates"":[{""Id"":-2129811963,""NodePositionX"":-2.29229236,""NodePositionY"":1.951952},{""Id"":1859521555,""NodePositionX"":-1.55155087,""NodePositionY"":0.120120347}]}}";
    }
}