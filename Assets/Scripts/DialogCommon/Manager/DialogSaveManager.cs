using System.Collections.Generic;
using System.IO;
using System.Linq;
using DialogCommon.Model;
using Newtonsoft.Json;
using UnityEngine;

namespace DialogCommon.Manager
{
    public class DialogSaveManager : IDialogSaveManager
    {
        private const string DialogScenarioExtension = "dm";
        private const string DialogScenarioSubFolder = "/Scenarios";
    
        public List<string> SavedDialogNames => 
            Directory.GetFiles(DialogFolderPath, $"*.{DialogScenarioExtension}")
                .Select(str =>
                {
                    string fileName = new FileInfo(str).Name;
                    return fileName[..(fileName.Length - DialogScenarioExtension.Length - 1)];
                })
                .ToList();

        private string DialogFolderPath => Application.persistentDataPath + DialogScenarioSubFolder;

        public DialogSaveManager()
        {
            if (!Directory.Exists(DialogFolderPath))
            {
                Directory.CreateDirectory(DialogFolderPath);
            }
        }
        
        public ScenarioModel LoadDialog(string name)
        {
            return JsonConvert.DeserializeObject<ScenarioModel>(GetFullScenarioPath(name));
        }

        public void DeleteDialog(string name)
        {
            File.Delete(GetFullScenarioPath(name));
        }

        private string GetFullScenarioPath(string name)
        {
            return $"{DialogFolderPath}/{name}.{DialogScenarioExtension}";
        }
    }
}