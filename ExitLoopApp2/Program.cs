namespace ExitLoopApp2
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
                    Console.WriteLine("Ваш ввод:");
                    break;
                }
                else
                {
                    Console.WriteLine("Вы вышли из программы.");
                }
            }
        }
    }
}