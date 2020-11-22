using System;
using System.Collections.Generic;
using System.Linq;

namespace laba_5
{
    public class StandartView
    {
        public static string[] ToStringArray(string str, char separator = ' ')
            => str.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);

        //  ох уж эти многосоставные имена да фамилии и города...
        public static string ConverteToStandartString(string str)
        {
            var strArr = ToStringArray(str, '-');

            str = null;

            for (var i = 0; i < strArr.Length; i++)
            {
                strArr[i] = ConverteToStandartWord(strArr[i]);
                str += (strArr.Length - i == 1) ? strArr[i] : strArr[i] + '-';
            }

            return str;
        }

        public static string ConverteToStandartWord(string word)
        {
            WordEr(word);
			// ну это его идея использовать перечисления
            return word.Substring(0, 1).ToUpper() + word[1..].ToLower();
        }

        public static bool IsAWord(string word) => word.ToCharArray().All(char.IsLetter) && word.Length > 0;

        public static void WordEr(string word)
        {
            if (!IsAWord(word))
            {
                throw new System.Exception($"ошибка в {word} это вырожение не имеет смысла");
            }
        }

        public static bool IsLogin(string login)
        {
			if(login.Length > 0)
			{
				foreach (var c in login)
				{
					if (!(char.IsLetterOrDigit(c) || c == '@' || c == '_' || c == '.'))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

        public static void LoginEr(string login)
        {
            if (!IsLogin(login))
            {
                throw new System.Exception($"ошибка в {login} это не формат логина");
            }
        }

        // несколько не понял, как это работает, пришлось просто переделать, неосознавая(
        static public string ConverteToStandartPhoneNumber(string phoneNumber)
        {
            PhoneNumberEr(phoneNumber);

            phoneNumber = new System.Text.RegularExpressions.Regex(@"\D").Replace(phoneNumber, string.Empty);

            if (phoneNumber.Length == 11)
            {
                phoneNumber = Convert.ToInt64(phoneNumber).ToString("#(###)###-##-##");
            }
            else
            {
                throw new System.Exception($"{phoneNumber} не поддерживаемый формат");
            }

            return (!phoneNumber.StartsWith('8')) ? '+' + phoneNumber : phoneNumber;
        }

        public static bool IsPhoneNumber(string phoneNumber)
        {
			var count = 0;

            foreach (var c in phoneNumber)
            {
                if (char.IsDigit(c))
				{
					count++;
				}
				else if (!(c == '+' || c == '(' || c == '-' || c == ')' || c == ' '))
                {
                    return false;
                }
            }
			return count == 11;
        }

        public static void PhoneNumberEr(string phoneNumber)
        {
            if (!IsPhoneNumber(phoneNumber))
            {
                throw new System.Exception($"{phoneNumber} не номер телефона");
            }
        }

        public static DateTime ConverteStringToDate(string str)
        {
            var strArr = ToStringArray(str);

            var temp = new string[3];

            if (strArr.Length == 1)
            {
                strArr = ToStringArray(str, '.');
            }

            for (var i = 0; i < temp.Length; i++)
            {
                temp[i] = strArr[i];
            }

            var mounth = IsAWord(temp[1]) ? Mounth(temp[1]) : int.Parse(temp[1]);

            return new DateTime(int.Parse(temp[2]), mounth, int.Parse(temp[0]));
        }

        private static int Mounth(string mounth)
        {
            var temp = _mounth[ConverteToStandartWord(mounth)];

            return (temp != 0) ? temp : throw new Exception("Такого месяца нет");
        }

        private readonly static SortedDictionary<string, int> _mounth = new SortedDictionary<string, int>
        {
            ["Января"] = 1,
            ["Февраля"] = 2,
            ["Марта"] = 3,
            ["Апреля"] = 4,
            ["Мая"] = 5,
            ["Июня"] = 6,
            ["Июля"] = 7,
            ["Августа"] = 8,
            ["Сентября"] = 9,
            ["Октября"] = 10,
            ["Ноября"] = 11,
            ["Декабря"] = 12
        };
    }
}
