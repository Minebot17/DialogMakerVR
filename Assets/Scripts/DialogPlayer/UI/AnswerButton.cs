using System;
using DialogCommon.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogPlayer.UI
{
    public class AnswerButton : MonoBehaviour, IAnswerButton
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;
        
        public void Setup(AnswerModel model, Action<AnswerModel> onSelect)
        {
            _text.text = model.MainText;
            _button.onClick.AddListener(() => onSelect?.Invoke(model));
        }
    }
}