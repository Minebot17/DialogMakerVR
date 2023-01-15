using System;
using System.Collections.Generic;
using DialogCommon.Model;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace DialogPlayer.UI
{
    public class AnswersContainer : MonoBehaviour, IAnswersContainer
    {
        private DiContainer _container;

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }
        
        public void Setup(List<AnswerModel> answerModels, Action<AnswerModel> onSelect)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach (var model in answerModels)
            {
                SpawnButton(model, onSelect);
            }
        }
        
        private void SpawnButton(AnswerModel model, Action<AnswerModel> onSelect)
        {
            var buttonPrefab = Addressables.LoadAssetAsync<GameObject>("AnswerButton").WaitForCompletion();
            var button = _container.InstantiatePrefabForComponent<IAnswerButton>(buttonPrefab, transform);
            button.Setup(model, onSelect);
        }
    }
}