using DialogCommon.Model;

namespace DialogMaker.Manager
{
    public interface IDialogConnector
    {
        int TransitionSceneId { get; }
        string Text { get; }

        void Initialize(IDialogSceneNode parent, AnswerModel answerModel);
        AnswerModel Serialize();
    }
}