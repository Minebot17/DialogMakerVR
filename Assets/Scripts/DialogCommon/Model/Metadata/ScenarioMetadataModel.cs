using System;
using System.Collections.Generic;

namespace DialogCommon.Model.Metadata
{
    [Serializable]
    public class ScenarioMetadataModel
    {
        public List<DialogSceneMetadataModel> SceneMetadates { get; set; }
    }
}