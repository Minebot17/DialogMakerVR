﻿using System.Collections.Generic;
using DialogCommon.Model;
using DialogCommon.Model.Metadata;
using UnityEngine;

namespace DialogMaker.Manager
{
    public interface IDialogSceneNode
    {
        int Id { get; }
        string Text { get; }
        List<(GameObject, IDialogConnector)> Connectors { get; }

        void Initialize(DialogSceneModel sceneModel, DialogSceneMetadataModel metadata, bool isDefaultNode);
        void SetSelected(bool isSelected);
        
        DialogSceneModel SerializeScene();
        DialogSceneMetadataModel SerializeMetadata();
    }
}