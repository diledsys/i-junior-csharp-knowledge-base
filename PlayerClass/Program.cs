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
        private string _firstName;
        private string _lastName;
        private int _health;

        public Player(string firstName, string lastName, int health)
        {
            this._firstName = firstName;
            this._lastName = lastName;
            this._health = health;
        }


        public void DisplayInfo()
        {
            Console.WriteLine($"Player: {_firstName} {_lastName}, Health: {_health}");
        }
    }
}
