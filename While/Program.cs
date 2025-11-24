namespace Cycle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string ExitWord = "exit";
            string input;

            while (true)
            {
                Console.Write("Введите любое слово (для выхода введите: exit ");
                input = Console.ReadLine();

                if (input == ExitWord)
                {
                    Console.WriteLine("Вы вышли из программы.");
                    break;
                }
                else
                {
                    Console.WriteLine("продолжаем..");
                }
            }
        }
        }
    }