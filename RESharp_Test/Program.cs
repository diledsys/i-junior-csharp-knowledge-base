#region

using System;
using System.Collections.Generic;

#endregion

namespace test_project
{
    public interface IRunner
    {
        void Run();
    }

    #region 🤦 лишний регион

    public class Aquarium : IRunner
    {
        private delegate void Printer(string s);

        private enum Mode
        {
            Off,
            On,
            Pause
        }

        internal static class Utils
        {
            public static int Sum(int a, int b)
            {
                return a + b;
            }
        }

        public const int MaxFish = 42;
        public static readonly string Version = "1.0";
        private static int _count;

        private readonly List<string> _names = new List<string>();
        private int _age;

        public Aquarium()
        {
            _count++;
            _age = 0;
            _names.Add("Nemo");
            _names.Add("dory");
        }

        // индексатор и свойство болтаются где-попало
        public string this[int i]
        {
            get => _names[i];
            set => _names[i] = value;
        }

        public string Title { get; } = " My Aquarium ";

        // реализация интерфейса где-то внизу
        void IRunner.Run()
        {
            Run();
        }

        public void Run()
        {
            _age++;
            if (_age > 99)
                Console.WriteLine("old");
            Console.WriteLine($"{Title} v{Version} ({_count})");
        }

        #region ещё один странный регион

        public int Calc(int a,
            int b)
        {
            var res = Utils.Sum(a, b);
            return res;
        }

        #endregion


        protected void PRINT_ALL()
        {
            Printer p = s => Console.WriteLine(s);
            foreach (var n in _names)
                p(n.ToUpper());
        }

        private void Addfish(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;
            _names.Add(name);
        }

        // приватные методы напоследок, без отступов
        private bool Has(string n)
        {
            return _names.Contains(n);
        }

        public event EventHandler SomethingHappened;
    }

    #endregion

    // ещё один тип в файле
    internal class Helper
    {
        public static void Log(string m)
        {
            Console.WriteLine(m);
        }
    }
}
