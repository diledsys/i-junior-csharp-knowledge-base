using System;
using System.Collections.Generic;

namespace PlayerDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerDB db = new PlayerDB();

            while (true)
            {
                Console.WriteLine("\n===== Меню базы данных игроков =====");
                Console.WriteLine("1. Добавить игрока");
                Console.WriteLine("2. Забанить игрока по ID");
                Console.WriteLine("3. Разбанить игрока по ID");
                Console.WriteLine("4. Удалить игрока по ID");
                Console.WriteLine("5. Показать всех игроков");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите опцию: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        try
                        {
                            Console.Write("Введите ID: ");
                            int id = int.Parse(Console.ReadLine());

                            Console.Write("Введите ник: ");
                            string nickname = Console.ReadLine();

                            Console.Write("Введите уровень: ");
                            int level = int.Parse(Console.ReadLine());

                            db.AddPlayer(id, nickname, level);
                        }
                        catch
                        {
                            Console.WriteLine("[!] Неверный ввод.");
                        }
                        break;

                    case "2":
                        Console.Write("Введите ID игрока для бана: ");
                        if (int.TryParse(Console.ReadLine(), out int banId))
                            db.BanPlayer(banId);
                        else
                            Console.WriteLine("[!] Неверный ID.");
                        break;

                    case "3":
                        Console.Write("Введите ID игрока для разбана: ");
                        if (int.TryParse(Console.ReadLine(), out int unbanId))
                            db.UnbanPlayer(unbanId);
                        else
                            Console.WriteLine("[!] Неверный ID.");
                        break;

                    case "4":
                        Console.Write("Введите ID игрока для удаления: ");
                        if (int.TryParse(Console.ReadLine(), out int delId))
                            db.DeletePlayer(delId);
                        else
                            Console.WriteLine("[!] Неверный ID.");
                        break;

                    case "5":
                        db.ShowAllPlayers();
                        break;

                    case "0":
                        Console.WriteLine("Выход из программы...");
                        return;

                    default:
                        Console.WriteLine("[!] Неверная опция.");
                        break;
                }
            }
        }

        class Player
        {
            public int Id { get; }
            public string Nickname { get; set; }
            public int Level { get; set; }
            public bool IsBanned { get; set; }

            public Player(int id, string nickname, int level)
            {
                Id = id;
                Nickname = nickname;
                Level = level;
                IsBanned = false;
            }

            public override string ToString()
            {
                string status = IsBanned ? "Banned" : "Active";
                return $"ID: {Id} | Nickname: {Nickname} | Level: {Level} | Status: {status}";
            }
        }

        class PlayerDB
        {
            private Dictionary<int, Player> players = new Dictionary<int, Player>();

            public void AddPlayer(int id, string nickname, int level)
            {
                if (players.ContainsKey(id))
                {
                    Console.WriteLine("[!] Игрок с таким ID уже существует.");
                    return;
                }

                players[id] = new Player(id, nickname, level);
                //players.Add(id, new Player(id, nickname,level);
                Console.WriteLine("[+] Игрок успешно добавлен.");
            }

            public void BanPlayer(int id)
            {
                if (players.TryGetValue(id, out Player player))
                {
                    player.IsBanned = true;
                    Console.WriteLine("[!] Игрок забанен.");
                }
                else
                {
                    Console.WriteLine("[!] Игрок не найден.");
                }
            }

            public void UnbanPlayer(int id)
            {
                if (players.TryGetValue(id, out Player player))
                {
                    player.IsBanned = false;
                    Console.WriteLine("[+] Игрок разбанен.");
                }
                else
                {
                    Console.WriteLine("[!] Игрок не найден.");
                }
            }

            public void DeletePlayer(int id)
            {
                if (players.Remove(id))
                {
                    Console.WriteLine("[-] Игрок удалён.");
                }
                else
                {
                    Console.WriteLine("[!] Игрок не найден.");
                }
            }

            public void ShowAllPlayers()
            {
                if (players.Count == 0)
                {
                    Console.WriteLine("[i] В базе данных нет игроков.");
                    return;
                }

                foreach (var player in players.Values)
                {
                    Console.WriteLine(player);
                }
            }
        }
    }
}