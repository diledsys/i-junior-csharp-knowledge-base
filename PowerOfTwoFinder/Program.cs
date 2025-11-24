using System;

namespace PowerOfTwoFinder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int randomMin = 10;
            const int randomMax = 25;
            const int powerOfTwo = 2;

            Random random = new Random();
            int number = random.Next(randomMin, randomMax + 1);

            int power = 0;
            int value = 1;

            while (value <= number)
            {
                value *= powerOfTwo;
                power++;
            }

            Console.WriteLine($"Случайное число: {number}");
            Console.WriteLine($"Минимальная степень {powerOfTwo}, превышающая {number}: {power}");
            Console.WriteLine($"{powerOfTwo} в степени {power} = {value}");
        }
    }
}
