using System;

namespace laba_5
{
    class ZodiacSign
    {
        public enum _zodiacSign
        {
            aries,
            taurus,
            twins,
            cancer,
            leo,
            virgo,
            libra,
            scorpio,
            sagittarius,
            capricorn,
            aquarius,
            pisces,
        }

        private readonly _zodiacSign _myZodiacSign;

        public _zodiacSign MyZodiacSign { get { return _myZodiacSign; } }

        public ZodiacSign(in DateTime birthday)
        {
            int year = birthday.Year;
            if ((new DateTime(year, 12, 22) <= birthday) || (birthday <= new DateTime(year, 1, 19)))
                _myZodiacSign = _zodiacSign.aquarius;
            else if (birthday <= new DateTime(year, 2, 18))
                _myZodiacSign = _zodiacSign.aquarius;
            else if (birthday <= new DateTime(year, 3, 21))
                _myZodiacSign = _zodiacSign.pisces;
            else if (birthday <= new DateTime(year, 4, 19))
                _myZodiacSign = _zodiacSign.aries;
            else if (birthday <= new DateTime(year, 5, 20))
                _myZodiacSign = _zodiacSign.taurus;
            else if(birthday <= new DateTime(year, 6, 20))
                _myZodiacSign = _zodiacSign.twins;
            else if (birthday <= new DateTime(year, 7, 22))
                _myZodiacSign = _zodiacSign.cancer;
            else if (birthday <= new DateTime(year, 8, 22))
                _myZodiacSign = _zodiacSign.leo;
            else if (birthday <= new DateTime(year, 9 ,22))
                _myZodiacSign = _zodiacSign.virgo;
            else if (birthday <= new DateTime(year, 10, 22))
                _myZodiacSign = _zodiacSign.libra;
            else if (birthday <= new DateTime(year, 11, 21))
                _myZodiacSign = _zodiacSign.scorpio;
            else if (birthday <= new DateTime(year, 12, 21))
                _myZodiacSign = _zodiacSign.sagittarius;
        }

        public int IsAPaer(in ZodiacSign zodiacSignPaer)
        {
            int temp = Math.Abs(_myZodiacSign - zodiacSignPaer._myZodiacSign);

            temp = (temp <= 6) ? temp : 12 - temp;

            if (temp == 0 || temp == 6)
                return 3;
            else if (temp == 2)
                return 6;
            else if (temp == 4)
                return 9;
            return 0;
        }
    }
}