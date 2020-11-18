using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace laba_5
{
    abstract class Human
    {
        private readonly string _login;

        private readonly byte[] _password;

        public string Login { get{ return _login; } }

        public Human(string login, string password)
        {
            StandartView.LoginEr(login);

            _login = login;

            _password = HachPassword(password);
        }

        public bool IsMyPassword(string password)
        {
            var temp = HachPassword(password);

            return _password.SequenceEqual(temp);
        }

        private byte[] HachPassword(string password)
        {
            byte[] tmpSource;

            tmpSource = ASCIIEncoding.ASCII.GetBytes(password);

            return new MD5CryptoServiceProvider().ComputeHash(tmpSource);
        }

        public abstract bool IsAvailable();
    }
}