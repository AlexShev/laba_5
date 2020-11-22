using System;

namespace laba_5
{
    public class ZodiacSign
    {
        public enum ZodiacSigns
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

        public ZodiacSigns MyZodiacSign { get; }

        public ZodiacSign(in DateTime birthday)
        {
            var year = birthday.Year;

            if ((new DateTime(year, 12, 22) <= birthday) || (birthday <= new DateTime(year, 1, 19)))
            {
                MyZodiacSign = ZodiacSigns.aquarius;
            }
            else if (birthday <= new DateTime(year, 2, 18))
            {
                MyZodiacSign = ZodiacSigns.aquarius;
            }
            else if (birthday <= new DateTime(year, 3, 21))
            {
                MyZodiacSign = ZodiacSigns.pisces;
            }
            else if (birthday <= new DateTime(year, 4, 19))
            {
                MyZodiacSign = ZodiacSigns.aries;
            }
            else if (birthday <= new DateTime(year, 5, 20))
            {
                MyZodiacSign = ZodiacSigns.taurus;
            }
            else if (birthday <= new DateTime(year, 6, 20))
            {
                MyZodiacSign = ZodiacSigns.twins;
            }
            else if (birthday <= new DateTime(year, 7, 22))
            {
                MyZodiacSign = ZodiacSigns.cancer;
            }
            else if (birthday <= new DateTime(year, 8, 22))
            {
                MyZodiacSign = ZodiacSigns.leo;
            }
            else if (birthday <= new DateTime(year, 9, 22))
            {
                MyZodiacSign = ZodiacSigns.virgo;
            }
            else if (birthday <= new DateTime(year, 10, 22))
            {
                MyZodiacSign = ZodiacSigns.libra;
            }
            else if (birthday <= new DateTime(year, 11, 21))
            {
                MyZodiacSign = ZodiacSigns.scorpio;
            }
            else if (birthday <= new DateTime(year, 12, 21))
            {
                MyZodiacSign = ZodiacSigns.sagittarius;
            }
        }

        public int IsAPaer(in ZodiacSign zodiacSignPaer)
        {
            var temp = Math.Abs(MyZodiacSign - zodiacSignPaer.MyZodiacSign);

            temp = (temp <= 6) ? temp : 12 - temp;

            if (temp == 0 || temp == 6)
            {
                return 3;
            }
            else if (temp == 2)
            {
                return 6;
            }
            else if (temp == 4)
            {
                return 9;
            }

            return 0;
        }

		public override string ToString() => MyZodiacSign.ToString();
	}
}
