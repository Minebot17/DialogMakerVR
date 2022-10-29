using System;

namespace DialogCommon.Model.AnswerAction
{
    [Serializable]
    public class TransitionAnswerActionModel : IAnswerActionModel
    {
        public int ToDialogSceneId { get; set; }
        public string MainText { get; set; }
    }
}