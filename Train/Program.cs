using System;
using System.Collections.Generic;


namespace Train
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                Dispatcher dispatcher = new Dispatcher();
                Menu menu = new Menu(dispatcher);

                bool isRunning = true;

                while (isRunning)
                {
                    menu.Show();

                    Console.Write("\nВыберите пункт меню: ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int choiceNum) == false ||
                        Enum.IsDefined(typeof(Option), choiceNum) == false)
                    {
                        Console.WriteLine("Ошибка: неверный пункт меню!");
                        continue;
                    }

                    Option choice = (Option)choiceNum;
                    isRunning = menu.HandleChoice(choice);
                }
            }
        }
    }

    internal enum Option
    {
        CreateTrain = 1,
        Exit
    }
    internal class Menu
    {
        private Dispatcher _dispatcher;

        public Menu(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void Show()
        {
            Console.WriteLine("\n=== КОНФИГУРАТОР ПОЕЗДОВ ===");
            _dispatcher.ShowAllTrainsShort();

            Console.WriteLine($"\n{(int)Option.CreateTrain}. Создать новый поезд");
            Console.WriteLine($"{(int)Option.Exit}. Выход");
        }

        public bool HandleChoice(Option option)
        {
            Console.Clear();

            switch (option)
            {
                case Option.CreateTrain:
                    CreateTrain();
                    break;

                case Option.Exit:
                    ExitProgram();
                    return false;
            }

            return true;
        }

        private void CreateTrain()
        {
            _dispatcher.CreateTrain();
        }

        private void ExitProgram()
        {
            Console.WriteLine("Завершение работы программы...");
        }
    }

    internal class Dispatcher
    {
        private List<Train> _trains;
        private Random _random;

        public Dispatcher()
        {
            _trains = new List<Train>();
            _random = new Random();
        }

        public void CreateTrain()
        {
            Console.WriteLine("=== СОЗДАНИЕ ПОЕЗДА ===");

            string route = CreateRoute();
            int passengersCount = SellTickets(route);
            Train train = FormTrain(route, passengersCount);

            _trains.Add(train);

            Console.WriteLine("\nПоезд успешно создан!");
            train.ShowFullInfo();
        }

        private string CreateRoute()
        {
            string from = Input("Введите начальную станцию: ");
            string to = Input("Введите конечную станцию: ");
            string route = $"{from} --> {to}";
            Console.WriteLine($"Создано направление: {route}");
            return route;
        }

        private String Input(String prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        private int SellTickets(string route)
        {
            int minPassengers = 50;
            int maxPassengers = 300;

            int passengersCount = _random.Next(minPassengers, maxPassengers + 1);

            Console.WriteLine($"На направление {route} куплено {passengersCount} билетов.");

            return passengersCount;
        }

        private Train FormTrain(string route, int passengersCount)
        {
            int minWagonCapacity = 40;
            int maxWagonCapacity = 60;

            Console.WriteLine("\nФормируем состав поезда...");

            Train train = new Train(route);

            int totalSeats = 0;

            while (totalSeats < passengersCount)
            {
                int capacity = _random.Next(minWagonCapacity, maxWagonCapacity + 1);
                train.AddWagon(new Wagon(capacity));
                totalSeats += capacity;
            }

            train.LoadPassengers(passengersCount);

            return train;
        }

        public void ShowAllTrainsShort()
        {
            if (_trains.Count == 0)
            {
                Console.WriteLine("Пока нет созданных поездов.");
                return;
            }

            Console.WriteLine("Список созданных поездов:");

            for (int i = 0; i < _trains.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_trains[i].GetShortInfo()}");
            }
        }
    }

    internal class Train
    {
        private List<Wagon> _wagons;
        private int _passengersCount;

        public Train(string route)
        {
            Route = route;
            _wagons = new List<Wagon>();
        }

        public string Route { get; private set; }

        public void AddWagon(Wagon wagon)
        {
            _wagons.Add(wagon);
        }

        public void LoadPassengers(int passengersCount)
        {
            _passengersCount = passengersCount;
        }

        public void ShowFullInfo()
        {
            Console.WriteLine($"\n=== Поезд {Route} ===");
            Console.WriteLine($"Пассажиров: {_passengersCount}");
            Console.WriteLine($"Количество вагонов: {_wagons.Count}");

            for (int i = 0; i < _wagons.Count; i++)
            {
                Console.WriteLine($"  Вагон {i + 1}: вместимость {_wagons[i].Capacity}");
            }
        }

        public string GetShortInfo()
        {
            return $"{Route} — пассажиров: {_passengersCount}, вагонов: {_wagons.Count}";
        }
    }

    internal class Wagon
    {
        public int Capacity { get; private set; }

        public Wagon(int capacity)
        {
            Capacity = capacity;
        }
    }
}
