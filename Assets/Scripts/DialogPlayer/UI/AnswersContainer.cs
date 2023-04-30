using System;
using System.Collections.Generic;
using DialogCommon.Model;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace DialogPlayer.UI
{
    public class AnswersContainer : MonoBehaviour
    {
        private DiContainer _container;

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }
        
        public void Setup(List<AnswerModel> answerModels, Action<int, AnswerModel> onSelect)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < answerModels.Count; i++)
            {
                SpawnButton(answerModels[i], onSelect, i);
            }
        }
        
        private void SpawnButton(AnswerModel model, Action<int, AnswerModel> onSelect, int index)
        {
            var buttonPrefab = Addressables.LoadAssetAsync<GameObject>("AnswerButton").WaitForCompletion();
            var button = _container.InstantiatePrefabForComponent<IAnswerButton>(buttonPrefab, transform);
            button.Setup(model, onSelect, index);
        }
    }
}