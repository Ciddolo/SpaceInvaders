using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    class Enemy
    {
        SpriteObj sprite = new SpriteObj("Assets/alien2a.png");
        string[] files = new string[2];
        Animation animation;
        Vector2 centeredPosition;
        int alienType;
        int points;
        bool isAlive = true;
        float movementSpeed;

        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
        public Vector2 CenteredPosition { get { return centeredPosition; } }
        public Vector2 SpritePosition { get { return sprite.Position; } set { sprite.Position = value; } }
        public int Width { get { return sprite.Width; } }
        public int Height { get { return sprite.Height; } }
        public int Points { get { return points; } }
        public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }

        public Enemy(int alienType, Vector2 position)
        {
            this.alienType = alienType;
            switch (alienType)
            {
                case 1:
                    {
                        sprite = new SpriteObj("Assets/alien1a.png", position);
                        files[0] = "Assets/alien1a.png";
                        files[1] = "Assets/alien1b.png";
                        break;
                    }
                case 2:
                    {
                        sprite = new SpriteObj("Assets/alien2a.png", position);
                        files[0] = "Assets/alien2a.png";
                        files[1] = "Assets/alien2b.png";
                        break;
                    }
                case 3:
                    {
                        sprite = new SpriteObj("Assets/alien3a.png", position);
                        files[0] = "Assets/alien3a.png";
                        files[1] = "Assets/alien3b.png";
                        break;
                    }
            }
            animation = new Animation(files, sprite, 1.5f);
            centeredPosition = new Vector2(sprite.Position.X + sprite.Width / 2, sprite.Position.Y + sprite.Height / 2);
            points = this.alienType * 10;
        }

        // UPDATE

        private void MovementManager()
        {
            float positionX = sprite.Position.X + movementSpeed * Game.DeltaTime;
            sprite.Position = new Vector2(positionX, sprite.Position.Y);
            centeredPosition = new Vector2(sprite.Position.X + sprite.Width / 2, sprite.Position.Y + sprite.Height / 2);
        }
        public bool IsCollideWithBullet(PlayerBullet bullet)
        {
            if (centeredPosition.Sub(bullet.Point).GetLength() < sprite.Width / 2 + bullet.Width / 2 && isAlive)
            {
                isAlive = false;
                Player.Score += points;
                return true;
            }
            return false;
        }

        public void Update()
        {
            if (isAlive)
            {
                MovementManager();
                animation.Update();
            }
        }

        // DRAW

        public void Draw()
        {
            if (isAlive)
            {
                sprite.Draw();
            }
        }
    }
}