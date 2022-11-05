using System;
using DialogCommon.Model;
using DialogCommon.Model.Metadata;

namespace DialogCommon.Utils
{
    [Serializable]
    public class SaveFileDm
    {
        public ScenarioModel ScenarioModel { get; set; }
        public ScenarioMetadataModel ScenarioMetadataModel { get; set; }

        public SaveFileDm(ScenarioModel scenarioModel, ScenarioMetadataModel scenarioMetadataModel)
        {
            ScenarioModel = scenarioModel;
            ScenarioMetadataModel = scenarioMetadataModel;
        }
    }
}