using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    class Brick
    {
        private Color color = new Color(0, 200, 0);
        private Vector2 position;

        private int side = (int)BricksValues.side;
        private bool isAlive = true;

        public Vector2 Position { get { return this.position; } }
        public int Side { get { return this.side; } }
        public bool IsAlive { get { return this.isAlive; } set { this.isAlive = value; } }

        public Brick(Vector2 position)
        {
            this.position = position;
        }

        //DRAW

        public void Draw(Window window)
        {
            GfxTools.DrawRectangle(window, this.position, this.side, this.side, this.color);
        }
    }
}