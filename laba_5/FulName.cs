using System;
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


        // спорное решение конечно, ход расчитан для более серьёзного анализа
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
            _ferstName = StandartView.ConverteToStandartString(ferstName);

            _midlName = StandartView.ConverteToStandartString(midlName);

            _secondName = StandartView.ConverteToStandartString(secondName);
        }

        public FulName(string fulName)
        {
            string[] sfm = fulName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (sfm.Length > 3)
                throw new System.Exception($"{fulName} нарушение формата ввода");

           // хочется использовать конструктор повыше
            _ferstName = StandartView.ConverteToStandartString(sfm[1]);

            _midlName = StandartView.ConverteToStandartString(sfm[2]);

            _secondName = StandartView.ConverteToStandartString(sfm[0]);
        }

        public override string ToString()
        {
            return $"{_secondName} {_ferstName} {_midlName}";
        }
    }
}