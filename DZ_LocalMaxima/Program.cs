            // ДЗ: Локальные максимумы
            // Дан одномерный массив целых чисел из 30 элементов.
            // Найдите все локальные максимумы и вывести их. (Элемент является локальным максимумом, если он больше своих соседей)
            // Крайний элемент является локальным максимумом, если он больше своего соседа.
            // Программа должна работать с массивом любого размера.
            // Массив всех локальных максимумов не нужен.
using System;

namespace DZ_LocalMaxima
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 30;
            int[] array = new int[size];
            int minimumRandomValue = 1;
            int maximumRandomValue = 100;

            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(minimumRandomValue, maximumRandomValue);
            }

            Console.WriteLine("Исходный массив:");

            for (int i = 0; i < size; i++)
            {
                Console.Write(array[i] + " ");
            }

            Console.WriteLine("\nЛокальные максимумы: ");

            for (int i = 0; i < size; i++)
            {
                if ((i == 0 && array[i] > array[i + 1]) ||
                    (i == size - 1 && array[i] > array[i - 1]) ||
                    (i > 0 && i < size - 1 && array[i] > array[i - 1] && array[i] > array[i + 1]))
                {
                    Console.Write(array[i] + " ");
                }
            }
        }
    }
}
