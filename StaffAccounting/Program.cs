using System;
using System.Collections.Generic;

namespace StaffAccounting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string AddCommand = "add";
            const string RemoveCommand = "remove";
            const string ShowCommand = "show";
            const string ExitCommand = "exit";

            var employeesByPosition = new Dictionary<string, List<string>>();

            Console.WriteLine($"Кадровый учет. Команды: {AddCommand}, {RemoveCommand}, {ShowCommand}, {ExitCommand}");

            bool isRunning = true;

            while (isRunning)
            {
                Console.Write("\nВведите команду: ");
                string command = Console.ReadLine()?.Trim().ToLower();

                switch (command)
                {
                    case AddCommand:
                        AddEmployee(employeesByPosition);
                        break;

                    case RemoveCommand:
                        RemoveEmployee(employeesByPosition);
                        break;

                    case ShowCommand:
                        ShowAll(employeesByPosition);
                        break;

                    case ExitCommand:
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine($"Неизвестная команда: {command}");
                        break;
                }
            }
        }

        private static void AddEmployee(Dictionary<string, List<string>> staffByPosition)
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

            if (staffByPosition.ContainsKey(position) == false)
            {
                staffByPosition[position] = new List<string>();
            }

            staffByPosition[position].Add(fullName);
            Console.WriteLine($"Сотрудник {fullName} добавлен на должность {position}.");
        }

        private static void RemoveEmployee(Dictionary<string, List<string>> staffByPosition)
        {
            Console.Write("Введите должность: ");
            string position = Console.ReadLine()?.Trim();

            if (staffByPosition.ContainsKey(position) == false)
            {
                Console.WriteLine($"Такой должности нет: {position}");
                return;
            }

            Console.Write("Введите полное имя сотрудника для удаления: ");
            string fullName = Console.ReadLine()?.Trim();

            bool isRemoved = staffByPosition[position].Remove(fullName);

            if (isRemoved)
            {
                Console.WriteLine($"Сотрудник {fullName} удалён с должности {position}.");

                if (staffByPosition[position].Count == 0)
                {
                    staffByPosition.Remove(position);
                    Console.WriteLine($"Должность {position} удалена, так как больше нет сотрудников.");
                }
            }
            else
            {
                Console.WriteLine($"{fullName} — такого сотрудника нет на этой должности.");
            }
        }

        private static void ShowAll(Dictionary<string, List<string>> staffByPosition)
        {
            if (staffByPosition.Count == 0)
            {
                Console.WriteLine("Нет данных о сотрудниках.");
                return;
            }

            Console.WriteLine("\nПолная информация:");

            foreach (var entry in staffByPosition)
            {
                Console.WriteLine($"Должность: {entry.Key}");

                foreach (string employee in entry.Value)
                {
                    Console.WriteLine($" - {employee}");
                }
            }
        }
    }
}
