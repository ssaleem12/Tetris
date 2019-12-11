using System;
using System.Diagnostics;

namespace Tetris
{
    public class GameStats
    {
        // data
        private int levelNum;
        private int lineCount;
        private int gameScore;
        private Box[,] gameGrid;
        Tuple<String, int>[] highScores = {
            Tuple.Create("",5000),Tuple.Create("",4000),Tuple.Create("",3000),Tuple.Create("",2000),Tuple.Create("",1000)
            };


        public GameStats()
        {
            this.levelNum = 1;
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

        public void scoreUpdate(int lines)
        {
            int level = getLevelNum();
            int curScore = getScore();
            int addedScore = 0;
            switch (lines)
            {
                case 1:
                    addedScore = 40 * (level);
                    break;
                case 2:
                    addedScore = 100 * (level);
                    break;
                case 3:
                    addedScore = 300 * (level);
                    break;
                case 4:
                    addedScore = 1200 * (level);
                    break;
                default:
                    break;
            }
            setScore(curScore + addedScore);

        }

        public void UpdateHighScores()
        {
            bool change = false;
            int score = getScore();
            if (score >= highScores[4].Item2)
            {
                highScores[4] = Tuple.Create("YOU", score);
                change = true;
            }
            if (change == true)
            { 
                sortHighScores();
            }
        }

        public void sortHighScores()
        {
            for(int i = 4; i > 0; i--)
            {
                if(highScores[i].Item2 > highScores[i - 1].Item2)
                {
                    Tuple<String, int> temp = Tuple.Create(highScores[i - 1].Item1, highScores[i - 1].Item2);
                    Debug.WriteLine("{0}", temp.Item2);
                    highScores[i - 1] = Tuple.Create(highScores[i].Item1, highScores[i].Item2);
                    highScores[i] = temp;
                    
                }
            }
        }

        public Tuple<String, int>[] getHighScores()
        {
            return highScores;
        }


    }
}
