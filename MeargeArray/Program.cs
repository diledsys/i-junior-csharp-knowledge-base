using System;
using System.Collections.Generic;

namespace MeargeArray
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] array1 = { "1", "2", "1" };
            string[] array2 = { "3", "2" };

            //List<string> result = MergeUnique(array1, array2);
            string[][] allArrays = new string[][] { array1, array2 };
            List<string> result = MergeUnique(allArrays);

            Console.WriteLine("Результат:");
            Console.WriteLine(string.Join(", ", result));
        }

        public static List<string> MergeUnique(string[][] arrays)
        {
            List<string> result = new List<string>();

            foreach (var array in arrays)
            {
                foreach (var item in array)
                {
                    if (result.Contains(item) == false)
                        result.Add(item);
                }
            }

            return result;
        }
    }
}
