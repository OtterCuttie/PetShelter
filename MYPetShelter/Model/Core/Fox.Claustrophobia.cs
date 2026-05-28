using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Fox
    {
        protected bool _hasClaustrophobia;
        public bool HasClaustrophobia => _hasClaustrophobia;
        public Fox(bool hasClaustrophobia=true) : base(hasClaustrophobia)
        {
            _hasClaustrophobia = true;
        }
    }
}
