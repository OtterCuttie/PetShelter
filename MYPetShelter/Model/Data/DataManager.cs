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
        private readonly FileManager _fileManager;
        private readonly ReportGenerator _reportGenerator;

        public DataManager(FileManager fileManager,ReportGenerator reportGenerator)
        {
            _fileManager = fileManager;
            _reportGenerator = reportGenerator;
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
    }
}
