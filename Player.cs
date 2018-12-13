using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    class Player
    {
        SpriteObj sprite = new SpriteObj("Assets/player.png");
        Vector2 centeredPosition;
        Vector2 cannonPosition;
        PlayerBullet[] bullets = new PlayerBullet[10];
        float movementSpeed = 0f;
        float shootCooldown = 0f;
        float invincibleCooldown = 0f;
        float flickeringCooldown = 0f;
        int bulletIndex = 0;
        static int lives = 3;
        static int score = 0;

        const float maxSpeed = 200f;
        const float shootTempo = 0.75f;
        const float invincibleTempo = 1f;
        const float flickeringTempo = 1f;

        public static int Score { get { return score; } set { score = value; } }
        public static int Lives { get { return lives; } set { lives = value; } }
        public SpriteObj Sprite { get { return sprite; } }

        public Player()
        {
            Vector2 startPosition = new Vector2(GfxTools.Win.width / 2 - sprite.Width / 2, GfxTools.Win.height - Game.Edge * 1.5f - sprite.Height);
            sprite = new SpriteObj("Assets/player.png", startPosition);
            centeredPosition = new Vector2(sprite.Position.X + sprite.Width / 2, sprite.Position.Y + sprite.Height / 2);
            for (int i = 0; i < bullets.Length; i++)
            {
                SpriteObj bulletSprite = new SpriteObj("Assets/playerBullet.png");
                bullets[i] = new PlayerBullet(bulletSprite);
            }
        }

        // INPUT

        public void Input()
        {
            if (GfxTools.Win.GetKey(KeyCode.A) || GfxTools.Win.GetKey(KeyCode.Left))
            {
                movementSpeed = maxSpeed * -Game.DeltaTime;
            }
            else if (GfxTools.Win.GetKey(KeyCode.D) || GfxTools.Win.GetKey(KeyCode.Right))
            {
                movementSpeed = maxSpeed * Game.DeltaTime;
            }
            else
            {
                movementSpeed = 0;
            }

            if (GfxTools.Win.GetKey(KeyCode.Space))
            {
                if (shootCooldown <= 0)
                {
                    Shoot();
                    shootCooldown = shootTempo;
                }
            }
        }

        // UPDATE

        private void Shoot()
        {
            float cannonPositionX = sprite.Position.X + sprite.Width / 2 - bullets[bulletIndex].Sprite.Width / 2;
            float cannonpositionY = sprite.Position.Y - bullets[bulletIndex].Sprite.Height;
            cannonPosition = new Vector2(cannonPositionX, cannonpositionY);
            bullets[bulletIndex].Sprite = new SpriteObj("Assets/playerBullet.png", cannonPosition);
            bullets[bulletIndex].IsFired = true;
            bulletIndex++;
            if (bulletIndex >= bullets.Length)
            {
                bulletIndex = 0;
            }
        }
        private void UpdateBullets()
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                if (!bullets[i].IsFired)
                    continue;
                bullets[i].Update();
                if (EnemiesManager.IsCollideWithBullet(bullets[i]) ||
                    Mothership.IsCollideWithBullet(bullets[i]))
                {
                    bullets[i].IsFired = false;
                }
            }
        }
        private bool IsCollideWithBullet(EnemyBullet[] bullets)
        {
            if (invincibleCooldown <= 0)
            {
                for (int i = 0; i < bullets.Length; i++)
                {
                    if (!bullets[i].IsFired)
                        continue;
                    if (centeredPosition.Sub(bullets[i].Point).GetLength() < sprite.Width / 2 + bullets[i].Width / 2)
                    {
                        bullets[i].IsFired = false;
                        lives--;
                        invincibleCooldown = invincibleTempo;
                        flickeringCooldown = flickeringTempo;
                        return true;
                    }
                }
            }
            return false;
        }
        private bool BulletCollideWithBullet()
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                if (!bullets[i].IsFired)
                    continue;
                if (EnemiesManager.BulletCollideWithBullet(bullets[i]))
                    return true;
            }
            return false;
        }
        private void MovementManager()
        {
            float deltaX = movementSpeed;
            sprite.Position = new Vector2(sprite.Position.X + deltaX, sprite.Position.Y);
            float maxX = sprite.Position.X + sprite.Width;
            float minX = sprite.Position.X;

            if (maxX > GfxTools.Win.width - Game.Edge)
            {
                float overflowX = maxX - (GfxTools.Win.width - Game.Edge);
                sprite.Position = new Vector2(sprite.Position.X - overflowX, sprite.Position.Y);
                deltaX -= overflowX;
            }
            else if (minX < Game.Edge)
            {
                float overflowX = minX - Game.Edge;
                sprite.Position = new Vector2(sprite.Position.X - overflowX, sprite.Position.Y);
                deltaX -= overflowX;
            }

            sprite.Translate(deltaX, 0);
        }
        private void Timer()
        {
            shootCooldown -= Game.DeltaTime;
            invincibleCooldown -= Game.DeltaTime;
            flickeringCooldown -= Game.DeltaTime;
        }

        public void Update()
        {
            MovementManager();
            UpdateBullets();
            IsCollideWithBullet(EnemiesManager.Bullets);
            BulletCollideWithBullet();
            centeredPosition = new Vector2(sprite.Position.X + sprite.Width / 2, sprite.Position.Y + sprite.Height / 2);
            Timer();
        }

        // DRAW

        private void DrawSprite()
        {
            if (!(flickeringCooldown > 0f && flickeringCooldown <= 0.2f ||
                flickeringCooldown >= 0.4f && flickeringCooldown <= 0.6f ||
                flickeringCooldown >= 0.8f && flickeringCooldown <= 1f))
            {
                sprite.Draw();
            }
        }
        private void DrawBullets()
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].Draw();
            }
        }
        private void DrawLives()
        {
            SpriteObj[] livesArray = new SpriteObj[lives];
            Vector2 livesPosition = new Vector2(Game.Edge, GfxTools.Win.height - Game.Edge);
            for (int i = 0; i < lives; i++)
            {
                livesArray[i] = new SpriteObj("Assets/player.png", livesPosition);
                livesArray[i].Draw();
                livesPosition.X += livesArray[i].Sprite.width * 1.2f;
            }
        }

        public void Draw()
        {
            DrawSprite();
            DrawBullets();
            DrawLives();
        }
    }
}