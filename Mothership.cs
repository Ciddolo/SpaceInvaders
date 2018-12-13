using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    static class Mothership
    {
        static SpriteObj sprite = new SpriteObj("Assets/mothership.png", new Vector2(-100, -100));
        static float spawnCooldown = 15f;
        static float movementSpeed = 150f;
        static int points;
        static bool isAlive = false;
        static Vector2 centeredPosition = new Vector2(sprite.Position.X + sprite.Width / 2, sprite.Position.Y + sprite.Height / 2);

        private const float spawnTempo = 15f;

        static Vector2 CenteredPosition { get { return centeredPosition; } }
        static Vector2 SpritePosition { get { return sprite.Position; } set { sprite.Position = value; } }
        static float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
        static int Width { get { return sprite.Width; } }
        static int Height { get { return sprite.Height; } }

        static private void Starting()
        {
            if (spawnCooldown <= 0)
            {
                Random random = new Random();
                points = random.Next(5, 31) * 10;
                Vector2 startingPosition;
                int randomNumber = random.Next(2);
                if (randomNumber == 0)
                {
                    startingPosition = new Vector2(-Width, Game.Edge);
                    if (movementSpeed < 0)
                    {
                        MovementSpeed *= -1;
                    }
                }
                else
                {
                    startingPosition = new Vector2(GfxTools.Win.width, Game.Edge);
                    if (movementSpeed > 0)
                    {
                        MovementSpeed *= -1;
                    }
                }
                SpritePosition = startingPosition;
                spawnCooldown = spawnTempo;
                isAlive = true;
            }
        }
        static private void MovementManager()
        {
            if (isAlive)
            {
                float positionX = sprite.Position.X + movementSpeed * Game.DeltaTime;
                SpritePosition = new Vector2(positionX, sprite.Position.Y);
                centeredPosition = new Vector2(sprite.Position.X + sprite.Width / 2, sprite.Position.Y + sprite.Height / 2);
            }
        }
        static private void IsGoAway()
        {
            if (movementSpeed > 0 && SpritePosition.X > GfxTools.Win.width || movementSpeed < 0 && SpritePosition.X < -Width)
            {
                isAlive = false;
            }
        }
        static public bool IsCollideWithBullet(PlayerBullet bullet)
        {
            if (centeredPosition.Sub(bullet.Point).GetLength() < Width / 2 + bullet.Width / 2 && isAlive)
            {
                isAlive = false;
                Player.Score += points;
                return true;
            }
            return false;
        }
        static private void Timer()
        {
            if (!isAlive)
            {
                spawnCooldown -= Game.DeltaTime;
            }
        }

        static public void Update()
        {
            Starting();
            MovementManager();
            IsGoAway();
            Timer();
        }

        static public void Draw()
        {
            if (isAlive)
            {
                sprite.Draw();
            }
        }
    }
}