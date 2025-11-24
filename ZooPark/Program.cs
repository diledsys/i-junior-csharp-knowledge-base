#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ZooConsole;

internal static class Program
{
    private static void Main()
    {
        var zoo = new Zoo();
        zoo.Run();
    }

    internal enum Gender
    {
        Male,
        Female
    }

    internal class Animal
    {
        public Animal(string speciesKey, string speciesName, string nickname, Gender gender, string sound)
        {
            SpeciesKey = speciesKey;
            SpeciesName = speciesName;
            Nickname = nickname;
            Gender = gender;
            Sound = sound;
        }

        public string SpeciesKey { get; }

        public string SpeciesName { get; }

        public string Nickname { get; }

        public Gender Gender { get; }

        public string Sound { get; }
    }

    internal class Enclosure
    {
        private readonly List<Animal> _animals = new();

        public Enclosure(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public int Id { get; }

        public string Title { get; }

        public string Description { get; }

        public IReadOnlyList<Animal> Animals =>
            _animals;

        public void AddAnimal(Animal animal)
        {
            _animals.Add(animal);
        }

        public void PrintInfo()
        {
            Console.WriteLine($"=== Вольер #{Id}: {Title} ===");
            Console.WriteLine(Description);
            Console.WriteLine();

            var total = _animals.Count;
            var males = _animals.Count(animal => animal.Gender == Gender.Male);
            var females = _animals.Count(animal => animal.Gender == Gender.Female);

            Console.WriteLine($"Всего животных: {total}");
            Console.WriteLine($"Самцы: {males}, Самки: {females}");

            var bySpecies = _animals
                .GroupBy(animal => new { animal.SpeciesKey, animal.SpeciesName })
                .OrderBy(grouping => grouping.Key.SpeciesName);

            Console.WriteLine();
            Console.WriteLine("Состав вольера по видам:");

            foreach (var grouping in bySpecies)
            {
                var any = grouping.First();
                Console.WriteLine($"{grouping.Key.SpeciesName} — {grouping.Count()} шт.  (звук: \"{any.Sound}\")");
            }

            Console.WriteLine();
            Console.WriteLine("Животные поимённо:");

            foreach (var animal in _animals.OrderBy(animal => animal.SpeciesName).ThenBy(animal => animal.Nickname))
            {
                var genderStr = animal.Gender == Gender.Male ? "самец" : "самка";
                Console.WriteLine($"   - {animal.SpeciesName} «{animal.Nickname}», {genderStr}, издаёт: {animal.Sound}");
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey(true);
        }
    }

    internal class Zoo
    {
        private readonly List<Enclosure> _enclosures = new();

        public Zoo()
        {
            Seed();
        }

        public void Run()
        {
            var running = true;
            const string quit = "Q";

            while (running)
            {
                Console.Clear();
                PrintMenu(quit);

                Console.Write("Ваш выбор: ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input.Equals(quit, StringComparison.OrdinalIgnoreCase))
                {
                    running = false;
                    break;
                }

                if (int.TryParse(input, out var id))
                {
                    var enc = GetById(id);

                    if (enc is not null)
                    {
                        Console.Clear();
                        enc.PrintInfo();
                    }
                    else
                    {
                        Console.WriteLine("Такого вольера нет. Повторите ввод.");
                        Thread.Sleep(1500);
                    }
                }
                else
                {
                    Console.WriteLine("Введите номер вольера или Q для выхода.");
                    Thread.Sleep(1200);
                }
            }

            Console.Clear();
            Console.WriteLine("До встречи в зоопарке!");
            Thread.Sleep(700);
        }

        public Enclosure? GetById(int id)
        {
            return _enclosures.FirstOrDefault(enclosure => enclosure.Id == id);
        }

        private void PrintMenu(string quitKey)
        {
            const int alignment = 2;

            Console.WriteLine("====================================");
            Console.WriteLine("            З О О П А Р К");
            Console.WriteLine("====================================");
            Console.WriteLine("Выберите вольер, к которому подойти:\n");

            foreach (var enclosure in _enclosures.OrderBy(enclosure => enclosure.Id))
            {
                var total = enclosure.Animals.Count;
                var species = enclosure.Animals.Select(animal => animal.SpeciesName).Distinct().Count();
                Console.WriteLine(
                    $"  {enclosure.Id,alignment} — {enclosure.Title}  (животных: {total}, видов: {species})");
            }

            Console.WriteLine();
            Console.WriteLine($"  {quitKey} — Выход");
            Console.WriteLine();
        }

        private void Seed()
        {
            var enclosure1 = new Enclosure(
                1,
                "Африканская савана",
                "Просторный вольер с тёплым климатом и укрытиями. Здесь живут львы и зебры."
            );
            enclosure1.AddAnimal(new Animal("lion", "Лев", "Симба", Gender.Male, "Ррр-РРР!"));
            enclosure1.AddAnimal(new Animal("lion", "Львица", "Нала", Gender.Female, "Ррр-Ррр"));
            enclosure1.AddAnimal(new Animal("zebra", "Зебра", "Зейна", Gender.Female, "И-го-го!"));
            enclosure1.AddAnimal(new Animal("zebra", "Зебра", "Зед", Gender.Male, "И-го-го!"));

            var enclosure2 = new Enclosure(
                2,
                "Дом приматов",
                "Тёплый павильон с лианами и настилами. Обитатели — гориллы и капуцины."
            );
            enclosure2.AddAnimal(new Animal("gorilla", "Горилла", "Бруно", Gender.Male, "У-у-у!"));
            enclosure2.AddAnimal(new Animal("gorilla", "Горилла", "Мила", Gender.Female, "У-у-у..."));
            enclosure2.AddAnimal(new Animal("capuchin", "Капуцин", "Чико", Gender.Male, "Киа-кия!"));
            enclosure2.AddAnimal(new Animal("capuchin", "Капуцин", "Лила", Gender.Female, "Киа!"));

            var enclosure3 = new Enclosure(
                3,
                "Птичий сад",
                "Купольный вольер с деревьями и водоёмом. Поющие и ночные птицы."
            );
            enclosure3.AddAnimal(new Animal("parrot", "Попугай", "Рико", Gender.Male, "Пррривет!"));
            enclosure3.AddAnimal(new Animal("parrot", "Попугай", "Коко", Gender.Female, "Карр!"));
            enclosure3.AddAnimal(new Animal("owl", "Сова", "Хедвиг", Gender.Female, "Ух-ту!"));
            enclosure3.AddAnimal(new Animal("owl", "Сова", "Скул", Gender.Male, "Ух-ух!"));

            var enclosure4 = new Enclosure(
                4,
                "Мир рептилий",
                "Террариумы с подогревом и бассейнами. Крокодилы и змеи."
            );
            enclosure4.AddAnimal(new Animal("croc", "Крокодил", "Данди", Gender.Male, "Щёлк!"));
            enclosure4.AddAnimal(new Animal("croc", "Крокодил", "Грета", Gender.Female, "Шшш..."));
            enclosure4.AddAnimal(new Animal("snake", "Питон", "Спираль", Gender.Female, "Сссс..."));
            enclosure4.AddAnimal(new Animal("snake", "Питон", "Кольцо", Gender.Male, "Ссс..."));

            var enclosure5 = new Enclosure(
                5,
                "Полярная зона",
                "Холодный павильон со льдом и бассейном. Белые медведи и пингвины."
            );
            enclosure5.AddAnimal(new Animal("polar_bear", "Белый медведь", "Норд", Gender.Male, "РРР-Роар!"));
            enclosure5.AddAnimal(new Animal("polar_bear", "Белая медведица", "Снежка", Gender.Female, "Ро-оар"));
            enclosure5.AddAnimal(new Animal("penguin", "Пингвин", "Тако", Gender.Male, "Кряк-кряк"));
            enclosure5.AddAnimal(new Animal("penguin", "Пингвин", "Миса", Gender.Female, "Кьи-п!"));

            _enclosures.AddRange([enclosure1, enclosure2, enclosure3, enclosure4, enclosure5]);
        }
    }
}
