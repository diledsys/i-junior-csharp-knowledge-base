using System;


namespace BarHealth
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int value = 10;
            int maxValue = 20;
            int position = 0;
            const ConsoleColor Red = ConsoleColor.Red;
            const ConsoleColor Green = ConsoleColor.Green;

            BarHalth(value, 0, maxValue, Red);
            Console.ReadKey();
            BarHalth(5, 0, maxValue, Red);
            Console.ReadKey();

        }
        static void BarHalth(int value, int position, int maxValue, ConsoleColor color)
        {
            ConsoleColor defaultColor = Console.BackgroundColor;

            Console.Write("[");

            Console.BackgroundColor = color;

            for (int i = 1; i <= value; i++)
            {
                Console.SetCursorPosition(i, position);
                Console.Write(" ");
            }

            Console.BackgroundColor = defaultColor;

            for (int i = value; i <= maxValue; i++)
            {
                Console.SetCursorPosition(i, position);
                Console.Write(" ");
            }

            Console.SetCursorPosition(maxValue, position);
            Console.Write("]");
        }
    }
}
