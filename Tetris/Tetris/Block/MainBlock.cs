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
        Sound instaDown = new Sound("space.wav");

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


            if (gameGrid[e , a] == null && gameGrid[f, b] == null && gameGrid[g, c] == null && gameGrid[h, d] == null)
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
        public bool instantDown(Box[,] gameGrid)
        {
            instaDown.play();
            int[] storeFourY = new int[4];

            storeFourY = findFourY(gameGrid);
            int maxYIndex = FindMaxIndex(storeFourY);
            Console.WriteLine(storeFourY[0]+" " + storeFourY[1] + " " + storeFourY[2] + " "  + storeFourY[3] + " ");

            int prevYValue;
            int originY = 0;

            switch (maxYIndex)
            {
                case 0:

                    prevYValue = b1.y;
                    originY = DoubleCheck(prevYValue, storeFourY, maxYIndex, gameGrid);
                    b1.y = originY + 1;
                    b2.y = originY + (b2.y - prevYValue) + 1;
                    b3.y = originY + (b3.y - prevYValue) + 1;
                    b4.y = originY + (b4.y - prevYValue) + 1;
                    break;

                case 1:
                    prevYValue = b2.y;
                    originY = DoubleCheck(prevYValue, storeFourY, maxYIndex, gameGrid);
                    b2.y = originY + 1;
                    b1.y = originY + (b1.y - prevYValue) + 1;
                    b3.y = originY + (b3.y - prevYValue) + 1;
                    b4.y = originY + (b4.y - prevYValue) + 1;
                    break;

                case 2:
                    prevYValue = b3.y;
                    originY = DoubleCheck(prevYValue, storeFourY, maxYIndex, gameGrid);
                    b3.y = originY +1;
                    b1.y = originY + (b1.y - prevYValue) + 1;
                    b2.y = originY + (b2.y - prevYValue) + 1;
                    b4.y = originY + (b4.y - prevYValue) + 1;
                    break;

                case 3:
                    prevYValue = b4.y;
                    originY = DoubleCheck(prevYValue, storeFourY, maxYIndex, gameGrid);
                    b4.y = originY +1;
                    b1.y = originY + (b1.y - prevYValue) + 1;
                    b2.y = originY + (b2.y - prevYValue) + 1;
                    b3.y = originY + (b3.y - prevYValue) + 1;
                    break;
                default:
                    break;

            }

            return false;
        }
        private int DoubleCheck(int prevYValue, int[] storeFourY, int maxYIndex, Box[,] gameGrid)
        {
            int possibleYValue = storeFourY[maxYIndex];
            bool landingOne = false;
            bool landingTwo = false;
            bool landingThree = false;
            bool landingFour = false;
            bool goodLanding = false;


            while (goodLanding == false)
            {
                //situation where longer bricks can go through the grid
                bool safeGroundFour = checklandingFour(possibleYValue + b4.y - prevYValue + 1);
                
                if (safeGroundFour == true)
                {
                    landingOne = gameGrid[possibleYValue + b1.y - prevYValue + 1, b1.x] == null;
                    landingTwo = gameGrid[possibleYValue + b2.y - prevYValue + 1, b2.x] == null;
                    landingThree = gameGrid[possibleYValue + b3.y - prevYValue + 1, b3.x] == null;
                    landingFour = gameGrid[possibleYValue + b4.y - prevYValue + 1, b4.x] == null;
                }
                else
                {
                    possibleYValue += 2;
                    landingOne = true;
                    landingTwo = true;
                    landingThree = true;
                    landingFour = true;
                }


                if (landingOne && landingTwo && landingThree && landingFour)
                {
                    goodLanding = true;
                }
                else
                {
                    possibleYValue += 1;
                }

            }
            return possibleYValue;
        }
        private int[] findFourY(Box[,] gameGrid)
        {
            int[] tempY = new int[4];
            tempY[0] = findMaxY(gameGrid, b1);
            tempY[1] = findMaxY(gameGrid, b2);
            tempY[2] = findMaxY(gameGrid, b3);
            tempY[3] = findMaxY(gameGrid, b4);
            return tempY;
        }

        private int findMaxY(Box[,] gameGrid, Box checkBox)
        {
            int tempY = 0;

            for (int i = 0; i < checkBox.y; i++)
            {
                if ((gameGrid[i, checkBox.x] != null))
                {
                    tempY = i;
                }
            }
            return tempY; 
        }

        private int FindMaxIndex(int[] tempArray)
        {
            int maxIndex = 0;
            int maxValue = tempArray[0];
            for (int i = 0; i < tempArray.Length; i++)
            {
                if (maxValue < tempArray[i])
                {
                    maxValue = tempArray[i];
                    maxIndex = i;
                }
            }
            return maxIndex;
        }

        private bool checklandingFour(int block4Location)
        {
            bool safe = true;
            if(block4Location == -1)
            {
                safe = false;
            }
            return safe;
        }

        void SetOrientation(Shape.Orientation newOrient)
        {
            this.currentOrientation = newOrient;
        }

        public virtual void Rotate(Box[,] gameGrid)
        {

        }

        public virtual void drawMe()
        {

        }
    }
}
