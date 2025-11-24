using System;
using System.Collections.Generic;

namespace DynamicArray
{
    internal class Program
    {
        private const string ExitCommand = "exit";
        private const string SumCommand = "sum";


        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();

            ShowPrompt();

            bool isRunning = true;

            while (isRunning == true)
            {
                ShowArray(numbers);

                Console.Write($"Введите число, {SumCommand} или {ExitCommand}: ");

                string input = Console.ReadLine().Trim().ToLower();

                switch (input)
                {
                    case ExitCommand:
                        Console.WriteLine("Выход из программы.");
                        isRunning = false;
                        break;

                    case SumCommand:
                        Console.WriteLine($"Сумма всех введённых чисел: {CalculateSum(numbers)}\n");
                        break;

                    default:
                        AddNumber(numbers, input);
                        break;
                }
            }
        }

        private static void ShowPrompt()
        {
            Console.WriteLine($"Введите число для добавления в массив. Введите {SumCommand} для вывода суммы, {ExitCommand} для выхода.");
        }

        private static void ShowArray(List<int> numbers)
        {
            string output = numbers.Count > 0 ? string.Join(", ", numbers) : "пустой";
            Console.WriteLine("\nТекущий массив: " + output);
        }

        private static int CalculateSum(List<int> numbers)
        {
            int sum = 0;

            foreach (int number in numbers)
                sum += number;

            return sum;
        }

        private static bool AddNumber(List<int> numbers, string input)
        {
            if (int.TryParse(input, out int number))
            {
                numbers.Add(number);
                Console.WriteLine($"Добавлено: {number}");
            }
            else
            {
                Console.WriteLine($"Некорректный ввод. Пожалуйста, введите число, {SumCommand} или {ExitCommand}.");
            }

            return true;
        }
    }
}
