using System.Collections.Generic;
using System.Linq;
using DialogCommon.Manager;
using DialogCommon.Model;
using DialogCommon.Model.Metadata;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DialogMaker.Manager
{
    public class DialogSceneNode : MonoBehaviour, IDialogSceneNode
    {
        [SerializeField] private TextMeshProUGUI _nodeText;
        [SerializeField] private Transform _connectorsParent;
        [SerializeField] private GameObject _connectorPrefab;
        [SerializeField] private TextMeshProUGUI _defaultNodeText;
        [SerializeField] private Image _backgroundImage;

        private DiContainer _container;
        private ISaveValues _saveValues;
            
        private int _id;
        private bool _isSelected;

        public int Id { get; private set; }
        public string Text => _nodeText.text;
        public List<(GameObject, IDialogConnector)> Connectors { get; } = new();

        [Inject]
        public void Inject(DiContainer container)
        {
            _container = container;
        }

        public void Initialize(DialogSceneModel sceneModel, DialogSceneMetadataModel metadata, bool isDefaultNode)
        {
            Id = sceneModel.Id;
            _nodeText.text = sceneModel.MainText;
            transform.position = metadata.NodePosition;
            
            // spawn connectors
            foreach (var answerModel in sceneModel.Answers)
            {
                var connector = _container.InstantiatePrefab(_connectorPrefab, _connectorsParent);
                var connectorComponent = connector.GetComponent<IDialogConnector>();
                connectorComponent.Initialize(this, answerModel);
                Connectors.Add((connector, connectorComponent));
            }
            
            _defaultNodeText.gameObject.SetActive(isDefaultNode);
        }

        public void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;
            _backgroundImage.color = isSelected ? new Color(1, 0.8f, 0.8f) : Color.white;
        }

        public DialogSceneModel SerializeScene()
        {
            return new DialogSceneModel
            {
                Id = _id,
                MainText = Text,
                Answers = Connectors.Select(x => x.Item2.Serialize()).ToList()
            };
        }

        public DialogSceneMetadataModel SerializeMetadata()
        {
            return new DialogSceneMetadataModel
            {
                NodePosition = transform.position
            };
        }
    }
}