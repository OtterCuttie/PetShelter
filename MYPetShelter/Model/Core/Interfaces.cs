using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface ICountable
    {
        public int Count();
        public int Count(Type Type);
        public int Percentage(Type Type);
    }
    public interface IFilter
    {
        public List<Pet> Filter(Type Type);
    }
    public interface IChangeable
    {
        void AddPet(Pet pet);
        void RemovePet(Pet pet);
    }
}