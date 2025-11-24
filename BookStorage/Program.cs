using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//BookStorageApp /
//│
//├── Program.cs
//├── Menu.cs
//├── Book.cs
//├── BookStorage.cs
//└── TextConstants.cs

namespace Book_Storage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BookStorage storage = new BookStorage();
            Menu menu = new Menu(storage);
            bool isRunning = true;

            while (isRunning == true)
            {
                menu.Show();
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choiceNum) == false ||
                    Enum.IsDefined(typeof(MenuOption), choiceNum) == false)
                {
                    Console.WriteLine("Выбран неверный пункт меню!");
                    continue;
                }

                MenuOption choice = (MenuOption)choiceNum;
                menu.HandleChoice(choice);

                if (choice == MenuOption.Exit)
                    isRunning = false;
            }
        }

        static class TextConstants
        {
            public const string Title = "Название";
            public const string Author = "Автор";
            public const string Year = "Год";
            public const string Genre = "Жанр";

            public const string MenuAdd = "Добавить книгу";
            public const string MenuRemove = "Удалить книгу";
            public const string MenuShowAll = "Показать все книги";
            public const string MenuSearch = "Поиск книги";
            public const string MenuExit = "Выход";
        }

        enum SearchParameter
        {
            Title = 1,
            Author,
            Year,
            Genre
        }

        enum MenuOption
        {
            AddBook = 1,
            RemoveBook,
            ShowAll,
            Search,
            Exit
        }

        static string GetParameterName(SearchParameter parameter)
        {
            switch (parameter)
            {
                case SearchParameter.Title:
                    return TextConstants.Title;

                case SearchParameter.Author:
                    return TextConstants.Author;

                case SearchParameter.Year:
                    return TextConstants.Year;

                case SearchParameter.Genre:
                    return TextConstants.Genre;

                default:
                    return "Неизвестно";
            }
        }

        class Menu
        {
            private readonly BookStorage _storage;

            public Menu(BookStorage storage)
            {
                _storage = storage;
            }

            public void Show()
            {
                Console.WriteLine("\n=====- МЕНЮ -=====");
                Console.WriteLine($"{(int)MenuOption.AddBook}. {TextConstants.MenuAdd}");
                Console.WriteLine($"{(int)MenuOption.RemoveBook}. {TextConstants.MenuRemove}");
                Console.WriteLine($"{(int)MenuOption.ShowAll}. {TextConstants.MenuShowAll}");
                Console.WriteLine($"{(int)MenuOption.Search}. {TextConstants.MenuSearch}");
                Console.WriteLine($"{(int)MenuOption.Exit}. {TextConstants.MenuExit}");
                Console.Write("Выберите пункт: ");
            }

            public void HandleChoice(MenuOption choice)
            {
                switch (choice)
                {
                    case MenuOption.AddBook:
                        AddBook();
                        break;

                    case MenuOption.RemoveBook:
                        RemoveBook();
                        break;

                    case MenuOption.ShowAll:
                        _storage.ShowAllBooks();
                        break;

                    case MenuOption.Search:
                        SearchBook();
                        break;

                    case MenuOption.Exit:
                        Console.WriteLine("\nВыбран Выход");
                        break;
                }
            }

            private void AddBook()
            {
                Console.Write($"Введите {TextConstants.Title}: ");
                string title = Console.ReadLine();

                Console.Write($"Введите {TextConstants.Author}: ");
                string author = Console.ReadLine();

                Console.Write($"Введите {TextConstants.Year}: ");
                int year = int.TryParse(Console.ReadLine(), out int y) ? y : 0;

                Console.Write($"Введите {TextConstants.Genre}: ");
                string genre = Console.ReadLine();

                _storage.AddBook(new Book(title, author, year, genre));
            }

            private void RemoveBook()
            {
                Console.Write($"Введите {TextConstants.Title} книги для удаления: ");
                string title = Console.ReadLine();
                _storage.RemoveBook(title);
            }

            private void SearchBook()
            {
                Console.WriteLine("\nВыберите параметр поиска:");
                foreach (SearchParameter option in Enum.GetValues(typeof(SearchParameter)))
                {
                    Console.WriteLine("{0}. {1}", (int)option, GetParameterName(option));
                }
                Console.Write("Ваш выбор: ");

                if (int.TryParse(Console.ReadLine(), out int paramNum) == false ||
                    Enum.IsDefined(typeof(SearchParameter), paramNum) == false)
                {
                    Console.WriteLine("Неверный параметр поиска! (Выберите из списка)");
                    return;
                }

                SearchParameter parameter = (SearchParameter)paramNum;
                string parameterName = GetParameterName(parameter);

                Console.WriteLine($"\nВаш выбор: {paramNum}. Поиск по {parameterName}");
                Console.Write($"Введите {parameterName} для поиска: ");

                string value = Console.ReadLine();
                Console.WriteLine($"\nИщем книги по {parameterName}: \"{value}\"");

                _storage.SearchBooks(parameter, value);
            }
        }

        class Book
        {
            public Book(string title, string author, int year, string genre)
            {
                Title = title;
                Author = author;
                Year = year;
                Genre = genre;
            }

            public string Title { get; }
            public string Author { get; }
            public int Year { get; }
            public string Genre { get; }

            public override string ToString()
            {
                return $"{TextConstants.Title}: {Title}, " +
                       $"{TextConstants.Author}: {Author}, " +
                       $"{TextConstants.Year}: {Year}, " +
                       $"{TextConstants.Genre}: {Genre}";
            }
        }

        class BookStorage
        {
            private readonly List<Book> _books = new List<Book>();

            public void AddBook(Book book)
            {
                _books.Add(book);
                Console.WriteLine("Книга добавлена!");
            }

            public void RemoveBook(string title)
            {
                int removed = _books.RemoveAll(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
                Console.WriteLine(removed > 0 ? " Книга удалена!" : "Книга не найдена!");
            }

            public void ShowAllBooks()
            {
                if (_books.Count == 0)
                {
                    Console.WriteLine("Хранилище пусто.");
                    return;
                }

                Console.WriteLine("\nВсе книги:");
                foreach (var book in _books)
                    Console.WriteLine(book);
            }

            public void SearchBooks(SearchParameter parameter, string value)
            {
                IEnumerable<Book> foundBooks = Enumerable.Empty<Book>();

                switch (parameter)
                {
                    case SearchParameter.Title:
                        foundBooks = _books.Where(book => book.Title.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;

                    case SearchParameter.Author:
                        foundBooks = _books.Where(book => book.Author.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;

                    case SearchParameter.Year:
                        if (int.TryParse(value, out int year))
                            foundBooks = _books.Where(book => book.Year == year);
                        break;

                    case SearchParameter.Genre:
                        foundBooks = _books.Where(book => book.Genre.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;
                }

                if (foundBooks.Any())
                {
                    Console.WriteLine("\nНайденные книги:");
                    foreach (var book in foundBooks)
                        Console.WriteLine(book);
                }
                else
                {
                    Console.WriteLine(" Книги не найдены.");
                }
            }
        }
    }
}

