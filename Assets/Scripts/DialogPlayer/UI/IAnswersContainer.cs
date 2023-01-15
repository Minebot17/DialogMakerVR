using System;
using System.Collections.Generic;
using DialogCommon.Model;

namespace DialogPlayer.UI
{
    public interface IAnswersContainer
    {
        void Setup(List<AnswerModel> answerModels, Action<AnswerModel> onSelect);
    }
}