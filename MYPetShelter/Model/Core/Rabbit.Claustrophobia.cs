using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Rabbit
    {
        protected bool _hasClaustrophobia;
        public bool HasClaustrophobia => _hasClaustrophobia;
        public Rabbit(bool hasClaustrophobia=false) : base(hasClaustrophobia)
        {
            _hasClaustrophobia = false;
        }
    }
}
