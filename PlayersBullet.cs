using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    class PlayersBullet
    {
        private Sprite sprite = new Sprite("Assets/bulletbullet.png");
        private Sprite explosionSprite = new Sprite("Assets/explosion.png");
        private Vector2 position = new Vector2(0, 0);

        private const float explosionTimer = 0.5f;

        private float explosionCooldown = 0f;
        private float movementSpeed = (float)PlayersBulletsValues.movementSpeed;
        private bool isFired = false;
        private bool isExploded = false;

        public Sprite ExplosionSprite { get { return this.explosionSprite; } }
        public Vector2 Position { get { return this.position; } set { this.position = value; } }
        public int Width { get { return this.sprite.width; } }
        public int Height { get { return this.sprite.height; } }
        public int ExplosionWidth { get { return this.explosionSprite.width; } }
        public int ExplosionHeight { get { return this.explosionSprite.height; } }
        public bool IsFired { get { return this.isFired; } set { this.isFired = value; } }
        public bool IsExploded { get { return this.isExploded; } set { this.isExploded = value; } }

        public PlayersBullet()
        {
        }

        // METHODS

        public bool ShowingExplosion()
        {
            return this.explosionCooldown <= explosionTimer;
        }
        public void IncreaseExplosionCooldown(Window window)
        {
            this.explosionCooldown += window.deltaTime;
        }
        public void ResetExplosionCooldown()
        {
            this.explosionCooldown = 0f;
        }

        // UPDATE

        private void MovementManager(Window window)
        {
            this.position.Y -= this.movementSpeed * window.deltaTime;
        }

        public void Update(Window window)
        {
            MovementManager(window);
        }

        // DRAW

        public void Draw(Window window)
        {
            GfxTools.DrawSprite(window, this.position, this.sprite);
        }
    }
}