using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Shelter<T> : ICountable where T : Pet
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public bool HasOpenArea { get; set; }

        protected List<T> _pets = new List<T>();

        public Shelter(string name, int capacity, bool hasOpenArea)
        {
            Name = name;
            Capacity = capacity;
            HasOpenArea = hasOpenArea;
        }

        public List<T> GetPets()
        {
            return _pets.ToList();
        }

        public int Count()
        {
            return _pets.Count;
        }

        public int Count(Type Type)
        {
            return _pets.Count(pet => pet.GetType() == Type);
        }

        public int Percentage(Type Type)
        {
            if (_pets.Count == 0) return 0;
            return (int)((double)Count(Type) / _pets.Count * 100);
        }
        public List<Pet> Filter(Type Type)
        {
            // Создаем делегат для фильтрации по типу
            Func<T, bool> filterDelegate = (pet) => pet.GetType() == Type;

            // Применяем делегат
            return _pets.Where(filterDelegate)
                       .Cast<Pet>()
                       .ToList();
        }
    }

    public partial class Shelter<T> where T : Pet
    {
        /// Перегрузка метода Filter для фильтрации по виду и клаустрофобии
        public List<Pet> Filter(Type animalType, bool hasClaustrophobia)
        {
            // Создаем делегат для фильтрации по типу
            Func<T, bool> typeFilter = (pet) => pet.GetType() == animalType;

            // Создаем делегат для фильтрации по клаустрофобии
            Func<T, bool> claustrophobiaFilter = (pet) => pet.HasClaustrophobia == hasClaustrophobia;

            // Применяем оба делегата последовательно
            var filteredByType = _pets.Where(typeFilter);
            var filteredByBoth = filteredByType.Where(claustrophobiaFilter);

            return filteredByBoth.Cast<Pet>().ToList();
        }
    }

    public partial class Shelter<T> : IChangeable where T : Pet
    {
        /// Добавление животного в приют (реализация IChangeable)
        public void AddPet(Pet pet)
        {
            if (pet is T typedPet)
            {
                // Проверка клаустрофобии
                if (typedPet.HasClaustrophobia && !HasOpenArea)
                {
                    throw new InvalidOperationException(
                        $"Животное {typedPet.Name} с клаустрофобией нельзя поместить в закрытый приют {Name}");
                }

                // Проверка вместимости
                if (_pets.Count >= Capacity)
                {
                    throw new InvalidOperationException($"Приют {Name} переполнен!");
                }

                _pets.Add(typedPet);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Приют {Name} принимает только животных типа {typeof(T).Name}");
            }
        }

        /// Удаление животного из приюта (реализация IChangeable)
        public void RemovePet(Pet pet)
        {
            if (pet is T typedPet)
            {
                _pets.Remove(typedPet);
            }
        }
    }
}
