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
        private string _name; private int _age; private double _weight; // iswild, arrivaldate, hasvaccinated,
                                                                        // special marks(colour, scar, habbits,behavior etc)
        public string Name => _name; public int Age => _age; public double Weight => _weight;
        public Pet(string name, int age, double weight)
        {
            _name=name; _age=age; _weight=weight;
        }
    }
    public class Cat : Pet
    {
        private double _averageFurLength; private bool _catchesRodents;
        public double AverageFurLength => _averageFurLength; public bool CatchesRodentsme => _catchesRodents;
        public class Cat(_averageFurLength) : base(name,)
        { 
        
        }
    }
    public class Dog : Pet
    {
        private string _breed; private bool _isTrained;
        public string Breed => _breed; public bool IsTrained => _isTrained;
    }
    public class Fox : Pet
    {
        private int _huntingSkills; private bool _tamingLevel;
        public int HuntingSkills => _huntingSkills; public bool TamingLevel => _tamingLevel;
    }
    public class Racoon : Pet
    {
        private int _destructionLevel; private bool _handFed;
        public int DestructionLevel => _destructionLevel; public bool HandFed => _handFed;
    }
    public class Parrot : Pet
    {
        private double _wingLength; private bool _isTalking;
        public double WingLength => _wingLength; public bool IsTalking => _isTalking;
    }
}
