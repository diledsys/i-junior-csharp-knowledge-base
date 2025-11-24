using System;

namespace Subsequence
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int start = 5;
            int step = 7;
            int max = 103;

            for (int i = start; i <= max; i += step)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
        }
    }
}