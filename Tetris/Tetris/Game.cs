using System;
using System.Diagnostics;

namespace Tetris
{
    class Tetris : Azul.Game
    {
        // Data: --------------------------------------------

        // Audio: ---------------------------------------
        Sound theme = new Sound("theme.wav");
        Sound rotate = new Sound("shoot.wav");
        Sound drop = new Sound("fall.wav");
        Sound clearLine = new Sound("clear.wav");
        Sound gameOver = new Sound("gameover.wav");
        Sound spaceBar = new Sound("space.wav");

        //IrrKlang.ISoundEngine AudioEngine = null;

        //IrrKlang.ISound music = null;
        public float vol_delta = 0.005f;

        //IrrKlang.ISoundSource srcShoot = null;
        //IrrKlang.ISound sndShoot = null;

        // Font: ----------------------------------------
        Azul.Texture pFont;

        // Demo: ----------------------------------------
        Azul.Texture pText;
        GameStats stats;
        //int statsCount = 0;
        int count = 0;
        Azul.AZUL_KEY prevLeftKey = 0;
        Azul.AZUL_KEY prevRightKey = 0;
        Azul.AZUL_KEY prevUpKey = 0;
        Azul.AZUL_KEY prevDownKey = 0;
        Azul.AZUL_KEY BackSpaceKey = 0;
        Azul.AZUL_KEY SpaceKey = 0;
        Azul.AZUL_KEY pause = 0;
        Box[,] gameGrid;

        MainBlock currentBlock;
        MainBlock nextBlock;
        int speed = 30;
        int movementSpeed = 15;
        int downSpeed = 5;
        int nextLevel = 10;

        bool isGameDone = false;
        bool gamePaused = false;
        bool dropPlayed = false;
        bool startMenu = true;

        bool landedBlock = false;
        bool canPressSpace = true;

        public void drawArray()
        {
            for (int row = 0; row < gameGrid.GetLength(0); row++)
            {
                for (int col = 0; col < gameGrid.GetLength(1); col++)
                {
                    Box box = gameGrid[row, col];

                    if (gameGrid[row, col] != null)
                    {
                        SOM.drawBox(col, row, box.boxColor);
                    }
                }
            }
        }

