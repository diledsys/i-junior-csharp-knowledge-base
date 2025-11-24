using System;
using System.Collections.Generic;

namespace ExempleInterface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<IAnimal> animals = new List<IAnimal>
            {

                new Dog(),
                new Cat()
            };
            foreach (var animal in animals)
            {
                if (animal is Dog dog)
                {
                    dog.Fetch();
                }
                animal.Speak();
            }
            Console.ReadLine();
            List<IEnemy> enemies = new List<IEnemy>
            {
                new Goblin(),
                new Troll()
            };
            foreach (var enemy in enemies)
            {
                if (enemy is Dog dog)
                {
                    dog.Fetch();
                }
                else if (enemy is Troll troll)
                {
                    Console.WriteLine("This enemy is a Troll with health: " + troll.Health);
                }

                enemy.TakeDamage(30);
                enemy.TakeDamage(50);
                enemy.TakeDamage(30);
            }
        }
    }
    interface IAnimal
    {
        void Speak();
    }
    class Dog : IAnimal
    {
        public void Speak()
        {
            Console.WriteLine("Woof!");
        }

        public void Fetch()
        {
            Console.WriteLine("Dog is fetching the ball!");
        }
    }
    class Cat : IAnimal
    {
        public void Speak()
        {
            Console.WriteLine("Meow!");
        }
    }

    interface IEnemy
    {
        void TakeDamage(int amount);
    }

    class Goblin : IEnemy
    {
        public int Health { get; set; } = 100;
        public void TakeDamage(int amount)
        {
            Health -= amount;
            Console.WriteLine($"Goblin takes {amount} damage, health now {Health}");
            if (Health <= 0)
            {
                Console.WriteLine("Goblin is defeated!");
            }
        }
    }
    class Troll : IEnemy
    {
        public int Health { get; private set; } = 150;
        public void TakeDamage(int amount)
        {
            Health -= amount;
            Console.WriteLine($"Troll takes {amount} damage, health now {Health}");
            if (Health <= 0)
            {
                Console.WriteLine("Troll is defeated!");
            }
        }
    }
}
