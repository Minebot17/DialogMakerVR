using DialogCommon.Model;

namespace DialogMaker.Manager
{
    public interface IDialogConnector
    {
        int TransitionSceneId { get; }
        string Text { get; set; }
        int Index { get; }

        void Initialize(IDialogSceneNode parent, AnswerModel answerModel);
        void UpdateIndex();
        void SpawnNewAnswerLine();
        void SetAnswerLineNode(IDialogSceneNode sceneNode);
        AnswerModel Serialize();
    }
}