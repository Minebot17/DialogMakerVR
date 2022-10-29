using System;
using System.Collections.Generic;

namespace DialogCommon.Model
{
    [Serializable]
    public class ScenarioModel
    {
        public int StartSceneId { get; set; }
        public List<DialogSceneModel> Scenes { get; set; }
    }
}