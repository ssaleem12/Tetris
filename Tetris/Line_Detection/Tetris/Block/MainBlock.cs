using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class MainBlock
    {
        public Box b1;
        public Box b2;
        public Box b3;
        public Box b4;
        Shape.Orientation currentOrientation = Shape.Orientation.ORIENT_0;

        public MainBlock(Box b1, Box b2, Box b3, Box b4)
        {
            this.b1 = b1;
            this.b2 = b2;
            this.b3 = b3;
            this.b4 = b4;
        }

        public void moveLeft(Box[,] gameGrid)
        {
            int a = b1.x - 1;
            int b = b2.x - 1;
            int c = b3.x - 1;
            int d = b4.x - 1;

            int e = b1.y;
            int f = b2.y;
            int g = b3.y;
            int h = b4.y;

            if (a < 0 || b < 0 || c < 0 || d < 0)
            {
                return;
            }

            if (gameGrid[e, a] == null && gameGrid[f, b] == null && gameGrid[g, c] == null && gameGrid[h, d] == null)
            {
                b1.x = b1.x - 1;
                b2.x = b2.x - 1;
                b3.x = b3.x - 1;
                b4.x = b4.x - 1;
            }

        }

        public void moveRight(Box[,] gameGrid)
        {
            int a = b1.x + 1;
            int b = b2.x + 1;
            int c = b3.x + 1;
            int d = b4.x + 1;

            int e = b1.y;
            int f = b2.y;
            int g = b3.y;
            int h = b4.y;

            if (a > 9 || b > 9 || c > 9 || d > 9)
            {
                return;
            }

            if (gameGrid[e, a] == null && gameGrid[f, b] == null && gameGrid[g, c] == null && gameGrid[h, d] == null)
            {
                b1.x = b1.x + 1;
                b2.x = b2.x + 1;
                b3.x = b3.x + 1;
                b4.x = b4.x + 1;
            }

        }
        public bool moveDown(Box[,] gameGrid)
        {
            int a = b1.x;
            int b = b2.x;
            int c = b3.x;
            int d = b4.x;

            int e = b1.y - 1;
            int f = b2.y - 1;
            int g = b3.y - 1;
            int h = b4.y - 1;


            if (e < 0 || f < 0 || g < 0 || h < 0)
            {
                gameGrid[e + 1, a] = b1;
                gameGrid[f + 1, b] = b2;
                gameGrid[g + 1, c] = b3;
                gameGrid[h + 1, d] = b4;
                return false;
            }


            if (gameGrid[e, a] == null && gameGrid[f, b] == null && gameGrid[g, c] == null && gameGrid[h, d] == null)
            {
                b1.y = b1.y - 1;
                b2.y = b2.y - 1;
                b3.y = b3.y - 1;
                b4.y = b4.y - 1;

                return true;
            }
            gameGrid[e + 1, a] = b1;
            gameGrid[f + 1, b] = b2;
            gameGrid[g + 1, c] = b3;
            gameGrid[h + 1, d] = b4;

            return false;
        }
        void SetOrientation(Shape.Orientation newOrient)
        {
            this.currentOrientation = newOrient;
        }

        public virtual void Rotate()
        {

        }

        public virtual void drawMe()
        {

        }
    }
}
