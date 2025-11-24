using System;

namespace CurrencyConverter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const decimal RateEurToUsd = 1.11m;   // 1 EUR -> 1.11 USD
            const decimal RateUsdToIls = 3.70m;   // 1 USD -> 3.70 ILS
            const decimal RateIlsToUsd = 0.27m;   // 1 ILS -> 0.27 USD
            const decimal RateEurToIls = 4.00m;   // 1 EUR -> 4.00 ILS
            const decimal RateIlsToEur = 0.25m;   // 1 ILS -> 0.25 EUR
            const decimal RateUsdToEur = 0.90m;   // 1 USD -> 0.90 EUR

            Console.WriteLine("=== Мультивалютный кошелек ===");
            Console.WriteLine("Валюты: USD, EUR, ILS");
            Console.WriteLine("Требование: баланс не может быть отрицательным.\nДробную часть пишим через запятую\n");

            decimal usd = PromptNonNegative("Enter initial USD balance: ");
            decimal eur = PromptNonNegative("Enter initial EUR balance: ");
            decimal ils = PromptNonNegative("Enter initial ILS balance: ");

            while (true)
            {
                ShowBalances(usd, eur, ils);
                Console.Write(RateIlsToEur);
                ShowMenu();

                Console.Write("Выберите вариант: ");
                string choice = Console.ReadLine();

                if (choice == "0")
                {
                    Console.WriteLine("Программа завершена по выбору пользователя.");
                    break;
                }

                switch (choice)
                {
                    case "1": // USD -> EUR
                        {
                            decimal amount = PromptNonNegativeAmount("USD", usd);
                            if (!HasEnough(usd, amount)) break;
                            decimal credited = amount * RateUsdToEur;
                            usd -= amount;
                            eur += credited;
                            PrintConversion("USD", "EUR", amount, credited);
                            break;
                        }
                    case "2": // EUR -> USD
                        {
                            decimal amount = PromptNonNegativeAmount("EUR", eur);
                            if (!HasEnough(eur, amount)) break;
                            decimal credited = amount * RateEurToUsd;
                            eur -= amount;
                            usd += credited;
                            PrintConversion("EUR", "USD", amount, credited);
                            break;
                        }
                    case "3": // USD -> ILS
                        {
                            decimal amount = PromptNonNegativeAmount("USD", usd);
                            if (!HasEnough(usd, amount)) break;
                            decimal credited = amount * RateUsdToIls;
                            usd -= amount;
                            ils += credited;
                            PrintConversion("USD", "ILS", amount, credited);
                            break;
                        }
                    case "4": // ILS -> USD
                        {
                            decimal amount = PromptNonNegativeAmount("ILS", ils);
                            if (!HasEnough(ils, amount)) break;
                            decimal credited = amount * RateIlsToUsd;
                            ils -= amount;
                            usd += credited;
                            PrintConversion("ILS", "USD", amount, credited);
                            break;
                        }
                    case "5": // EUR -> ILS
                        {
                            decimal amount = PromptNonNegativeAmount("EUR", eur);
                            if (!HasEnough(eur, amount)) break;
                            decimal credited = amount * RateEurToIls;
                            eur -= amount;
                            ils += credited;
                            PrintConversion("EUR", "ILS", amount, credited);
                            break;
                        }
                    case "6": // ILS -> EUR
                        {
                            decimal amount = PromptNonNegativeAmount("ILS", ils);
                            if (!HasEnough(ils, amount)) break;
                            decimal credited = amount * RateIlsToEur;
                            ils -= amount;
                            eur += credited;
                            PrintConversion("ILS", "EUR", amount, credited);
                            break;
                        }
                    default:
                        Console.WriteLine("Неизвестный вариант. Выберите 0..6.\n");
                        break;
                }
            }
        }
        
        static void ShowMenu()
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1) USD -> EUR");
            Console.WriteLine("2) EUR -> USD");
            Console.WriteLine("3) USD -> ILS");
            Console.WriteLine("4) ILS -> USD");
            Console.WriteLine("5) EUR -> ILS");
            Console.WriteLine("6) ILS -> EUR");
            Console.WriteLine("0) Exit");
        }
        static void ShowBalances(decimal usd, decimal eur, decimal ils)
        {
            Console.WriteLine($"\nБалансы: USD = {usd}; EUR = {eur}; ILS = {ils}");
        }

        static decimal PromptNonNegative(string message)
        {
            while (true)
            {
                Console.Write(message);
                decimal value = Convert.ToDecimal(Console.ReadLine());
                if (value < 0)
                {
                    Console.WriteLine("Значение должно быть неотрицательным. Попробуйте ещё раз.");
                    continue;
                }
                return value;
            }
        }

        static decimal PromptNonNegativeAmount(string fromCurrency, decimal currentBalance)
        {
            while (true)
            {
                Console.Write($"Enter amount in {fromCurrency} (<= balance {currentBalance:F2}): ");
                decimal value = Convert.ToDecimal(Console.ReadLine());
                if (value < 0)
                {
                    Console.WriteLine("Сумма должна быть неотрицательной. Попробуйте ещё раз..");
                    continue;
                }
                return value;
            }
        }

        static bool HasEnough(decimal balance, decimal amount)
        {
            if (amount > balance)
            {
                Console.WriteLine("Недостаточно средств. Операция отменена..\n");
                return false;
            }
            return true;
        }

        static void PrintConversion(string from, string to, decimal debited, decimal credited)
        {
            Console.WriteLine($"конверт {debited} {from} -> {credited} {to}\n");
        }
    }
}
