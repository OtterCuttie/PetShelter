using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Model.Core
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
    public partial class Cat : Pet
    {
        private double _averageFurLength; private bool _catchesRodents;
        public double AverageFurLength => _averageFurLength; public bool CatchesRodentsme => _catchesRodents;
        public Cat(string name, int age, double weigth,string specialMarks, double averageFurLength,  bool catchesRodents) : base(name, age, weigth, specialMarks)
        {
            _averageFurLength = averageFurLength;
            _catchesRodents = catchesRodents;
        }
    }
    public partial class Dog : Pet
    {
        private string _breed; private bool _isTrained;
        public string Breed => _breed; public bool IsTrained => _isTrained;
        public Dog(string name, int age, double weigth, string specialMarks,  string breed, bool isTrained) : base(name, age, weigth, specialMarks)
        {
            _breed = breed;
            _isTrained = isTrained;
        }
    }
    public partial class Rabbit : Pet
    {
        private string _fur; private double _lenghOfEars;
        public string Fur => _fur; public double LenghOfEarsg => _lenghOfEars;
        public Rabbit(string name, int age, double weigth, string specialMarks, string fur, double lenghOfEars) : base(name, age, weigth, specialMarks)
        {
            _fur = fur;
            _lenghOfEars = lenghOfEars;
        }
    }
    public partial class Fox : Pet
    {
        private int _huntingSkills; private bool _tamingLevel;
        public int HuntingSkills => _huntingSkills; public bool TamingLevel => _tamingLevel;
        public Fox(string name, int age, double weigth, string specialMarks, int huntingSkills, bool tamingLevel) : base(name, age, weigth, specialMarks)
        {
            _huntingSkills = huntingSkills;
            _tamingLevel = tamingLevel;
        }
    }
    public partial class Raccoon : Pet
    {
        private int _destructionLevel; private bool _handFed;
        public int DestructionLevel => _destructionLevel; public bool HandFed => _handFed;
        public Raccoon(string name, int age, double weigth, string specialMarks, int destructionLevel, bool handFed) : base(name, age, weigth, specialMarks)
        {
            _destructionLevel = destructionLevel;
            _handFed = handFed;
        }
    }
    public partial class Parrot : Pet
    {
        private double _wingLength; private bool _isTalking;
        public double WingLength => _wingLength; public bool IsTalking => _isTalking;
        public Parrot(string name, int age, double weigth, string specialMarks,  double wingLength, bool isTalking) : base(name, age, weigth, specialMarks)
        {
            _wingLength = wingLength;
            _isTalking = isTalking;
        }
    }

}
