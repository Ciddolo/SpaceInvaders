using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    class PlayersBulletssManager
    {
        private PlayersBullet[] bullets = new PlayersBullet[(int)PlayersBulletsValues.sizeOfArray];

        public PlayersBullet[] Bullets { get { return this.bullets; } }

        public PlayersBulletssManager()
        {
            for (int i = 0; i < this.bullets.Length; i++)
            {
                this.bullets[i] = new PlayersBullet();
            }
        }

        // METHODS

        // UPDATE

        private bool IsCollideWithBullet(EnemysBullet[] bullets)
        {
            for (int i = 0; i < this.bullets.Length; i++)
            {
                if (!this.bullets[i].IsFired)
                {
                    continue;
                }
                for (int j = 0; j < bullets.Length; j++)
                {
                    if (!bullets[j].IsFired)
                    {
                        continue;
                    }
                    Vector2 enemysBulletsPoint = new Vector2(bullets[j].Position.X + bullets[j].Width / 2, bullets[j].Position.Y + bullets[j].Height);
                    Vector2 bulletsPoint = new Vector2(this.bullets[i].Position.X + this.bullets[i].Width / 2, this.bullets[i].Position.Y);
                    if (bulletsPoint.Sub(enemysBulletsPoint).GetLength() <= this.bullets[i].Width / 2 + bullets[j].Width / 2)
                    {
                        this.bullets[i].IsFired = false;
                        this.bullets[i].IsExploded = true;
                        bullets[j].IsFired = false;
                        return true;
                    }
                }
            }
            return false;
        }
        private void ExplosionManager(Window window, PlayersBullet bullet)
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
        private void UpdateAllBullets(Window window, EnemysBullet[] bullets)
        {
            for (int i = 0; i < this.bullets.Length; i++)
            {
                ExplosionManager(window, this.bullets[i]);
                if (!this.bullets[i].IsFired)
                    continue;
                this.bullets[i].Update(window);
                if (this.bullets[i].Position.Y < (float)FieldsValues.verticalEdges / 2)
                {
                    this.bullets[i].IsFired = false;
                    this.bullets[i].IsExploded = true;
                }
            }
            IsCollideWithBullet(bullets);
        }

        public void Update(Window window, EnemysBullet[] bullets)
        {
            UpdateAllBullets(window, bullets);
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
                    Vector2 explosionPosition = new Vector2(bullets[i].Position.X - bullets[i].ExplosionWidth / 2, bullets[i].Position.Y - bullets[i].ExplosionHeight);
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