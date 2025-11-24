using System;
using TestMoveUnity;

class Program
{
    static void Main()
    {
        ObjectManager manager = new ObjectManager();

        manager.AddObject(new MyObject { Id = 1, Name = "Объект 1" });
        manager.AddObject(new MyObject { Id = 2, Name = "Объект 2" });

        Console.WriteLine("Список объектов:");
        manager.PrintAll();

        Console.WriteLine("Удаляем объект с ID = 1");
        manager.RemoveObjectById(1);

        Console.WriteLine("Список после удаления:");
        manager.PrintAll();
    }
}
