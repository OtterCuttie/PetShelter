using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Shelter() : ICountable, IFilter
    {
        private string _name; private int _maxCapacity; private bool _hasOpenArea;
        public string Name => _name; public int MaxCapacity => _maxCapacity; public bool HasOpenArea => _hasOpenArea;

        public int Count()
        {
            throw new NotImplementedException();
        }
        public int Count(Pet Type)
        {
            throw new NotImplementedException();
        }
        public void Filter(Pet Type)
        {
            throw new NotImplementedException();
        }
        public int Percentage(Pet Type)
        {
            throw new NotImplementedException();
        }
    }
}
