using System;

namespace ShiftingArrayValues
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 1, 2, 3, 4, 5 };

            Console.WriteLine("Исходный массив: " + string.Join(", ", numbers));

            bool isInputValid = false;

            while (isInputValid == false)
            {
                Console.Write("Введите количество позиций для сдвига влево: ");

                if (int.TryParse(Console.ReadLine(), out int shiftCount))
                {
                    ShiftLeft(numbers, shiftCount);
                    isInputValid = true;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
                }

                Console.WriteLine("Массив после сдвига: " + string.Join(", ", numbers));
            }
        }

        private static void ShiftLeft(int[] array, int shiftCount)
        {
            int length = array.Length;
            shiftCount %= length;

            for (int i = 0; i < shiftCount; i++)
            {
                int first = array[0];

                for (int j = 0; j < length - 1; j++)
                {
                    array[j] = array[j + 1];
                }

                array[length - 1] = first;
            }
        }
    }
}
