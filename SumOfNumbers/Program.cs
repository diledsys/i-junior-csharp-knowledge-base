using System;

namespace SumOfNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number;
            int minValue = 1, maxValue = 100;
            int primaryDivisor = 3, secondaryDivisor = 5;
            int sum = 0;
            string sequence = "";

            Random random = new Random();
            number = random.Next(minValue, maxValue + 1);

            Console.WriteLine($"Случайное число: {number}");

            for (int i = 1; i <= number; i++)
            {
                if (i % primaryDivisor == 0 || i % secondaryDivisor == 0)
                {
                    sum += i;
                    sequence += i + " ";
                }
            }

            Console.WriteLine($"Подходящие числа: {sequence.Trim()}");
            Console.WriteLine($"Сумма: {sum}");
        }
    }
}
