using System;
using System.Collections.Generic;
using System.Linq;
using DialogCommon.Manager;
using DialogCommon.Model;
using DialogCommon.Model.Metadata;
using DialogCommon.Utils;
using DialogMaker.UI.Panel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace DialogMaker.Manager
{
    public class MakerManager : IMakerManager, IInitializable
    {
        public event Action<IDialogSceneNode> OnNodeSelected;
        public event Action<IDialogConnector, IDialogSceneNode> OnAnswerConnected; 

        private readonly DiContainer _container;
        private readonly Transform _editorCanvas;
        private readonly IPanelManager _panelManager;
        private readonly IDialogSaveManager _dialogSaveManager;
        private readonly ISaveValues _saveValues;

        private IDialogSceneNode _defaultScene;
        private IDialogSceneNode _selectedScene;
        
        public List<(GameObject, IDialogSceneNode)> Nodes { get; } = new();
        public LineRenderer ActiveAnswerLine { get; set; }
        public IDialogConnector ActiveAnswerConnector { get; set; }
        public IDialogSceneNode DefaultScene => _defaultScene;

        public MakerManager(
            DiContainer container, 
            [Inject(Id = "EditorCanvas")] Transform editorCanvas,
            IPanelManager panelManager,
            IDialogSaveManager dialogSaveManager,
            ISaveValues saveValues
        ) {
            _container = container;
            _editorCanvas = editorCanvas;
            _panelManager = panelManager;
            _dialogSaveManager = dialogSaveManager;
            _saveValues = saveValues;
        }
        
        public void Initialize()
        {
            if (string.IsNullOrEmpty(_saveValues.OpenedScenarioName))
            {
                return;
            }
            
            Deserialize(_dialogSaveManager.LoadDialog(_saveValues.OpenedScenarioName));
        }

        public IDialogSceneNode SpawnNode(DialogSceneModel sceneModel, DialogSceneMetadataModel metadataModel)
        {
            var nodePrefab = Addressables.LoadAssetAsync<GameObject>("DialogSceneNode").WaitForCompletion();
            var node = _container.InstantiatePrefabForComponent<IDialogSceneNode>(nodePrefab, _editorCanvas);
            
            if (Nodes.Count == 0)
            {
                _defaultScene = node;
            }
            
            node.Initialize(sceneModel, metadataModel, _defaultScene == node);
            Nodes.Add((((MonoBehaviour)node).gameObject, node));

            return node;
        }

        public void SelectNode(IDialogSceneNode sceneNode)
        {
            if (_selectedScene == sceneNode)
            {
                return;
            }
             
            if (ActiveAnswerLine)
            {
                if ((sceneNode as MonoBehaviour).gameObject != (ActiveAnswerConnector as MonoBehaviour).transform.parent.parent.gameObject)
                {
                    var connector = ActiveAnswerConnector;
                    connector.SetAnswerLineNode(sceneNode);
                    OnAnswerConnected?.Invoke(connector, sceneNode);
                }
                
                return;
            }
            
            _selectedScene = sceneNode;

            foreach (var tuple in Nodes)
            {
                tuple.Item2.OnSelected(tuple.Item2 == sceneNode);
            }

            _panelManager.OpenPanel<EditSceneSidePanel>();
            OnNodeSelected?.Invoke(sceneNode);
        }

        public IDialogSceneNode FindNode(int nodeId)
        {
            return Nodes.Find(n => n.Item2.Id == nodeId).Item2;
        }

        public void RemoveNode(IDialogSceneNode sceneNode)
        {
            var index = Nodes.FindIndex(x => x.Item2 == sceneNode);
            Nodes.RemoveAt(index);
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

        private void Deserialize(SaveFileDm saveFileDm)
        {
            var metadates = new Dictionary<int, DialogSceneMetadataModel>();
            foreach (var sceneMetadata in saveFileDm.ScenarioMetadataModel.SceneMetadates)
            {
                metadates.Add(sceneMetadata.Id, sceneMetadata);
            }

            var modelsToSpawn = new List<DialogSceneModel>(saveFileDm.ScenarioModel.Scenes);
            var defaultModel = saveFileDm.ScenarioModel.Scenes
                .Find(s => s.Id == saveFileDm.ScenarioModel.StartSceneId);
            modelsToSpawn.Remove(defaultModel);

            var nodesDict = new Dictionary<int, IDialogSceneNode>();
            nodesDict.Add(defaultModel.Id, SpawnNode(defaultModel, metadates[defaultModel.Id]));
            foreach (var sceneModel in modelsToSpawn)
            {
                nodesDict.Add(sceneModel.Id, SpawnNode(sceneModel, metadates[sceneModel.Id]));
            }

            foreach (var tuple in Nodes)
            {
                foreach (var connector in tuple.Item2.Connectors)
                {
                    if (connector.Item2.TransitionSceneId == 0)
                    {
                        continue;
                    }
                    
                    connector.Item2.SetAnswerLineNode(nodesDict[connector.Item2.TransitionSceneId]);
                }
            }
        }
    }
}