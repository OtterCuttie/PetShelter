using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
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
}
