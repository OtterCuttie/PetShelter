using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Shelter : ICountable, IFilter
    {
        private string _name; private int _maxCapacity; private bool _hasOpenArea;
        private List<Pet> _pets;                // КОЛЛЕКЦИЯ ПИТОМЦЕВ
        public string Name => _name; public int MaxCapacity => _maxCapacity; public bool HasOpenArea => _hasOpenArea;
        public List<Pet> Pets => _pets;
        public Shelter(string name, int maxCapacity, bool hasOpenArea)
        {
            _name = name;
            _maxCapacity = maxCapacity;
            _hasOpenArea = hasOpenArea;
            _pets = new List<Pet>();
        }
        public int Count()
        {
            return _pets.Count;                 //  LINQ-метод Count()
        }
        public int Count(Pet Type)
        {
            return _pets.Count(p=>p.GetType()==type);
        }
        public int Percentage(Pet Type)
        {
            int all_pets=Count();
            if (all_pets == 0) return 0;
            int countType = Count(Type);
            return (int)Math.Round(countType * 100 /(double)all_pets);
        }
        public List<Pet> Filter(Pet Type)
        {
            return _pets.Where(p=>p.GetType()==type || p.GetType() == null).ToList();      //  ОСТАВЛЯЕТ ЖИВОТНЫХ ДАННОГО ТИПА
        }
        public List<Pet> Filter(Pet Type, bool hasClaustrophobia)
        {
            List<Pet> filter_pets = new List<Pet>();
            for (int i = 0; i < _pets.Count; i++)
            {
                bool  b_type=false;
                if (Type == null || _pets[i].GetType() == Type.GetType()) b_type = true;
                bool b_hasClaus = false;
                if (_pets[i].HasClaustrophobia == hasClaustrophobia) b_hasClaus = true;
                if (b_type && b_hasClaus) filter_pets.Add(_pets[i]);
            }
            return filter_pets;
        }
    }
}
