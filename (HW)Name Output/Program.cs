using System;

namespace HwNameOutput
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите символ: ");
            string symbol = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(symbol))
            {
                Console.WriteLine("Ошибка: введён пустой символ.");
            }

            char frameChar = symbol[0];

            Console.Write("Введите имя: ");
            string name = Console.ReadLine();

            string framedName = $"{frameChar}{name}{frameChar}";
            int lineLength = framedName.Length;

            Console.WriteLine(new string(frameChar, lineLength));
            Console.WriteLine(framedName);
            Console.WriteLine(new string(frameChar, lineLength));
            {
            }
        }
    }
}