using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    static class Game
    {
        static Window window;
        static Player player;
        static SpriteText scoreText;
        static bool isPause = true;
        static float pauseCooldown = 0f;

        private const float pauseTempo = 0.1f;
        public  const int Edge = 50;
        static public float DeltaTime { get { return window.deltaTime; } }

        static Game()
        {
            window = new Window(1000, 700, "Space Invaders", PixelFormat.RGB);
            GfxTools.Init(window);
            EnemiesManager.Init();
            player = new Player();
            scoreText = new SpriteText(new Vector2(window.width - Edge - 140, window.height - Edge), Player.Score.ToString("D6"));
        }

        public static void Play()
        {
            while (window.opened)
            {
                GfxTools.Clean();

                // INPUT

                if (window.GetKey(KeyCode.Esc) || window.GetKey(KeyCode.Return))
                    return;

                if (window.GetKey(KeyCode.P) && pauseCooldown <= 0)
                {
                    isPause = !isPause;
                    pauseCooldown = pauseTempo;
                }

                player.Input();

                // UPDATE

                if (!isPause && Player.Lives > 0 && EnemiesManager.EnemiesAlive > 0)
                {
                    player.Update();
                    EnemiesManager.Update();
                }
                pauseCooldown -= window.deltaTime;
                scoreText.Text = Player.Score.ToString("D6");

                // DRAW

                player.Draw();
                scoreText.Draw();
                EnemiesManager.Draw();

                if (isPause)
                {
                    GfxTools.DrawRectangle(window.width / 2 - 30, window.height / 2 - 45, 60, 90, 0, 0, 0);
                    GfxTools.DrawRectangle(window.width / 2 - 25, window.height / 2 - 40, 20, 80, 255, 255, 255);
                    GfxTools.DrawRectangle(window.width / 2 + 5, window.height / 2 - 40, 20, 80, 255, 255, 255);
                }

                window.Blit();
            }
        }
    }
}