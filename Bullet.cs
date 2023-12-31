using System;

namespace Tanks
{
    class Bullet
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int DirectionX { get; private set; }
        public int DirectionY { get; private set; }
        public bool IsActive { get; set; }

        public Bullet(int x, int y, int dirX, int dirY)
        {
            X = x;
            Y = y;
            DirectionX = dirX;
            DirectionY = dirY;
            IsActive = false;
        }

        public void Activate(int startX, int startY, int dirX, int dirY)
        {
            X = startX;
            Y = startY;
            DirectionX = dirX;
            DirectionY = dirY;
            IsActive = true;
        }

        public void Move()
        {
            if (IsActive)
            {
                Clear();
                X += DirectionX;
                Y += DirectionY;

                if (X < 0 || X >= Console.WindowWidth || Y < 0 || Y >= Console.WindowHeight)
                {
                    IsActive = false;
                }
                else
                {
                    Draw();
                }
            }
        }

        public void Draw()
        {
            if (IsActive) // Рисуем только если пуля активна
            {
                Console.SetCursorPosition(X, Y);
                Console.Write("*");
            }
        }

        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(" ");
        }
    }
}
