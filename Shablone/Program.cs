using System;
using System.Collections.Generic;

namespace CardGame {
    internal class Program {
        static void Main(string[] args) {
            Dealer dealer = new Dealer();
            dealer.StartGame();
        }
    }

    public class Card {
        // 1️⃣ Поля — отсутствуют (используем автосвойства)
        // 2️⃣ Конструктор
        public Card(string suit, string rank) {
            Suit = suit;
            Rank = rank;
        }

        // 3️⃣ Свойства
        public string Suit { get; }
        public string Rank { get; }

        // 4️⃣ Методы
        public override string ToString() {
            return $"{Rank} of {Suit}";
        }
    }

    public class Deck {
        // 1️⃣ Поля
        private readonly List<Card> _cards;
        private readonly Random _random = new Random();

        // 2️⃣ Конструктор
        public Deck() {
            _cards = new List<Card>();

            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            foreach (string suit in suits) {
                foreach (string rank in ranks) {
                    _cards.Add(new Card(suit, rank));
                }
            }
        }

        // 3️⃣ Свойства
        public int CardsLeft => _cards.Count;

        // 4️⃣ Методы
        public void Shuffle() {
            for (int i = _cards.Count - 1; i > 0; i--) {
                int j = _random.Next(i + 1);
                (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
            }
        }

        public Card DrawCard() {
            if (_cards.Count == 0)
                throw new InvalidOperationException("Колода пуста!");

            Card card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }
    }

    public class Player {
        // 1️⃣ Поля
        private readonly List<Card> _cards = new List<Card>();

        // 2️⃣ Конструктор — не требуется

        // 3️⃣ Свойства — не требуется

        // 4️⃣ Методы
        public void ReceiveCard(Card card) {
            _cards.Add(card);
        }

        public void ShowHand() {
            Console.WriteLine("\nКарты игрока:");

            foreach (var card in _cards) {
                Console.WriteLine(card);
            }
        }
    }

    public class Dealer {
        // 1️⃣ Поля
        private readonly Deck _deck;
        private readonly Player _player;

        // 2️⃣ Конструктор
        public Dealer() {
            _deck = new Deck();
            _player = new Player();
        }

        // 3️⃣ Свойства — не требуется

        // 4️⃣ Методы
        public void StartGame() {
            _deck.Shuffle();

            Console.Write("Сколько карт выдать игроку? ");
            if (int.TryParse(Console.ReadLine(), out int count)) {
                for (int i = 0; i < count; i++) {
                    _player.ReceiveCard(_deck.DrawCard());
                }

                _player.ShowHand();
                Console.WriteLine($"\nОсталось карт в колоде: {_deck.CardsLeft}");
            }
            else {
                Console.WriteLine("Ошибка ввода.");
            }
        }
    }
}
