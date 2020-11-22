using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace laba_5
{
    public abstract class Human
    {
        private readonly byte[] _password;

        public string Login { get; }

        public Human(string login, string password, bool isStandertView = false)
        {
            if(isStandertView) StandartView.LoginEr(login);

            Login = login;

            _password = HachPassword(password);
        }

        public bool IsMyPassword(string password)
        {
            var temp = HachPassword(password);

            return _password.SequenceEqual(temp);
        }

		// он сказал сделать статическим
        private static byte[] HachPassword(string password)
        {
            byte[] tmpSource;

            tmpSource = ASCIIEncoding.ASCII.GetBytes(password);

            return new MD5CryptoServiceProvider().ComputeHash(tmpSource);
        }

        public static bool operator ==(Human c1, Human c2) => c1.Login == c2.Login && c1._password.SequenceEqual(c2._password);

        public static bool operator !=(Human c1, Human c2) => !(c1 == c2);

        public override bool Equals(object obj)
        {
            var c2 = obj as Human;

            return this == c2;
        }

        // не понимаю - зачем перегружать
        public override int GetHashCode() => System.HashCode.Combine(Login + _password);
    }
}
