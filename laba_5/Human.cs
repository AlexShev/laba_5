using System;
using System.Collections.Generic;
using System.Text;

namespace laba_5
{
    abstract class Human
    {
        private readonly string _login;

        private readonly string _password;

        public string Login { get{ return _login; } }

        public Human(string login, string password)
        {
            StandartView.LoginEr(login);
            _login = login;
            _password = password;
        }

        public bool IsMyPassword(string password)
        {
            return _password == password;
        }

        public abstract bool Posibility();
    }
}
