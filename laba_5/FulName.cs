using System;

namespace laba_5
{
    public class FulName
    {
        public string FirstName { get; }

        public string MidlName { get; }

        public string SecondName { get; }

        public FulName(string secondName, string firstName, string midlName)
        {
            FirstName = StandartView.ConverteToStandartString(firstName);

            MidlName = StandartView.ConverteToStandartString(midlName);

            SecondName = StandartView.ConverteToStandartString(secondName);
        }

        public FulName(string fulName)
        {
            string[] sfm = fulName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (sfm.Length > 3)
            {
                throw new System.Exception($"{fulName} нарушение формата ввода");
            }

            // хочется использовать конструктор повыше
            FirstName = StandartView.ConverteToStandartString(sfm[1]);

            MidlName = StandartView.ConverteToStandartString(sfm[2]);

            SecondName = StandartView.ConverteToStandartString(sfm[0]);
        }

        public override string ToString() => $"{SecondName} {FirstName} {MidlName}";
    }
}