        public void printArray()
        {
            for (int row = 0; row < gameGrid.GetLength(0); row++)
            {
                for (int col = 0; col < gameGrid.GetLength(1); col++)
                {
                    Box box = gameGrid[row, col];

                    if (gameGrid[row, col] != null)
                    {
                        Console.WriteLine("NOT NULL");
                    }
                }
            }
        }
        public MainBlock reGenerateBlock()
        {
            Random rnd = new Random();
            int x = rnd.Next(1, 8);
            int i  = (rnd.Next() * 65536);
            Console.WriteLine(i);
            switch (x)
            {
                case 1:
                    if (gameGrid[Constants.GAME_MAX_Y, 5] != null ||
                        gameGrid[Constants.GAME_MAX_Y, 6] != null ||
                        gameGrid[Constants.GAME_MAX_Y, 7] != null ||
                        gameGrid[Constants.GAME_MAX_Y - 1, 5] != null)
                    {
                        gameover();
                    }
                    return new BlockL(new Box(5, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_BLUE), new Box(6, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_BLUE), new Box(7, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_BLUE), new Box(5, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_BLUE));
                case 2:
                    if (gameGrid[Constants.GAME_MAX_Y, 5] != null ||
                        gameGrid[Constants.GAME_MAX_Y, 4] != null ||
                        gameGrid[Constants.GAME_MAX_Y, 6] != null ||
                        gameGrid[Constants.GAME_MAX_Y, 7] != null)
                    {
                        gameover();
                    }
                    return new BlockI(new Box(5, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_ORANGE), new Box(4, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_ORANGE), new Box(6, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_ORANGE), new Box(7, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_ORANGE));
                case 3:
                    if (gameGrid[Constants.GAME_MAX_Y, 5] != null ||
                        gameGrid[Constants.GAME_MAX_Y, 4] != null ||
                        gameGrid[Constants.GAME_MAX_Y, 6] != null ||
                        gameGrid[Constants.GAME_MAX_Y - 1, 5] != null)
                    {
                        gameover();
                    }
                    return new BlockT(new Box(5, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_YELLOW), new Box(4, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_YELLOW), new Box(6, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_YELLOW), new Box(5, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_YELLOW));
                case 4:
                    if (gameGrid[Constants.GAME_MAX_Y, 5] != null ||
                        gameGrid[Constants.GAME_MAX_Y, 4] != null ||
                        gameGrid[Constants.GAME_MAX_Y - 1, 6] != null ||
                        gameGrid[Constants.GAME_MAX_Y - 1, 5] != null)
                    {
                        gameover();
                    }
                    return new BlockZ1(new Box(5, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_GREEN), new Box(4, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_LT_GREEN), new Box(6, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_LT_GREEN), new Box(5, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_LT_GREEN));
                case 5:
                    if (gameGrid[Constants.GAME_MAX_Y, 5] != null ||
                        gameGrid[Constants.GAME_MAX_Y - 1, 4] != null ||
                        gameGrid[Constants.GAME_MAX_Y, 6] != null ||
                        gameGrid[Constants.GAME_MAX_Y - 1, 5] != null)
                    {
                        gameover();
                    }
                    return new BlockZ2(new Box(5, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_CYAN), new Box(4, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_CYAN), new Box(6, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_CYAN), new Box(5, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_CYAN));
                case 6:
                    if (gameGrid[Constants.GAME_MAX_Y, 6] != null ||
                        gameGrid[Constants.GAME_MAX_Y - 1, 5] != null ||
                        gameGrid[Constants.GAME_MAX_Y, 4] != null ||
                        gameGrid[Constants.GAME_MAX_Y, 5] != null)
                    {
                        gameover();
                    }
                    return new BlockL2(new Box(6, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_PURPLE), new Box(6, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_PURPLE), new Box(5, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_PURPLE), new Box(4, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_PURPLE));
                case 7:
                    if (gameGrid[Constants.GAME_MAX_Y, 4] != null ||
                        gameGrid[Constants.GAME_MAX_Y - 1, 4] != null ||
                        gameGrid[Constants.GAME_MAX_Y, 5] != null ||
                        gameGrid[Constants.GAME_MAX_Y - 1, 5] != null)
                    {
                        gameover();
                    }
                    return new BlockS(new Box(4, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_RED), new Box(4, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_RED), new Box(5, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_RED), new Box(5, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_RED));
                default:
                    return new BlockL(new Box(Constants.GAME_MIN_X, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_BLUE), new Box(Constants.GAME_MIN_X + 1, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_BLUE), new Box(Constants.GAME_MIN_X + 2, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_BLUE), new Box(Constants.GAME_MIN_X, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_BLUE));
            }

        }

        //Add game ending code here
        private void gameover()

        {
            gameOver.play();
            theme.gameOver();
            stats.UpdateHighScores();
            isGameDone = true;
        }

        private void resetGame()
        {
            clearGrid();
            stats.setLevelNum(1);
            stats.setLineCount(0);
            stats.setScore(0);
            speed = 30;
            theme.play(true);
            landedBlock = false;
            canPressSpace = true;

        }

        private void clearGrid()
        {
            for (int i = 0; i <= 29; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    gameGrid[i, j] = null;
                }
            }
        }

        public bool checkBlockLine(Box checkBox)
        {
            if (checkBox.y == 30)
            {

                gameover();
            }
            int tempx = 0;
            while ((tempx < 9) && (gameGrid[checkBox.y, tempx]) != null)
            {
                tempx += 1;
            }
            if ((tempx == 9) && (gameGrid[checkBox.y, tempx]) != null)
            {
                clearLine.play();
                return true;
            }
            else
            {
                return false;
            }

        }

        public void removeLines(Box[] linesToRemove)
        {
            int[] rows = new int[4];
            for (int i = 0; i < 4; i++)
            {
                if (linesToRemove[i] == null)
                {
                    rows[i] = -1;
                }
                else
                {
                    rows[i] = linesToRemove[i].y;
                }

            }

            Array.Sort(rows);

            for (int i = 0; i < 4; i++)
            {
                if (rows[i] >= 0)
                {
                    //Console.WriteLine("removing line at row {0}", rows[i]);
                    removeLine(rows[i]);
                    for (int j = i; j < 4; j++)
                    {
                        rows[j] = rows[j] - 1;
                    }
                }
            }

        }


