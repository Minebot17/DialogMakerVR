using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.SimpleZip;
using DialogCommon.Model;
using Newtonsoft.Json;
using UnityEngine;

namespace DialogCommon.Manager
{
    public class ReportSaveManager : IReportSaveManager
    {
        private const string DialogScenarioExtension = "rep";
        private const string DialogScenarioSubFolder = "/Reports";
    
        public List<string> SavedReportNames => 
            Directory.GetFiles(DialogFolderPath, $"*.{DialogScenarioExtension}")
                .Select(str =>
                {
                    string fileName = new FileInfo(str).Name;
                    return fileName.Substring(0, fileName.Length - DialogScenarioExtension.Length - 1);
                })
                .ToList();

        private string DialogFolderPath => Application.persistentDataPath + DialogScenarioSubFolder;

        public ReportSaveManager()
        {
            if (!Directory.Exists(DialogFolderPath))
            {
                Directory.CreateDirectory(DialogFolderPath);
            }
        }
        
        public ReportModel LoadReport(string name)
        {
            string savedJson = Zip.Decompress(File.ReadAllBytes(GetFullReportPath(name)));
            return JsonConvert.DeserializeObject<ReportModel>(savedJson);
        }

        public void SaveReport(string name, ReportModel model)
        {
            string saveJson = JsonConvert.SerializeObject(model, Formatting.Indented);
            File.WriteAllBytes($"{DialogFolderPath}/{name}.{DialogScenarioExtension}", Zip.Compress(saveJson));
        }

        public void DeleteReport(string name)
        {
            File.Delete(GetFullReportPath(name));
        }

        private string GetFullReportPath(string name)
        {
            return $"{DialogFolderPath}/{name}.{DialogScenarioExtension}";
        }
    }
}