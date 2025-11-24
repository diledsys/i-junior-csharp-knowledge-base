namespace ExitLoopApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input;

            Console.WriteLine("Введите любое слово (для выхода введите: exit):");

            do
            {
                Console.Write("Ваш ввод: ");
                input = Console.ReadLine();

            } while (input != "exit");

            Console.WriteLine("Вы вышли из программы.");
        }
    }
}
