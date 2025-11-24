using System;
using System.Collections.Generic;

namespace CardGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dealer dealer = new Dealer();
            dealer.StartGame();
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

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
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
                throw new InvalidOperationException("Колода пуста!");

            Card card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }
    }

    public class Player
    {

        private readonly List<Card> _cards = new List<Card>();

        public void ReceiveCard(Card card)
        {
            _cards.Add(card);
        }

        public void ShowHand()
        {
            Console.WriteLine("\nКарты игрока:");

            foreach (var card in _cards)
            {
                Console.WriteLine(card);
            }
        }
    }

    public class Dealer
    {
        private readonly Deck _deck;
        private readonly Player _player;

        public Dealer()
        {
            _deck = new Deck();
            _player = new Player();
        }

        public void StartGame()
        {
            _deck.Shuffle();

            Console.Write("Сколько карт выдать игроку? ");
            if (int.TryParse(Console.ReadLine(), out int count))
            {
                for (int i = 0; i < count; i++)
                {
                    _player.ReceiveCard(_deck.DrawCard());
                }

                _player.ShowHand();
                Console.WriteLine($"\nОсталось карт в колоде: {_deck.CardsLeft}");
            }
            else
            {
                Console.WriteLine("Ошибка ввода.");
            }
        }
    }
}
