using DialogCommon.Model;
using TMPro;
using UnityEngine;

namespace DialogMaker.Manager
{
    public class DialogConnector : MonoBehaviour, IDialogConnector
    {
        [SerializeField] private TextMeshProUGUI _iconText;

        public int TransitionSceneId { get; private set; }
        public string Text { get; private set; }

        public void Initialize(IDialogSceneNode parent, AnswerModel answerModel)
        {
            _iconText.text = (parent.Connectors.FindIndex(x => x.Item1 == gameObject) + 1).ToString();
            TransitionSceneId = answerModel.ToDialogSceneId;
            Text = answerModel.MainText;
        }

        public AnswerModel Serialize()
        {
            return new AnswerModel
            {
                MainText = Text,
                ToDialogSceneId = TransitionSceneId
            };
        }
    }
}