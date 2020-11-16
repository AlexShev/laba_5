using System;
using System.Collections.Generic;
using System.Text;

namespace laba_5
{
    class Admin:Human
    {
        public Admin(string login, string passward) : base(login, passward) { }

        public override bool Posibility() { return true; }
    }
}
