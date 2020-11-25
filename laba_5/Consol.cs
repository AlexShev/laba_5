using System;

namespace laba_5
{
	// не вижу смыса делать его прям суперским, всё равно сносить
	// нужен для демнстрации логики
	public class ConsolIneractor
    {
        public static void Run()
        {
            var dataBase = new DataBase();

			dataBase.GeneratorDataBase(1000);

			var condition = true;

            while (condition)
            {
				Console.Clear();

                Menu();

                switch (ReadComend())
                {
                    case ('1'): RunForClient(dataBase); break;
                    case ('2'): RunForClient(dataBase, true);  break;
                    case ('3'): RunForAdmin(dataBase);  break;
                    case ('0'): Console.WriteLine("До свидания"); condition = false; break;
                    default: Console.WriteLine("Такой команды нет"); break;
                }
			}
        }

        private static void Menu()
        {
            Console.WriteLine("1: Войти");
            Console.WriteLine("2: Зарегестрироваться");
            Console.WriteLine("3: Войти как администратор");
            Console.WriteLine("0: Завершение работы");
        }

        private static char ReadComend()
        {
            Console.Write(">>");

			var comand = Console.ReadKey().KeyChar;

			Console.Clear();

            return comand;
        }

		private static void Сontinue()
        {
			Console.Write("Для продолжения нажмите любую клавишу");

			Console.ReadKey();

			Console.Clear();
		}

