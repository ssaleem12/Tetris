using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class BlockS : MainBlock
    {
        Shape.Orientation currentOrientation = Shape.Orientation.ORIENT_0;

        public BlockS(Box b1, Box b2, Box b3, Box b4) : base(b1, b2, b3, b4)
        {
        }
        public override void drawMe()
        {
            Shape.drawSquare(b1.x, b1.y, currentOrientation);
        }
        public override void Rotate()
        {
        }
    }
}
