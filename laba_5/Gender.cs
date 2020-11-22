using System;
using System.Collections.Generic;

namespace laba_5
{
	public class Gender
    {
        public enum Sex
        {
            female,
            masculine
        }

        public Sex MySex { get; }

        private static readonly SortedDictionary<string, int> _sex = new SortedDictionary<string, int>
        {
            ["F"] = 0, ["Female"] = 0, ["Fem"] = 0, ["Ж"] = 0, ["Женский"] = 0, ["Жен"] = 0,
            ["M"] = 1, ["Masculine"] = 1, ["Masc"] = 1, ["М"] = 1, ["Мужской"] = 1, ["Муж"] = 1
        };

        public Gender(string sex, bool isStandartView = false)
        {
            // защита от некоректного ввода
            var temp = _sex[(isStandartView) ? sex : StandartView.ConverteToStandartWord(sex)];

            MySex = (temp == 1) ? Sex.masculine : (temp == 0) ? Sex.female : throw new Exception("Пол не определён");
        }

		public static bool IsMyGender(string sex, bool isStandartView = false)
			=> _sex.ContainsKey((isStandartView) ? sex : StandartView.ConverteToStandartWord(sex));

        // если что то это посоветовал компилятор
        public static bool operator ==(Gender c1, Gender c2) => c1.MySex == c2.MySex;

        public static bool operator !=(Gender c1, Gender c2) => c1.MySex != c2.MySex;

        public override string ToString() => MySex.ToString();
    }
}
