using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class EnemyBullet
    {
        SpriteObj sprite = new SpriteObj("Assets/alienBullet1a.png");
        string[] files = new string[2] { "Assets/alienBullet1a.png", "Assets/alienBullet1b.png" };
        Animation animation;
        Vector2 point;
        bool isFired;

        const float movementSpeed = 200;

        public SpriteObj Sprite { get { return sprite; } set { sprite = value; } }
        public Vector2 Point { get { return point; } }
        public bool IsFired { get { return isFired; } set { isFired = value; } }
        public int Width { get { return sprite.Width; } }

        public void SetSpritePosition(Vector2 position)
        {
            sprite.Position = position;
        }

        public EnemyBullet()
        {
            animation = new Animation(files, sprite, 12);
            point = new Vector2(sprite.Position.X + sprite.Width / 2, sprite.Position.Y + sprite.Height);
        }

        // UPDATE

        private void MovementManager()
        {
            float deltaY = movementSpeed * Game.DeltaTime;
            sprite.Translate(0, deltaY);
            point = new Vector2(sprite.Position.X + sprite.Width / 2, sprite.Position.Y + sprite.Height);
            if (sprite.Position.Y > GfxTools.Win.height - Game.Edge)
            {
                isFired = false;
            }
        }

        public void Update()
        {
            if (isFired)
            {
                MovementManager();
                animation.Update();
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