        private void removeLine(int row)
        {
            for (int i = 0; i <= Constants.GAME_MAX_X; i++)
            {
                for (int j = row; j <= Constants.GAME_MAX_Y - 1; j++)
                {
                    gameGrid[j, i] = gameGrid[j + 1, i];
                }

            }
        }


        public void levelUpdate(int lines)
        {
            int curLevel = stats.getLevelNum();
            int calcNextlevel = curLevel + 1;
            int linesD = lines / 10;
            if (lines >= nextLevel)
            {
                if (speed != 1)
                {
                    double tempSpeed = speed / 1.5;
                    Console.WriteLine("Speed double {0}", tempSpeed);
                    speed = Convert.ToInt32(tempSpeed);
                    Console.WriteLine("Speed int {0}", speed);

                }

                nextLevel = nextLevel + 10;
                stats.setLevelNum(calcNextlevel);
            }
        }

        

        public int checkLine(MainBlock boc)
        {

            Box blockOne = boc.b1;
            Box blockTwo = boc.b2;
            Box blockThree = boc.b3;
            Box blockFour = boc.b4;

            bool lineOne = checkBlockLine(blockOne);
            bool lineTwo = checkBlockLine(blockTwo);
            bool lineThre = checkBlockLine(blockThree);
            bool lineFour = checkBlockLine(blockFour);


            Box[] ValidBlocks = new Box[4];
            int lineCount = 0;
            if (lineOne == true)
            {
                ValidBlocks[0] = blockOne;
                lineCount++;
            }
            if (lineTwo == true)
            {
                ValidBlocks[1] = blockTwo;
                lineCount++;
            }
            if (lineThre == true)
            {
                ValidBlocks[2] = blockThree;
                lineCount++;
            }
            if (lineFour == true)
            {
                ValidBlocks[3] = blockFour;
                lineCount++;
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if((ValidBlocks[i] != null) && (ValidBlocks[j] != null))
                    {
                        if (i != j && ValidBlocks[i].y == ValidBlocks[j].y)
                        {
                            ValidBlocks[j] = null;
                            lineCount--;

                        }
                    }
                }
            }

