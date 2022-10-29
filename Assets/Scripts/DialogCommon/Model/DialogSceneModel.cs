using System;
using System.Collections.Generic;

namespace DialogCommon.Model
{
    [Serializable]
    public class DialogSceneModel
    {
        public int Id { get; set; }
        public string MainText { get; set; }
        public List<AnswerModel> Answers { get; set; }
    }
}