		private static void RunForClient(DataBase dataBase, bool isNewClient = false)
        {
			Console.WriteLine("Добро пожаловать\n");

            var temp = (isNewClient) ? AddClient(dataBase) : Identification();

			if (isNewClient || dataBase.IsMyClient(temp))
            {
				Console.Clear();

				if (isNewClient)
				{
					Console.Write("Если хотите продолжить нажмите 1, иначе другую клавишу... ");

					var condition = Console.ReadKey().Key;

					Console.Clear();

					if (!(condition == ConsoleKey.D1))
					{
						return;
					}
				}

				var condition1 = true;

                while (condition1)
                {
                    try
                    {
						Console.Clear();

						MenuForClient();

                        switch (ReadComend())
                        {
                            case ('1'): FindPaer(dataBase, temp); break;
                            case ('2'): Console.WriteLine("Спасибо, нам это очень важно\n"); Сontinue(); break;
                            case ('3'): DeleteClient(dataBase, temp); condition1 = false; break;
                            case ('0'): Console.WriteLine("Вы вышли из своего аккаунта\n"); condition1 = false; break;
                            default: Console.WriteLine("Такой команды нет\n"); break;
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
                Console.WriteLine("Такого пользователя пока нет, зарегестрируйтесь\n");

				Сontinue();
			}
			else
            {
                Console.WriteLine("Вы ошиблись при вводе пароля\n");

				Сontinue();
			}
		}

        private static string[] Identification() => new string[2] { ReadLogin(), ReadPassword() };

        private static string ReadLogin()
        {
			while (true)
			{
				Console.WriteLine("Введите логин:");

				var login = Console.ReadLine();

				Console.WriteLine();

				if (StandartView.IsLogin(login))
				{
					return login;
				}
				else
				{
					Console.WriteLine("Не возможный формат логина\n");
				}
			}
        }

        private static string ReadPassword()
        {
			while (true)
			{
				Console.WriteLine("Введите пароль:");

				var password = Console.ReadLine();

				Console.WriteLine();

				if (password.Length > 0 && !password.Contains(' '))
				{
					return password;
				}
				else
				{
					Console.WriteLine("Не возможный формат пароля\n");
				}
			}
        }

        private static void MenuForClient()
        {
            Console.WriteLine("1: Подобрать пару");
            Console.WriteLine("2: Поощрить нашу работу");
            Console.WriteLine("3: Удалить свой аккаунт");
            Console.WriteLine("0: Выйти");
        }

        private static void FindPaer(DataBase dataBase, string[] person)
        {
			var counter = 0;

			Console.Write("Введите максимальную разницу в возрасте: ");

			var maxDiferentAge = int.Parse(Console.ReadLine());

            foreach (var c in dataBase.FindPiars(person, maxDiferentAge))
            {
                foreach (var c1 in c.Value)
                {
					Console.Write($"\n\n№{++counter} ");

					PrintClient(c1);
                }
            }

			if(counter == 0)
            {
                Console.Write("Мы не смогли подобрать Вам пару, подождите немного, надеемся, что скоро мы сможем это сделать\n");
            }

			Сontinue();
		}

		private static void PrintClient(Client client)
		{
			Console.Write($"ФИО {client.MyFulName} родил{((client.MySex.MySex == Gender.Sex.masculine) ? "ся" : "ась" )} " +
				$"{client.MyBirthday.ToShortDateString()} возраст {client.MyAge} контакты: логин {client.Login} номер телефона {client.MyPhoneNumber} \n\n");
		}

		private static void DeleteClient(DataBase dataBase, string[] loginPassword)
        {
            Console.WriteLine("Для подтверждения нажмите 1");

            if (Console.ReadKey().Key == ConsoleKey.D1)
            {
                dataBase.DeleteClientByClient(loginPassword);

                Console.WriteLine("\nСпасибо за использование нашей программы, надеемся, что мы Вам помогли подобрать пару\n");
            }
            else
            {
                Console.WriteLine("\nОтмена команды\n");
            }
        }

        private static string[] AddClient(DataBase dataBase)
        {
            Console.WriteLine("Введите необходимые данные");

            var temp = ReadClientInformation(dataBase);

            dataBase.AddСlient(new Client(temp, true));

            return temp[0..2];
        }


        private static string[] ReadClientInformation(DataBase dataBase)
        {
            var result = new string[7];

            var temp = ReadLogAndPas(dataBase);

            result[0] = temp[0]; result[1] = temp[1];

            Console.WriteLine("\nВведите ФИО:");
            result[2] = ReadFullName();

            while (true)
            {
                Console.WriteLine("\nПол:");

                result[3] = StandartView.ConverteToStandartWord(Console.ReadLine());

                if (Gender.IsMyGender(result[3], true))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nВы вели что-то не так, попробуйте ещё раз");
                }
            }

			while (true)
			{
				try
				{
					Console.WriteLine("\nДата рождения:");

					result[4] = Console.ReadLine();

					StandartView.ConverteStringToDate(result[4]);

					break;
				}
				catch (Exception)
				{
					Console.WriteLine("\nВы вели что-то не так, попробуйте ещё раз");
				}
			}

			while (true)
			{
                try
                {
					Console.WriteLine("\nГород проживания:");

					result[5] = SetString(Console.ReadLine());

					break;
				}
				catch (Exception)
				{
					Console.WriteLine("\nВы вели что-то не так, попробуйте ещё раз");
				}
			}

			while (true)
			{
				Console.WriteLine("\nНомер телефона:");

				result[6] = Console.ReadLine();

				if (StandartView.IsPhoneNumber(result[6]))
				{
					break;
				}
				else
				{
					Console.WriteLine("\nВы вели что-то не так, попробуйте ещё раз");
				}
			}

			Console.WriteLine();

            return result;
        }

		private static string[] ReadLogAndPas(DataBase dataBase)
		{
			var temp = new string[2];

			string control;

			while (true)
			{
				temp[0] = ReadLogin();

				if (!StandartView.IsLogin(temp[0]))
				{
					Console.WriteLine("Извините, это не может быть логином");
				}
				else if (dataBase.IsFreeLoginAdmins(temp[0]))
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

		private static string SetString(string str)
        {
            try
            {
                var res = StandartView.ConverteToStandartString(str);

                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nОшибка при вводе " + ex.Message);

                throw;
            }

        }

        private static string ReadFullName()
        {
            var res = string.Empty;

            while (true)
            {
                try
                {
                    string[] strArrey;

                    while (true)
                    {
                        strArrey = StandartView.ToStringArray(Console.ReadLine());

                        if (strArrey.Length == 2)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nВы что-то так ввели, должно быть три параметра - фамилия, имя\n");

                            Console.WriteLine("Если у Вас составное имя/фамилия - пишите через тире\n");

                            Console.WriteLine("Введите данные повторно\n");
                        }
                    }

                    res += SetString(strArrey[0]) + ' ' + SetString(strArrey[1]);
                    
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nВведите данные повторно\n");

                    res = null;
                }
            }
            return res;

        }

        private static void MenuForAdmin()
        {
            Console.WriteLine("1: Добавить клиента в базу");
            Console.WriteLine("2: Удалить клиента из базу");
            Console.WriteLine("3: Добавить админа");
            Console.WriteLine("4: Удалить свой аккаунт");
			Console.WriteLine("5: Удалить всю базу данных");
			Console.WriteLine("0: Выйти");
        }

        private static void RunForAdmin(DataBase dataBase)
        {
            var temp = Identification();

            var admin = new Admin(temp[0], temp[1]);

            if (dataBase.IsMyAdmin(admin))
            {
                var condition = true;

                while (condition)
                {
                    try
                    {
						Console.Clear();

						MenuForAdmin();

                        switch (ReadComend())
                        {
                            case ('1'): AddHumanByAdmin(dataBase); break;
                            case ('2'): DeleteHumanByAdmin(dataBase, admin); break;
                            case ('3'): AddHumanByAdmin(dataBase, true); break;
                            case ('4'): DeleteHumanByAdmin(dataBase, admin, true); condition = false; break;
							case ('5'):	DeleteDataBase(ref dataBase, admin); break;
							case ('0'): Console.WriteLine("Вы вышли из аккаунта администратора\n"); condition = false; break;
                            default: Console.WriteLine("Такой команды нет\n"); break;
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
                Console.WriteLine("Ну поиграли и хватит, Вы не администратор\n");
            }
            else
            {
                Console.WriteLine("Вы ошиблись при вводе пароля\n");
            }

			Сontinue();
		}

        private static void AddHumanByAdmin(DataBase dataBase, bool addAdmin = false)
        {
            if (addAdmin)
            {
                var temp = ReadLogAndPas(dataBase);

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
                    var loginClient = ReadLogin();

                    dataBase.DeleteClientByAdmin(admin, loginClient);
                }
            }
            else
            {
                Console.WriteLine("что-то пошло не так");
            }
        }

		private static void DeleteDataBase(ref DataBase dataBase, Admin admin)
		{
			Console.WriteLine("Подтвердите решения введя пароль");

			if (admin.IsMyPassword(ReadPassword()))
			{
				dataBase.DeleteAllClient(admin);
			}
			else
			{
				Console.WriteLine("Отмена команды\n");
			}
		}
    }
}
