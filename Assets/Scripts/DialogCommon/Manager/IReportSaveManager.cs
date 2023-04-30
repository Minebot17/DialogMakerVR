using System.Collections.Generic;
using DialogCommon.Model;

namespace DialogCommon.Manager
{
    public interface IReportSaveManager
    {
        List<string> SavedReportNames { get; }
        
        ReportModel LoadReport(string name);
        void SaveReport(string name, ReportModel reportModel);
        void DeleteReport(string name);
    }
}