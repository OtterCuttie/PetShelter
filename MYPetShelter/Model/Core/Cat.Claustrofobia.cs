using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Cat
    {
        protected bool _hasClaustrophobia;
        public bool HasClaustrophobia => _hasClaustrophobia;
        public Cat(bool hasClaustrophobia):base (hasClaustrophobia)
        {
            _hasClaustrophobia = hasClaustrophobia;
        }
    }
}
