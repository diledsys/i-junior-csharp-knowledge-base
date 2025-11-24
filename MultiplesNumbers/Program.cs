using System;

namespace MultiplesNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int randomMin = 10;
            const int randomMax = 25;

            Random random = new Random();
            int divisor = random.Next(randomMin, randomMax + 1);

            const int rangeStart = 50;
            const int rangeEnd = 150;

            int multiplesCount = 0;

            for (int i = 0; i <= rangeEnd; i += divisor)
            {
                if (i >= rangeStart)
                {
                    multiplesCount++;
                }
            }

            Console.WriteLine($"Значение N: {divisor}");
            Console.WriteLine($"Количество чисел от {rangeStart} до {rangeEnd}, кратных числу {divisor}: {multiplesCount}");
        }
    }
}
