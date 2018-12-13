using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    class Barrier
    {
        private Brick[] bricks = new Brick[(int)BarriersValues.bricksInOneBarrier];
        private Vector2 position;

        private int rows = (int)BarriersValues.rowsAndColumns;
        private int columns = (int)BarriersValues.rowsAndColumns;
        private int side = (int)BarriersValues.side;
        private bool isAlive = true;

        public Brick[] Bricks { get { return this.bricks; } }
        public Vector2 Position { get { return this.position; } }
        public int Side { get { return this.side; } }
        public bool IsAlive
        {
            get
            {
                for (int i = 0; i < this.bricks.Length; i++)
                {
                    if (this.bricks[i].IsAlive)
                    {
                        return true;
                    }
                }
                return false;
            }
            set { this.isAlive = value; }
        }

        public Barrier(Vector2 position)
        {
            this.position = position;
            float x = position.X;
            float y = position.Y;
            int index = 0;
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns; j++)
                {
                    if ((i == 0 && j == 0) || (i == 0 && j == 1) || (i == 0 && j == 6) || (i == 0 && j == 7) ||
                        (i == 1 && j == 0) || (i == 1 && j == 7) ||
                        (i == 6 && j == 3) || (i == 6 && j == 4) ||
                        (i == 7 && j == 2) || (i == 7 && j == 3) || (i == 7 && j == 4) || (i == 7 && j == 5))
                    {
                        position.X += (int)BricksValues.side;
                        continue;
                    }
                    bricks[index] = new Brick(position);
                    index++;
                    position.X += (int)BricksValues.side;
                }
                position.X = x;
                position.Y += (int)BricksValues.side;
            }
        }

        // METHODS

        // UPDATE

        public void Update(Window window)
        {
            this.isAlive = IsAlive;
        }

        // DRAW

        public void Draw(Window window)
        {
            for (int i = 0; i < bricks.Length; i++)
            {
                if (!bricks[i].IsAlive)
                    continue;
                bricks[i].Draw(window);
            }
        }
    }
}