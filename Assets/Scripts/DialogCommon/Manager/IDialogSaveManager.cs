using System.Collections.Generic;
using DialogCommon.Model;

namespace DialogCommon.Manager
{
    public interface IDialogSaveManager
    {
        List<string> SavedDialogNames { get; }
        
        ScenarioModel LoadDialog(string name);
        void DeleteDialog(string name);
    }
}