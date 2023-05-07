using DialogCommon.Model;
using DialogCommon.Utils;

namespace DialogPlayer.Manager
{
    public interface IReportRecorder
    {
        void StartRecord(SaveFileDm file);
        void RecordAnswer(int id, int answerIndex);
        ReportModel EndRecord();
    }
}