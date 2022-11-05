using System;
using System.Collections.Generic;
using System.Linq;
using DialogCommon.Model;
using DialogCommon.Model.Metadata;
using DialogCommon.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace DialogMaker.Manager
{
    public class MakerManager : IMakerManager
    {
        public event Action<IDialogSceneNode> OnNodeSelected; 

        private readonly DiContainer _container;
        private readonly Transform _editorCanvas;

        private IDialogSceneNode _defaultScene;
        private IDialogSceneNode _selectedScene;
        
        public List<(GameObject, IDialogSceneNode)> Nodes { get; } = new();

        public MakerManager(
            DiContainer container, 
            [Inject(Id = "EditorCanvas")] Transform editorCanvas
        ) {
            _container = container;
            _editorCanvas = editorCanvas;
        }

        public IDialogSceneNode SpawnNode(DialogSceneModel sceneModel)
        {
            var nodePrefab = Addressables.LoadAssetAsync<GameObject>("DialogSceneNode").WaitForCompletion();
            var node = _container.InstantiatePrefabForComponent<IDialogSceneNode>(nodePrefab, _editorCanvas);
            
            if (Nodes.Count == 0)
            {
                _defaultScene = node;
            }
            
            node.Initialize(sceneModel, new DialogSceneMetadataModel
            {
                NodePosition = Vector2.zero
            }, _defaultScene == node);
            Nodes.Add((((MonoBehaviour)node).gameObject, node));

            return node;
        }

        public void SelectNode(IDialogSceneNode sceneNode)
        {
            if (_selectedScene == sceneNode)
            {
                return;
            }
            
            _selectedScene = sceneNode;

            foreach (var tuple in Nodes)
            {
                tuple.Item2.SetSelected(tuple.Item2 == sceneNode);
            }
            
            OnNodeSelected?.Invoke(sceneNode);
        }

        public ScenarioModel SerializeScenario()
        {
            return new ScenarioModel
            {
                StartSceneId = _defaultScene.Id,
                Scenes = Nodes.Select(x => x.Item2.SerializeScene()).ToList()
            };
        }

        public ScenarioMetadataModel SerializeMetadata()
        {
            return new ScenarioMetadataModel
            {
                SceneMetadates = Nodes.Select(x => x.Item2.SerializeMetadata()).ToList()
            };
        }
    }
}