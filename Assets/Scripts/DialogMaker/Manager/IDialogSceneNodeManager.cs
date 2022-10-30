using System.Collections.Generic;
using DialogCommon.Model;
using UnityEngine;

namespace DialogMaker.Manager
{
    public interface IDialogSceneNodeManager
    {
        List<(GameObject, IDialogSceneNode)> Nodes { get; }
        
        void SpawnNode(DialogSceneModel sceneModel);
        void SelectNode(IDialogSceneNode sceneNode);
        ScenarioModel SerializeToScenario();
    }
}