using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    class ScoreSprites
    {
        private Sprite[] sprites;
        private Vector2 position;
        private string score;

        public Vector2 Position { get { return this.position; } set { this.position = value; } }
        public string Score { get { return this.score; } set { this.score = value; } }

        public ScoreSprites(Vector2 position, string score)
        {
            this.score = score;
            this.position = position;
            this.sprites = new Sprite[32];
        }

        // UPDATE

        public void Update(string score)
        {
            this.score = score;
            for (int i = 0; i < this.score.Length; i++)
            {
                char currentNumber = score[i];
                sprites[i] = new Sprite("Assets/number" + currentNumber + ".png");
            }
        }

        // DRAW

        private void DrawScore(Window window)
        {
            Vector2 position = this.position;
            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[i] == null)
                {
                    return;
                }
                else
                {
                    GfxTools.DrawSprite(window, position, sprites[i]);
                    position.X += sprites[i].width + 2;
                }
            }
        }
        public void Draw(Window window)
        {
            DrawScore(window);
        }
    }
}