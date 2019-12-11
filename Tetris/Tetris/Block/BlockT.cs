using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class BlockT : MainBlock
    {

        Shape.Orientation currentOrientation = Shape.Orientation.ORIENT_0;

        public BlockT(Box b1, Box b2, Box b3, Box b4) : base(b1, b2, b3, b4)
        {
        }
        public override void drawMe()
        {
            Shape.drawT(b1.x, b1.y, currentOrientation);
        }
        public override void Rotate(Box[,] gameGrid)
        {
            if (currentOrientation == Shape.Orientation.ORIENT_0)
            {
                if (b1.y == 0 || b1.y > 28) return; 
            }
            if (currentOrientation == Shape.Orientation.ORIENT_1)
            {
                if (b1.x == 9 || b1.y == 0) return;
            }
            if (currentOrientation == Shape.Orientation.ORIENT_2)
            {
                if (b1.y == 0 || b1.y > 28) return;
            }
            if (currentOrientation == Shape.Orientation.ORIENT_3)
            {
               if (b1.x == 0 || b1.y == 0)  return; 
            }
            Shape.Orientation nextOrientation = this.currentOrientation;
            nextOrientation = nextOrientation + 1;
            nextOrientation = ((Shape.Orientation)(((int)nextOrientation) & 3));
            switch (nextOrientation)
            {
                case Shape.Orientation.ORIENT_0:
                    int o = b1.y;
                    int y4 = b1.y - 1;
                    int x2 = b1.x - 1;
                    int x3 = b1.x + 1;
                    int x4 = b1.x;

                    if(y4 > 29)
                    {
                        break;
                    }
                    if (gameGrid[o, x2] != null || gameGrid[o, x3] != null || gameGrid[y4, x4] != null)
                    {
                        break;
                    }
                    else
                    {
                        b3.y = b2.y = b1.y;
                        b4.y = b1.y - 1;
                        b2.x = b1.x - 1;
                        b3.x = b1.x + 1;
                        b4.x = b1.x;
                        this.currentOrientation += 1;
                        this.currentOrientation = ((Shape.Orientation)(((int)this.currentOrientation) & 3));
                    }
                    break;

                case Shape.Orientation.ORIENT_1:
                    int o1 = b1.x;
                    int x41 = b1.x - 1;
                    int y21 = b1.y + 1;
                    int y31 = b1.y - 1;
                    int y41 = b1.y;

                    if (y41 > 29 || y31 > 29 || y21 > 29)
                    {
                        break;
                    }
                    if (gameGrid[y41, x41] != null || gameGrid[y31, o1] != null || gameGrid[y41, o1] != null)
                    {
                        break;
                    }
                    else
                    {
                        b3.x = b2.x = b1.x;
                        b4.x = b1.x - 1;
                        b2.y = b1.y + 1;
                        b3.y = b1.y - 1;
                        b4.y = b1.y;
                        this.currentOrientation += 1;
                        this.currentOrientation = ((Shape.Orientation)(((int)this.currentOrientation) & 3));
                    }
                    break;

                case Shape.Orientation.ORIENT_2:
                    int o2 = b1.y;
                    int y42 = b1.y + 1;
                    int x22 = b1.x + 1;
                    int x32 = b1.x - 1;
                    int x42 = b1.x;

                    if (y42 > 29)
                    {
                        break;
                    }
                    if (gameGrid[o2, x22] != null || gameGrid[o2, x32] != null || gameGrid[y42, x42] != null)
                    {
                        break;
                    }
                    else
                    {
                        b3.y = b2.y = b1.y;
                        b4.y = b1.y + 1;
                        b2.x = b1.x + 1;
                        b3.x = b1.x - 1;
                        b4.x = b1.x;
                        this.currentOrientation += 1;
                        this.currentOrientation = ((Shape.Orientation)(((int)this.currentOrientation) & 3));
                    }
                    break;

                case Shape.Orientation.ORIENT_3:
                    int o3 = b1.x;
                    int x43 = b1.x + 1;
                    int y23 = b1.y - 1;
                    int y33 = b1.y + 1;
                    int y43 = b1.y;

                    if (y43 > 29 || y33 > 29 || y23 > 29)
                    {
                        break;
                    }
                    if (gameGrid[y43, x43] != null || gameGrid[y33, o3] != null || gameGrid[y43, o3] != null)
                    {
                        break;
                    }
                    else
                    {
                        b3.x = b2.x = b1.x;
                        b4.x = b1.x + 1;
                        b2.y = b1.y - 1;
                        b3.y = b1.y + 1;
                        b4.y = b1.y;
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
