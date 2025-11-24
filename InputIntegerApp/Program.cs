using System;

namespace InputIntegerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = ReadInt();
            Console.WriteLine($"Вы ввели число: {number}");
        }

        private static int ReadInt()
        {
            int result;
            Console.Write("Введите целое число: ");

            while (!int.TryParse(Console.ReadLine(), out result))
                Console.Write("Ошибка. Введите корректное целое число: ");

            return result;
        }
    }
}
