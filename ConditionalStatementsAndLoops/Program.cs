using System;

namespace LargestElement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int totalRowsInMatrix = 10;
            int totalColumnsInMatrix = 10;
            int valueToReplaceWith = 0;
            int minimumRandomValue = 1;
            int maximumRandomValue = 100;

            int[,] matrix = new int[totalRowsInMatrix, totalColumnsInMatrix];
            Random random = new Random();

            for (int i = 0; i < totalRowsInMatrix; i++)
            {
                for (int j = 0; j < totalColumnsInMatrix; j++)
                {
                    matrix[i, j] = random.Next(minimumRandomValue, maximumRandomValue + 1);
                }
            }

            int maxElement = matrix[0, 0];

            for (int i = 0; i < totalRowsInMatrix; i++)
            {
                for (int j = 0; j < totalColumnsInMatrix; j++)
                {
                    if (matrix[i, j] > maxElement)
                        maxElement = matrix[i, j];
                }
            }

            Console.WriteLine("Исходная матрица:\n");

            for (int i = 0; i < totalRowsInMatrix; i++)
            {
                for (int j = 0; j < totalColumnsInMatrix; j++)
                {
                    if (matrix[i, j] == maxElement)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    Console.Write(matrix[i, j].ToString("D2") + " ");
                }

                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\nМаксимальный элемент матрицы: {maxElement}\n");

            for (int i = 0; i < totalRowsInMatrix; i++)
            {
                for (int j = 0; j < totalColumnsInMatrix; j++)
                {
                    if (matrix[i, j] == maxElement)
                    {
                        matrix[i, j] = valueToReplaceWith;
                    }
                }
            }

            Console.WriteLine($"Матрица после замены максимального элемента на {valueToReplaceWith}:\n");

            for (int i = 0; i < totalRowsInMatrix; i++)
            {
                for (int j = 0; j < totalColumnsInMatrix; j++)
                {
                    Console.Write(matrix[i, j].ToString("D2") + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
