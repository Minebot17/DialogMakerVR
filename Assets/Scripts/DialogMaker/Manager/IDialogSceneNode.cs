using System;
using System.Collections.Generic;
using DialogCommon.Model;
using DialogCommon.Model.Metadata;
using ModestTree.Util;
using UnityEngine;

namespace DialogMaker.Manager
{
    public interface IDialogSceneNode
    {
        event Action OnPositionChanged;
        
        int Id { get; }
        string Text { get; set; }
        List<(GameObject, IDialogConnector)> Connectors { get; }

        void Initialize(DialogSceneModel sceneModel, DialogSceneMetadataModel metadata, bool isDefaultNode);
        void OnSelected(bool isSelected);
        IDialogConnector CreateNewConnector();
        void RemoveConnector(IDialogConnector connector);
        void MoveAnswer(IDialogConnector connector, bool toUp);
        
        DialogSceneModel SerializeScene();
        DialogSceneMetadataModel SerializeMetadata();
    }
}