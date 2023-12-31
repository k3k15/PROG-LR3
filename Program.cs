using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Tanks
{
    class Program
    {
        static void Main(string[] args)
        {
            int score = 0;
            Console.CursorVisible = false;
            Console.WindowHeight = 20;
            Console.WindowWidth = 50;

            PlayerTank playerTank = new PlayerTank(10, 10);
            List<EnemyTank> enemyTanks = new List<EnemyTank>
            {
            new EnemyTank(30, 5),
            new EnemyTank(40, 8),
            new EnemyTank(20, 11),
            };

            List<Bullet> playerBullets = new List<Bullet>();
            List<Bullet> enemyBullets = new List<Bullet>();

            bool running = true;

            while (running)
            {
                Console.Clear();

                if (enemyTanks.Count(tank => !tank.IsDestroyed) < 2)
                {
                    Random rnd = new Random();
                    int x = rnd.Next(0, Console.WindowWidth);
                    int y = rnd.Next(0, Console.WindowHeight);
                    enemyTanks.Add(new EnemyTank(x, y));
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }

                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        playerBullets.Add(playerTank.Shoot(playerTank.DirectionX, playerTank.DirectionY));
                    }

                    switch (key.Key)
                    {
                        case ConsoleKey.W:
                            playerTank.Move(0, -1);
                            playerTank.DirectionX = 0;
                            playerTank.DirectionY = -1;
                            break;
                        case ConsoleKey.A:
                            playerTank.Move(-1, 0);
                            playerTank.DirectionX = -1;
                            playerTank.DirectionY = 0;
                            break;
                        case ConsoleKey.S:
                            playerTank.Move(0, 1);
                            playerTank.DirectionX = 0;
                            playerTank.DirectionY = 1;
                            break;
                        case ConsoleKey.D:
                            playerTank.Move(1, 0);
                            playerTank.DirectionX = 1;
                            playerTank.DirectionY = 0;
                            break;
                    }
                }

                // Проверяем столкновения танков и пуль перед перерисовкой
                foreach (var bullet in playerBullets)
                {
                    // Проверяем попадание во вражеские танки
                    foreach (var enemyTank in enemyTanks)
                    {
                        if (!enemyTank.IsDestroyed && enemyTank.IsHitBy(bullet))
                        {
                            enemyTank.Destroy();
                            bullet.IsActive = false;
                            break;
                        }
                    }
                }

                foreach (var bullet in playerBullets)
                {
                    bullet.Move();

                    foreach (var enemyTank in enemyTanks)
                    {
                        if (!enemyTank.IsDestroyed && enemyTank.IsHitBy(bullet))
                        {
                            enemyTank.Destroy();
                            bullet.IsActive = false;
                            score++;
                            break;
                        }
                    }

                    enemyTanks.RemoveAll(tank => tank.IsDestroyed);

                    bullet.Draw();
                }

                foreach (var bullet in enemyBullets)
                {
                    bullet.Move();

                    // Проверяем попадание
                    if (!playerTank.IsDestroyed && playerTank.IsHitBy(bullet))
                    {
                        playerTank.Destroy();
                        bullet.IsActive = false;
                        running = false; // Конец игры
                        break;
                    }

                    bullet.Draw();
                }

                foreach (var enemyTank in enemyTanks)
                {
                    enemyTank.AutoMove();
                    enemyTank.Draw();

                    if (new Random().Next(0, 100) < 5)
                    {
                        enemyBullets.Add(enemyTank.Shoot(enemyTank.DirectionX, enemyTank.DirectionY));
                    }
                }

                playerTank.Draw();

                Thread.Sleep(50);
            }

            Console.Clear();
            Console.WriteLine($"Ваш счет: {score}");
        }
    }
}