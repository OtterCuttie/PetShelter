using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Pet
    {
        protected bool _hasClaustrophobia;
        public bool HasClaustrophobia => _hasClaustrophobia;
        public Pet(bool hasClaustrophobia)
        {
            _hasClaustrophobia = hasClaustrophobia;
        }
    }
    public partial class Cat
    {
        protected bool _hasClaustrophobia;
        public bool HasClaustrophobia => _hasClaustrophobia;
        public Cat(bool hasClaustrophobia) : base(hasClaustrophobia)
        {
            _hasClaustrophobia = hasClaustrophobia;
        }
    }
    public partial class Dog
    {
        protected bool _hasClaustrophobia;
        public bool HasClaustrophobia => _hasClaustrophobia;
        public Dog(bool hasClaustrophobia) : base(hasClaustrophobia)
        {
            _hasClaustrophobia = hasClaustrophobia;
        }
    }
    public partial class Fox
    {
        protected bool _hasClaustrophobia;
        public bool HasClaustrophobia => _hasClaustrophobia;
        public Fox(bool hasClaustrophobia = true) : base(hasClaustrophobia)
        {
            _hasClaustrophobia = true;
        }
    }
    public partial class Parrot
    {
        protected bool _hasClaustrophobia;
        public bool HasClaustrophobia => _hasClaustrophobia;
        public Parrot(bool hasClaustrophobia = true) : base(hasClaustrophobia)
        {
            _hasClaustrophobia = true;
        }
    }
    public partial class Rabbit
    {
        protected bool _hasClaustrophobia;
        public bool HasClaustrophobia => _hasClaustrophobia;
        public Rabbit(bool hasClaustrophobia = false) : base(hasClaustrophobia)
        {
            _hasClaustrophobia = false;
        }
    }
    public partial class Raccoon
    {
        protected bool _hasClaustrophobia;
        public bool HasClaustrophobia => _hasClaustrophobia;
        public Raccoon(bool hasClaustrophobia) : base(hasClaustrophobia)
        {
            _hasClaustrophobia = hasClaustrophobia;
        }
    }
}
