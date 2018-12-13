using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    static class EnemiesManager
    {
        static Enemy[] enemies = new Enemy[55];
        static EnemyBullet[] bullets = new EnemyBullet[10];
        static int enemiesAlive = 55;
        static int rows = 5;
        static int columns = 11;
        static int standardWidth = 48;
        static int standardHeight = 32;
        static int bulletIndex = 0;
        static float movementSpeed = 10;
        static float shotCooldown = 0f;
        static int[] lowestRow = new int[columns];

        const float shotTempo = 1f;

        public static int EnemiesAlive { get { return enemiesAlive; } set { enemiesAlive = value; } }
        public static EnemyBullet[] Bullets { get { return bullets; } }

        public static void Init()
        {
            int enemyIndex = 0;
            int padding = 14;
            float positionX = GfxTools.Win.width / 2 - ((48 + 13) * 11) / 2;
            float positionY = Game.Edge * 2;
            Vector2 position = new Vector2(positionX, positionY);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (i == 0)
                    {
                        position.X += 8;
                        enemies[enemyIndex] = new Enemy(3, position);
                        position.X += standardWidth + padding - 8;
                    }
                    else if (i == 1 || i == 2)
                    {
                        position.X += 2;
                        enemies[enemyIndex] = new Enemy(2, position);
                        position.X += standardWidth + padding - 2;
                    }
                    else if (i == 3 || i == 4)
                    {
                        enemies[enemyIndex] = new Enemy(1, position);
                        position.X += standardWidth + padding;
                    }
                    enemies[enemyIndex].MovementSpeed = movementSpeed;
                    enemyIndex++;
                }
                position.X = positionX;
                position.Y += standardHeight + padding;
            }
            lowestRow = GetLowestRowAlive();
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = new EnemyBullet();
            }
        }

        // UPDATE

        public static int[] GetLowestRowAlive()
        {
            int[] lowestRowAlive = new int[columns];

            int index = -1;
            int lowestRowAliveIndex = 0;
            bool columnAlive = false;

            for (int i = columns; i > 0; i--)
            {
                for (int j = rows; j > 0; j--)
                {
                    index = columns * j - i;

                    if (enemies[index].IsAlive)
                    {
                        lowestRowAlive[lowestRowAliveIndex] = index;
                        lowestRowAliveIndex++;
                        columnAlive = true;
                        break;
                    }
                    else
                    {
                        columnAlive = false;
                    }
                }
                if (!columnAlive)
                {
                    lowestRowAlive[lowestRowAliveIndex] = -1;
                    lowestRowAliveIndex++;
                }
            }
            return lowestRowAlive;
        }
        private static int GetRightmostColumnAlive()
        {
            int index = -1;
            bool columnAlive = false;
            for (int i = 1; i <= columns; i++)
            {
                for (int j = rows; j > 0; j--)
                {
                    index = columns * j - i;

                    if (enemies[index].IsAlive)
                    {
                        columnAlive = true;
                        break;
                    }
                    else
                    {
                        columnAlive = false;
                    }
                }
                if (columnAlive)
                {
                    return index;
                }
            }
            return -1;
        }
        private static int GetLeftmostColumnAlive()
        {
            int index = -1;
            bool columnAlive = false;
            for (int i = columns; i > 0; i--)
            {
                for (int j = rows; j > 0; j--)
                {
                    index = columns * j - i;

                    if (enemies[index].IsAlive)
                    {
                        columnAlive = true;
                        break;
                    }
                    else
                    {
                        columnAlive = false;
                    }
                }
                if (columnAlive)
                {
                    return index;
                }
            }
            return -1;
        }
        private static void MovementManager()
        {
            int index;
            if (movementSpeed > 0)
            {
                index = GetRightmostColumnAlive();
                if (index >= 0)
                {
                    if (enemies[index].SpritePosition.X + enemies[index].Width > GfxTools.Win.width - Game.Edge)
                    {
                        movementSpeed *= -1;
                        for (int i = 0; i < enemies.Length; i++)
                        {
                            enemies[i].SpritePosition = new Vector2(enemies[i].SpritePosition.X, enemies[i].SpritePosition.Y + standardHeight / 4);
                            enemies[i].MovementSpeed = movementSpeed;
                        }
                    }
                }
            }
            else
            {
                index = GetLeftmostColumnAlive();
                if (index >= 0)
                {
                    if (enemies[index].SpritePosition.X < Game.Edge)
                    {
                        movementSpeed *= -1;
                        for (int i = 0; i < enemies.Length; i++)
                        {
                            enemies[i].SpritePosition = new Vector2(enemies[i].SpritePosition.X, enemies[i].SpritePosition.Y + standardHeight / 4);
                            enemies[i].MovementSpeed = movementSpeed;
                        }
                    }
                }
            }
        }
        private static void MovementSpeedmodifier()
        {
            if (movementSpeed > 0)
                movementSpeed += 2;
            else
                movementSpeed -= 2;
            if (enemiesAlive % columns == 0)
            {
                movementSpeed *= 1.5f;
            }
            if (enemiesAlive == 1)
            {
                if (movementSpeed > 0)
                    movementSpeed = 1000;
                else
                    movementSpeed = -1000;
            }
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].MovementSpeed = movementSpeed;
            }
        }
        public static bool IsCollideWithBullet(PlayerBullet bullet)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if(enemies[i].IsCollideWithBullet(bullet))
                {
                    enemiesAlive--;     
                    MovementSpeedmodifier();
                    lowestRow = GetLowestRowAlive();
                    return true;
                }                
            }
            return false;
        }
        public static bool BulletCollideWithBullet(PlayerBullet bullet)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                if (!bullets[i].IsFired)
                    continue;
                if (bullets[i].Point.Sub(bullet.Point).GetLength() < bullets[i].Sprite.Width / 1.5f + bullet.Width / 1.5f ||
                    bullets[i].Point.Sub(new Vector2(bullet.Point.X, bullet.Sprite.Height)).GetLength() < bullets[i].Sprite.Width / 1.5f + bullet.Width / 1.5f)
                {
                    bullets[i].IsFired = false;
                    bullet.IsFired = false;
                    return true;
                }
            }
            return false;
        }
        private static int Shooter()
        {
            Random random = new Random();
            return lowestRow[random.Next(lowestRow.Length)];
        }
        private static void Shoot()
        {
            int shooterIndex = Shooter();
            if (shotCooldown <= 0)
            {
                if (shooterIndex >= 0)
                {
                    Vector2 shooterPosition = enemies[shooterIndex].CenteredPosition;
                    shooterPosition.Y += standardHeight / 2;
                    bullets[bulletIndex].IsFired = true;
                    bullets[bulletIndex].SetSpritePosition(shooterPosition);
                    bulletIndex++;
                    if (bulletIndex >= bullets.Length)
                    {
                        bulletIndex = 0;
                    }
                    shotCooldown = shotTempo;
                }
            }
            shotCooldown -= Game.DeltaTime;
        }
        private static void Landing()
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (!enemies[i].IsAlive)
                    continue;
                if (enemies[i].SpritePosition.Y + enemies[i].Height> GfxTools.Win.height - Game.Edge * 1.5f)
                    Player.Lives = 0;
            }
        }

        public static void UpdateBullets()
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].Update();
            }
        }
        public static void UpdateEnemies()
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].Update();
            }
        }

        public static void Update()
        {
            MovementManager();
            Landing();
            UpdateEnemies();
            Shoot();
            UpdateBullets();
            Mothership.Update();
        }

        // DRAW

        private static void DrawBullets()
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].Draw();
            }
        }
        private static void DrawEnemies()
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].Draw();
            }
        }

        public static void Draw()
        {
            DrawEnemies();
            DrawBullets();
            Mothership.Draw();
        }
    }
}