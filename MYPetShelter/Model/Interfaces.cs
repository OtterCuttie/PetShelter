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
        public int Count(Pet Type);
        public int Percentage(Pet Type);
    }
    public interface IFilter
    {
        public void Filter(Pet Type);
    }
}