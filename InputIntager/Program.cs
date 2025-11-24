using System;

namespace InputIntegerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int userNumber = 0;
            bool isInputValid = false;

            while (isInputValid == false)
            {
                Console.Write("Введите целое число: ");
                string input = Console.ReadLine();

                isInputValid = TryParseInteger(input, out int parsedNumber);
                userNumber = parsedNumber;
            }

            Console.WriteLine($"Вы ввели число: {userNumber}");
        }

        private static bool TryParseInteger(string input, out int result)
        {
            if (int.TryParse(input, out result) == false)
            {
                Console.WriteLine($"Некорректный ввод: '{input}'. Пожалуйста, введите целое число.");
                return false;
            }

            return true;
        }
    }
}
