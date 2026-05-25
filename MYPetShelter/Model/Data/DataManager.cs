using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model.Data
{
    public class DataManager
    {
        public ShelterRepository ShelterRepository { get; private set; }
        public ReportGenerator ReportGenerator { get; private set; }

        private string _currentReportFormat;

        public DataManager(string dataDirectory = "Data", string reportsDirectory = "Reports")
        {
            ShelterRepository = new ShelterRepository(new JsonFileManager(), dataDirectory);
            ReportGenerator = new ReportGenerator(reportsDirectory);
            _currentReportFormat = "json"; // формат по умолчанию
        }

        /// Текущий формат отчетов
        public string CurrentReportFormat
        {
            get => _currentReportFormat;
            set
            {
                if (_currentReportFormat != value && (value == "json" || value == "xml"))
                {
                    string oldFormat = _currentReportFormat;
                    _currentReportFormat = value;
                    OnFormatChanged(oldFormat, value);
                }
            }
        }

        /// Сохранить отчет с подходящими питомцами в текущем формате
        public string SaveCurrentReport(List<Pet> pets, Shelter<Pet> selectedShelter = null)
        {
            if (_currentReportFormat == "json")
            {
                return ReportGenerator.SaveReportAsJson(pets, selectedShelter);
            }
            else
            {
                return ReportGenerator.SaveReportAsXml(pets, selectedShelter);
            }
        }

        /// Обработчик смены формата
        private void OnFormatChanged(string oldFormat, string newFormat)
        {
            // При смене формата копируем данные из всех отчетов старого формата в новый
            ReportGenerator.CopyReportsToFormat(newFormat);
        }

        /// Получить всех питомцев из всех приютов
        public List<Pet> GetAllPets()
        {
            return ShelterRepository.GetAllPets();
        }

        /// Получить питомцев по фильтрам
        public List<Pet> GetFilteredPets(Shelter<Pet> selectedShelter, Type animalType, bool? requireOpenArea = null)
        {
            var sheltersToSearch = selectedShelter != null
                ? new List<Shelter<Pet>> { selectedShelter }
                : ShelterRepository.GetAll();

            if (requireOpenArea.HasValue)
            {
                sheltersToSearch = sheltersToSearch.Where(s => s.HasOpenArea == requireOpenArea.Value).ToList();
            }

            var result = new List<Pet>();

            foreach (var shelter in sheltersToSearch)
            {
                var pets = shelter.Filter(animalType);
                result.AddRange(pets);
            }

            return result;
        }

         /// Добавить питомца в приют
        public bool AddPetToShelter(Pet pet, Shelter<Pet> shelter)
        {
            try
            {
                shelter.AddPet(pet);
                ShelterRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// Удалить питомца из приюта
        public bool RemovePetFromShelter(Pet pet, Shelter<Pet> shelter)
        {
            shelter.RemovePet(pet);
            ShelterRepository.Save();
            return true;
        }
    }
}
