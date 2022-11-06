using DialogCommon.Model;
using TMPro;
using UnityEngine;

namespace DialogMaker.Manager
{
    public class DialogConnector : MonoBehaviour, IDialogConnector
    {
        [SerializeField] private TextMeshProUGUI _iconText;

        public int TransitionSceneId { get; private set; }
        public string Text { get; set; }
        public int Index { get; private set; }

        private IDialogSceneNode _parent;

        public void Initialize(IDialogSceneNode parent, AnswerModel answerModel)
        {
            _parent = parent;
            UpdateIndex();
            _iconText.text = Index.ToString();
            TransitionSceneId = answerModel.ToDialogSceneId;
            Text = answerModel.MainText;
        }

        public void UpdateIndex()
        {
            Index = _parent.Connectors.FindIndex(x => x.Item1 == gameObject) + 1;
            _iconText.text = Index.ToString();
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