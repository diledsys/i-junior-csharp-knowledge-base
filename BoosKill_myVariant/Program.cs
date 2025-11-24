using System;

namespace BoosKill_myVariant
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int HeroFireball = 35;
            const int ManaHeroMax = 25;
            const int HeroAttackPowerMax = 12;
            const int DmgExplosion = 45;
            const int HealingMax = 5;
            const int AmountOfLife = 3;

            Random random = new Random();

            int bossHealth = 100;
            int bossAttackPowerMax = random.Next(15, 35);

            bool fireballIsReady = false;
            int herosManna = 45;
            int heroHealth = 120;
            int mannaHero = 0;
            int lifeHero = AmountOfLife;

            while (true)
            {
                int heroDamage = 0;

                showMenu();

                string choiceOfSkills = Console.ReadLine();

                switch (choiceOfSkills)
                {

                    case "1":
                        heroDamage = HeroAttackPowerMax;
                        Console.WriteLine($"Обычная атака {heroDamage} -> урон босу.");


                        break;

                    case "2":
                        heroDamage = HeroFireball;
                        Console.WriteLine($"Применен огненый шар {heroDamage} -> урон босу.");

                        if (mannaHero >= ManaHeroMax)
                        {
                            fireballIsReady = true;
                        }
                        else
                        {
                            Console.WriteLine($"не хватает манны для активации взрыва. Манна: {mannaHero}.");
                        }
                        break;

                    case "3":
                        if (fireballIsReady)
                        {
                            heroDamage = DmgExplosion;
                            fireballIsReady = false; // расходуем заряд
                            Console.WriteLine($"Взрыв на {heroDamage} урона (заряд из Огненного шара).");
                        }
                        else
                        {
                            //validMove = false;
                            Console.WriteLine("Взрыв недоступен: сначала нужен успешный Огненный шар! Ход пропущен.");
                        }
                        break;

                    case "4":
                        lifeHero--;
                        if (lifeHero > 0)
                        {
                            heroHealth += HealingMax;
                            Console.WriteLine($"применил лечени: +  {HealingMax} к здоровью");
                        }
                        break;

                    default:
                        Console.WriteLine("Промах! Бос атакует!");
                        break;
                }

                ShowStatus(heroHealth, bossHealth, mannaHero, fireballIsReady);

                if (bossHealth > 0 && heroHealth > 0)
                {
                    bossHealth -= heroDamage;
                    heroHealth -= bossAttackPowerMax;
                    mannaHero += heroDamage;
                }


                bool bossDead = bossHealth <= 0;
                bool heroDead = heroHealth <= 0;

                if (bossDead && heroDead)
                {
                    Console.WriteLine("\nНичья! Вы повергли Босса, но пали в том же раунде.");
                    break;
                }
                if (bossDead)
                {
                    Console.WriteLine("\nПобеда! Босс уничтожен.");
                    break;
                }
                if (heroDead)
                {
                    Console.WriteLine("\nПоражение... Герой пал. Босс радуеться.");
                    break;
                }
            }
        }
        static void showMenu()
        {
            Console.WriteLine("\nВыберите умение:");
            Console.WriteLine("1) Обычная атака");
            Console.WriteLine("2) Огненный шар (тратит ману)");
            Console.WriteLine("3) Взрыв (только если до этого был Огненный шар)");
            Console.WriteLine("4) Лечение (ограничено)");
            Console.Write("Ваш выбор: ");
        }

        static void ShowStatus(int heroHealth, int bossHealth, int mannaHero, bool fireballIsReady)
        {
            Console.WriteLine($"\n[Герой] жизни: {heroHealth} Манна: {mannaHero}  Взрыв заряжен: {(fireballIsReady ? "Да" : "Нет")}");
            Console.WriteLine($"[Босс ] жизни: {bossHealth}");
        }
    }
}
