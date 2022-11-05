using System;
using UnityEngine;

namespace DialogCommon.Model.Metadata
{
    [Serializable]
    public class DialogSceneMetadataModel
    {
        public int Id { get; set; }
        public float NodePositionX { get; set; }
        public float NodePositionY { get; set; }
    }
}