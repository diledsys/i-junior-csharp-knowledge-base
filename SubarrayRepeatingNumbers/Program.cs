// Подмассив повторений чисел
// В массиве чисел найдите самый длинный подмассив из одинаковых чисел.
// Дано 30 чисел.Вывести в консоль сам массив, число, которое само больше раз повторяется подряд и количество повторений.
// Дополнительный массив не надо создавать.
// Пример 1: {
//                 5, 5, 9, 9, 9, 5, 5}
//             -число 9 повторяется 3 раза подряд.
// Пример 2: {
//                 5, 5, 5, 3, 3, 3, 3}
//             -число 3 повторяется 4 раза подряд.

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
                }
                else
                {
                    currentRepeatCount = 1;
                }

                if (currentRepeatCount > maxRepeatCount)
                {
                    maxRepeatCount = currentRepeatCount;
                    number = numbers[i];
                }
            }

            Console.WriteLine($"\nЧисло {number} повторяется {maxRepeatCount} раза(ов) подряд.");
        }
    }
}
