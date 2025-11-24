using System;

class Program
{
    static void Main()
    {
        const string MenuAdd = "1";
        const string MenuPrint = "2";
        const string MenuDelete = "3";
        const string MenuSearch = "4";
        const string MenuExit = "5";

        string[] fullNames = new string[0];
        string[] positions = new string[0];

        bool isMenuExit = false;

        while (isMenuExit == false)
        {
            Console.WriteLine("\nМЕНЮ:");
            Console.WriteLine($"{MenuAdd} - Добавить досье");
            Console.WriteLine($"{MenuPrint} - Показать все досье");
            Console.WriteLine($"{MenuDelete} - Удалить досье");
            Console.WriteLine($"{MenuSearch} - Поиск по фамилии");
            Console.WriteLine($"{MenuExit} - Выход");
            Console.Write("Введите пункт меню: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case MenuAdd:
                    AddRecord(ref fullNames, ref positions);
                    break;

                case MenuPrint:
                    PrintRecords(fullNames, positions);
                    break;

                case MenuDelete:
                    DeleteRecord(ref fullNames, ref positions);
                    break;

                case MenuSearch:
                    SearchBySurname(fullNames, positions);
                    break;

                case MenuExit:
                    isMenuExit = true;
                    break;

                default:
                    Console.WriteLine("Неверный выбран пункт меню!");
                    break;
            }
        }
    }

    static void AddRecord(ref string[] fullNames, ref string[] positions)
    {
        Console.Write("Введите ФИО: ");
        string fullNameInput = Console.ReadLine();
        Console.Write("Введите должность: ");
        string inputPosition = Console.ReadLine();

        fullNames = AppendElement(fullNames, fullNameInput);
        positions = AppendElement(positions, inputPosition);

        Console.WriteLine("Досье добавлено.");
    }

    static void PrintRecords(string[] fullNames, string[] positions)
    {
        if (fullNames.Length == 0)
        {
            Console.WriteLine("Список досье пуст.");
        }
        else
        {
            for (int i = 0; i < fullNames.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {fullNames[i]} - {positions[i]}");
            }
        }
    }

    static void DeleteRecord(ref string[] fullNames, ref string[] positions)
    {
        Console.Write("Введите номер досье для удаления: ");
        string inputRecordNumber = Console.ReadLine();
        int indexToRemove;

        if (int.TryParse(inputRecordNumber, out indexToRemove) == false || indexToRemove < 1 || indexToRemove > fullNames.Length)
        {
            Console.WriteLine("Неверный номер.");
            return;
        }

        string nameToDelete = fullNames[indexToRemove - 1];

        indexToRemove--;

        fullNames = RemoveElementAt(fullNames, indexToRemove);
        positions = RemoveElementAt(positions, indexToRemove);

        Console.WriteLine($"Досье номер {inputRecordNumber} ({nameToDelete}) удалено.");
    }

    static void SearchBySurname(string[] fullNames, string[] positions)
    {
        Console.Write("Введите фамилию для поиска: ");
        string searchSurname = Console.ReadLine();

        bool isFound = false;
        for (int i = 0; i < fullNames.Length; i++)
        {
            string[] parts = fullNames[i].Split(' ');
            if (parts.Length > 0 && parts[0].Equals(searchSurname, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"{i + 1}. {fullNames[i]} - {positions[i]}");
                isFound = true;
            }
        }

        if (isFound == false)
        {
            Console.WriteLine("Ничего не найдено.");
        }
    }

    static string[] AppendElement(string[] array, string newElement)
    {
        string[] newArray = new string[array.Length + 1];

        for (int i = 0; i < array.Length; i++)
        {
            newArray[i] = array[i];
        }

        newArray[array.Length] = newElement;
        return newArray;
    }

    static string[] RemoveElementAt(string[] array, int indexToRemove)
    {
        string[] newArray = new string[array.Length - 1];

        for (int i = 0; i < indexToRemove; i++)
        {
            newArray[i] = array[i];
        }

        for (int i = indexToRemove + 1; i < array.Length; i++)
        {
            newArray[i - 1] = array[i];
        }

        return newArray;
    }
}
