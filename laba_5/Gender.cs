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
            ["f"] = 0, ["female"] = 0, ["fem"] = 0, ["ж"] = 0, ["женский"] = 0, ["жен"] = 0,
            ["m"] = 1, ["masculine"] = 1, ["male"] = 1, ["м"] = 1, ["мужской"] = 1, ["муж"] = 1
        };

        public Gender(string sex)
        {
            // защита от некоректного ввода
            var temp = _sex[sex.ToLower()];

            MySex = (temp == 1) ? Sex.masculine : (temp == 0) ? Sex.female : throw new Exception("Пол не определён");
        }

		public static bool IsMyGender(string sex, bool isStandartView = false)
			=> _sex.ContainsKey(sex.ToLower());

        // если что то это посоветовал компилятор
        public static bool operator ==(Gender c1, Gender c2) => c1.MySex == c2.MySex;

        public static bool operator !=(Gender c1, Gender c2) => c1.MySex != c2.MySex;

        public override string ToString() => MySex.ToString();
    }
}
