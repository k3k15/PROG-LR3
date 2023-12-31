using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    class EnemyTank : Tank
    {

        public EnemyTank(int x, int y) : base(x, y) { }

        public void AutoMove()
        {
            Random rnd = new Random();
            int changeDirection = rnd.Next(1, 101);

            if (changeDirection <= 5) // 5% вероятность изменить направление движения
            {
                int direction = rnd.Next(1, 5); // Random direction: 1 - up, 2 - down, 3 - left, 4 - right

                switch (direction)
                {
                    case 1:
                        DirectionX = 0;
                        DirectionY = -1;
                        break;
                    case 2:
                        DirectionX = 0;
                        DirectionY = 1;
                        break;
                    case 3:
                        DirectionX = -1;
                        DirectionY = 0;
                        break;
                    case 4:
                        DirectionX = 1;
                        DirectionY = 0;
                        break;
                }
            }

            Move(DirectionX, DirectionY);
        }

        public override Bullet Shoot(int DirectionX, int DirectionY)
        {
            Bullet bullet = new Bullet(X, Y, DirectionX, DirectionY);
            bullet.Activate(X, Y, DirectionX, DirectionY);
            return bullet;
        }

        public override void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("Ж");
        }
    }
}
