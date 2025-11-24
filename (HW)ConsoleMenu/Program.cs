using System;

internal class Program
{
    static void Main(string[] args)
    {
        bool isRunning = true;

        const string CommandHello = "hello";
        const string CommandInfo = "info";
        const string CommandRandom = "random";
        const string CommandClear = "clear";
        const string CommandExit = "exit";
        
        int minValue = 1;
        int maxValue = 100;

        Random random = new Random();

        while (isRunning)
        {
            Console.WriteLine("\n=== МЕНЮ КОМАНД ===");
            Console.WriteLine($"{CommandHello}     - Вывести приветствие");
            Console.WriteLine($"{CommandInfo}     - Вывести информацию");
            Console.WriteLine($"{CommandRandom} - Показать случайное число");
            Console.WriteLine($"{CommandClear}  - Очистить консоль");
            Console.WriteLine($"{CommandExit}   - Выйти из программы");

            Console.Write("\nВведите команду: ");
            string input = Console.ReadLine().ToLower();

            switch (input)
            {
                case CommandHello:
                    Console.WriteLine("Привет! Это первая команда.");
                    break;

                case CommandInfo:
                    Console.WriteLine("Информация: Это учебная программа: курс я джуниор Условные операторы и циклы.");
                    break;

                case CommandRandom:
                    int number = random.Next(minValue, maxValue + 1);
                    Console.WriteLine($"Случайное число: {number}");
                    break;

                case CommandClear:
                    Console.Clear();
                    break;

                case CommandExit:
                    Console.WriteLine("Выход из программы...");
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Неизвестная команда. Попробуйте снова.");
                    break;
            }
        }
    }
}

