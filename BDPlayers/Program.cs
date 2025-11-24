using System;
using System.Collections.Generic;
using System.Linq;

namespace BDPlayers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerDatabase database = new PlayerDatabase();

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
                    isExit = Selector(option, database);
                }
                else
                {
                    Console.WriteLine("Некорректный выбор. Повторите ввод.");
                }
            }

            Console.WriteLine("Выход...");
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n--- МЕНЮ ---");
            Console.WriteLine($"{(int)MenuOption.AddPlayer}. Добавить игрока");
            Console.WriteLine($"{(int)MenuOption.ShowPlayers}. Показать всех игроков");
            Console.WriteLine($"{(int)MenuOption.BanPlayer}. Забанить игрока");
            Console.WriteLine($"{(int)MenuOption.UnbanPlayer}. Разбанить игрока");
            Console.WriteLine($"{(int)MenuOption.RemovePlayer}. Удалить игрока");
            Console.WriteLine($"{(int)MenuOption.Exit}. Выход");
        }

        static bool Selector(MenuOption option, PlayerDatabase database)
        {
            switch (option)
            {
                case MenuOption.AddPlayer:
                    string nickname = ReadString("Введите ник игрока: ");
                    int level = ReadInt("Введите уровень: ");
                    database.AddPlayer(nickname, level);
                    break;

                case MenuOption.ShowPlayers:
                    database.ShowAll();
                    break;

                case MenuOption.BanPlayer:
                    int banId = ReadInt("Введите ID игрока для бана: ");
                    database.BanPlayer(banId);
                    break;

                case MenuOption.UnbanPlayer:
                    int unbanId = ReadInt("Введите ID игрока для разбана: ");
                    database.UnbanPlayer(unbanId);
                    break;

                case MenuOption.RemovePlayer:
                    int removeId = ReadInt("Введите ID игрока для удаления: ");
                    database.RemovePlayer(removeId);
                    break;

                case MenuOption.Exit:
                    return true;
            }

            return false;
        }

        static int ReadInt(string message)
        {
            int value;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out value))
                    return value;

                Console.WriteLine("Ошибка: введите корректное число.");
            }
        }

        static string ReadString(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }

    public enum MenuOption
    {
        AddPlayer = 1,
        ShowPlayers,
        BanPlayer,
        UnbanPlayer,
        RemovePlayer,
        Exit
    }

    public class Player
    {
        public int Id
        {
            get; set;
        }
        public string Nickname
        {
            get; set;
        }
        public int Level {
            get; set;
        }
        public bool IsBanned
        {
            get; private set;
        }



        public Player(int id, string nickname, int level)
        {
            Id = id;
            Nickname = nickname;
            Level = level;
            IsBanned = false;
        }

        public void Ban() => IsBanned = true;
        public void Unban() => IsBanned = false;

        public override string ToString()
        {
            string banStatus = IsBanned ? "ЗАБАНЕН" : "Активен";
            return $"ID: {Id} | Ник: {Nickname} | Уровень: {Level} | Статус: {banStatus}";
        }
    }

    public class PlayerDatabase
    {
        private List<Player> _players = new List<Player>();
        private int _nextId = 1;

        public void AddPlayer(string nickname, int level)
        {
            Player player = new Player(_nextId, nickname, level);
            _players.Add(player);
            Console.WriteLine($"Игрок добавлен: {player}");
            _nextId++;
        }

        public void BanPlayer(int id)
        {
            Player player = FindPlayerById(id);
            if (player != null)
            {
                player.Ban();
                Console.WriteLine("Игрок забанен.");
            }
        }

        public void UnbanPlayer(int id)
        {
            Player player = FindPlayerById(id);
            if (player != null)
            {
                player.Unban();
                Console.WriteLine("Игрок разбанен.");
            }
        }

        public void RemovePlayer(int id)
        {
            Player player = FindPlayerById(id);
            if (player != null)
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

        private Player FindPlayerById(int id)
        {
            Player player = _players.FirstOrDefault(p => p.Id == id);
            if (player == null)
                Console.WriteLine("Игрок не найден.");
            return player;
        }
    }
}
