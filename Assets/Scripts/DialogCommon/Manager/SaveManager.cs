using System.Collections.Generic;
using System.IO;
using System.Linq;
using DialogCommon.Model;
using UnityEngine;

namespace DialogCommon.Manager
{
    public class SaveManager : ISaveManager
    {
        public List<string> SavedDialogNames => 
            Directory.GetFiles(Application.persistentDataPath, "*.dm")
                .Select(str => str.Substring(Application.persistentDataPath.Length + 1, str.Length - Application.persistentDataPath.Length - 4))
                .ToList();
        
        public ScenarioModel LoadDialog(string name)
        {
            // TODO load
            return null;
        }

        public void DeleteDialog(string name)
        {
            // TODO delete
        }
    }
}