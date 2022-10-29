using System;
using System.Collections.Generic;
using DialogCommon.Model.AnswerAction;

namespace DialogCommon.Model
{
    [Serializable]
    public class AnswerModel
    {
        public AnswerActionType AnswerActionType { get; set; }
        public IAnswerActionModel AnswerActionModel { get; set; }
    }
}