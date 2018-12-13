using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    static class GfxTools
    {
        public static Window Win;

        public static void Init(Window window)
        {
            Win = window;
        }

        public static void Clean()
        {
            for (int i = 0; i < Win.bitmap.Length; i++)
            {
                Win.bitmap[i] = 0;
            }
        }
        public static void PutPixel(int x, int y, byte r, byte g, byte b)
        {
            if (x < 0 || x >= Win.width || y < 0 || y >= Win.height)
                return;
            int index = (y * Win.width + x) * 3;
            Win.bitmap[index] = r;
            Win.bitmap[index + 1] = g;
            Win.bitmap[index + 2] = b;
        }
        public static void DrawHorizontalLine(int x, int y, int width, byte r, byte g, byte b)
        {
            for (int i = 0; i < width; i++)
            {
                PutPixel(x + i, y, r, g, b);
            }
        }
        public static void DrawVerticalLine(int x, int y, int height, byte r, byte g, byte b)
        {
            for (int i = 0; i < height; i++)
            {
                PutPixel(x, y + i, r, g, b);
            }
        }
        public static void DrawRectangle(int x, int y, int width, int height, byte r, byte g, byte b)
        {
            for (int i = 0; i < height; i++)
            {
                DrawHorizontalLine(x, y + i, width, r, g, b);
            }
        }
        public static void DrawSprite(Sprite sprite, int x, int y)
        {
            for (int i = 0; i < sprite.height; i++)
            {
                for (int j = 0; j < sprite.width; j++)
                {
                    int tempX = x + j, tempY = y + i;

                    if (tempX < 0 || tempX >= Win.width || tempY < 0 || tempY >= Win.height)
                    {
                        continue;
                    }

                    int spriteByteIndex = (i * sprite.width + j) * 4;

                    byte spriteR = sprite.bitmap[spriteByteIndex];
                    byte spriteG = sprite.bitmap[spriteByteIndex + 1];
                    byte spriteB = sprite.bitmap[spriteByteIndex + 2];
                    byte spriteA = sprite.bitmap[spriteByteIndex + 3];
                    float alpha = spriteA / 255;

                    int windowByteIndex = (tempY * Win.width + tempX) * 3;

                    byte windowR = Win.bitmap[windowByteIndex];
                    byte windowG = Win.bitmap[windowByteIndex + 1];
                    byte windowB = Win.bitmap[windowByteIndex + 2];

                    Win.bitmap[windowByteIndex] = (byte)(spriteR * alpha + windowR * (1 - alpha));
                    Win.bitmap[windowByteIndex + 1] = (byte)(spriteG * alpha + windowG * (1 - alpha));
                    Win.bitmap[windowByteIndex + 2] = (byte)(spriteB * alpha + windowB * (1 - alpha));
                }
            }
        }
    }
}