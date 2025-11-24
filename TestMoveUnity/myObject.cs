using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMoveUnity
{
    internal class MyObject
    {
        public int Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"ID: {Id}, Name: {Name}");
        }
    }
}
