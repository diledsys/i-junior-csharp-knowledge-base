// ключевое значения условия для выбора цикла:
//Считать количество итераций не надо. Даже если максимальное число будет равно 789, в коде изменится только максимальное число

// Обоснование выбора
// мой выбор на while так как мы заранее не знаем количество итераций, но мы знаем какое число последнее

namespace Subsequence
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int start = 5;
            int step = 7;
            int max = 103;
            int current = start;

            while (current <= max)
            {
                Console.Write(current + " ");
                current += step;
            }

            Console.WriteLine();
        }
    }
}