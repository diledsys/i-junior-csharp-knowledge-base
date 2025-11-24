namespace Cycle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string message;
            int countMessages;

            Console.Write("оставте тут своё сообшение: ");
            message = Console.ReadLine();

            Console.Write("укажите количество повторов для вашего сообшения : ");
            countMessages = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < countMessages; i++)
            {
                Console.WriteLine(message);
            }
        }
    }
}
