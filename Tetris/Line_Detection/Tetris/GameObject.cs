using System;

namespace Tetris
{
    public class GameStats
    {
        // data
        private int levelNum;
        private int lineCount;
        private int gameScore;
        private Box[,] gameGrid;


        public GameStats()
        {
            this.levelNum = 0;
            this.lineCount = 0;
            this.gameScore = 0;
            this.gameGrid = new Box[30, 10];
        }

        public int getLevelNum()
        {
            return this.levelNum;
        }

        public void setLevelNum(int level)
        {
            this.levelNum = level;
        }

        public Box[,] getGameGrid()
        {
            return this.gameGrid;
        }

        //public void setGrid(int level)
        //{
        //  this.levelNum = level;
        //}

        public int getLineCount()
        {
            return this.lineCount;
        }

        public void setLineCount(int line)
        {
            this.lineCount = line;
        }

        public int getScore()
        {
            return this.gameScore;
        }

        public void setScore(int score)
        {
            this.gameScore = score;
        }


    }
}
