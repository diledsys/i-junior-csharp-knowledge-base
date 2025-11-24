// Пользователь вводит числа, и программа их запоминает.
// Как только пользователь введёт команду sum, программа выведет сумму всех веденных чисел.
// Выход из программы должен происходить только в том случае, если пользователь введет команду exit.
// Если введено не sum и не exit, значит это число и его надо добавить в массив.
// В начале цикла надо выводить в консоль все числа, которые содержатся в массиве, а значит их ввел пользователь ранее.
// Программа должна работать на основе расширения массива.
// Внимание, нельзя использовать List<T> и Array.Resize

using System;
using System.Collections.Generic;

namespace DynamicArray
{
    internal class Program
    {
        private const string ExitCommand = "exit";
        private const string SumCommand = "sum";

        private static List<int> numbers = new List<int>();

        static void Main(string[] args)
        {
            ShowPrompt();

            bool isRunning = true;

            while (isRunning)
            {
                ShowArray();
                Console.Write("Введите число, 'sum' или 'exit': ");
                string input = Console.ReadLine().Trim().ToLower();
                isRunning = HandleCommand(input);
            }
        }

        private static void ShowPrompt()
        {
            Console.WriteLine("Введите число для добавления в массив. Введите 'sum' для вывода суммы, 'exit' для выхода.");
        }

        private static void ShowArray()
        {
            string output = numbers.Count > 0 ? string.Join(", ", numbers) : "пустой";
            Console.WriteLine("\nТекущий массив: " + output);
        }

        private static bool HandleCommand(string command)
        {
            switch (command)
            {
                case ExitCommand:
                    Console.WriteLine("Выход из программы.");
                    return false;

                case SumCommand:
                    Console.WriteLine($"Сумма всех введённых чисел: {CalculateSum()}\n");
                    return true;

                default:
                    return AddNumber(command);
            }
        }

        private static int CalculateSum()
        {
            int sum = 0;
            foreach (int number in numbers)
                sum += number;
            return sum;
        }

        private static bool AddNumber(string input)
        {
            if (int.TryParse(input, out int number))
            {
                numbers.Add(number);
                Console.WriteLine($"Добавлено: {number}");
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите число, 'sum' или 'exit'.");
            }
            return true;
        }
    }
}

