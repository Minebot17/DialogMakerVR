using System.Collections.Generic;
using System.Linq;
using DialogCommon.Manager;
using DialogCommon.Model;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace DialogMaker.Manager
{
    public class DialogSceneNodeManager : IDialogSceneNodeManager
    {
        private readonly DiContainer _container;
        private readonly Transform _editorCanvas;
        private readonly ISaveValues _saveValues;
        
        public List<(GameObject, IDialogSceneNode)> Nodes { get; } = new();

        public DialogSceneNodeManager(
            DiContainer container, 
            [Inject(Id = "EditorCanvas")] Transform editorCanvas,
            ISaveValues saveValues
        ) {
            _container = container;
            _editorCanvas = editorCanvas;
            _saveValues = saveValues;
        }

        public async void SpawnNode(DialogSceneModel sceneModel)
        {
            var nodePrefab = await Addressables.LoadAssetAsync<GameObject>("DialogSceneNode").Task;
            var node = _container.InstantiatePrefabForComponent<IDialogSceneNode>(nodePrefab, _editorCanvas);
            node.Initialize(sceneModel);

            if (Nodes.Count == 0)
            {
                _saveValues.DefaultSceneIdName = node.Id;
            }
        }

        public void SelectNode(IDialogSceneNode sceneNode)
        {
            // TODO select node
        }

        public ScenarioModel SerializeToScenario()
        {
            return new ScenarioModel
            {
                StartSceneId = _saveValues.DefaultSceneIdName,
                Scenes = Nodes.Select(x => x.Item2.Serialize()).ToList()
            };
        }
    }
}