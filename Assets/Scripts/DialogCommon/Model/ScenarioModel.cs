﻿using System;
using System.Collections.Generic;
using DialogCommon.Utils;

namespace DialogCommon.Model
{
    [Serializable]
    public class ScenarioModel
    {
        public string Name { get; set; }
        public int StartSceneId { get; set; }
        public List<DialogSceneModel> Scenes { get; set; }
        public PlayerScenes PlayerSceneId { get; set; }
    }
}