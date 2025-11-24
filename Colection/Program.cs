using System;
using System.Collections.Generic;
using System.Linq;

namespace Colection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            list.Capacity = 5;
            int c = list.Capacity;
            // Console.WriteLine(c);

            for (int i = 0; i < c; i++)
            {
                list.Add(i);
            }
            // foreach (int str in list)
            // {
            //     Console.WriteLine(str);
            // }
            // //bool isEmpty = list.Contains("20");
            // //Console.WriteLine(isEmpty);
            //
            // var ro = list.AsReadOnly();
            // //list.Add("ASD");
            // Console.WriteLine(string.Concat(ro));
            //
            // list.ForEach(y => Console.WriteLine(y));
            // // int r1 = list.BinarySearch("51");
            // //Console.WriteLine(r1);
            // list.Reverse();
            // list.ForEach(y => Console.WriteLine(y));

            list.ForEach(y =>
            {
                int x = y * y;
                Console.WriteLine(x);
            });
            int[] ints = list.ToArray();
            Queue<int> q = new Queue<int>();
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            q.Enqueue(4);
            q.Enqueue(5);
            q.CopyTo(ints, 0);
            Console.WriteLine(string.Join(";", q.GetEnumerator()));
            {

            }
            {
                Stack<int> stack = new Stack<int>();
                stack.Push(1);
                stack.Push(2);
                stack.Push(3);
                stack.Push(4);
                stack.Push(5);
                foreach (var item in stack)
                {
                    Console.WriteLine(item);
                }
                stack.ElementAt(0);

                Dictionary<int, String> keyValuePairs = new Dictionary<int, String>();
                for (int i = 0; i < 20; i++)
                {
                    keyValuePairs.Add(i, Convert.ToChar(i + 32).ToString());
                }
                foreach (var key in keyValuePairs)
                {
                    Console.Write(string.Join(";", key.Value));

                }

            }
        }
    }
}
