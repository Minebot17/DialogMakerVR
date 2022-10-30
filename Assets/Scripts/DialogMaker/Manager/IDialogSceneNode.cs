using System.Collections.Generic;
using DialogCommon.Model;
using UnityEngine;

namespace DialogMaker.Manager
{
    public interface IDialogSceneNode
    {
        int Id { get; }
        string Text { get; }
        List<(GameObject, IDialogConnector)> Connectors { get; }

        void Initialize(DialogSceneModel sceneModel);
        DialogSceneModel Serialize();
    }
}