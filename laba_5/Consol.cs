using System;
using System.Collections.Generic;
using System.Text;

namespace laba_5
{
    // не вижу смыса делать его прям суперским, всё равно сносить
    // нужен для демнстрации логики
    class ConsolIneractor
    {
        private static string[] Identification()
        {
            string[] temp = new string[2];

            Console.WriteLine("\nВведите логин:");
            temp[0] = Console.ReadLine();

            Console.WriteLine("Введите пароль:");
            temp[1] = Console.ReadLine();

            return temp;
        }

        private static string[] ReadLogAndPas()
        {
            string[] temp = new string[2] { "", "" };
            string control = "";

            Console.WriteLine("\nВведите логин:");
            temp[0] = Console.ReadLine();

            while (true)
            {
                Console.WriteLine("Введите пароль:");
                temp[1] = Console.ReadLine();

                Console.WriteLine("Введите пароль повторно:");
                control = Console.ReadLine();

                if (temp[1] == control) break;
                else Console.WriteLine("Вы ошиблись при вводе, они должны быть одинаковы");
            }
            return temp;

        }

        private static string[] ReadClientInformation()
        {
            string[] temp = new string[4];

            Console.WriteLine("\nВведите ФИО:");
            temp[0] = Console.ReadLine();

            Console.WriteLine("\nДата рождения:");
            temp[1] = Console.ReadLine();

            Console.WriteLine("\nГород проживания:");
            temp[2] = Console.ReadLine();

            Console.WriteLine("\nНомер телефона:");
            temp[3] = Console.ReadLine();

            return temp;
        }

        private static void MenuForClient()
        {
            Console.WriteLine("1: Подобрать пару");
            Console.WriteLine("2: Выйти");
            Console.WriteLine("3: Поощерить нашу работу");
            Console.WriteLine("4: Удалить свой аккаунт");
        }

        private static void MenuForAdmin()
        {
            Console.WriteLine("1: Добавить клиента в базу");
            Console.WriteLine("2: Добавить админа");
            Console.WriteLine("3: Удалить свой аккаунт");
            Console.WriteLine("0: Выйти");
        }

        private static void Menu()
        {
            Console.WriteLine("1: Войти");
            Console.WriteLine("2: Зарегестрироваться");
            Console.WriteLine("3: Войти как администратор");
            Console.WriteLine("0: Завершение работы");
        }

        private static void RunForAdmin(DataBase dataBase)
        {
        //    string[] temp = Identification();

        //    if (dataBase.IsAdmin(temp[0], temp[1]))
            {
                bool condition = true;

                while (condition)
                {
                    MenuForAdmin();

                    char comad = Console.ReadKey().KeyChar;

                    switch (comad)
                    {
                        case ('1'): AddHuman(dataBase); break;
                        case ('2'): AddHuman(dataBase, true); break;
                        case ('3'): break;
                        case ('0'): Console.WriteLine("\nВы вышли из аккаунта администратора"); condition = false; break;
                        default: Console.WriteLine("\nТакой команды нет"); break;
                    }
                }
            }
          //  else Console.WriteLine("\nТакого администратора нет, скорее всего вы ошиблись при вводе");
        }

        private static void AddHuman(DataBase dataBase, bool isAdmin = false)
        {
            string[] temp = ReadLogAndPas();

            if (isAdmin)
            {
                dataBase.AddAdmin(new Admin(temp[0], temp[1]));
            }
            else
            {
                string[] temp2 = ReadClientInformation();

           //     dataBase.AddСlient(new Client(new FulName(temp2[0]), StandartView.ConverteStringToDate(temp2[1]), temp2[2], temp2[3], temp[0], temp[1]));

                dataBase.AddСlient(new Client(new FulName("МУРАВЬЁВ-АПОСТОЛ ПЁТР АЛЕКСЕЕВИЧ"), new DateTime(1971, 5, 13),
                   "Ростов-на-дону", "8 912 345 67 01", "murovey_apostol01", "IYyGyUbyigHG"));
            }
        }

        public static void Run()
        {
            var dataBase = new DataBase();

            bool condition = true;

            while (condition)
            {
                Menu();

                char comad = Console.ReadKey().KeyChar;

                switch (comad)
                {
                    case ('1'): break; 
                    case ('2'): break;
                    case ('3'): RunForAdmin(dataBase); break;
                    case ('0'): Console.WriteLine("\nДо свидания"); condition = false; break;
                    default: Console.WriteLine("\nДо свидания"); break;
                }
            }
        }

    }
}
