using System.Collections.Generic;
using DialogCommon.Model;
using DialogCommon.Model.Metadata;
using UnityEngine;

namespace DialogMaker.Manager
{
    public interface IDialogSceneNode
    {
        int Id { get; }
        string Text { get; set; }
        List<(GameObject, IDialogConnector)> Connectors { get; }

        void Initialize(DialogSceneModel sceneModel, DialogSceneMetadataModel metadata, bool isDefaultNode);
        void OnSelected(bool isSelected);
        IDialogConnector CreateNewConnector();
        void RemoveConnector(IDialogConnector connector);
        
        DialogSceneModel SerializeScene();
        DialogSceneMetadataModel SerializeMetadata();
    }
}