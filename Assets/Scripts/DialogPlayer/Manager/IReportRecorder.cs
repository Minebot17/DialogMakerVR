using DialogCommon.Model;

namespace DialogPlayer.Manager
{
    public interface IReportRecorder
    {
        void StartRecord(ScenarioModel model);
        void RecordAnswer(int id, int answerIndex);
        ReportModel EndRecord();
    }
}