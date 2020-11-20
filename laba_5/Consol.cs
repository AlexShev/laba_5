using System;

namespace laba_5
{
    // не вижу смыса делать его прям суперским, всё равно сносить
    // нужен для демнстрации логики
    public class ConsolIneractor
    {
        private static string ReadLogin()
        {
            Console.WriteLine("Введите логин:");

            string temp = Console.ReadLine();

            Console.WriteLine();

            return temp;
        }

        private static string ReadPassword()
        {
            Console.WriteLine("Введите пароль:");

            string temp = Console.ReadLine();

            Console.WriteLine();

            return temp;
        }

        private static string[] Identification() => new string[2] { ReadLogin(), ReadPassword() };

        private static string[] ReadLogAndPas(DataBase dataBase)
        {
            string[] temp = new string[2];

            string control;

            while (true)
            {
                temp[0] = ReadLogin();

                if (dataBase.IsFreeLoginAdmins(temp[0]))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Извините, пользоавтель с таким логином уже есть, придумайте новый");
                }
            }

            while (true)
            {
                temp[1] = ReadPassword();

                Console.WriteLine("Введите пароль повторно:");

                control = Console.ReadLine();

                if (temp[1] == control)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Вы ошиблись при вводе, они должны быть одинаковы");
                }
            }
            return temp;
        }

        private static string[] ReadClientInformation(DataBase dataBase)
        {
            string[] result = new string[7];

            string[] temp = ReadLogAndPas(dataBase);

            result[0] = temp[0]; result[1] = temp[1];

            Console.WriteLine("\nВведите ФИО:");
            result[2] = Console.ReadLine();

            Console.WriteLine("\nПол:");
            result[3] = Console.ReadLine();

            Console.WriteLine("\nДата рождения:");
            result[4] = Console.ReadLine();

            Console.WriteLine("\nГород проживания:");
            result[5] = Console.ReadLine();

            Console.WriteLine("\nНомер телефона:");
            result[6] = Console.ReadLine();

            Console.WriteLine();

            return result;
        }

        private static void MenuForClient()
        {
            Console.WriteLine("1: Подобрать пару");
            Console.WriteLine("2: Поощерить нашу работу");
            Console.WriteLine("3: Удалить свой аккаунт");
            Console.WriteLine("0: Выйти");
        }

        private static void MenuForAdmin()
        {
            Console.WriteLine("1: Добавить клиента в базу");
            Console.WriteLine("2: Удалить клиента из базу");
            Console.WriteLine("3: Добавить админа");
            Console.WriteLine("4: Удалить свой аккаунт");
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
            string[] temp = Identification();

            Admin admin = new Admin(temp[0], temp[1]);

            if (dataBase.IsMyAdmin(admin))
            {
                bool condition = true;

                while (condition)
                {
                    try
                    {
                        MenuForAdmin();

                        switch (ReadComend())
                        {
                            case ('1'): AddHumanByAdmin(dataBase); break;
                            case ('2'): DeleteHumanByAdmin(dataBase, admin); break;
                            case ('3'): AddHumanByAdmin(dataBase, true); break;
                            case ('4'): DeleteHumanByAdmin(dataBase, admin, true); condition = false; break;
                            case ('0'): Console.WriteLine("\nВы вышли из аккаунта администратора\n"); condition = false; break;
                            default: Console.WriteLine("\nТакой команды нет\n"); break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            else if (dataBase.IsFreeLoginAdmins(temp[0]))
            {
                Console.WriteLine("\nНу поиграли и хватит, Вы не администратор\n");
            }
            else
            {
                Console.WriteLine("\nВы ошиблись при вводе пароля\n");
            }
        }

        private static void AddHumanByAdmin(DataBase dataBase, bool addAdmin = false)
        {
            if (addAdmin)
            {
                string[] temp = ReadLogAndPas(dataBase);

                dataBase.AddAdmin(new Admin(temp[0], temp[1]));
            }
            else
            {
                AddClient(dataBase);
            }
        }

        private static void DeleteHumanByAdmin(DataBase dataBase, Admin admin, bool deliteaAdmin = false)
        {
            Console.WriteLine("Подтвердите решения введя пароль");

            if (admin.IsMyPassword(ReadPassword()))
            {
                if (deliteaAdmin)
                {
                    dataBase.DeleteAdmin(admin);
                }
                else
                {
                    string loginClient = ReadLogin();

                    dataBase.DeleteClientByAdmin(admin, loginClient);
                }
            }
            else
            {
                Console.WriteLine("что-то пошло не так");
            }
        }

        private static void AddClient(DataBase dataBase)
        {
            Console.WriteLine("Введите необходимые данные");

            dataBase.AddСlient(new Client(ReadClientInformation(dataBase)));
        }


        private static void DeleteClient(DataBase dataBase, string[] loginPassword)
        {
            Console.WriteLine("Введите необходимые данные");

            dataBase.DeleteClientByClient(loginPassword);

        }

        private static void RunForClient(DataBase dataBase)
        {
            string[] temp = Identification();

            if (dataBase.IsMyClient(temp))
            {
                bool condition = true;

                while (condition)
                {
                    try
                    {
                        MenuForClient();

                        switch (ReadComend())
                        {
                            case ('1'): FindPaer(dataBase, temp); break;
                            case ('2'): Console.WriteLine("\nСпасибо, нам это очень важно"); break;
                            case ('3'): DeleteClient(dataBase, temp); condition = false; break;
                            case ('0'): Console.WriteLine("\nВы вышли из аккаунта администратора"); condition = false; break;
                            default: Console.WriteLine("\nТакой команды нет"); break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            else if (dataBase.IsFreeLoginClients(temp[0]))
            {
                Console.WriteLine("\nТакого пользователя пока нет, зарегестрируйтесь\n");
            }
            else
            {
                Console.WriteLine("\nВы ошиблись при вводе пароля\n");
            }
        }

        private static void FindPaer(DataBase dataBase, string[] person)
        {
            foreach (var c in dataBase.FindPiars(person, 5))
            {
                foreach (var c1 in c.Value)
                {
                    c1.Show();
                }
            }
        }

        public static void Run()
        {
            var dataBase = new DataBase();

            bool condition = true;

            while (condition)
            {
                Menu();

                switch (ReadComend())
                {
                    case ('1'): RunForClient(dataBase); break;
                    case ('2'): AddClient(dataBase); break;
                    case ('3'): RunForAdmin(dataBase); break;
                    case ('0'): Console.WriteLine("\nДо свидания"); condition = false; break;
                    default: Console.WriteLine("\nТакой команды нет"); break;
                }
            }
        }

        private static char ReadComend()
        {
            Console.Write(">>");

            char comand = Console.ReadKey().KeyChar;

            Console.WriteLine('\n');

            return comand;
        }

    }
}
