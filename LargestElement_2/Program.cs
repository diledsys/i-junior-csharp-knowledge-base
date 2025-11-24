using System;

namespace LargestElement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int RowCount = 10;
            const int ColumnCount = 10;
            const int Min = 1;
            const int Max = 100;
            const int ReplaceValue = 0;

            int[,] matrix = new int[RowCount, ColumnCount];
            Random random = new Random();

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    matrix[i, j] = random.Next(Min, Max + 1);
                }
            }

            int maxElement = matrix[0, 0];

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (matrix[i, j] > maxElement)
                        maxElement = matrix[i, j];
                }
            }

            Console.WriteLine("Исходная матрица:\n");

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
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

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (matrix[i, j] == maxElement)
                        matrix[i, j] = ReplaceValue;
                }
            }

            Console.WriteLine("Матрица после замены максимального элемента на 0:\n");

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    Console.Write(matrix[i, j].ToString("D2") + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
