using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class ShelterRepository
    {
        private readonly FileManager _serializer;
        private readonly string _sheltersFilePath;
        private List<Shelter<Pet>> _shelters;

        public ShelterRepository(FileManager serializer, string dataDirectory = "Data")
        {
            _serializer = serializer;

            if (!Directory.Exists(dataDirectory))
                Directory.CreateDirectory(dataDirectory);

            _sheltersFilePath = Path.Combine(dataDirectory, "shelters");
            LoadOrCreateShelters();
        }

        private void LoadOrCreateShelters()
        {
            var loadedShelters = _serializer.Load<List<Shelter<Pet>>>(_sheltersFilePath);

            if (loadedShelters == null || !loadedShelters.Any())
            {
                _shelters = CreateTestShelters();
                Save();
            }
            else
            {
                _shelters = loadedShelters;
            }
        }

        private List<Shelter<Pet>> CreateTestShelters()
        {
            var allPets = CreateTestPets();
            var shelters = new List<Shelter<Pet>>();

            // Приют 1 - с открытой территорией
            var shelter1 = new Shelter<Pet>("Солнечный приют", 20, true);
            foreach (var pet in allPets.Take(4))
            {
                shelter1.AddPet(pet);  // Используем AddPet из IChangeable
            }
            shelters.Add(shelter1);

            // Приют 2 - закрытый (без открытой территории)
            var shelter2 = new Shelter<Pet>("Уютный дом", 15, false);
            foreach (var pet in allPets.Skip(4).Take(4))
            {
                shelter2.AddPet(pet);
            }
            shelters.Add(shelter2);

            // Приют 3 - с открытой территорией
            var shelter3 = new Shelter<Pet>("ЗооМир", 30, true);
            foreach (var pet in allPets.Skip(8).Take(4))
            {
                shelter3.AddPet(pet);
            }
            shelters.Add(shelter3);

            // Приют 4 - с открытой территорией
            var shelter4 = new Shelter<Pet>("Ласковый зверь", 10, true);
            foreach (var pet in allPets.Skip(12).Take(3))
            {
                shelter4.AddPet(pet);
            }
            shelters.Add(shelter4);

            return shelters;
        }

        private List<Pet> CreateTestPets()
        {
            var pets = new List<Pet>();

            // Кошки (5 шт)
            pets.Add(new Cat("Мурзик", 3, 5.5, true, "Британец", true));
            pets.Add(new Cat("Барсик", 2, 4.2, false, "Сиамская", false));
            pets.Add(new Cat("Снежок", 5, 6.0, true, "Персидская", true));
            pets.Add(new Cat("Рыжик", 1, 3.8, false, "Дворовая", true));
            pets.Add(new Cat("Васька", 4, 5.0, true, "Мейн-кун", false));

            // Собаки (5 шт)
            pets.Add(new Dog("Шарик", 4, 15.5, false, "Дворняга", true));
            pets.Add(new Dog("Бобик", 2, 8.2, false, "Йоркширский терьер", false));
            pets.Add(new Dog("Рекс", 6, 28.0, false, "Немецкая овчарка", true));
            pets.Add(new Dog("Дружок", 3, 12.0, false, "Лабрадор", true));
            pets.Add(new Dog("Тузик", 1, 5.5, false, "Такса", false));

            // Кролики (5 шт)
            pets.Add(new Rabbit("Пушок", 2, 2.5, true, 5, true));
            pets.Add(new Rabbit("Зайка", 1, 1.8, true, 3, false));
            pets.Add(new Rabbit("Ушастик", 3, 3.0, true, 7, true));
            pets.Add(new Rabbit("Марти", 2, 2.2, false, 4, true));
            pets.Add(new Rabbit("Снежок", 1, 1.5, true, 6, false));

            return pets;
        }

        public List<Shelter<Pet>> GetAll() => _shelters.ToList();

        public Shelter<Pet> GetByName(string name)
        {
            return _shelters.FirstOrDefault(s => s.Name == name);
        }

        public void Add(Shelter<Pet> shelter)
        {
            if (!_shelters.Contains(shelter))
            {
                _shelters.Add(shelter);
                Save();
            }
        }

        public void Remove(Shelter<Pet> shelter)
        {
            _shelters.Remove(shelter);
            Save();
        }

        public void Save()
        {
            _serializer.Save(_sheltersFilePath, _shelters);
        }

        public List<Pet> GetAllPets()
        {
            var allPets = new List<Pet>();
            foreach (var shelter in _shelters)
            {
                allPets.AddRange(shelter.GetPets());
            }
            return allPets;
        }
    }
}
