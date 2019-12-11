using System;

namespace Tetris
{
    public class Box
    {
        public int x;
        public int y;
        public DrawColor.Shade boxColor;
        public Box(int i, int j, DrawColor.Shade color)
        {
            this.x = i;
            this.y = j;
            this.boxColor = color;
        }

    }

}
