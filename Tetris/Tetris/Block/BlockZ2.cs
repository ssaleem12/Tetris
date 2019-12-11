using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class BlockZ2:MainBlock
    {
        Shape.Orientation currentOrientation = Shape.Orientation.ORIENT_0;

        public BlockZ2(Box b1, Box b2, Box b3, Box b4) : base(b1, b2, b3, b4)
        {
        }
        public override void drawMe()
        {
            Shape.drawZ2(b1.x, b1.y, currentOrientation);
        }
        public override void Rotate(Box[,] gameGrid)
        {
            if (currentOrientation == Shape.Orientation.ORIENT_0)
            {
                if (b1.y == 1 || b1.y > 28) return;
            }
            if (currentOrientation == Shape.Orientation.ORIENT_1)
            {
                if (b1.y == 1 || b1.y > 28 || b1.x == 0 || b1.x == 9) return;
            }
            if (currentOrientation == Shape.Orientation.ORIENT_2)
            {
                if (b1.y == 1 || b1.y > 28) return;
            }
            if (currentOrientation == Shape.Orientation.ORIENT_3)
            {
                if (b1.y == 1 || b1.y > 28 || b1.x == 0) return;
            }
            Shape.Orientation nextOrientation = this.currentOrientation;
            nextOrientation = nextOrientation + 1;
            nextOrientation = ((Shape.Orientation)(((int)nextOrientation) & 3));
            
            switch (nextOrientation)
            {
                case Shape.Orientation.ORIENT_0:
                    int y2 = b1.y - 1;
                    int y3 = b1.y;
                    int y4 = b1.y - 1;
                    int x2 = b1.x - 1;
                    int x3 = b1.x + 1;
                    int x4 = b1.x;
                    if (y2 > 29 || y3 > 29 || y4 > 29)
                    {
                        break;
                    }
                    if (gameGrid[y2, x2] != null || gameGrid[y3, x3] != null || gameGrid[y4, x4] != null)
                    {
                        break;
                    }
                    else
                    {
                        b2.y = b1.y - 1;
                        b3.y = b1.y;
                        b4.y = b1.y - 1;
                        b2.x = b1.x - 1;
                        b3.x = b1.x + 1;
                        b4.x = b1.x;
                        this.currentOrientation += 1;
                        this.currentOrientation = ((Shape.Orientation)(((int)this.currentOrientation) & 3));
                    }
                    break;

                case Shape.Orientation.ORIENT_1:
                    int x21 = b1.x - 1;
                    int x31 = b1.x;
                    int x41 = b1.x - 1;
                    int y21 = b1.y + 1;
                    int y31 = b1.y - 1;
                    int y41 = b1.y;
                    if (y21 > 29 || y31 > 29 || y41 > 29)
                    {
                        break;
                    }
                    if (gameGrid[y21, x21] != null || gameGrid[y31, x31] != null || gameGrid[y41, x41] != null)
                    {
                        break;
                    }
                    else
                    {
                        b2.x = b1.x - 1;
                        b3.x = b1.x;
                        b4.x = b1.x - 1;
                        b2.y = b1.y + 1;
                        b3.y = b1.y - 1;
                        b4.y = b1.y;
                        this.currentOrientation += 1;
                        this.currentOrientation = ((Shape.Orientation)(((int)this.currentOrientation) & 3));
                    }
                    break;

                case Shape.Orientation.ORIENT_2:
                    int y22 = b1.y + 1;
                    int y32 = b1.y;
                    int y42 = b1.y + 1;
                    int x22 = b1.x + 1;
                    int x32 = b1.x - 1;
                    int x42 = b1.x;
                    if (y22 > 29 || y32 > 29 || y42 > 29)
                    {
                        break;
                    }
                    if (gameGrid[y22, x22] != null || gameGrid[y32, x32] != null || gameGrid[y42, x42] != null)
                    {
                        break;
                    }
                    else
                    {
                        b2.y = b1.y + 1;
                        b3.y = b1.y;
                        b4.y = b1.y + 1;
                        b2.x = b1.x + 1;
                        b3.x = b1.x - 1;
                        b4.x = b1.x;
                        this.currentOrientation += 1;
                        this.currentOrientation = ((Shape.Orientation)(((int)this.currentOrientation) & 3));
                    }
                    break;

                case Shape.Orientation.ORIENT_3:
                    int x23 = b1.x + 1;
                    int x33 = b1.x + 1;
                    int x43 = b1.x;
                    int y23 = b1.y - 1;
                    int y33 = b1.y;
                    int y43 = b1.y + 1;
                    if (y23 > 29 || y33 > 29 || y43 > 29)
                    {
                        break;
                    }
                    if (gameGrid[y23, x23] != null || gameGrid[y33, x33] != null || gameGrid[y43, x43] != null)
                    {
                        break;
                    }
                    else
                    {
                        b2.y = b1.y - 1;
                        b3.y = b1.y;
                        b4.y = b1.y + 1;
                        b2.x = b1.x + 1;
                        b3.x = b1.x + 1;
                        b4.x = b1.x;
                        this.currentOrientation += 1;
                        this.currentOrientation = ((Shape.Orientation)(((int)this.currentOrientation) & 3));
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
