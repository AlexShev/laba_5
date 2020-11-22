namespace laba_5
{
    public class FulName
    {
        public string FirstName { get; }

        public string MidlName { get; }

        public string SecondName { get; }

        public FulName(string secondName, string firstName, string midlName, bool isStandartView = false)
        {
            FirstName = (isStandartView) ? firstName: StandartView.ConverteToStandartString(firstName);

            MidlName = (isStandartView) ? midlName: StandartView.ConverteToStandartString(midlName);

            SecondName = (isStandartView) ? secondName : StandartView.ConverteToStandartString(secondName);
        }

        public FulName(string fulName, bool isStandartView = false)
        {
            var sfm = StandartView.ToStringArray(fulName);

            if (sfm.Length > 3)
            {
                throw new System.Exception($"{fulName} нарушение формата ввода");
            }

            FirstName = (isStandartView) ? sfm[1] : StandartView.ConverteToStandartString(sfm[1]);

            MidlName = (isStandartView) ? sfm[2] : StandartView.ConverteToStandartString(sfm[2]);

            SecondName = (isStandartView) ? sfm[0] : StandartView.ConverteToStandartString(sfm[0]);
        }

        public override string ToString() => $"{SecondName} {FirstName} {MidlName}";
    }
}
