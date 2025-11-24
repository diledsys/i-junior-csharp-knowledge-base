using System;

namespace LocalMaxima
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();

            const int numberOfElements = 30;
            int[] arrayIntegers = new int[numberOfElements];
            int minimumRandomValue = 1;
            int maximumRandomValue = 100;

            for (int i = 0; i < numberOfElements; i++)
            {
                arrayIntegers[i] = rand.Next(minimumRandomValue, maximumRandomValue + 1);
            }

            Console.WriteLine("Массив:");
            Console.WriteLine(string.Join("; ", arrayIntegers));

            Console.WriteLine("\nЛокальные максимумы:");

            if (numberOfElements > 1 && arrayIntegers[0] > arrayIntegers[1])
            {
                Console.Write(arrayIntegers[0] + " ");
            }

            for (int i = 1; i < numberOfElements - 1; i++)
            {
                if (arrayIntegers[i] > arrayIntegers[i - 1] && arrayIntegers[i] > arrayIntegers[i + 1])
                {
                    Console.Write(arrayIntegers[i] + " ");
                }
            }

            int lastIndex = numberOfElements - 1;
            if (numberOfElements > 1 && arrayIntegers[lastIndex] > arrayIntegers[lastIndex - 1])
            {
                Console.Write(arrayIntegers[lastIndex] + " ");
            }

            Console.WriteLine();
        }
    }
}
