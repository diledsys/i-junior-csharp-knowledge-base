using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter
{
    internal class Program
    {
        // ---------- Exchange rates (fixed in code) ----------
        // Naming makes direction explicit: From -> To
        // You can change the numbers to whatever you need.
        private const decimal rateUsdToEur = 0.90m;   // 1 USD  -> 0.90 EUR (uses multiplication)
        private const decimal rateEurToUsd = 1.11m;   // 1 EUR  -> 1.11 USD (uses multiplication)
        private const decimal rateUsdToIls = 3.70m;   // 1 USD  -> 3.70 ILS (uses multiplication)
        private const decimal rateIlsToUsd = 0.27m;   // 1 ILS  -> 0.27 USD (uses multiplication)
        private const decimal rateEurToIls = 4.00m;   // 1 EUR  -> 4.00 ILS (uses multiplication)
        private const decimal rateIlsToEur = 0.25m;   // 1 ILS  -> 0.25 EUR (uses multiplication)

        // If you NEED some cases to be division-only: set paired rates consistent with division and use "/" in those cases.
        // В этой реализации все 6 кейсов используют умножение (одно действие арифметики в кейсе).

        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Simple Multi-Currency Wallet ===");
            Console.WriteLine("Currencies: USD, EUR, ILS");
            Console.WriteLine("All balances must be non-negative.\n");

            decimal usd = PromptNonNegative("Enter initial USD balance: ");
            decimal eur = PromptNonNegative("Enter initial EUR balance: ");
            decimal ils = PromptNonNegative("Enter initial ILS balance: ");

            while (true)
            {
                ShowBalances(usd, eur, ils);
                ShowMenu();

                Console.Write("Choose an option: ");
                var choice = Console.ReadLine()?.Trim();

                if (choice == "0")
                {
                    Console.WriteLine("Bye! Program finished by user choice.");
                    break;
                }

                switch (choice)
                {
                    case "1": // USD -> EUR
                        {
                            var amount = PromptAmount("USD", usd);
                            if (!HasEnough(usd, amount)) break;
                            var credited = amount * rateUsdToEur; // single operation: multiplication
                            usd -= amount;
                            eur += credited;
                            PrintConversion("USD", "EUR", amount, credited);
                            break;
                        }
                    case "2": // EUR -> USD
                        {
                            var amount = PromptAmount("EUR", eur);
                            if (!HasEnough(eur, amount)) break;
                            var credited = amount * rateEurToUsd; // single operation: multiplication
                            eur -= amount;
                            usd += credited;
                            PrintConversion("EUR", "USD", amount, credited);
                            break;
                        }
                    case "3": // USD -> ILS
                        {
                            var amount = PromptAmount("USD", usd);
                            if (!HasEnough(usd, amount)) break;
                            var credited = amount * rateUsdToIls; // single operation: multiplication
                            usd -= amount;
                            ils += credited;
                            PrintConversion("USD", "ILS", amount, credited);
                            break;
                        }
                    case "4": // ILS -> USD
                        {
                            var amount = PromptAmount("ILS", ils);
                            if (!HasEnough(ils, amount)) break;
                            var credited = amount * rateIlsToUsd; // single operation: multiplication
                            ils -= amount;
                            usd += credited;
                            PrintConversion("ILS", "USD", amount, credited);
                            break;
                        }
                    case "5": // EUR -> ILS
                        {
                            var amount = PromptAmount("EUR", eur);
                            if (!HasEnough(eur, amount)) break;
                            var credited = amount * rateEurToIls; // single operation: multiplication
                            eur -= amount;
                            ils += credited;
                            PrintConversion("EUR", "ILS", amount, credited);
                            break;
                        }
                    case "6": // ILS -> EUR
                        {
                            var amount = PromptAmount("ILS", ils);
                            if (!HasEnough(ils, amount)) break;
                            var credited = amount * rateIlsToEur; // single operation: multiplication
                            ils -= amount;
                            eur += credited;
                            PrintConversion("ILS", "EUR", amount, credited);
                            break;
                        }
                    default:
                        Console.WriteLine("Unknown option. Please choose 0..6.\n");
                        break;
                }
            }
        }

        // -------- Helpers --------

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
            Console.WriteLine($"\nBalances: USD = {usd:F2}, EUR = {eur:F2}, ILS = {ils:F2}");
        }

         static decimal PromptNonNegative(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (TryParseDecimal(Console.ReadLine(), out var value) && value >= 0)
                    return value;

                Console.WriteLine("Invalid number. Use digits and optional decimal separator. Value must be non-negative.");
            }
        }

         static decimal PromptAmount(string fromCurrency, decimal currentBalance)
        {
            while (true)
            {
                Console.Write($"Enter amount in {fromCurrency} (<= balance {currentBalance:F2}): ");
                if (TryParseDecimal(Console.ReadLine(), out var value) && value >= 0)
                    return value;

                Console.WriteLine("Invalid amount. Try again.");
            }
        }

        static bool HasEnough(decimal balance, decimal amount)
        {
            if (amount > balance)
            {
                Console.WriteLine("Not enough balance. Operation cancelled.\n");
                return false;
            }
            return true;
        }

         static void PrintConversion(string from, string to, decimal debited, decimal credited)
        {
            Console.WriteLine($"Converted {debited:F2} {from} -> {credited:F2} {to}\n");
        }

        // Accepts either comma or dot, tries common cultures
         static bool TryParseDecimal(string input, out decimal value)
        {
            input = (input ?? "").Trim();
            // normalize: allow both "," and "."
            var normalized = input.Replace(',', '.');
            if (decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                return true;

            // fallback to current culture
            return decimal.TryParse(input, NumberStyles.Number, CultureInfo.CurrentCulture, out value);
        }
    }
}
