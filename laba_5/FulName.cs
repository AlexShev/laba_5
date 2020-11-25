namespace laba_5
{
    public class FulName
    {
        public string FirstName { get; }

        public string SecondName { get; }

        public FulName(string secondName, string firstName, bool isStandartView = false)
        {
            FirstName = (isStandartView) ? firstName: StandartView.ConverteToStandartString(firstName);

            SecondName = (isStandartView) ? secondName : StandartView.ConverteToStandartString(secondName);
        }

        public FulName(string fulName, bool isStandartView = false)
        {
            var sfm = StandartView.ToStringArray(fulName);

            if (sfm.Length > 2)
            {
                throw new System.Exception($"{fulName} нарушение формата ввода");
            }

            FirstName = (isStandartView) ? sfm[1] : StandartView.ConverteToStandartString(sfm[1]);

            SecondName = (isStandartView) ? sfm[0] : StandartView.ConverteToStandartString(sfm[0]);
        }

        public override string ToString() => $"{SecondName} {FirstName}";
    }
}
