using DialogCommon.Manager;
using NUnit.Framework;

namespace Tests
{
    public class SavedValuesTest
    {
        private ISaveValues _saveValues;
        private string _prevOpenedScene;
        private string _prevUserName;

        [SetUp]
        public void Setup()
        {
            ReInitSaveValues();
            _prevOpenedScene = _saveValues.OpenedScenarioName;
            _prevUserName = _saveValues.UserName;
        }

        [Test]
        public void OpenedSceneSave()
        {
            var toSave = "Test123";
            _saveValues.OpenedScenarioName = toSave;
            ReInitSaveValues();
            Assert.AreEqual(_saveValues.OpenedScenarioName, toSave);
        }
        
        [Test]
        public void UserNameSave()
        {
            var toSave = "Test123";
            _saveValues.UserName = toSave;
            ReInitSaveValues();
            Assert.AreEqual(_saveValues.UserName, toSave);
        }

        [TearDown]
        public void TearDown()
        {
            _saveValues.OpenedScenarioName = _prevOpenedScene;
            _saveValues.UserName = _prevUserName;
        }

        private void ReInitSaveValues()
        {
            _saveValues = new SaveValues();
        }
    }
}