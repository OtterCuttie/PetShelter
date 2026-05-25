using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model.Data
{
    /// Генератор отчетов "Подборка_№Х_от_дата"
    public class ReportGenerator
    {
        private readonly string _reportsDirectory;
        private int _nextReportNumber;

        public ReportGenerator(string reportsDirectory = "Reports")
        {
            _reportsDirectory = reportsDirectory;

            // Создаем директорию для отчетов
            if (!Directory.Exists(_reportsDirectory))
                Directory.CreateDirectory(_reportsDirectory);

            // Определяем следующий номер отчета
            _nextReportNumber = GetNextReportNumber();
        }

        /// Генерирует имя файла для отчета
        private string GenerateReportName()
        {
            string dateStr = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            return $"Подборка_{_nextReportNumber}_от_{dateStr}";
        }

        /// Сохраняет отфильтрованных питомцев в JSON файл
        public string SaveReportAsJson(List<Pet> pets, Shelter<Pet> selectedShelter = null)
        {
            string reportName = GenerateReportName();
            string fullPath = Path.Combine(_reportsDirectory, reportName);

            var reportData = new ReportData
            {
                ReportNumber = _nextReportNumber,
                CreationDate = DateTime.Now,
                SelectedShelter = selectedShelter?.Name ?? "Все приюты",
                Pets = pets,
                TotalCount = pets.Count
            };

            var jsonSerializer = new JsonFileManager();
            jsonSerializer.Save(fullPath, reportData);

            _nextReportNumber++;

            return fullPath + ".json";
        }

        /// Сохраняет отфильтрованных питомцев в XML файл
        public string SaveReportAsXml(List<Pet> pets, Shelter<Pet> selectedShelter = null)
        {
            string reportName = GenerateReportName();
            string fullPath = Path.Combine(_reportsDirectory, reportName);

            var reportData = new ReportData
            {
                ReportNumber = _nextReportNumber,
                CreationDate = DateTime.Now,
                SelectedShelter = selectedShelter?.Name ?? "Все приюты",
                Pets = pets,
                TotalCount = pets.Count
            };

            var xmlSerializer = new XMLFileManager();
            xmlSerializer.Save(fullPath, reportData);

            _nextReportNumber++;

            return fullPath + ".xml";
        }

        /// Загружает отчет по номеру (автоматически определяет формат)
        public ReportData LoadReport(int reportNumber)
        {
            // Ищем JSON файл
            var jsonFiles = Directory.GetFiles(_reportsDirectory, $"Подборка_{reportNumber}_*.json");
            if (jsonFiles.Length > 0)
            {
                string pathWithoutExt = Path.Combine(_reportsDirectory,
                    Path.GetFileNameWithoutExtension(jsonFiles[0]));
                var jsonSerializer = new JsonFileManager();
                return jsonSerializer.Load<ReportData>(pathWithoutExt);
            }

            // Ищем XML файл
            var xmlFiles = Directory.GetFiles(_reportsDirectory, $"Подборка_{reportNumber}_*.xml");
            if (xmlFiles.Length > 0)
            {
                string pathWithoutExt = Path.Combine(_reportsDirectory,
                    Path.GetFileNameWithoutExtension(xmlFiles[0]));
                var xmlSerializer = new XMLFileManager();
                return xmlSerializer.Load<ReportData>(pathWithoutExt);
            }

            return null;
        }

        /// Получает все отчеты
        public List<ReportData> GetAllReports()
        {
            var reports = new List<ReportData>();

            // Загружаем JSON отчеты
            var jsonFiles = Directory.GetFiles(_reportsDirectory, "*.json");
            foreach (var file in jsonFiles)
            {
                string pathWithoutExt = Path.Combine(_reportsDirectory,
                    Path.GetFileNameWithoutExtension(file));
                var jsonFileManager = new JsonFileManager();
                var report = jsonFileManager.Load<ReportData>(pathWithoutExt);
                if (report != null)
                    reports.Add(report);
            }

            // Загружаем XML отчеты
            var xmlFiles = Directory.GetFiles(_reportsDirectory, "*.xml");
            foreach (var file in xmlFiles)
            {
                string pathWithoutExt = Path.Combine(_reportsDirectory,
                    Path.GetFileNameWithoutExtension(file));
                var xmlSerializer = new XMLFileManager();
                var report = xmlSerializer.Load<ReportData>(pathWithoutExt);
                if (report != null && !reports.Any(r => r.ReportNumber == report.ReportNumber))
                    reports.Add(report);
            }

            return reports.OrderBy(r => r.ReportNumber).ToList();
        }

        private int GetNextReportNumber()
        {
            var reports = GetAllReports();
            if (!reports.Any())
                return 1;

            return reports.Max(r => r.ReportNumber) + 1;
        }

        /// Копирует данные из всех отчетов текущего формата в новый формат
        public void CopyReportsToFormat(string targetFormat)
        {
            var allReports = GetAllReports();

            foreach (var report in allReports)
            {
                string reportName = $"Подборка_{report.ReportNumber}_от_{report.CreationDate:yyyy-MM-dd_HH-mm-ss}";
                string fullPath = Path.Combine(_reportsDirectory, reportName);

                if (targetFormat.ToLower() == "json")
                {
                    var jsonSerializer = new JsonFileManager();
                    jsonSerializer.Save(fullPath, report);
                }
                else if (targetFormat.ToLower() == "xml")
                {
                    var xmlSerializer = new XMLFileManager();
                    xmlSerializer.Save(fullPath, report);
                }
            }
        }
    }

    /// DTO для хранения данных отчета
    [Serializable]
    public class ReportData
    {
        public int ReportNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public string SelectedShelter { get; set; }
        public List<Pet> Pets { get; set; }
        public int TotalCount { get; set; }
    }
}
}
