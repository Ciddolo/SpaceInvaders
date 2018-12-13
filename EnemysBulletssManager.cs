using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    class EnemysBulletssManager
    {
        private EnemysBullet[] bullets = new EnemysBullet[(int)EnemysBulletsValues.sizeOfArray];

        public EnemysBullet[] Bullets { get { return this.bullets; } }

        public EnemysBulletssManager()
        {
            for (int i = 0; i < this.bullets.Length; i++)
            {
                this.bullets[i] = new EnemysBullet();
            }
        }

        // METHODS

        // UPDATE

        private void DestroyAllBulletsHittingFloor(Player player)
        {
            for (int i = 0; i < this.bullets.Length; i++)
            {
                if (!this.bullets[i].IsFired)
                    continue;
                if (this.bullets[i].Position.Y >= player.Position.Y + player.Height)
                {
                    this.bullets[i].IsFired = false;
                    this.bullets[i].IsExploded = true;
                }
            }
        }
        private void ExplosionManager(Window window, EnemysBullet bullet)
        {
            if (bullet.IsExploded)
            {
                if (bullet.ShowingExplosion())
                {
                    bullet.IncreaseExplosionCooldown(window);
                }
                else
                {
                    bullet.IsExploded = false;
                    bullet.ResetExplosionCooldown();
                }
            }
        }
        private void UpdateAllBullets(Window window)
        {
            for (int i = 0; i < this.bullets.Length; i++)
            {
                ExplosionManager(window, bullets[i]);
                if (!this.bullets[i].IsFired)
                    continue;
                this.bullets[i].Update(window);
            }
        }

        public void Update(Window window, Player player)
        {
            UpdateAllBullets(window);
            DestroyAllBulletsHittingFloor(player);
        }

        // DRAW

        private void DrawAllBullets(Window window)
        {
            for (int i = 0; i < this.bullets.Length; i++)
            {
                if (this.bullets[i].IsFired)
                    this.bullets[i].Draw(window);
                if (this.bullets[i].IsExploded)
                {
                    Vector2 explosionPosition = new Vector2(bullets[i].Position.X - bullets[i].ExplosionWidth / 2, bullets[i].Position.Y + bullets[i].ExplosionHeight);
                    GfxTools.DrawSprite(window, explosionPosition, this.bullets[i].ExplosionSprite);
                }
            }
        }

        public void Draw(Window window)
        {
            DrawAllBullets(window);
        }
    }
}