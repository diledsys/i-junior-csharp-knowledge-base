using System;

namespace BossBattleGame
{
    class Program
    {
        const int HeroMaxHealth = 100;
        const int HeroMaxMana = 50;
        const int BossMaxHealth = 150;
        const int BossMinAttack = 10;
        const int BossMaxAttack = 20;
        const int NormalAttackDamage = 15;
        const int FireballDamage = 30;
        const int FireballManaCost = 20;
        const int ExplosionDamage = 50;
        const int HealAmount = 25;
        const int ManaRestoreAmount = 20;
        const int MaxHealCount = 3;

        const string CommandNormalAttack = "1";
        const string CommandFireball = "2";
        const string CommandExplosion = "3";
        const string CommandHeal = "4";

        static int heroHealth = HeroMaxHealth;
        static int heroMana = HeroMaxMana;
        static int bossHealth = BossMaxHealth;
        static bool fireballUsed = false;
        static int healCount = MaxHealCount;
        static Random random = new Random();

        static void Main()
        {
            Console.WriteLine("Добро пожаловать в бой с Боссом!");

            while (heroHealth > 0 && bossHealth > 0)
            {
                DisplayStatus();

                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Обычная атака");
                Console.WriteLine("2. Огненный шар (тратит ману)");
                Console.WriteLine("3. Взрыв (только после огненного шара)");
                Console.WriteLine("4. Лечение (ограничено)");
                Console.Write("Ваш выбор: ");
                string input = Console.ReadLine();

                bool playerActed = PerformHeroAction(input);

                if (!playerActed)
                {
                    Console.WriteLine("Вы пропустили ход из-за неверной команды или условия.");
                }

                int bossAttack = random.Next(BossMinAttack, BossMaxAttack + 1);
                heroHealth -= bossAttack;
                Console.WriteLine($"Босс атакует и наносит {bossAttack} урона!");

                if (heroHealth <= 0 && bossHealth <= 0)
                {
                    Console.WriteLine("\nНичья! Вы и босс пали одновременно.");
                    break;
                }
                else if (heroHealth <= 0)
                {
                    Console.WriteLine("\nВы были убиты Боссом.");
                    break;
                }
                else if (bossHealth <= 0)
                {
                    Console.WriteLine("\nВы победили Босса!");
                    break;
                }
            }
        }

        static void DisplayStatus()
        {
            Console.WriteLine("\n===== СТАТУС =====");
            Console.WriteLine($"Здоровье Героя: {heroHealth}/{HeroMaxHealth}");
            Console.WriteLine($"Мана Героя: {heroMana}/{HeroMaxMana}");
            Console.WriteLine($"Осталось лечений: {healCount}");
            Console.WriteLine($"Здоровье Босса: {bossHealth}/{BossMaxHealth}");
            Console.WriteLine("==================");
        }

        static bool PerformHeroAction(string input)
        {
            switch (input)
            {
                case CommandNormalAttack:
                    bossHealth -= NormalAttackDamage;
                    Console.WriteLine($"Вы нанесли обычную атаку: {NormalAttackDamage} урона Боссу.");
                    return true;

                case CommandFireball:
                    if (heroMana >= FireballManaCost)
                    {
                        heroMana -= FireballManaCost;
                        bossHealth -= FireballDamage;
                        fireballUsed = true;
                        Console.WriteLine($"Вы использовали Огненный шар и нанесли {FireballDamage} урона Боссу.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Недостаточно маны для Огненного шара.");
                        return false;
                    }

                case CommandExplosion:
                    if (fireballUsed)
                    {
                        bossHealth -= ExplosionDamage;
                        fireballUsed = false;
                        Console.WriteLine($"Вы использовали Взрыв и нанесли {ExplosionDamage} урона Боссу.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Сначала нужно использовать Огненный шар перед Взрывом.");
                        return false;
                    }

                case CommandHeal:
                    if (healCount > 0)
                    {
                        int heal = Math.Min(HealAmount, HeroMaxHealth - heroHealth);
                        int manaRestore = Math.Min(ManaRestoreAmount, HeroMaxMana - heroMana);
                        heroHealth += heal;
                        heroMana += manaRestore;
                        healCount--;
                        Console.WriteLine($"Вы вылечили себя на {heal} и восстановили {manaRestore} маны.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("У вас больше нет лечений.");
                        return false;
                    }

                default:
                    Console.WriteLine("Неверная команда. атакует БОСС");
                    return false;
            }
        }
    }
}
