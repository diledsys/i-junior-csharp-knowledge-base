// Локальные максимумы
// Дан одномерный массив целых чисел из 30 элементов.
// Найдите все локальные максимумы и вывести их. (Элемент является локальным максимумом, если он больше своих соседей)
// Крайний элемент является локальным максимумом, если он больше своего соседа.
// Программа должна работать с массивом любого размера.
// Массив всех локальных максимумов не нужен.

using System;

namespace LocalMaxima
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[30];
            int min = 0;
            int max = array.Length;

            Random rand = new Random();

            for (int i = 0; i < max; i++)
            {
                array[i] = rand.Next(min, max + 1);
            }

            Console.WriteLine("содержимое массива");
            Console.WriteLine(string.Join(";", array));
            Console.WriteLine("все локальные максимумы");

            for (int i = 0; i < array.Length; i++)
            {
                if (i == 0 && array[i] > array[i + 1])
                {
                    Console.Write(array[i] + " ");
                }
                else if (i == array.Length - 1 && array[i] > array[i - 1])
                {
                    Console.Write(array[i] + " ");
                }
                else if (i > 0 && i < array.Length - 1)
                {
                    if (array[i] > array[i - 1] && array[i] > array[i + 1])
                    {
                        Console.Write(array[i] + " ");
                    }
                }
            }

            Console.WriteLine();
        }
    }
}
