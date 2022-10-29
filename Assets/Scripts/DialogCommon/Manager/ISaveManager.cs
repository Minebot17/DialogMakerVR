using System.Collections.Generic;
using DialogCommon.Model;

namespace DialogCommon.Manager
{
    public interface ISaveManager
    {
        List<string> SavedDialogNames { get; }
        
        ScenarioModel LoadDialog(string name);
        void DeleteDialog(string name);
    }
}