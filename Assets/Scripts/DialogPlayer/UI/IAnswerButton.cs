﻿using System;
using DialogCommon.Model;

namespace DialogPlayer.UI
{
    public interface IAnswerButton
    {
        void Setup(AnswerModel model, Action<AnswerModel> onSelect);
    }
}