using System;
using System.Collections.Generic;

namespace CustomerQueueApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int minimumRandomPurchaseAmount = 99;
            int maximumRandomPurchaseAmount = 600;
            int minimumRandomNumberClients = 1;
            int maximumRandomNumberClients = 12;
            int amount;
            int numberClients = 0;
            int totalBalance = 0;
            int customerNumber = 1;

            Random randomAmount = new Random();
            Random randomClients = new Random();
            numberClients = randomClients.Next(minimumRandomNumberClients, maximumRandomNumberClients + 1);

            Queue<int> customerPurchases = new Queue<int>();

            for (int i = 0; i < numberClients; i++)
            {
                amount = randomAmount.Next(minimumRandomPurchaseAmount, maximumRandomPurchaseAmount + 1);
                customerPurchases.Enqueue(amount);
            }

            while (customerPurchases.Count > 0)
            {
                int purchase = customerPurchases.Dequeue();

                totalBalance += purchase;

                Console.Clear();
                Console.WriteLine($"Обслуживание клиента №{customerNumber}");
                Console.WriteLine($"Сумма покупки: {purchase} монет");
                Console.WriteLine($"Общий счёт: {totalBalance} монет");

                customerNumber++;

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }

            Console.Clear();
            Console.WriteLine($"Очередь из {numberClients} человек  закончилась Все клиенты обслужены.");
            Console.WriteLine($"Итоговая сумма на счёте: {totalBalance} монет");
        }
    }
}