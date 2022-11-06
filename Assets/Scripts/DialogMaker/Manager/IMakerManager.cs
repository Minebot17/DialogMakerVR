using System;
using System.Collections.Generic;
using DialogCommon.Model;
using DialogCommon.Model.Metadata;
using UnityEngine;

namespace DialogMaker.Manager
{
    public interface IMakerManager
    {
        event Action<IDialogSceneNode> OnNodeSelected;
        
        List<(GameObject, IDialogSceneNode)> Nodes { get; }
        
        IDialogSceneNode SpawnNode(DialogSceneModel sceneModel, DialogSceneMetadataModel metadataModel);
        void SelectNode(IDialogSceneNode sceneNode);
        IDialogSceneNode FindNode(int nodeId);
        
        ScenarioModel SerializeScenario();
        ScenarioMetadataModel SerializeMetadata();
    }
}