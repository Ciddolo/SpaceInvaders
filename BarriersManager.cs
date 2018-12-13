using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    class BarriersManager
    {
        private Barrier[] barriers = new Barrier[(int)BarriersValues.sizeOfArray];

        public Barrier[] Barriers { get { return this.barriers; } }

        public BarriersManager(Window window)
        {
            Vector2 position = new Vector2(window.width / 10 + (float)BarriersValues.side / 2, window.height - (float)FieldsValues.horizontalEdge * 2.5f);
            for (int i = 0; i < barriers.Length; i++)
            {
                barriers[i] = new Barrier(position);
                position.X += (float)BarriersValues.side + window.width / 10;
            }
        }

        // METHODS

        // UPDATE

        private bool IsCollideWithEnemy(Enemy[] enemies)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (!enemies[i].IsAlive)
                {
                    continue;
                }
                for (int j = 0; j < this.barriers.Length; j++)
                {
                    if (!this.barriers[j].IsAlive)
                    {
                        continue;
                    }
                    //Vector2 barriersCenter = new Vector2(this.barriers[j].Position.X + this.barriers[j].Side / 2, this.barriers[j].Position.Y + this.barriers[j].Side / 2);
                    Vector2 enemysCenter = new Vector2(enemies[i].PositionX + enemies[i].Width / 2, enemies[i].PositionY - enemies[i].Height * 2);
                    //if (enemysCenter.Sub(barriersCenter).GetLength() <= enemies[i].Width / 2 + this.barriers[j].Side / 2)
                    //{
                        for (int z = 0; z < this.barriers[j].Bricks.Length; z++)
                        {
                            if (!this.barriers[j].Bricks[z].IsAlive)
                            {
                                continue;
                            }
                            Vector2 bricksCenter = new Vector2(this.barriers[j].Bricks[z].Position.X + this.barriers[j].Bricks[z].Side / 2, this.barriers[j].Bricks[z].Position.Y + this.barriers[j].Bricks[z].Side / 2);
                            if (bricksCenter.Sub(enemysCenter).GetLength() <= (int)BarriersValues.side / 2 + enemies[i].Width / 2)
                            {
                                this.barriers[j].Bricks[z].IsAlive = false;
                                return true;
                            }
                        }
                    //}
                }
            }
            return false;
        }
        private bool IsCollideWithPlayerBullet(PlayersBullet[] bullets)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                if (!bullets[i].IsFired)
                {
                    continue;
                }
                for (int j = 0; j < this.barriers.Length; j++)
                {
                    if (!this.barriers[j].IsAlive)
                        continue;
                    Vector2 barriersCenter = new Vector2(this.barriers[j].Position.X + this.barriers[j].Side / 2, this.barriers[j].Position.Y + this.barriers[j].Side / 2);
                    Vector2 bulletsPoint = new Vector2(bullets[i].Position.X + bullets[i].Width / 2, bullets[i].Position.Y);
                    if (barriersCenter.Sub(bulletsPoint).GetLength() <= this.barriers[j].Side / 2 + bullets[i].Width * 10)
                    {
                        for (int z = this.barriers[j].Bricks.Length - 1; z >= 0; z--)
                        {
                            if (!this.barriers[j].Bricks[z].IsAlive)
                            {
                                continue;
                            }
                            Vector2 bricksCenter = new Vector2((float)(this.barriers[j].Bricks[z].Position.X + this.barriers[j].Bricks[z].Side / 2), (float)(this.barriers[j].Bricks[z].Position.Y + this.barriers[j].Bricks[z].Side / 2));
                            if (bricksCenter.Sub(bulletsPoint).GetLength() <= this.barriers[j].Bricks[z].Side / 2 + bullets[i].Width / 2)
                            {
                                this.barriers[j].Bricks[z].IsAlive = false;
                                bullets[i].IsFired = false;
                                bullets[i].IsExploded = true;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        private bool IsCollideWithEnemiesBullet(EnemysBullet[] bullets)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                if (!bullets[i].IsFired)
                {
                    continue;
                }
                for (int j = 0; j < this.barriers.Length; j++)
                {
                    if (!this.barriers[j].IsAlive)
                        continue;
                    Vector2 barriersCenter = new Vector2(this.barriers[j].Position.X + this.barriers[j].Side / 2, this.barriers[j].Position.Y + this.barriers[j].Side / 2);
                    Vector2 bulletsPoint = new Vector2(bullets[i].Position.X + bullets[i].Width / 2, bullets[i].Position.Y + bullets[i].Height);
                    if (barriersCenter.Sub(bulletsPoint).GetLength() <= this.barriers[j].Side / 2 + bullets[i].Width / 2)
                    {
                        for (int z = 0; z < this.barriers[j].Bricks.Length; z++)
                        {
                            if (!this.barriers[j].Bricks[z].IsAlive)
                            {
                                continue;
                            }
                            Vector2 bricksCenter = new Vector2(this.barriers[j].Bricks[z].Position.X + this.barriers[j].Bricks[z].Side / 2, this.barriers[j].Bricks[z].Position.Y + this.barriers[j].Bricks[z].Side / 2);
                            if (bricksCenter.Sub(bulletsPoint).GetLength() <= this.barriers[j].Bricks[z].Side / 2 + bullets[i].Width / 2)
                            {
                                this.barriers[j].Bricks[z].IsAlive = false;
                                bullets[i].IsFired = false;
                                bullets[i].IsExploded = true;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void Update(Window window, PlayersBulletssManager playersBulletssManager, EnemiesManager enemiesManager)
        {
            if (IsCollideWithEnemy(enemiesManager.Enemies) ||
                IsCollideWithPlayerBullet(playersBulletssManager.Bullets) ||
                IsCollideWithEnemiesBullet(enemiesManager.BulletssManager.Bullets))
            {
                for (int i = 0; i < barriers.Length; i++)
                {
                    barriers[i].Update(window);
                }
            }
        }

        // DRAW

        public void Draw(Window window)
        {
            for (int i = 0; i < barriers.Length; i++)
            {
                barriers[i].Draw(window);
            }
        }
    }
}