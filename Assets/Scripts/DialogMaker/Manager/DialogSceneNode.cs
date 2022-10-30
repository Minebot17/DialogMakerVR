using System.Collections.Generic;
using System.Linq;
using DialogCommon.Model;
using TMPro;
using UnityEngine;
using Zenject;

namespace DialogMaker.Manager
{
    public class DialogSceneNode : MonoBehaviour, IDialogSceneNode
    {
        [SerializeField] private TextMeshProUGUI _nodeText;
        [SerializeField] private Transform _connectorsParent;
        [SerializeField] private GameObject _connectorPrefab;

        private DiContainer _container;
            
        private int _id;

        public int Id { get; private set; }
        public string Text => _nodeText.text;
        public List<(GameObject, IDialogConnector)> Connectors { get; } = new();

        [Inject]
        public void Inject(DiContainer container)
        {
            _container = container;
        }

        public void Initialize(DialogSceneModel sceneModel)
        {
            Id = sceneModel.Id;
            _nodeText.text = sceneModel.MainText;
            
            // spawn connectors
            foreach (var answerModel in sceneModel.Answers)
            {
                var connector = _container.InstantiatePrefab(_connectorPrefab, _connectorsParent);
                var connectorComponent = connector.GetComponent<IDialogConnector>();
                connectorComponent.Initialize(this, answerModel);
                Connectors.Add((connector, connectorComponent));
            }
        }

        public DialogSceneModel Serialize()
        {
            return new DialogSceneModel
            {
                Id = _id,
                MainText = Text,
                Answers = Connectors.Select(x => x.Item2.Serialize()).ToList()
            };
        }
    }
}