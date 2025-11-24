using System;

namespace CurrencyConverter_2
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            const string ExitOption = "0";
            const string UsdToEurOption = "1";
            const string EurToUsdOption = "2";
            const string UsdToIlsOption = "3";
            const string IlsToUsdOption = "4";
            const string EurToIlsOption = "5";
            const string IlsToEurOption = "6";

            const decimal RateEurToUsd = 1.11m;
            const decimal RateUsdToIls = 3.70m;
            const decimal RateIlsToUsd = 0.27m;
            const decimal RateEurToIls = 4.00m;
            const decimal RateIlsToEur = 0.25m;
            const decimal RateUsdToEur = 0.90m;

            Console.WriteLine("=== Мультивалютный кошелек ===");
            Console.WriteLine("Валюты: USD, EUR, ILS");
            Console.WriteLine("Требование: баланс не может быть отрицательным.\nДробную часть пишим через запятую\n");

            decimal usd = PromptNonNegative("Введите начальный баланс в долларах США: ");
            decimal eur = PromptNonNegative("Введите начальный баланс в евро: ");
            decimal ils = PromptNonNegative("Введите начальный баланс ILS: ");

            bool isRunning = true;

            while (isRunning)
            {
                ShowBalances(usd, eur, ils);

                ShowMenu();

                Console.Write("Выберите вариант: ");
                string choice = Console.ReadLine();

                if (choice == ExitOption)
                {
                    Console.WriteLine("Программа завершена по выбору пользователя.");
                    isRunning = false;

                    continue;
                }

                switch (choice)
                {
                    case UsdToEurOption:
                        {
                            decimal amount = PromptNonNegativeAmount("USD", usd);

                            if (HasEnough(usd, amount) == false)
                                break;

                            decimal credited = amount * RateUsdToEur;
                            usd -= amount;
                            eur += credited;

                            PrintConversion("USD", "EUR", amount, credited);
                            break;
                        }

                    case EurToUsdOption:
                        {
                            decimal amount = PromptNonNegativeAmount("EUR", eur);

                            if (HasEnough(eur, amount) == false)
                                break;

                            decimal credited = amount * RateEurToUsd;
                            eur -= amount;
                            usd += credited;

                            PrintConversion("EUR", "USD", amount, credited);
                            break;
                        }

                    case UsdToIlsOption:
                        {
                            decimal amount = PromptNonNegativeAmount("USD", usd);

                            if (HasEnough(usd, amount) == false)
                                break;

                            decimal credited = amount * RateUsdToIls;
                            usd -= amount;
                            ils += credited;

                            PrintConversion("USD", "ILS", amount, credited);
                            break;
                        }

                    case IlsToUsdOption:
                        {
                            decimal amount = PromptNonNegativeAmount("ILS", ils);

                            if (HasEnough(ils, amount) == false)
                                break;

                            decimal credited = amount * RateIlsToUsd;
                            ils -= amount;
                            usd += credited;

                            PrintConversion("ILS", "USD", amount, credited);
                            break;
                        }

                    case EurToIlsOption:
                        {
                            decimal amount = PromptNonNegativeAmount("EUR", eur);

                            if (HasEnough(eur, amount) == false)
                                break;

                            decimal credited = amount * RateEurToIls;
                            eur -= amount;
                            ils += credited;

                            PrintConversion("EUR", "ILS", amount, credited);
                            break;
                        }

                    case IlsToEurOption:
                        {
                            decimal amount = PromptNonNegativeAmount("ILS", ils);

                            if (HasEnough(ils, amount) == false)
                                break;

                            decimal credited = amount * RateIlsToEur;
                            ils -= amount;
                            eur += credited;

                            PrintConversion("ILS", "EUR", amount, credited);
                            break;
                        }

                    default:

                        Console.WriteLine("Неизвестный вариант. Выберите от 0 до 6.\n");
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
            Console.WriteLine("0) Выход");
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
                Console.Write($"Введите сумму в {fromCurrency} (<- Баланс {currentBalance}): ");
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