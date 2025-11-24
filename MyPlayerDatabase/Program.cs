using System;
using System.Collections.Generic;

namespace MyPlayerDatabase
{
    internal class Program
    {
        static void Main()
        {
            const int AddPlayer = 1;
            const int BanPlayer = 2;
            const int UnbanPlayer = 3;
            const int RemovePlayer = 4;
            const int ShowPlayers = 5;
            const int Exit = 0;

            var db = new PlayerDatabase();
            bool isRunning = true;

            while (isRunning)
            {
                int choice = ShowMenu();

                switch (choice)
                {
                    case Exit:
                        Console.WriteLine("Выход из программы...");
                        isRunning = false;
                        break;
                    case AddPlayer:
                        db.AddPlayer();
                        break;
                    case BanPlayer:
                        db.SetPlayerBanStatus(true);
                        break;
                    case UnbanPlayer:
                        db.SetPlayerBanStatus(false);
                        break;
                    case RemovePlayer:
                        db.RemovePlayer();
                        break;
                    case ShowPlayers:
                        db.ShowPlayers();
                        break;
                    default:
                        Console.WriteLine("Неверная опция.");
                        break;
                }
            }
        }

        static int ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n======= Меню базы данных игроков =======");
                Console.WriteLine("1. Добавить игрока");
                Console.WriteLine("2. Забанить игрока по ID");
                Console.WriteLine("3. Разбанить игрока по ID");
                Console.WriteLine("4. Удалить игрока по ID");
                Console.WriteLine("5. Показать всех игроков");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите опцию: ");

                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice <= 5)
                    return choice;

                Console.WriteLine("Неверный ввод. Повторите попытку.\n");
            }
        }
    }

    class Player
    {
        public long Id { get; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsBanned { get; set; }

        public Player(long id, string name, int level, bool isBanned)
        {
            Id = id;
            Name = name;
            Level = level;
            IsBanned = isBanned;
        }

        public override string ToString()
        {
            string status = IsBanned ? "ЗАБАНЕН" : "АКТИВЕН";
            return $"ID: {Id} | Ник: {Name} | Уровень: {Level} | Статус: {status}";
        }
    }

    class PlayerDatabase
    {
        private Dictionary<long, Player> _playerDb = new Dictionary<long, Player>();

        public void AddPlayer()
        {
            long id;

            while (true)
            {
                Console.Write("Введите ID: ");
                if (long.TryParse(Console.ReadLine(), out id) && !_playerDb.ContainsKey(id))
                    break;

                Console.WriteLine("ID занят или неверный формат. Повторите ввод.");
            }

            Console.Write("Введите имя игрока: ");
            string name = Console.ReadLine();

            int level;
            while (true)
            {
                Console.Write("Введите уровень игрока: ");
                if (int.TryParse(Console.ReadLine(), out level))
                    break;

                Console.WriteLine("Ошибка: введите числовое значение.");
            }

            Console.Write("Игрок забанен? (да/нет): ");
            string input = Console.ReadLine()?.Trim().ToLower();
            bool isBanned = input == "да" || input == "1" || input == "true";

            _playerDb.Add(id, new Player(id, name, level, isBanned));
            Console.WriteLine("Игрок успешно добавлен.");
        }

        public void RemovePlayer()
        {
            Console.Write("Введите ID игрока для удаления: ");
            if (long.TryParse(Console.ReadLine(), out long id) && _playerDb.Remove(id))
                Console.WriteLine("Игрок удалён.");
            else
                Console.WriteLine("Игрок с таким ID не найден.");
        }

        public void SetPlayerBanStatus(bool isBanned)
        {
            Console.Write("Введите ID игрока: ");
            if (long.TryParse(Console.ReadLine(), out long id))
            {
                if (_playerDb.TryGetValue(id, out Player player))
                {
                    player.IsBanned = isBanned;
                    Console.WriteLine(isBanned ? "Игрок ЗАБАНЕН." : "Игрок РАЗБАНЕН.");
                }
                else
                {
                    Console.WriteLine("Игрок с таким ID не найден.");
                }
            }
            else
            {
                Console.WriteLine("Неверный формат ID.");
            }
        }

        public void ShowPlayers()
        {
            if (_playerDb.Count == 0)
            {
                Console.WriteLine("База данных пуста.");
                return;
            }

            Console.WriteLine("\nСписок игроков:");
            foreach (var player in _playerDb.Values)
                Console.WriteLine(player);
        }
    }
}
