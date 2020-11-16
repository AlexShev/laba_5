using System.Collections.Generic;

namespace laba_5
{
    class СompatibilityByName
    {
        // очень криво, но в голову ничего лучше не пришло
        private static readonly Dictionary<int, SortedSet<char>> _map
            = new Dictionary<int, SortedSet<char>>
            {
                {1, new SortedSet<char> {'а', 'и','с', 'ъ'} },
                {2, new SortedSet<char> {'б', 'й','т', 'ы'} },
                {3, new SortedSet<char> {'в', 'к','у', 'ь'} },
                {4, new SortedSet<char> {'г', 'л','ф', 'э'} },
                {5, new SortedSet<char> {'д', 'м','х', 'ю'} },
                {6, new SortedSet<char> {'е', 'н','ц', 'я'} },
                {7, new SortedSet<char> {'ё', 'о','ч'} },
                {8, new SortedSet<char> {'ж', 'п','ш'} },
                {9, new SortedSet<char> {'з', 'р','щ'} },
            };

        private static readonly int[,] _arr = 
        {
            {5,8,3,8,3,9,7,7,9},
            {8,3,7,5,4,6,6,8,5},
            {3,7,3,5,7,6,2,5,6},
            {8,5,5,3,8,5,7,3,8},
            {3,4,7,8,9,9,8,3,8},
            {9,6,6,5,9,5,6,9,5},
            {7,6,2,7,8,6,4,2,9},
            {8,8,5,3,3,9,2,6,5},
            {9,5,6,8,8,5,9,5,6}
        };

        private readonly int _myNamber;
        public int MyNamber { get { return _myNamber; } }

        public СompatibilityByName(in string name)
        {
            int temp = 0;
            foreach (var c in name)
            {
                for (int i = 1; i < 10; i++)
                {
                    bool tempBool = _map[i].Contains(char.ToLower(c));
                    temp += tempBool ? i : 0;
                    if (tempBool) break;
                }
            }
            _myNamber = DigitsSum(temp);
        }

        public int IsAPaer(in СompatibilityByName person)
        {
            return _arr[_myNamber-1, person._myNamber-1];
        }

        private static int DigitsSum(int number)
        {
            if (number == 0) return 0;
            return number % 10 + DigitsSum(number / 10);
        }
    }
}
