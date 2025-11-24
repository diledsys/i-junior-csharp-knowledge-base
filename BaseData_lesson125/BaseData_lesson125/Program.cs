using System;
using System.Collections.Generic;
using System.Linq;

namespace BDPlayers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            DatabaseView controller = new DatabaseView(database);
            controller.Run();
        }
    }

    public class DatabaseView
    {
        private readonly Database _database;

        public DatabaseView(Database database)
        {
            _database = database;
        }

        public void Run()
        {
            bool isExit = false;

            while (isExit == false)
            {
                ShowMenu();
                Console.Write("Выберите пункт: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int optionNumber) &&
                    Enum.IsDefined(typeof(MenuOption), optionNumber))
                {
                    MenuOption option = (MenuOption)optionNumber;
                    isExit = HandleOption(option);
                }
                else
                {
                    Console.WriteLine("Некорректный выбор. Повторите ввод.");
                }
            }

            Console.WriteLine("Выход...");
        }
        private void ShowMenu()
        {
            Console.WriteLine("\n--- МЕНЮ ---");
            Console.WriteLine($"{(int)MenuOption.AddPlayer}. Добавить игрока");
            Console.WriteLine($"{(int)MenuOption.ShowPlayers}. Показать всех игроков");
            Console.WriteLine($"{(int)MenuOption.BanPlayer}. Забанить игрока");
            Console.WriteLine($"{(int)MenuOption.UnbanPlayer}. Разбанить игрока");
            Console.WriteLine($"{(int)MenuOption.RemovePlayer}. Удалить игрока");
            Console.WriteLine($"{(int)MenuOption.Exit}. Выход");
        }

        private bool HandleOption(MenuOption option)
        {
            switch (option)
            {
                case MenuOption.AddPlayer:
                    AddNewPlayer();
                    break;

                case MenuOption.ShowPlayers:
                    _database.ShowAll();
                    break;

                case MenuOption.BanPlayer:
                    HandlePlayerById("Введите ID игрока для бана: ", _database.BanPlayer);
                    break;

                case MenuOption.UnbanPlayer:
                    HandlePlayerById("Введите ID игрока для разбана: ", _database.UnbanPlayer);
                    break;

                case MenuOption.RemovePlayer:
                    HandlePlayerById("Введите ID игрока для удаления: ", _database.RemovePlayer);
                    break;

                case MenuOption.Exit:
                    return true;
            }

            return false;
        }

        private void AddNewPlayer()
        {
            _database.AddPlayer(ReadString("Введите ник игрока: "), ReadInt("Введите уровень: "));
        }

        private void HandlePlayerById(string prompt, Action<int> action)
        {
            int id = ReadInt(prompt);
            action(id);
        }

        private int ReadInt(string message)
        {
            int value;
            Console.Write(message);

            while (int.TryParse(Console.ReadLine(), out value) == false)
                Console.Write($"\nОшибка: введите корректное число.\n{message}");

            return value;
        }

        private string ReadString(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        private enum MenuOption
        {
            AddPlayer = 1,
            ShowPlayers,
            BanPlayer,
            UnbanPlayer,
            RemovePlayer,
            Exit
        }
    }

    public class Player
    {
        public Player(int id, string nickname, int level)
        {
            Id = id;
            Nickname = nickname;
            Level = level;
            IsBanned = false;
        }

        public int Id { get; }

        public int Level { get; }

        public string Nickname { get; }

        public bool IsBanned { get; private set; }

        public void Ban() => IsBanned = true;

        public void Unban() => IsBanned = false;

        public override string ToString()
        {
            string banStatus = IsBanned ? "ЗАБАНЕН" : "Активен";
            return $"ID: {Id} | Ник: {Nickname} | Уровень: {Level} | Статус: {banStatus}";
        }
    }

    public class Database
    {
        private List<Player> _players = new List<Player>();
        private int _nextPlayerID = 1;

        public void AddPlayer(string nickname, int level)
        {
            Player player = new Player(_nextPlayerID, nickname, level);
            _players.Add(player);
            Console.WriteLine($"Игрок добавлен: {player}");
            _nextPlayerID++;
        }

        public void BanPlayer(int id)
        {
            if (TryGetPlayerById(id, out Player player))
            {
                player.Ban();
                Console.WriteLine("Игрок забанен.");
            }
        }

        public void UnbanPlayer(int id)
        {
            if (TryGetPlayerById(id, out Player player))
            {
                player.Unban();
                Console.WriteLine("Игрок разбанен.");
            }
        }

        public void RemovePlayer(int id)
        {
            if (TryGetPlayerById(id, out Player player))
            {
                _players.Remove(player);
                Console.WriteLine("Игрок удалён.");
            }
        }

        public void ShowAll()
        {
            if (_players.Count == 0)
            {
                Console.WriteLine("Список игроков пуст.");
                return;
            }

            foreach (var player in _players)
                Console.WriteLine(player);
        }

        private bool TryGetPlayerById(int id, out Player foundPlayer)
        {
            foundPlayer = _players.FirstOrDefault(player => player.Id == id);

            if (foundPlayer == null)
            {
                Console.WriteLine("Игрок не найден.");
                return false;
            }

            return true;
        }
    }
}

