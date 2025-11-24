#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

#endregion

namespace Fish
{
    internal class Program
    {
        private static void Main()
        {
            var aquarium = new Aquarium(10);
            aquarium.Work();
        }
    }

    internal class Fish
    {
        private static int _sNextId;

        public Fish(string name)
        {
            Id = ++_sNextId;

            var minLife = 5;
            var maxLife = 15;

            Name = name;
            Age = 0;
            Lifespan = UserUtils.GenerateRandomNumber(minLife, maxLife);
        }

        public int Id { get; }

        public string Name { get; }

        public int Age { get; private set; }

        public int Lifespan { get; }

        public bool IsAlive =>
            Age < Lifespan;

        public void GrowOld()
        {
            if (IsAlive == false)
                return;

            Age++;
        }

        public void ShowStatus()
        {
            var visualizationColorStatusLevel = 3;

            var color = ConsoleColor.Green;

            if (IsAlive == false)
                color = ConsoleColor.Red;
            else if (Lifespan - Age <= visualizationColorStatusLevel)
                color = ConsoleColor.Yellow;

            Console.ForegroundColor = color;

            var status = IsAlive ? "Жива" : "x Мертва x";
            var healthBars = IsAlive ? Math.Max(1, Lifespan - Age) : 0;
            var bars = new string('|', healthBars).PadRight(Lifespan, '.');

            Console.WriteLine($" {Name,-10} Id:{Id,-8} Возраст: {Age,2}/{Lifespan,-2} [{bars}] {status}");
            Console.ResetColor();
        }
    }

    internal class Aquarium
    {
        private readonly List<Fish> _fishes = new List<Fish>();

        public Aquarium(int capacity)
        {
            Capacity = capacity;
        }

        public int Capacity { get; }

        public void AddFish(string name)
        {
            if (_fishes.Count >= Capacity)
            {
                ShowMessage("Аквариум переполнен!");
                return;
            }

            var fish = new Fish(name);
            _fishes.Add(fish);
            ShowMessage($"[ Добавлена Рыба по имени: {fish.Name} с Id: {fish.Id} ]");
        }

        public void RemoveFishById(string fishId)
        {
            int id;

            if (int.TryParse(fishId, out id) == false)
            {
                ShowMessage("Id должен быть цифровым значением");
                return;
            }

            if (_fishes.Count == 0)
            {
                ShowMessage("Аквариум пуст!");
                return;
            }

            var fish = _fishes.FirstOrDefault(targetFish => targetFish.Id == id);

            if (fish == null)
            {
                ShowMessage($"Рыба c ID:{id} не найдена.");
                return;
            }

            ShowMessage($"Рыба по имени: {fish.Name} с Id: {id} была извлечена из аквариума.");
            _fishes.Remove(fish);
        }

        public void AgeAllFishes()
        {
            foreach (var fish in _fishes)
                fish.GrowOld();
        }

        public void ShowAllFishes()
        {
            ShowMessage("\n=== Аквариум ===\n", 1);

            if (_fishes.Count == 0)
            {
                ShowMessage("Аквариум пуст.", 1);
                return;
            }

            foreach (var fish in _fishes)
                fish.ShowStatus();

            var numberLiveFish = _fishes.Count(fish => fish.IsAlive);
            var numberDeadFish = _fishes.Count(fish => fish.IsAlive == false);

            Console.ForegroundColor = ConsoleColor.Cyan;
            ShowMessage(
                $"\nСтатистика: живых — {numberLiveFish}, мёртвых — {numberDeadFish}, всего — {_fishes.Count}/{Capacity}",
                1);
            Console.ResetColor();
        }

        public void Work()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Симулятор Аквариума";

            var isRunning = true;
            var day = 1;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine($"День -= {day++} =-\n");

                AgeAllFishes();
                ShowAllFishes();

                ShowMessage($"\nМеню: [{(int)MenuAction.AddFish}]Добавить. " +
                            $" [{(int)MenuAction.RemoveFish}]удалить по ID. " +
                            $" [{(int)MenuAction.NextDay}]следующий день." +
                            $" [{(int)MenuAction.Quit}]Выход.", 1);

                if ((int.TryParse(PromptRequired("ваш выбор:"), out var key) &&
                     Enum.IsDefined(typeof(MenuAction), key)) == false)
                {
                    ShowMessage("не верный ввод");
                    continue;
                }

                var action = (MenuAction)key;

                switch (action)
                {
                    case MenuAction.AddFish:
                        AddFish(PromptRequired("Введите имя рыбы: "));
                        break;

                    case MenuAction.RemoveFish:
                        RemoveFishById(PromptRequired("Введите Id рыбы для извлечения: "));
                        break;

                    case MenuAction.Quit:
                        isRunning = false;
                        break;

                    case MenuAction.NextDay:
                        break;
                }
            }

            ShowMessage("\nСимуляция завершена.");
        }

        private void ShowMessage(string message, int delayMs = 2000)
        {
            Console.WriteLine();
            Console.Write(message);
            Thread.Sleep(delayMs);
        }

        private string PromptRequired(string prompt)
        {
            var isEmpty = false;
            var read = "";

            Console.WriteLine();
            Console.Write(prompt);

            while (isEmpty == false)
            {
                read = Console.ReadLine();
                isEmpty = !string.IsNullOrWhiteSpace(read);

                if (isEmpty == false)
                    Console.Write($"Пустое значение не допускается.\n{prompt}");
            }

            return read;
        }

        private enum MenuAction
        {
            AddFish = 1,
            RemoveFish = 2,
            NextDay = 3,
            Quit = 4
        }
    }

    public static class UserUtils
    {
        private static readonly Random SRandom = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return SRandom.Next(min, max + 1);
        }
    }
}
