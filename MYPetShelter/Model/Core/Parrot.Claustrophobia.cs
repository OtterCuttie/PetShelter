using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Parrot
    {
        protected bool _hasClaustrophobia;
        public bool HasClaustrophobia => _hasClaustrophobia;
        public Parrot(bool hasClaustrophobia = true) : base(hasClaustrophobia)
        {
            _hasClaustrophobia = true;
        }
    }
}
