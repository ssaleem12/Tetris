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
        public override void Rotate()
        {

            this.currentOrientation += 1;
            this.currentOrientation = ((Shape.Orientation)(((int)this.currentOrientation) & 3));

            switch (this.currentOrientation)
            {
                case Shape.Orientation.ORIENT_0:
                    b3.y = b2.y = b1.y;
                    b4.y = b1.y - 1;
                    b2.x = b1.x - 1;
                    b3.x = b1.x + 1;
                    b4.x = b1.x;
                    break;

                case Shape.Orientation.ORIENT_1:
                    b3.x = b2.x = b1.x;
                    b4.x = b1.x - 1;
                    b2.y = b1.y + 1;
                    b3.y = b1.y - 1;
                    b4.y = b1.y;
                    break;

                case Shape.Orientation.ORIENT_2:
                    b3.y = b2.y = b1.y;
                    b4.y = b1.y + 1;
                    b2.x = b1.x + 1;
                    b3.x = b1.x - 1;
                    b4.x = b1.x;
                    break;

                case Shape.Orientation.ORIENT_3:
                    b3.x = b2.x = b1.x;
                    b4.x = b1.x + 1;
                    b2.y = b1.y - 1;
                    b3.y = b1.y + 1;
                    b4.y = b1.y;
                    break;

                default:
                    break;
            }
            while (b1.x > Constants.GAME_MAX_X || b2.x > Constants.GAME_MAX_X || b3.x > Constants.GAME_MAX_X || b4.x > Constants.GAME_MAX_X)
            {
                b1.x -= 1;
                b2.x -= 1;
                b3.x -= 1;
                b4.x -= 1;
            }

            while (b1.x < Constants.GAME_MIN_X || b2.x < Constants.GAME_MIN_X || b3.x < Constants.GAME_MIN_X || b4.x < Constants.GAME_MIN_X)
            {
                b1.x += 1;
                b2.x += 1;
                b3.x += 1;
                b4.x += 1;
            }
        }

    }
}
