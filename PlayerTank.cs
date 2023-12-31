using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    class PlayerTank : Tank
    {
        public PlayerTank(int x, int y) : base(x, y) { }

        public override void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("Н");
        }

        public override Bullet Shoot(int dirX, int dirY)
        {
            // Создаем и активируем пулю в назначенном направлении
            Bullet bullet = new Bullet(X, Y, dirX, dirY);
            bullet.Activate(X, Y, dirX, dirY);
            return bullet;
        }
    }
}
