using System;

namespace PropertyWork
{
    internal class Program
    {
        static void Main()
        {
            int positionX = 15;
            int positionY = 25;
            char symbol = '@';

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;

            Player player = new Player(positionX, positionY, symbol);
            Renderer renderer = new Renderer();

            renderer.Draw(player);

            Console.ReadKey();
        }
    }

    public class Player
    {
        public Player(int positionX, int positionY, char symbol)
        {
            PositionX = positionX;
            PositionY = positionY;
            Symbol = symbol;
        }

        public int PositionX
        {
            get; private set;
        }
        public int PositionY
        {
            get; private set;
        }
        public char Symbol
        {
            get; private set;
        }

        public void MoveTo(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }
    }

    public class Renderer
    {
        public void Draw(Player player)
        {
            Console.SetCursorPosition(player.PositionX, player.PositionY);
            Console.Write(player.Symbol);
        }
    }
}
