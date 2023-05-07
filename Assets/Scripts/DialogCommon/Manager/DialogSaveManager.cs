using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.SimpleZip;
using DialogCommon.Model;
using DialogCommon.Model.Metadata;
using DialogCommon.Utils;
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
                    return fileName.Substring(0, fileName.Length - DialogScenarioExtension.Length - 1);
                })
                .Concat(
                    Directory.GetFiles(StreamingAssetsDialogFolderPath, $"*.{DialogScenarioExtension}")
                    .Select(str =>
                    {
                        string fileName = new FileInfo(str).Name;
                        return "StreamingAssets/" + fileName.Substring(0, fileName.Length - DialogScenarioExtension.Length - 1);
                    }))
                .ToList();

        private string DialogFolderPath => Application.persistentDataPath + DialogScenarioSubFolder;
        private string StreamingAssetsDialogFolderPath => Application.streamingAssetsPath + "/Scenarios";

        public DialogSaveManager()
        {
            if (!Directory.Exists(DialogFolderPath))
            {
                Directory.CreateDirectory(DialogFolderPath);
            }
        }
        
        public SaveFileDm LoadDialog(string name)
        {
            string savedJson = Zip.Decompress(File.ReadAllBytes(GetFullScenarioPath(name)));
            return JsonConvert.DeserializeObject<SaveFileDm>(savedJson);
        }

        public void SaveDialog(string name, ScenarioModel model, ScenarioMetadataModel metadata)
        {
            model.Name = name;
            string saveJson = JsonConvert.SerializeObject(new SaveFileDm(model, metadata), Formatting.Indented);
            File.WriteAllBytes($"{DialogFolderPath}/{name}.{DialogScenarioExtension}", Zip.Compress(saveJson));
        }

        public void DeleteDialog(string name)
        {
            File.Delete(GetFullScenarioPath(name));
        }

        private string GetFullScenarioPath(string name)
        {
            if (name.Contains("StreamingAssets"))
            {
                return $"{StreamingAssetsDialogFolderPath}/{name[16..]}.{DialogScenarioExtension}";
            }
            return $"{DialogFolderPath}/{name}.{DialogScenarioExtension}";
        }
    }
}