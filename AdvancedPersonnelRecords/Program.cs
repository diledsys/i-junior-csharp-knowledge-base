using System;
using System.Collections.Generic;

namespace StaffAccounting
{
    internal class Program
    {
        private const string AddCommand = "add";
        private const string RemoveCommand = "remove";
        private const string ShowCommand = "show";
        private const string ExitCommand = "exit";

        static void Main(string[] args)
        {
            var staffByPosition = new Dictionary<string, List<string>>();
            var positions = new HashSet<string>();

            Console.WriteLine("Кадровый учет. Команды: add, remove, show, exit");

            bool isRunning = true;

            while (isRunning)
            {
                Console.Write("\nВведите команду: ");
                string command = Console.ReadLine()?.Trim().ToLower();

                switch (command)
                {
                    case AddCommand:
                        StaffManager.AddEmployee(staffByPosition, positions);
                        break;

                    case RemoveCommand:
                        StaffManager.RemoveEmployee(staffByPosition, positions);
                        break;

                    case ShowCommand:
                        StaffManager.ShowAll(staffByPosition, positions);
                        break;

                    case ExitCommand:
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }
            }
        }
    }

    internal static class StaffManager
    {
        public static void AddEmployee(
            Dictionary<string, List<string>> staffByPosition,
            HashSet<string> positions)
        {
            Console.Write("Введите должность: ");
            string position = Console.ReadLine()?.Trim();

            Console.Write("Введите полное имя сотрудника: ");
            string fullName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(position) || string.IsNullOrWhiteSpace(fullName))
            {
                Console.WriteLine("Некорректный ввод.");
                return;
            }

            if (!staffByPosition.ContainsKey(position))
            {
                staffByPosition[position] = new List<string>();
                positions.Add(position);
            }

            staffByPosition[position].Add(fullName);
            Console.WriteLine($"Сотрудник {fullName} добавлен на должность {position}.");
        }

        public static void RemoveEmployee(
            Dictionary<string, List<string>> staffByPosition,
            HashSet<string> positions)
        {
            Console.Write("Введите должность: ");
            string position = Console.ReadLine()?.Trim();

            if (!staffByPosition.ContainsKey(position))
            {
                Console.WriteLine("Такой должности нет.");
                return;
            }

            Console.Write("Введите полное имя сотрудника для удаления: ");
            string fullName = Console.ReadLine()?.Trim();

            if (staffByPosition[position].Remove(fullName))
            {
                Console.WriteLine($"Сотрудник {fullName} удалён с должности {position}.");

                if (staffByPosition[position].Count == 0)
                {
                    staffByPosition.Remove(position);
                    positions.Remove(position);
                    Console.WriteLine($"Должность {position} удалена, так как больше нет сотрудников.");
                }
            }
            else
            {
                Console.WriteLine("Такого сотрудника нет на этой должности.");
            }
        }

        public static void ShowAll(
            Dictionary<string, List<string>> staffByPosition,
            HashSet<string> positions)
        {
            if (staffByPosition.Count == 0)
            {
                Console.WriteLine("Нет данных о сотрудниках.");
                return;
            }

            Console.WriteLine("\nПолная информация:");

            foreach (string position in positions)
            {
                Console.WriteLine($"Должность: {position}");

                foreach (string employee in staffByPosition[position])
                {
                    Console.WriteLine($" - {employee}");
                }
            }
        }
    }
}
