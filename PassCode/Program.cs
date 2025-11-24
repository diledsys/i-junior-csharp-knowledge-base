using System;

class Program
{
    static void Main()
    {
        string secretPassword = "qwerty123";
        int maxAttempts = 3;

        int attemptsLeft = maxAttempts;

        for (int i = 0; i < maxAttempts; i++)
        {
            Console.Write("Введите пароль: ");
            string inputPasscode = Console.ReadLine();

            if (inputPasscode == secretPassword)
            {
                Console.WriteLine("\nДоступ разрешён.");
                Console.WriteLine("Секретное сообщение: Не забудь сделать DZ");
                break;
            }
            else if (i < maxAttempts)
            {
                attemptsLeft--;
                Console.WriteLine($"Неверный пароль. Осталось попыток: {attemptsLeft}\n");
            }
            else
            {
                Console.WriteLine("\nДоступ запрещён. Количество попыток исчерпано.");
            }
        }
    }
}
