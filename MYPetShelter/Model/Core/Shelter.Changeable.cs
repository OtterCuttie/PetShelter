using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Model.Core
{
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
