using System;

namespace StringArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] numberArray = {
                {1, 2},
                {3, 4},
                {5, 6}
            };

            int rowCount = numberArray.GetLength(0);
            int colCount = numberArray.GetLength(1);

            int sumSecondRow = 0;

            for (int j = 0; j < colCount; j++)
            {
                sumSecondRow += numberArray[1, j];
            }

            Console.WriteLine($"Сумма второй строки: {sumSecondRow}");

            int productFirstColumn = 1;

            for (int i = 0; i < rowCount; i++)
            {
                productFirstColumn *= numberArray[i, 0];
            }

            Console.WriteLine($"Произведение первого столбца: {productFirstColumn}");
        }
    }
}
