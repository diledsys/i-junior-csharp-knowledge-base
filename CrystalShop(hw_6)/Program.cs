using System;
namespace CrystalShop
{
    class Program
    {
        static void Main()
        {
            const int PricePerCrystal = 10;

            Console.WriteLine("***-= Покупка кристаллов =-***");

            Console.Write("Введите количество золота: ");
            int gold = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Цена одного кристалла: {PricePerCrystal} золота.");
            Console.Write("Сколько кристаллов хотите купить? ");
            int toBuy = Convert.ToInt32(Console.ReadLine());

            int cost = toBuy * PricePerCrystal;
            gold -= cost;
            int crystals = toBuy;

            Console.WriteLine($"Потрачено золота: {cost}");
            Console.WriteLine($"Остаток золота:{gold}");
            Console.WriteLine($"Кристаллы: {crystals}");
        }
    }
}
