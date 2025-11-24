using System;

namespace BossBattleGame
{
    class Program
    {
        static void Main()
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

            int heroHealth = HeroMaxHealth;
            int heroMana = HeroMaxMana;
            int bossHealth = BossMaxHealth;
            bool fireballUsed = false;
            int healCount = MaxHealCount;

            Random random = new Random();

            Console.WriteLine("Бой с Боссом!");

            while (heroHealth > 0 && bossHealth > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n===== СТАТУС =====");
                Console.ResetColor();
                Console.WriteLine($"Здоровье Героя: {heroHealth}/{HeroMaxHealth}");
                Console.WriteLine($"Мана Героя: {heroMana}/{HeroMaxMana}");
                Console.WriteLine($"Осталось лечений: {healCount}");
                Console.WriteLine($"Здоровье Босса: {bossHealth}/{BossMaxHealth}");
                Console.WriteLine("==================");

                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine($"{CommandNormalAttack}. Обычная атака");
                Console.WriteLine($"{CommandFireball}. Огненный шар (тратит ману)");
                Console.Write($"{CommandExplosion}. Взрыв (только после огненного шара) ");
                if (fireballUsed == false)
                {
                    Console.Write("не доступен\n");
                }
                else
                {
                    Console.Write("доступен\n");
                }

                Console.WriteLine($"{CommandHeal}. Лечение (ограничено)");
                Console.Write("Ваш выбор: ");

                string selectAction = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n========== - FIGHT - ==========");
                Console.ResetColor();

                bool playerActed = false;

                switch (selectAction)
                {
                    case CommandNormalAttack:
                        bossHealth -= NormalAttackDamage;
                        Console.WriteLine($"Вы нанесли обычную атаку: {NormalAttackDamage} урона Боссу.");
                        playerActed = true;
                        break;

                    case CommandFireball:
                        if (heroMana >= FireballManaCost)
                        {
                            heroMana -= FireballManaCost;
                            bossHealth -= FireballDamage;
                            fireballUsed = true;
                            Console.WriteLine($"Вы использовали Огненный шар и нанесли {FireballDamage} урона Боссу.");
                            playerActed = true;
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно маны для Огненного шара.");
                        }
                        break;

                    case CommandExplosion:
                        if (fireballUsed)
                        {
                            bossHealth -= ExplosionDamage;
                            fireballUsed = false;
                            Console.WriteLine($"Вы использовали Взрыв и нанесли {ExplosionDamage} урона Боссу.");
                            playerActed = true;
                        }
                        else
                        {
                            Console.WriteLine("Сначала нужно использовать Огненный шар перед Взрывом.");
                        }
                        break;

                    case CommandHeal:
                        if (healCount > 0)
                        {
                            int heal = Math.Min(HealAmount, HeroMaxHealth - heroHealth);
                            int manaRestore = Math.Min(ManaRestoreAmount, HeroMaxMana - heroMana);
                            heroHealth += heal;
                            heroMana += manaRestore;
                            healCount--;
                            Console.WriteLine($"Вы вылечили себя на {heal} и восстановили {manaRestore} маны.");
                            playerActed = true;
                        }
                        else
                        {
                            Console.WriteLine("У вас больше нет лечений.");
                        }
                        break;

                    default:
                        Console.WriteLine("Неверная команда. Вы пропускаете ход.");
                        break;
                }

                if (!playerActed)
                {
                    Console.WriteLine("Вы пропустили ход из-за ошибки или невозможности выполнить действие.");
                }

                // Атака Босса (выполняется каждый раунд)
                int bossAttack = random.Next(BossMinAttack, BossMaxAttack + 1);
                heroHealth -= bossAttack;
                Console.WriteLine($"Босс атакует и наносит {bossAttack} урона!");
            }

            // ---- Итог боя: один раз после цикла ----
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n===== ИТОГ БОЯ =====");
            Console.ResetColor();

            if (heroHealth <= 0 && bossHealth <= 0)
            {
                Console.WriteLine("Ничья! Вы и Босс пали одновременно.");
            }
            else if (heroHealth <= 0)
            {
                Console.WriteLine("Вы были убиты Боссом.");
            }
            else if (bossHealth <= 0)
            {
                Console.WriteLine("Поздравляем! Вы победили Босса!");
            }
            else
            {
                // На случай, если цикл завершился иным образом (защита от логических изменений)
                Console.WriteLine("Бой завершён.");
            }
        }
    }
}
