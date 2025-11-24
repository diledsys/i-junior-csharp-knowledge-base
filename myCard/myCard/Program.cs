using System;
using System.Collections.Generic;

namespace GameCard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }

    public class Game
    {
        public void Start()
        {
            bool isInputInvalid;
            int cardCount;

            Console.Write("Введите имя игрока: ");
            string playerName = Console.ReadLine();

            Player player = new Player(playerName);
            Dealer dealer = new Dealer();

            do
            {
                Console.Write("Сколько карт выдать игроку: ");
                isInputInvalid = true;

                if (int.TryParse(Console.ReadLine(), out cardCount) == false)
                {
                    Console.WriteLine("Ошибка: введите корректное число.");
                }
                else if (cardCount <= 0)
                {
                    Console.WriteLine("Ошибка: число должно быть больше нуля.");
                }
                else if (cardCount > dealer.CardsLeft)
                {
                    Console.WriteLine($"Ошибка: в колоде только {dealer.CardsLeft} карт(ы).");
                }
                else
                {
                    isInputInvalid = false;
                }

            } while (isInputInvalid);

            dealer.DealCards(player, cardCount);
            player.Show();

            Console.WriteLine($"\nОстаток карт в колоде: {dealer.CardsLeft}");
        }
    }

    public class Card
    {

        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public string Suit { get; }
        public string Rank { get; }

        public void Show()
        {
            Console.WriteLine($"{Rank} : {Suit}");
        }
    }

    public class Deck
    {

        private readonly List<Card> _cards;
        private readonly Random _random = new Random();

        public Deck()
        {
            _cards = new List<Card>();

            string[] suits = { "Черви", "Пики", "Бубна", "Крести" };
            string[] ranks = { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };

            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    _cards.Add(new Card(suit, rank));
                }
            }
        }

        public int CardsLeft => _cards.Count;

        public void Shuffle()
        {
            for (int i = _cards.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
            }
        }

        public Card DrawCard()
        {
            if (_cards.Count == 0)
                return null;

            Card card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }
    }

    public class Player
    {

        private readonly List<Card> _cards = new List<Card>();

        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public void TakeCard(Card card)
        {
            if (card != null)
                _cards.Add(card);
        }

        public void Show()
        {
            Console.WriteLine($"\nИгрок: {Name}");
            Console.WriteLine("Карты на руках:");

            foreach (Card card in _cards)
            {
                card.Show();
            }
        }
    }

    public class Dealer
    {

        private readonly Deck _deck;

        public Dealer()
        {
            _deck = new Deck();
            _deck.Shuffle();
        }

        public int CardsLeft => _deck.CardsLeft;

        public void DealCards(Player player, int count)
        {
            Console.WriteLine($"\nВыдача {count} карт игроку {player.Name}...\n");

            for (int i = 0; i < count; i++)
            {
                Card card = _deck.DrawCard();

                if (card != null)
                {
                    player.TakeCard(card);
                }
                else
                {
                    Console.WriteLine("Колода закончилась!");
                    break;
                }
            }
        }
    }
}
