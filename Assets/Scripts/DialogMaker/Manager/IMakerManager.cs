using System;
using System.Collections.Generic;
using DialogCommon.Model;
using DialogCommon.Model.Metadata;
using DialogCommon.Utils;
using UnityEngine;

namespace DialogMaker.Manager
{
    public interface IMakerManager
    {
        event Action<IDialogSceneNode> OnNodeSelected;
        event Action<IDialogConnector, IDialogSceneNode> OnAnswerConnected;
        
        List<(GameObject, IDialogSceneNode)> Nodes { get; }
        LineRenderer ActiveAnswerLine { get; set; }
        IDialogConnector ActiveAnswerConnector { get; set; }
        PlayerScenes PlayerSceneId { get; set; }
        IDialogSceneNode DefaultScene { get; }
        
        IDialogSceneNode SpawnNode(DialogSceneModel sceneModel, DialogSceneMetadataModel metadataModel);
        void SelectNode(IDialogSceneNode sceneNode);
        IDialogSceneNode FindNode(int nodeId);
        void RemoveNode(IDialogSceneNode sceneNode);
        
        ScenarioModel SerializeScenario();
        ScenarioMetadataModel SerializeMetadata();
    }
}