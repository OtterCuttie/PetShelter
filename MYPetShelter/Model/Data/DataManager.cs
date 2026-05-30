using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Model.Core;

namespace Model.Data
{
    public class DataManager
    {
        private  FileManager _fileManager;
        private  ReportGenerator _reportGenerator;
        private string _currentReportFormat = "json";
        public string CurrentReportFormat
        {
            get => _currentReportFormat;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;

                if (_currentReportFormat == value)
                    return;

                string oldFormat = _currentReportFormat;

                _currentReportFormat = value;

                OnFormatChanged(oldFormat, value);
            }
        }
        public DataManager(FileManager fileManager,ReportGenerator reportGenerator)
        {
            _fileManager = fileManager;
            _reportGenerator = reportGenerator;
            _currentReportFormat = fileManager.FileExtension;
        }

        // Сохранение приютов
        public void SaveShelters(List<Shelter<Pet>> shelters)
        {
            if (shelters == null)
                return;
            _fileManager.Serialize(shelters);
        }

        // Загрузка приютов
        public List<Shelter<Pet>> LoadShelters()
        {
            return _fileManager.Deserialize();
        }

        // Создание отчета
        public void CreateReport(
            List<Pet> pets)
        {
            _reportGenerator.CreateReport(pets);
        }
        private void OnFormatChanged(string oldFormat, string newFormat)
        {
            FileManager newManager = newFormat switch
            {
                "json" when !(_fileManager is JsonFileManager) =>
                    new JsonFileManager(_fileManager.Name, _fileManager.FolderPath, _fileManager.FileName, "json"),

                //"xml" when !(_fileManager is XmlFileManager) =>
                //    new XmlFileManager(_fileManager.Name, _fileManager.FolderPath, _fileManager.FileName, "xml"),

                _ => null
            };

            if (newManager != null)
                ChangeFileManager(newManager);
        }
        public void ChangeFileManager(FileManager newManager)
        {
            if (newManager == null)
                return;

            var shelters = LoadShelters();

            _fileManager = newManager;

            SaveShelters(shelters);
        }
    }
}
