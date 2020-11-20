using System;

namespace laba_5
{
    class Program
    {
        /* Вопросы:
         * 
         * чем == отличается от Equals(object obj)
         * 
         * стоит ли разграничивоть прастранство имён персоны и всего что с этим классом связанно с консолью?
         */

        // интерфейс в плохой нужен лишь для демонстрации логики
        static void Main(string[] args)
        {
            try
            {
                ConsolIneractor.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при занесении в базу данных\n" + e);
            }
        }
    }
}