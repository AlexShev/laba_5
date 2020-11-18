using System;

namespace laba_5
{
    class Client : Human
    {
        private readonly FulName _myFulName;

        private readonly DateTime _myBirthday;

        private readonly string _myCity;

        private readonly string _myPhoneNumber;

        private readonly char _mySex;

        private readonly ZodiacSign _myZodiacSign;

        private readonly СompatibilityByName _myCompatibilityByName;

        public Client(FulName myFulName, DateTime myBirthday, string myCity, string myPhoneNumber, string login, string password)
            :base(login, password)
        {
            // кривая проверка на age>=0
            // может стоит установить ограничение 18?
            _ = MyAge;

            _myFulName = myFulName;

            _myBirthday = myBirthday;

            _myCity = StandartView.ConverteToStandartString(myCity);

            _myPhoneNumber = StandartView.ConverteToStandartPhoneNumber(myPhoneNumber);

            _mySex = FulName.Sex(_myFulName);

            _myZodiacSign = new ZodiacSign(myBirthday);

            _myCompatibilityByName = new СompatibilityByName(_myFulName.FerstName);
        }

        public FulName MyFulName { get { return _myFulName; } }

        public DateTime MyBirthday { get { return _myBirthday; } }

        public char MySex { get { return _mySex; } }

        public string MyCity { get { return _myCity; } }

        public string MyPhoneNumber { get { return _myPhoneNumber; } }

        public string MyZodiacSign { get { return _myZodiacSign.MyZodiacSign.ToString(); } }

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

        public int IsAPaer(in Client person)
        {
            if (_mySex != person._mySex && _myCity == person._myCity)
            {
                return IsAPaerWithoutSexAndLocalization(person);
            }

            return 0;
        }

        public int IsAPaerWithoutSexAndLocalization(in Client person)
        {
            int result = 0;

            // спорное ограничение, но так как это не живое общение, то человек просто сам отфильтрует по возрасту
            int maxAgeDif = (MyAge >= 23) ? 5 : (MyAge <= 18) ? 1 : MyAge - 18;

            if (Math.Abs(MyAge - person.MyAge) > maxAgeDif)
                return 0;

            result += _myCompatibilityByName.IsAPaer(person._myCompatibilityByName);

            result += _myZodiacSign.IsAPaer(person._myZodiacSign);
            
            return result;
        }

        public override bool IsAvailable() { return false; }

        public void Show()
        {
            Console.WriteLine($"ФИО {MyFulName} пол {MySex} родился {MyBirthday.ToShortDateString()} возраст {MyAge} знак задиака {MyZodiacSign}");
        }
    }
}