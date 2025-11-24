using System;

namespace SortingNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numberArray = { 6, 4, 3, 4, 5, 2, 5, 3, 9, 10, 8 };

            Console.WriteLine($"массив чисел до сортировки: {string.Join(";", numberArray)}");

            for (int i = 0; i < numberArray.Length; i++)
            {
                for (int j = 0; j < numberArray.Length - 1 - i; j++)
                {
                    if (numberArray[j] > numberArray[j + 1])
                    {
                        int temp = numberArray[j];
                        numberArray[j] = numberArray[j + 1];
                        numberArray[j + 1] = temp;
                    }
                }
            }

            Console.Write("массив чисел после сортировки: ");

            foreach (int number in numberArray)
            {
                Console.Write(number + " ");
            }
        }
    }
}
