using System;
using System.Linq;

namespace laba_5
{
    class StandartView
    {
        //  ох уж эти многосоставные имена да фамилии...
        public static string ConverteToStandartString(string str)
        {
            string[] strArr = str.Split('-',StringSplitOptions.RemoveEmptyEntries);

            str = null;

            for (int i = 0; i < strArr.Length; i++)
            {
                strArr[i] = ConverteToStandartWord(strArr[i]);
                str += (strArr.Length - i == 1) ? strArr[i] : strArr[i] + '-';
            }

            return str;
        }

        public static string ConverteToStandartWord(string word)
        {
            WordEr(word);

            return word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();
        }

        public static bool IsAWord(string word)
        {
            return word.ToCharArray().All(char.IsLetter) && word.Length >= 2;
        }

        public static void WordEr(string word)
        {
            if (!IsAWord(word)) throw new System.Exception($"ошибка в {word} это вырожение не имеет смысла");
        }

        public static bool IsLogin(string login)
        {
            foreach (char c in login)
            {
                if (!(char.IsLetterOrDigit(c) || c == '@' || c == '_'|| c == '.'))
                    return false;
            }

            return true;
        }

        public static void LoginEr(string login)
        {
            if (!IsLogin(login)) throw new System.Exception($"ошибка в {login} это не формат логина");
        }

        // несколько не понял, как это работает, пришлось просто переделать, неосознавая(
        static public string ConverteToStandartPhoneNumber(string phoneNumber)
        {
            PhoneNumberEr(phoneNumber);

            phoneNumber = new System.Text.RegularExpressions.Regex(@"\D").Replace(phoneNumber, string.Empty);

            if (phoneNumber.Length == 11)
                phoneNumber = Convert.ToInt64(phoneNumber).ToString("#(###)###-##-##");
            else
                throw new System.Exception($"{phoneNumber} не поддерживаемый формат");

            return (!phoneNumber.StartsWith('8')) ? '+' + phoneNumber : phoneNumber;
        }

        public static bool IsPhoneNumber(string phoneNumber)
        {
            foreach (char c in phoneNumber)
            {
                if (!(char.IsDigit(c) || c == '+' || c == '(' || c == '-' || c == ')' || c == ' '))
                    return false;
            }
            return true;
        }

        public static void PhoneNumberEr(string phoneNumber)
        {
            if (!IsPhoneNumber(phoneNumber)) throw new System.Exception($"{phoneNumber} не номер телефона");
        }

        public static DateTime ConverteStringToDate(string str)
        {
            string[] strArr = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int size = (strArr[strArr.Length - 1] == "года") ? strArr.Length - 1 : strArr.Length;

            string[] temp = new string [size];

            for (int i = 0; i < temp.Length; i++) 
            {
                temp[i] = strArr[i];
            }

            // он же умный сам по себе зачем усложнять
            if (temp.Length != 3) throw new Exception("Не павильно введённая дата");

            var tempDate = new DateTime(int.Parse(temp[2]), Mounth(temp[1]), int.Parse(temp[0]));

            if (DateTime.Now < tempDate) throw new Exception("Человек пока ещё не родился");

            return tempDate;
        }

        private static int Mounth(string mounth)
        {
            string temp = ConverteToStandartWord(mounth);

            if (temp == "Января") return 1;
            else if (temp == "Февраля") return 2;
            else if (temp == "Марта") return 3;
            else if (temp == "Апреля") return 4;
            else if (temp == "Мая") return 5;
            else if (temp == "Июня") return 6;
            else if (temp == "Июля") return 7;
            else if (temp == "Августа") return 8;
            else if (temp == "Сентября") return 9;
            else if (temp == "Октября") return 10;
            else if (temp == "Ноября") return 11;
            else if (temp == "Декабря") return 12;
            else throw new Exception("Такого месяца нет");
        }
    }
}