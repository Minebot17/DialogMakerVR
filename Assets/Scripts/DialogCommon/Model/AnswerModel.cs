using System;

namespace DialogCommon.Model
{
    [Serializable]
    public class AnswerModel
    {
        public int ToDialogSceneId { get; set; }
        public string MainText { get; set; }
    }
}