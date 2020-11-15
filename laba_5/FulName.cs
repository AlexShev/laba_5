using System.Linq;

namespace laba_5
{
    class FulName
    {
        private readonly string _secondName;

        private readonly string _ferstName;

        private readonly string _midlName;

        public string FerstName { get { return _ferstName; } }
        public string MidlName { get { return _midlName; } }
        public string SecondName { get { return _secondName; } }

        public static char Sex(in FulName fulName)
        {
                if (fulName._midlName.EndsWith('ч'))
                    return 'М';
                else if (fulName._midlName.EndsWith('а'))
                    return 'Ж';
                else
                    return '-';
        }

        public FulName(string secondName, string ferstName, string midlName)
        {
            IsAName(ferstName);
            IsAName(midlName);
            IsAName(secondName);

            _ferstName = ConverteToStandartView(ferstName);
            _midlName = ConverteToStandartView(midlName);
            _secondName = ConverteToStandartView(secondName);
        }

        public override string ToString()
        {
            return $"{_secondName} {_ferstName} {_midlName}";
        }

        private void IsAName(string name)
        {
            if (!name.ToCharArray().All(char.IsLetter)) throw new System.Exception($"ошибка в {name} это не ФИО");
        }

        private string ConverteToStandartView(string name)
        {
            char[] arr = name.ToCharArray();

            arr[0] = char.ToUpper(arr[0]);

            for (int i = 1; i < arr.Length; i++)
            {
                arr[i] = char.ToLower(arr[i]);
            }

            return new string(arr);
        }
    }
}