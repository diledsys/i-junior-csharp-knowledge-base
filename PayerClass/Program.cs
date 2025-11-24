using System;

namespace PayerClass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("John", "Doe", 100);

            player.DisplayInfo();

        }
    }

    class Player
    {
        private string firstName;
        private string lastName;
        private int health;

        public Player(string firstName, string lastName, int health)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.health = health;
        }


        public void DisplayInfo()
        {
            Console.WriteLine($"Player: {firstName} {lastName}, Health: {health}");
        }
    }
}
