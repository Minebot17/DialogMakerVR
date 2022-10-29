using DialogCommon.Utils;

namespace DialogCommon.Manager
{
    public class SaveValues : ISaveValues
    {
        private readonly PrefsValue<string> _openedScenarioName = new("OpenedScenarioName", string.Empty);
        
        public string OpenedScenarioName
        {
            set => _openedScenarioName.Value = value;
            get => _openedScenarioName.Value;
        }
    }
}