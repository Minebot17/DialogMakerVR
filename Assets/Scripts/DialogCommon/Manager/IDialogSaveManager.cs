using System.Collections.Generic;
using DialogCommon.Model;
using DialogCommon.Model.Metadata;
using DialogCommon.Utils;

namespace DialogCommon.Manager
{
    public interface IDialogSaveManager
    {
        List<string> SavedDialogNames { get; }
        
        SaveFileDm LoadDialog(string name);
        void SaveDialog(string name, ScenarioModel model, ScenarioMetadataModel metadata);
        void DeleteDialog(string name);
    }
}