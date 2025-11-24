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
            Console.WriteLine("Требование: баланс не может быть отрицательным.\nДробную часть пишем через запятую\n");

            decimal usdBalance = 0m;
            decimal eurBalance = 0m;
            decimal ilsBalance = 0m;

            bool isOk = false;

            while (isOk == false)
            {
                Console.Write("Введите начальный баланс в долларах США (USD): ");
                isOk = decimal.TryParse(Console.ReadLine(), out usdBalance) && usdBalance >= 0m;

                if (isOk == false) Console.WriteLine("Значение должно быть неотрицательным.");
            }

            isOk = false;
            while (isOk == false)
            {
                Console.Write("Введите начальный баланс в евро (EUR): ");
                isOk = decimal.TryParse(Console.ReadLine(), out eurBalance) && eurBalance >= 0m;

                if (isOk == false) Console.WriteLine("Значение должно быть неотрицательным.");
            }

            isOk = false;
            while (isOk == false)
            {
                Console.Write("Введите начальный баланс в шекелях (ILS): ");
                isOk = decimal.TryParse(Console.ReadLine(), out ilsBalance) && ilsBalance >= 0m
                    ;
                if (isOk == false) Console.WriteLine("Значение должно быть неотрицательным.");
            }

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine($"\nБалансы: USD = {usdBalance}; EUR = {eurBalance}; ILS = {ilsBalance}");

                Console.WriteLine("\n--- MENU ---");
                Console.WriteLine($"{UsdToEurOption}) USD -> EUR");
                Console.WriteLine($"{EurToUsdOption}) EUR -> USD");
                Console.WriteLine($"{UsdToIlsOption}) USD -> ILS");
                Console.WriteLine($"{IlsToUsdOption}) ILS -> USD");
                Console.WriteLine($"{EurToIlsOption}) EUR -> ILS");
                Console.WriteLine($"{IlsToEurOption}) ILS -> EUR");
                Console.WriteLine($"{ExitOption}) Выход");

                Console.Write("Выберите вариант: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case ExitOption:
                        Console.WriteLine("Программа завершена по выбору пользователя.");
                        isRunning = false;
                        break;

                    case UsdToEurOption:
                        {
                            decimal amount = ReadNonNegativeAmount("USD", usdBalance);
                            if (amount <= usdBalance)
                            {
                                decimal credited = amount * RateUsdToEur;
                                usdBalance -= amount;
                                eurBalance += credited;
                                Console.WriteLine($"Конвертировано: {amount} USD -> {credited} EUR\n");
                            }
                            else
                            {
                                Console.WriteLine("Недостаточно средств. Операция отменена.\n");
                            }
                            break;
                        }

                    case EurToUsdOption:
                        {
                            decimal amount = ReadNonNegativeAmount("EUR", eurBalance);
                            if (amount <= eurBalance)
                            {
                                decimal credited = amount * RateEurToUsd;
                                eurBalance -= amount;
                                usdBalance += credited;
                                Console.WriteLine($"Конвертировано: {amount} EUR -> {credited} USD\n");
                            }
                            else
                            {
                                Console.WriteLine("Недостаточно средств. Операция отменена.\n");
                            }
                            break;
                        }

                    case UsdToIlsOption:
                        {
                            decimal amount = ReadNonNegativeAmount("USD", usdBalance);
                            if (amount <= usdBalance)
                            {
                                decimal credited = amount * RateUsdToIls;
                                usdBalance -= amount;
                                ilsBalance += credited;
                                Console.WriteLine($"Конвертировано: {amount} USD -> {credited} ILS\n");
                            }
                            else
                            {
                                Console.WriteLine("Недостаточно средств. Операция отменена.\n");
                            }
                            break;
                        }

                    case IlsToUsdOption:
                        {
                            decimal amount = ReadNonNegativeAmount("ILS", ilsBalance);
                            if (amount <= ilsBalance)
                            {
                                decimal credited = amount * RateIlsToUsd;
                                ilsBalance -= amount;
                                usdBalance += credited;
                                Console.WriteLine($"Конвертировано: {amount} ILS -> {credited} USD\n");
                            }
                            else
                            {
                                Console.WriteLine("Недостаточно средств. Операция отменена.\n");
                            }
                            break;
                        }

                    case EurToIlsOption:
                        {
                            decimal amount = ReadNonNegativeAmount("EUR", eurBalance);
                            if (amount <= eurBalance)
                            {
                                decimal credited = amount * RateEurToIls;
                                eurBalance -= amount;
                                ilsBalance += credited;
                                Console.WriteLine($"Конвертировано: {amount} EUR -> {credited} ILS\n");
                            }
                            else
                            {
                                Console.WriteLine("Недостаточно средств. Операция отменена.\n");
                            }
                            break;
                        }

                    case IlsToEurOption:
                        {
                            decimal amount = ReadNonNegativeAmount("ILS", ilsBalance);
                            if (amount <= ilsBalance)
                            {
                                decimal credited = amount * RateIlsToEur;
                                ilsBalance -= amount;
                                eurBalance += credited;
                                Console.WriteLine($"Конвертировано: {amount} ILS -> {credited} EUR\n");
                            }
                            else
                            {
                                Console.WriteLine("Недостаточно средств. Операция отменена.\n");
                            }
                            break;
                        }

                    default:
                        Console.WriteLine("Неизвестный вариант. Выберите от 0 до 6.\n");
                        break;
                }
            }
        }

        private static decimal ReadNonNegativeAmount(string fromCurrency, decimal currentBalance)
        {
            bool isOk = false;
            decimal value = 0m;

            while (isOk == false)
            {
                Console.Write($"Введите сумму в {fromCurrency} (<- Баланс {currentBalance}): ");
                isOk = decimal.TryParse(Console.ReadLine(), out value) && value >= 0m;
                if (isOk) Console.WriteLine("Сумма должна быть неотрицательной. Попробуйте ещё раз.");
            }

            return value;
        }
    }
}
