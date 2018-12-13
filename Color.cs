using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    struct Color
    {
        public byte R;
        public byte G;
        public byte B;

        public Color(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }
    }
}