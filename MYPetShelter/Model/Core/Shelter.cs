using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
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
}
