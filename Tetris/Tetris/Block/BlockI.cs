using System;

namespace Tetris
{
    public class BlockI : MainBlock
    {
        Shape.Orientation currentOrientation = Shape.Orientation.ORIENT_0;

        public BlockI(Box b1, Box b2, Box b3, Box b4) : base(b1, b2, b3, b4)
        {
        }
        public override void drawMe()
        {
            Shape.drawLine(b1.x, b1.y, currentOrientation);
        }
        public override void Rotate(Box[,] gameGrid)
        {
            if (currentOrientation == Shape.Orientation.ORIENT_0)
            {
               if (b1.y < 3 || b1.y > 28) return; 
            }
            if(currentOrientation == Shape.Orientation.ORIENT_1)
            {
                if (b1.x == 9 || b4.x < 2) return;
            }
            if(currentOrientation == Shape.Orientation.ORIENT_2)
            {
                if (b1.y < 2 || b1.y > 28)  return;
            }
            if(currentOrientation == Shape.Orientation.ORIENT_3)
            {
                if (b1.x >= 8 || b4.x < 1) return;
            }

            Shape.Orientation nextOrientation = this.currentOrientation;
            nextOrientation = nextOrientation + 1;
            nextOrientation = ((Shape.Orientation)(((int)nextOrientation) & 3));
            switch (nextOrientation)
            {
                case Shape.Orientation.ORIENT_0:
                    int x2 = b1.x - 1;
                    int x3 = b1.x + 1;
                    int x4 = b1.x + 2;
                    int o = b1.y;
                    if (gameGrid[o, x2] != null || gameGrid[o, x3] != null || gameGrid[o, x4] != null)
                    {
                        break;
                    }
                    else
                    {
                        b4.y = b3.y = b2.y = b1.y;
                        b2.x = b1.x - 1;
                        b3.x = b1.x + 1;
                        b4.x = b1.x + 2;
                        this.currentOrientation += 1;
                        this.currentOrientation = ((Shape.Orientation)(((int)this.currentOrientation) & 3));
                    }
                    break;

                case Shape.Orientation.ORIENT_1:
                    int y2 = b1.y + 1;
                    int y3 = b1.y - 1;
                    int y4 = b1.y - 2;
                    int o1 = b1.x;
                    if (y2 > 29 || y3 > 29 || y4 > 29)
                    {
                        break;
                    }
                    if (gameGrid[y2, o1] != null || gameGrid[y3, o1] != null || gameGrid[y4, o1] != null)
                    {
                        break;
                    }
                    else
                    {
                        b4.x = b3.x = b2.x = b1.x;
                        b2.y = b1.y + 1;
                        b3.y = b1.y - 1;
                        b4.y = b1.y - 2;
                        this.currentOrientation += 1;
                        this.currentOrientation = ((Shape.Orientation)(((int)this.currentOrientation) & 3));
                    }
                    break;

                case Shape.Orientation.ORIENT_2:
                    int x22 = b1.x + 1;
                    int x32 = b1.x - 1;
                    int x42 = b1.x - 2;
                    int o2 = b1.y;
                    if (gameGrid[o2, x22] != null || gameGrid[o2, x32] != null || gameGrid[o2, x42] != null)
                    {
                        break;
                    }
                    else
                    {
                        b4.y = b3.y = b2.y = b1.y;
                        b2.x = b1.x + 1;
                        b3.x = b1.x - 1;
                        b4.x = b1.x - 2;
                        this.currentOrientation += 1;
                        this.currentOrientation = ((Shape.Orientation)(((int)this.currentOrientation) & 3));
                    }
                    break;

                case Shape.Orientation.ORIENT_3:
                    int y23 = b1.y - 1;
                    int y33 = b1.y + 1;
                    int y43 = b1.y + 2;
                    int o3 = b1.x;
                    if (y23 > 29 || y33 > 29 || y43 > 29)
                    {
                        break;
                    }
                    if (gameGrid[y23, o3] != null || gameGrid[y33, o3] != null || gameGrid[y43, o3] != null)
                    {
                        break;
                    }
                    else
                    {
                        b4.x = b3.x = b2.x = b1.x;
                        b2.y = b1.y - 1;
                        b3.y = b1.y + 1;
                        b4.y = b1.y + 2;
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
