using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Core;

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

            int petsPerShelter = 5;
            int currentIndex = 0;

            // Приют 1
            var shelter1 = new Shelter<Pet>("Солнечный приют", 20, true);
            foreach (var pet in allPets.Skip(currentIndex).Take(petsPerShelter))
            {
                shelter1.AddPet(pet);
            }
            currentIndex += petsPerShelter;
            shelters.Add(shelter1);

            // Приют 2
            var shelter2 = new Shelter<Pet>("Уютный дом", 15, false);
            foreach (var pet in allPets.Skip(currentIndex).Take(petsPerShelter))
            {
                shelter2.AddPet(pet);
            }
            currentIndex += petsPerShelter;
            shelters.Add(shelter2);

            // Приют 3
            var shelter3 = new Shelter<Pet>("Приятный приют", 30, true);
            foreach (var pet in allPets.Skip(currentIndex).Take(petsPerShelter))
            {
                shelter3.AddPet(pet);
            }
            currentIndex += petsPerShelter;
            shelters.Add(shelter3);

            return shelters;
        }

        private List<Pet> CreateTestPets()
        {
            var pets = new List<Pet>();

            pets.Add(new Cat("Марк", 3, 5.5, "Сфинкс",2.2,false));
            pets.Add(new Cat("Вил", 2, 4.2, "белоруссый",1.4, false));
            pets.Add(new Cat("Снежок", 5, 6.0, "белый и пушистый", 4.3, true));

            pets.Add(new Dog("Собяка-кобяка", 4, 15.5, "Очень милый и тихий","Дворняга", true));
            pets.Add(new Dog("Рук", 2, 8.2, "очень любит играться с котами", "Коли", true));
            pets.Add(new Dog("Рекс", 6, 28.0, "добрый и очень громкий", "Немецкая овчарка", true));

            pets.Add(new Rabbit("Бонни", 2, 2.5, "громко орет","синяя шерсть",15));
            pets.Add(new Rabbit("Сильвер", 1, 1.8, "любит спать", "белая шерсть", 13));
            pets.Add(new Rabbit("Ушастик", 3, 3.0,"ласковый","серая шерсть с черными пятнышками" , 17));

            pets.Add(new Fox("Алиса", 3, 6,"шрам на глазу",true,true));
            pets.Add(new Fox("Фокси", 3, 6, "пират",true,false));

            pets.Add(new Parrot("Найджел", 3, 7, "очень противный, белый", 35,true));
            pets.Add(new Parrot("Яго", 2, 6, "похож на попугая Джафара", 20, true));

            pets.Add(new Raccoon("Камыш", 3, 6, "серый с белыми лапками и любит печеньки", 5, true));
            pets.Add(new Raccoon("Разрушитель", 4, 6, "опасный, РАЗРУШИТЕЛЬ ВСЕГО", 10, false));
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
