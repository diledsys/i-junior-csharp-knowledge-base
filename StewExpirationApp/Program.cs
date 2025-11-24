using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StewExpirationApp;

internal class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        new StewApplication().Run();
    }
}

internal class StewApplication
{
    private readonly List<StewCan> _cans;
    private readonly Random _random = new();

    public StewApplication()
    {
        _cans = CreateRandomCans(10); // минимум 10 записей
    }

    public void Run()
    {
        var currentYear = DateTime.Now.Year;

        Console.WriteLine("=== Определение просроченной тушёнки ===");
        Console.WriteLine($"Текущий год: {currentYear}");
        Console.WriteLine();

        Console.WriteLine("Все банки тушёнки:");
        PrintCans(_cans);

        // LINQ-запрос: все просроченные
        var expired = _cans
            .Where(can => can.ExpirationYear < currentYear)
            .ToList();

        Console.WriteLine("\nПросроченные банки тушёнки:");
        PrintCans(expired);

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    private List<StewCan> CreateRandomCans(int count)
    {
        var yearBefore = 2023;
        var yearaFter = 2025;

        var shelfLifeYears = 1;
        var expiryDateYears = 3;

        var cans = new List<StewCan>();

        for (var i = 0; i < count; i++)
        {
            var name = GetRandomName();
            var productionYear = GetRandomYear(yearBefore, yearaFter);
            var shelfLife = GetRandomShelfLife(shelfLifeYears, expiryDateYears);

            cans.Add(new StewCan(name, productionYear, shelfLife));
        }

        return cans;
    }

    private StewName GetRandomName()
    {
        var values = (StewName[])Enum.GetValues(typeof(StewName));
        var index = _random.Next(values.Length);
        return values[index];
    }

    private int GetRandomYear(int fromYear, int toYearInclusive)
    {
        return _random.Next(fromYear, toYearInclusive + 1);
    }

    private int GetRandomShelfLife(int fromYears, int toYearsInclusive)
    {
        return _random.Next(fromYears, toYearsInclusive + 1);
    }

    // ---------- Вывод ----------

    private static void PrintCans(IEnumerable<StewCan> cans)
    {
        var list = cans.ToList();

        if (list.Count == 0)
        {
            Console.WriteLine("(ничего нет)");
            return;
        }

        foreach (var can in list)
            Console.WriteLine(
                $"- {can.Name} | год производства: {can.ProductionYear}, " +
                $"срок годности: {can.ShelfLifeYears} г., " +
                $"год окончания: {can.ExpirationYear}");
    }
}

// ---------- Модель ----------

internal enum StewName
{
    Burenka,
    Korovka,
    Svinina
}

internal class StewCan
{
    public StewCan(StewName name, int productionYear, int shelfLifeYears)
    {
        Name = name;
        ProductionYear = productionYear;
        ShelfLifeYears = shelfLifeYears;
    }

    public StewName Name { get; }

    public int ProductionYear { get; }

    public int ShelfLifeYears { get; }

    public int ExpirationYear => ProductionYear + ShelfLifeYears;
}
