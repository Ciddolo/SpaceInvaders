using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    class PlayerBullet
    {
        SpriteObj sprite;
        Vector2 point;
        bool isFired = false;

        const float movementSpeed = 600;

        public SpriteObj Sprite { get { return sprite; } set { sprite = value; } }
        public Vector2 Point { get { return point; } }
        public bool IsFired { get { return isFired; } set { isFired = value; } }
        public int Width { get { return sprite.Width; } }

        public PlayerBullet(SpriteObj sprite)
        {
            this.sprite = sprite;
            point = new Vector2(sprite.Position.X + sprite.Width / 2, sprite.Position.Y);
        }

        // UPDATE

        private void MovementManager()
        {

            float deltaY = movementSpeed * Game.DeltaTime;
            sprite.Translate(0, -deltaY);
            point = new Vector2(sprite.Position.X + sprite.Width / 2, sprite.Position.Y);
            if (sprite.Position.Y < -16)
            {
                isFired = false;
            }

        }

        public void Update()
        {
            if (isFired)
            {
                MovementManager();
            }
        }

        // DRAW

        public void Draw()
        {
            if (isFired)
            {
                sprite.Draw();
            }
        }
    }
}