            //Console.WriteLine("Number of Lines to reomve {0}", lineCount);
            removeLines(ValidBlocks);
            return lineCount;

        }

        

        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            // Game Window Device setup
            this.SetWindowName("Tetris Framework");
            this.SetWidthHeight(Constants.SCREEN_WIDTH, Constants.SCREEN_HEIGHT);
            this.SetClearColor(0.4f, 0.4f, 0.8f, 1.0f);
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            //---------------------------------------------------------------------------------------------------------
            // Audio
            //---------------------------------------------------------------------------------------------------------
            theme.play(true);
            
            /*
            // Create the Audio Engine
            AudioEngine = new IrrKlang.ISoundEngine();

            // Play a sound file
            music = AudioEngine.Play2D("theme.wav",true);
            music.Volume = 0.2f;

            // Resident loads
            srcShoot = AudioEngine.AddSoundSourceFromFile("shoot.wav");
            sndShoot = AudioEngine.Play2D(srcShoot, false, false, false);
            sndShoot.Stop();
            */
            //---------------------------------------------------------------------------------------------------------
            // Setup Font
            //---------------------------------------------------------------------------------------------------------

            // Font - texture
            pFont = new Azul.Texture("consolas20pt.tga");
            Debug.Assert(pFont != null);

            GlyphMan.AddXml("Consolas20pt.xml", pFont);

            //---------------------------------------------------------------------------------------------------------
            // Load the Textures
            //---------------------------------------------------------------------------------------------------------

            // Red bird texture
            pText = new Azul.Texture("unsorted.tga");
            Debug.Assert(pText != null);

            //---------------------------------------------------------------------------------------------------------
            // Create Sprites
            //---------------------------------------------------------------------------------------------------------


            //---------------------------------------------------------------------------------------------------------
            // Demo variables
            //---------------------------------------------------------------------------------------------------------
            stats = new GameStats();

            gameGrid = stats.getGameGrid();

            currentBlock = reGenerateBlock();
            nextBlock = reGenerateBlock();


        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------
        int keyCount = 0;
        public override void Update()
        {
            // Snd update - Need to be called once a frame
            //AudioEngine.Update();            

            //-----------------------------------------------------------
            // Sound Experiments
            //-----------------------------------------------------------

            // Adjust music theme volume
            /*
            if (music.Volume > 0.30f)
            {
                vol_delta = -0.002f;
            }
            else if (music.Volume < 0.00f)
            {
                vol_delta = 0.002f;
            }
            music.Volume += vol_delta;
            */
            //--------------------------------------------------------
            // Rotate Sprite
            //--------------------------------------------------------


            //--------------------------------------------------------
            // Keyboard Input
            //--------------------------------------------------------
            Input.checkInput(ref pause
                , ref startMenu
                , ref gamePaused
                , ref theme
                , ref isGameDone
                , ref canPressSpace
                , ref currentBlock
                , ref nextBlock
                , ref prevLeftKey
                , ref keyCount
                , ref movementSpeed
                , ref gameGrid
                , ref prevRightKey
                , ref prevUpKey
                , ref prevDownKey
                , ref BackSpaceKey
                , ref SpaceKey
                , ref rotate
                , ref dropPlayed
                , ref downSpeed
                , ref drop
                , ref landedBlock
                , resetGame);
            

        }


        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {

            // Update background
            SOM.drawBackground();
            SOM.drawStrings(stats);

            //draw grid
            drawArray();
            if(startMenu == false)
            {
                //Draw the preview window - ADDING A TEMPORARY IF STATEMENT
                if (nextBlock.GetType().ToString().Equals("Tetris.BlockI"))
                {
                    Shape.drawLine(Constants.PREVIEW_WINDOW_X, Constants.PREVIEW_WINDOW_Y, Shape.Orientation.ORIENT_0);

                }
                if (nextBlock.GetType().ToString().Equals("Tetris.BlockL"))
                {
                    Shape.drawL1(Constants.PREVIEW_WINDOW_X, Constants.PREVIEW_WINDOW_Y, Shape.Orientation.ORIENT_3);
                }
                if (nextBlock.GetType().ToString().Equals("Tetris.BlockT"))
                {
                    Shape.drawT(Constants.PREVIEW_WINDOW_X, Constants.PREVIEW_WINDOW_Y, Shape.Orientation.ORIENT_0);
                }
                if (nextBlock.GetType().ToString().Equals("Tetris.BlockL2"))
                {
                    Shape.drawL2(Constants.PREVIEW_WINDOW_X, Constants.PREVIEW_WINDOW_Y, Shape.Orientation.ORIENT_0);
                }
                if (nextBlock.GetType().ToString().Equals("Tetris.BlockS"))
                {
                    Shape.drawSquare(Constants.PREVIEW_WINDOW_X, Constants.PREVIEW_WINDOW_Y, Shape.Orientation.ORIENT_3);
                }
                if (nextBlock.GetType().ToString().Equals("Tetris.BlockZ1"))
                {
                    Shape.drawZ1(Constants.PREVIEW_WINDOW_X, Constants.PREVIEW_WINDOW_Y, Shape.Orientation.ORIENT_0);
                }
                if (nextBlock.GetType().ToString().Equals("Tetris.BlockZ2"))
                {
                    Shape.drawZ2(Constants.PREVIEW_WINDOW_X, Constants.PREVIEW_WINDOW_Y, Shape.Orientation.ORIENT_0);
                }

                // Draw the current piece
                currentBlock.drawMe();

            }



            if (isGameDone == false && gamePaused)
            {
                SOM.PausedScreen();
               
            }

            if (startMenu)
            {
                SOM.StartScreen();
            }
            if (count == speed)
            {
                count = 0;

                if (!gamePaused && !startMenu && !currentBlock.moveDown(gameGrid))
                {
                    int lines = checkLine(currentBlock);
                    stats.setLineCount(stats.getLineCount() + lines);
                    stats.scoreUpdate(lines);
                    levelUpdate(stats.getLineCount());

                    landedBlock = true;
                    canPressSpace = true;
                    if (isGameDone == false)
                    {
                        currentBlock = nextBlock;
                        nextBlock = reGenerateBlock();
                        landedBlock = false;
                    }


                }
            }
            else
            {
                count++;
            }

            if(isGameDone == true)
            {
                Debug.WriteLine("GAME OVER!");
                gameOver.gameOver();
                SOM.GameOverScreen(stats);
            }


        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {

        }

    }
}

