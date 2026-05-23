using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Model
{
    public partial class Pet
    {
        private string _name; private int _age; private double _weight; 
        private string _specialMarks;                                                                
        public string Name => _name; public int Age => _age; public double Weight => _weight;
        public string SpecialMarks => _specialMarks;
        public Pet(string name, int age, double weight, string specialMarks)
        {
            _name=name; _age=age; _weight=weight; _specialMarks = specialMarks;
        }
    }
    public class Cat : Pet
    {
        private double _averageFurLength; private bool _catchesRodents;
        public double AverageFurLength => _averageFurLength; public bool CatchesRodentsme => _catchesRodents;
        public Cat(string name, int age, double weigth,string specialMarks) : base(name, age, weigth, specialMarks)
        { 
        
        }
    }
    public class Dog : Pet
    {
        private string _breed; private bool _isTrained;
        public string Breed => _breed; public bool IsTrained => _isTrained;
        public Dog(string name, int age, double weigth, string specialMarks) : base(name, age, weigth, specialMarks)
        {

        }
    }
    public class Rabbit : Pet
    {
        private string _fur; private double _lenghOfEars;
        public string Fur => _fur; public double LenghOfEarsg => _lenghOfEars;
        public Rabbit(string name, int age, double weigth, string specialMarks) : base(name, age, weigth, specialMarks)
        {
            
        }
    }
    public class Fox : Pet
        {
            private int _huntingSkills; private bool _tamingLevel;
            public int HuntingSkills => _huntingSkills; public bool TamingLevel => _tamingLevel;
        public Fox(string name, int age, double weigth, string specialMarks) : base(name, age, weigth, specialMarks)
        {

        }
    }
    public class Raccoon : Pet
    {
        private int _destructionLevel; private bool _handFed;
        public int DestructionLevel => _destructionLevel; public bool HandFed => _handFed;
        public Raccoon(string name, int age, double weigth, string specialMarks) : base(name, age, weigth, specialMarks)
        {

        }
    }
    public class Parrot : Pet
    {
        private double _wingLength; private bool _isTalking;
        public double WingLength => _wingLength; public bool IsTalking => _isTalking;
        public Parrot(string name, int age, double weigth, string specialMarks) : base(name, age, weigth, specialMarks)
        {

        }
    }

}
