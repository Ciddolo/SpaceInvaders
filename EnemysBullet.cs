using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    class EnemysBullet
    {
        private Sprite spriteA = new Sprite("Assets/alienBullet1a.png");
        private Sprite spriteB = new Sprite("Assets/alienBullet1b.png");
        private Sprite explosionSprite = new Sprite("Assets/explosion.png");
        private Vector2 position = new Vector2(0, 0);

        private const float stateTimer = 0.1f;
        private const float explosionTimer = 0.5f;

        private float explosionCooldown = 0f;
        private float movementSpeed = (float)EnemysBulletsValues.movementSpeed;
        private float stateCooldown = 0f;
        private bool isFired = false;
        private bool isExploded = false;
        private bool state = true;

        public Sprite ExplosionSprite { get { return this.explosionSprite; } }
        public Vector2 Position { get { return this.position; } set { this.position = value; } }
        public int Width { get { return this.spriteA.width; } }
        public int Height { get { return this.spriteA.height; } }
        public int ExplosionWidth { get { return this.explosionSprite.width; } }
        public int ExplosionHeight { get { return this.explosionSprite.height; } }
        public bool IsFired { get { return this.isFired; } set { this.isFired = value; } }
        public bool IsExploded { get { return this.isExploded; } set { this.isExploded = value; } }

        public EnemysBullet()
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
            this.position.Y += this.movementSpeed * window.deltaTime;
        }
        private void StateChanger(Window window)
        {
            this.stateCooldown -= window.deltaTime;
            if (this.stateCooldown <= 0)
            {
                this.state = !this.state;
                this.stateCooldown = stateTimer;
            }
        }

        public void Update(Window window)
        {
            MovementManager(window);
            StateChanger(window);
        }

        // DRAW

        public void Draw(Window window)
        {
            if (this.state)
                GfxTools.DrawSprite(window, this.position, this.spriteA);
            else
                GfxTools.DrawSprite(window, this.position, this.spriteB);
        }
    }
}