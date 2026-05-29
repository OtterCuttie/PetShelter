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
        private readonly DataManager _dataManager;

        private List<Shelter<Pet>> _shelters;

        public ShelterRepository(DataManager dataManager)
        {
            _dataManager = dataManager;
            LoadOrCreateShelters();
        }

        private void LoadOrCreateShelters()
        {
            var loadedShelters =_dataManager.LoadShelters();
            if (loadedShelters == null ||!loadedShelters.Any())
            {
                _shelters =CreateTestShelters();
                _dataManager.SaveShelters(_shelters);
            }
            else
            {
                _shelters =loadedShelters;
            }
        }

        public List<Shelter<Pet>> GetAll()
        {
            return _shelters.ToList();
        }

        public Shelter<Pet> GetByName(string name)
        {
            return _shelters.FirstOrDefault(shelter => shelter.Name == name);
        }

        public void AddShelter(Shelter<Pet> shelter)
        {
            _shelters.Add(shelter);
            _dataManager.SaveShelters(_shelters);
        }

        public void RemoveShelter(
            Shelter<Pet> shelter)
        {
            _shelters.Remove(shelter);
            _dataManager.SaveShelters(_shelters);
        }

        public void Save()
        {
            _dataManager.SaveShelters(_shelters);
        }

        private List<Shelter<Pet>>
            CreateTestShelters()
        {
            var allPets = CreateTestPets();

            var shelters =new List<Shelter<Pet>>();

            int petsPerShelter = 5;
            int currentIndex = 0;

            var shelter1 =
                new Shelter<Pet>("Солнечный приют",20,true);

            foreach (var pet in allPets.Skip(currentIndex).Take(petsPerShelter))
            {
                shelter1.AddPet(pet);
            }

            currentIndex += petsPerShelter;
            shelters.Add(shelter1);
            var shelter2 =new Shelter<Pet>("Уютный дом",15,false);

            foreach (var pet in allPets.Skip(currentIndex).Take(petsPerShelter))
            {
                shelter2.AddPet(pet);
            }

            currentIndex += petsPerShelter;

            shelters.Add(shelter2);

            var shelter3 =new Shelter<Pet>("Приятный приют",30,true);

            foreach (var pet in allPets.Skip(currentIndex).Take(petsPerShelter))
            {
                shelter3.AddPet(pet);
            }
            shelters.Add(shelter3);
            return shelters;
        }

        private List<Pet> CreateTestPets()
        {
            var pets = new List<Pet>();

            pets.Add(new Cat("Марк", 3, 5.5, "Сфинкс", 2.2, false));
            pets.Add(new Cat("Вил", 2, 4.2, "Белоруссый", 1.4, false));
            pets.Add(new Cat("Снежок", 5, 6.0, "Белый и пушистый", 4.3, true));

            pets.Add(new Dog("Собяка-кобяка", 4, 15.5, "Очень милый и тихий", "Дворняга", true));
            pets.Add(new Dog("Рук", 2, 8.2, "Любит играть с котами", "Коли", true));
            pets.Add(new Dog("Рекс", 6, 28.0, "Добрый и громкий", "Немецкая овчарка", true));

            pets.Add(new Rabbit("Бонни", 2, 2.5, "Громко орет", "Синяя шерсть", 15));
            pets.Add(new Rabbit("Сильвер", 1, 1.8, "Любит спать", "Белая шерсть", 13));
            pets.Add(new Rabbit("Ушастик", 3, 3.0, "Ласковый", "Серая шерсть", 17));

            pets.Add(new Fox("Алиса", 3, 6, "Шрам на глазу", true, true));
            pets.Add(new Fox("Фокси", 3, 6, "Пират", true, false));

            pets.Add(new Parrot("Найджел", 3, 7, "Очень вредный", 35, true));
            pets.Add(new Parrot("Яго", 2, 6, "Похож на попугая Джафара", 20, true));

            pets.Add(new Raccoon("Камыш", 3, 6, "Любит печеньки", 5, true));
            pets.Add(new Raccoon("Разрушитель", 4, 6, "Разрушитель всего", 10, false));

            return pets;
        }
    }
}
