using System;

namespace laba_5
{
    public class Client : Human
    {
        public FulName MyFulName { get; }

        public DateTime MyBirthday { get; }

        public Gender MySex { get; }

        public string MyCity { get; }

        public string MyPhoneNumber { get; }

        public ZodiacSign MyZodiacSign { get; }

        public СompatibilityByName MyCompatibilityByName { get; }

        public int MyAge
        {
            get
            {
                // там оказывается ещё сложнее из-за 29 февраля
                var age = DateTime.Now.Year - MyBirthday.Year;

                if (DateTime.Now.Month < MyBirthday.Month || (DateTime.Now.Month == MyBirthday.Month && DateTime.Now.Day < MyBirthday.Day))
                {
                    age--;
                }

                if (age < 0)
                {
                    throw new System.Exception("Человек ещё не родился");
                }

                return age;
            }
        }

        public Client(string login, string password, string myFulName, string sex, string myBirthday,
            string myCity, string myPhoneNumber, bool isStandartView = false) : base(login, password, isStandartView)
        {
            MyFulName = new FulName(myFulName, isStandartView);

            MyBirthday = StandartView.ConverteStringToDate(myBirthday);

            MyCity = (isStandartView) ? myCity : StandartView.ConverteToStandartString(myCity);

            MyPhoneNumber = (isStandartView) ? myPhoneNumber : StandartView.ConverteToStandartPhoneNumber(myPhoneNumber);

            MySex = new Gender(sex, isStandartView);

            MyZodiacSign = new ZodiacSign(MyBirthday);

            MyCompatibilityByName = new СompatibilityByName(MyFulName.FirstName);
        }     

        public Client(string[] str, bool isStandartView = false) : this(str[0], str[1], str[2], str[3], str[4], str[5], str[6], isStandartView) { }

        public int ScoreIsPaer(in Client person) => (MySex != person.MySex && MyCity == person.MyCity) ? ScoreIsPaerWithoutSexAndLocalization(person) : 0;

        public int ScoreIsPaerWithoutSexAndLocalization(in Client person, int maxAgeDiferent = 5)
        {
            if (Math.Abs(MyAge - person.MyAge) > maxAgeDiferent)
            {
                return 0;
            }

            var result = MyCompatibilityByName.IsAPaer(person.MyCompatibilityByName);

            result += MyZodiacSign.IsAPaer(person.MyZodiacSign);

            return result;
        }

        // clear ot cuda
        public void Show()
        {
            Console.WriteLine($"ФИО {MyFulName} пол {MySex} родился {MyBirthday.ToShortDateString()} возраст {MyAge} знак задиака {MyZodiacSign}");
        }
    }
}
