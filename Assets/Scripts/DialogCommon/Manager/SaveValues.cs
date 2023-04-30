using DialogCommon.Utils;

namespace DialogCommon.Manager
{
    public class SaveValues : ISaveValues
    {
        private readonly PrefsValue<string> _openedScenarioName = new("OpenedScenarioName", string.Empty);
        private readonly PrefsValue<string> _userName = new("UserName", string.Empty);

        public string OpenedScenarioName
        {
            set => _openedScenarioName.Value = value;
            get => _openedScenarioName.Value;
        }

        public string UserName
        {
            set => _userName.Value = value;
            get => _userName.Value;
        }
    }
}