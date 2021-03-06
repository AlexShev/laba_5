﻿using System;
using System.Linq;

namespace laba_5
{
    public class Program
    {
		/* Вопросы:
         * он кстати предложил мне обратно создать только читаемые поля и свойства
         * 
         * почему static это зло, если компилятор, говорит - сделайте метод статичным - это повысит производительность
         * 
         * чем == отличается от Equals(object obj)
         * 
         * стоит ли разграничивоть прастранство имён персоны и всего что с этим классом связанно с консолью?
         */

		// интерфейс плохой - нужен лишь для демонстрации логики
		public static void Main()
        {
            try
            {
                ConsolIneractor.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Не впойманая ошибка\n{e.Message}");
            }
        }
    }
}
