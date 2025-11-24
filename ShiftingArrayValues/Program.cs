using System;

namespace ShiftingArrayValues
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numberArray = { 1, 2, 3, 4, 5 };
            int shift;
            bool isNumber = false;

            Console.WriteLine("Исходный массив: " + string.Join(", ", numberArray));
            while (isNumber == false)
            {
                Console.Write("Введите количество позиций для сдвига влево: ");

                if (int.TryParse(Console.ReadLine(), out shift))
                {
                    ShiftArrayLeft(numberArray, shift);
                    isNumber = true;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите число");
                }

            }
            Console.WriteLine("Массив после сдвига: " + string.Join(", ", numberArray));
        }

        private static void ShiftArrayLeft(int[] array, int shift)
        {
            int length = array.Length;
            shift %= length;
            if (shift == 0)
                return;

            Reverse(array, 0, shift - 1);
            Reverse(array, shift, length - 1);
            Reverse(array, 0, length - 1);
        }

        private static void Reverse(int[] array, int start, int end)
        {
            while (start < end)
            {
                array[start] = array[end];
                int temp = array[start];
                array[end] = temp;
                start++;
                end--;
            }
        }
    }
}
