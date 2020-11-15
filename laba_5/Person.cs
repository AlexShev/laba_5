using System;

namespace laba_5
{
    class Person
    {
        private readonly FulName _myFulName;

        private readonly DateTime _myBirthday;

        private readonly char _mySex;

        private readonly ZodiacSign _myZodiacSign;

        private readonly СompatibilityByName _сompatibilityByName;


        public Person(FulName myFulName, DateTime birthday)
        {
            _myFulName = myFulName;

            _myBirthday = birthday;

            _mySex = FulName.Sex(_myFulName);

            _myZodiacSign = new ZodiacSign(birthday);

            _сompatibilityByName = new СompatibilityByName(_myFulName.FerstName);
        }

        public string MyFulName { get { return _myFulName.ToString(); } }

        public DateTime MyBirthday { get { return _myBirthday; } }

        public char MySex { get { return _mySex; } }


        // не поле, так как оно должно меняться
        public int MyAge
        {
            get
            {
                int age = DateTime.Now.Year - _myBirthday.Year;

                if (DateTime.Now.Month < _myBirthday.Month ||
                   (DateTime.Now.Month == _myBirthday.Month &&
                   DateTime.Now.Day < _myBirthday.Day))
                    age--;

                if (age < 0) throw new System.Exception("Человек ещё не родился");

                return age;
            }
        }

        public int IsAPaer(in Person person)
        {
            int result = 0;

            // спорное ограничение, но так как это не живое общение, то человек просто сам отфильтрует по возрасту
            int maxAgeDif = (MyAge >= 23) ? 5 : (MyAge <= 18) ? 1 : MyAge - 18;

            if (_mySex != person._mySex)
            {
                if (Math.Abs(MyAge - person.MyAge) > maxAgeDif)
                    return 0;

                result += _сompatibilityByName.IsAPaer(person._сompatibilityByName);

                result += _myZodiacSign.IsAPaer(person._myZodiacSign);
            }

            return result;
        }

        public string MyZodiacSign { get { return _myZodiacSign.MyZodiacSign.ToString(); } }

        public void Show()
        {
            Console.WriteLine($"ФИО {MyFulName} пол {MySex} родился {MyBirthday.ToShortDateString()} возраст {MyAge} знак задиака {MyZodiacSign}");
        }
    }
}