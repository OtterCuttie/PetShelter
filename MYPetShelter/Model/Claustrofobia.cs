using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Pet
    {
        private bool _hasClaustrophobia;
        public bool HasClaustrophobia => _hasClaustrophobia;
        public Pet(bool hasClaustrophobia)
        {
            _hasClaustrophobia = hasClaustrophobia;
        }
}
}
