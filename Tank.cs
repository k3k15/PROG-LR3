using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    abstract class Tank
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int DirectionX { get; set; }
        public int DirectionY { get; set; }
        public bool IsDestroyed { get; set; }

        protected Tank(int x, int y)
        {
            X = x;
            Y = y;
            DirectionX = 1; // Вражеские танки двигаются вправо по умолчанию
            DirectionY = 0;
            IsDestroyed = false;
        }

        public virtual void Move(int deltaX, int deltaY)
        {
            int newX = X + deltaX;
            int newY = Y + deltaY;

            if (newX >= 0 && newX < Console.WindowWidth && newY >= 0 && newY < Console.WindowHeight)
            {
                X = newX;
                Y = newY;
            }
        }

        public virtual void Destroy()
        {
            IsDestroyed = true;
            Console.SetCursorPosition(X, Y);
            Console.Write(" ");
        }

        public abstract Bullet Shoot(int dirX, int dirY);

        public abstract void Draw();

        public bool IsHitBy(Bullet bullet)
        {
            return X == bullet.X && Y == bullet.Y;
        }
    }
}
