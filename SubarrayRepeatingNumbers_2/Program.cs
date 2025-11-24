using System;

namespace SubarrayRepeatingNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int maxRepeatCount = 1;
            int currentRepeatCount = 1;
            int arraySize = 30;
            int[] numbers = new int[arraySize];
            int minimumRandomValue = 1;
            int maximumRandomValue = 6;
            Random random = new Random();

            for (int i = 0; i < arraySize; i++)
            {
                numbers[i] = random.Next(minimumRandomValue, maximumRandomValue + 1);
            }

            Console.WriteLine("Исходный массив:");
            Console.WriteLine(string.Join(" ", numbers));

            int number = numbers[0];

            for (int i = 1; i < arraySize; i++)
            {
                if (numbers[i] == numbers[i - 1])
                {
                    currentRepeatCount++;
                    if (currentRepeatCount > maxRepeatCount)
                    {
                        maxRepeatCount = currentRepeatCount;
                        number = numbers[i];
                    }
                }
                else
                {
                    currentRepeatCount = 1;
                }
            }

            Console.WriteLine($"\nЧисло {number} повторяется {maxRepeatCount} раза(ов) подряд.");
        }
    }
}