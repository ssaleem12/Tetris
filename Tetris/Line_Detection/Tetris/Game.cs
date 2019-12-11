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

        IrrKlang.ISoundEngine AudioEngine = null;

        IrrKlang.ISound music = null;
        public float vol_delta = 0.005f;

        IrrKlang.ISoundSource srcShoot = null;
        IrrKlang.ISound sndShoot = null;

        // Font: ----------------------------------------
        Azul.Texture pFont;

        // Demo: ----------------------------------------
        Azul.Texture pText;
        Azul.Sprite pRedBird;
        GameStats stats;
        int statsCount = 0;
        int count = 0;
        Azul.AZUL_KEY prevLeftKey = 0;
        Azul.AZUL_KEY prevRightKey = 0;
        Azul.AZUL_KEY prevUpKey = 0;
        Azul.AZUL_KEY prevDownKey = 0;
        Box[,] gameGrid;

        MainBlock currentBlock;
        MainBlock nextBlock;

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
            switch (x)
            {
                case 1:
                    return new BlockL(new Box(5, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_BLUE), new Box(6, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_BLUE), new Box(7, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_BLUE), new Box(5, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_BLUE));
                case 2:
                    return new BlockI(new Box(5, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_ORANGE), new Box(4, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_ORANGE), new Box(6, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_ORANGE), new Box(7, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_ORANGE));
                case 3:
                    return new BlockT(new Box(5, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_YELLOW), new Box(4, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_YELLOW), new Box(6, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_YELLOW), new Box(5, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_YELLOW));
                case 4:
                    return new BlockZ1(new Box(5, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_GREEN), new Box(4, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_LT_GREEN), new Box(6, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_LT_GREEN), new Box(5, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_LT_GREEN));
                case 5:
                    return new BlockZ2(new Box(5, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_CYAN), new Box(4, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_CYAN), new Box(6, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_CYAN), new Box(5, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_CYAN));
                case 6:
                    return new BlockL2(new Box(6, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_PURPLE), new Box(6, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_PURPLE), new Box(5, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_PURPLE), new Box(4, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_PURPLE));
                case 7:
                    return new BlockS(new Box(4, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_RED), new Box(4, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_RED), new Box(5, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_RED), new Box(5, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_RED));
                default:
                    return new BlockL(new Box(Constants.GAME_MIN_X, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_DK_BLUE), new Box(Constants.GAME_MIN_X + 1, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_BLUE), new Box(Constants.GAME_MIN_X + 2, Constants.GAME_MAX_Y, DrawColor.Shade.COLOR_BLUE), new Box(Constants.GAME_MIN_X, Constants.GAME_MAX_Y - 1, DrawColor.Shade.COLOR_BLUE));
            }

        }

        //Add game ending code here
        public void gameover()

        {
            SOM.drawGameOverScreen();
            gameOver.play();
            Console.WriteLine("GAME OVER");
        }

        public int checkBlockLine(Box checkBox)
        {
            if(checkBox.y >= 15)
            {
                //Console.WriteLine("GAME OVER");
                gameover();
            }
            int tempx = 0;
            while (tempx < 9 & gameGrid[checkBox.y, tempx] != null)
            {
                tempx += 1;
            }
            if (tempx == 9 & gameGrid[checkBox.y, tempx] != null)
            {
                Console.WriteLine("LINE DETECTED");
                clearLine.play();
                return checkBox.y;
            }
            else
            {
                Console.WriteLine("NO LINE DETECTED");
                return -1;
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
                    Console.WriteLine("removing line at row {0}", rows[i]);
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
            if (linesD == calcNextlevel)
            {
                stats.setLevelNum(calcNextlevel);
            }
        }

        public int checkLine(MainBlock boc)
        {

            Box blockOne = boc.b1;
            Box blockTwo = boc.b2;
            Box blockThree = boc.b3;
            Box blockFour = boc.b4;

            Console.WriteLine("{0} {1} {2} {3}", blockOne.y, blockTwo.y, blockThree.y, blockFour.y);

            int lineOne = checkBlockLine(blockOne);
            int lineTwo = checkBlockLine(blockTwo);
            int lineThre = checkBlockLine(blockThree);
            int lineFour = checkBlockLine(blockFour);


            Box[] ValidBlocks = new Box[4];
            int lineCount = 0;
            if (lineOne != -1)
            {
                ValidBlocks[0] = blockOne;
                lineCount++;
            }
            if (lineTwo != -1)
            {
                ValidBlocks[1] = blockTwo;

                lineCount++;
            }
            if (lineThre != -1)
            {
                ValidBlocks[2] = blockThree;
                lineCount++;
            }
            if (lineFour != -1)
            {
                ValidBlocks[3] = blockFour;
                lineCount++;
            }
            Console.WriteLine("Number of lines before {0}", lineCount);
            for (int i = 0; i < 4 && ValidBlocks[i] != null; i++)
            {
                for (int j = 0; j < 4 && ValidBlocks[j] != null; j++)
                {
                    if (i != j && ValidBlocks[i].y == ValidBlocks[j].y)
                    {
                        Console.WriteLine("{0} {1}", i, j);
                        ValidBlocks[j] = null;
                        lineCount--;

                    }
                }
            }

            Console.WriteLine("Number of Lines {0}", lineCount);


            removeLines(ValidBlocks);
            return lineCount;

        }

        public void scoreUpdate(int lines)
        {
            int level = stats.getLevelNum();
            int curScore = stats.getScore();
            int addedScore = 0;
            switch (lines)
            {
                case 1:
                    addedScore = 40 * (level + 1);
                    break;
                case 2:
                    addedScore = 100 * (level + 1);
                    break;
                case 3:
                    addedScore = 300 * (level + 1);
                    break;
                case 4:
                    addedScore = 1200 * (level + 1);
                    break;
                default:
                    break;
            }
            stats.setScore(curScore + addedScore);

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

            // Create the Audio Engine
            AudioEngine = new IrrKlang.ISoundEngine();

            // Play a sound file
            music = AudioEngine.Play2D("theme.wav", true);
            music.Volume = 0.2f;

            // Resident loads
            srcShoot = AudioEngine.AddSoundSourceFromFile("shoot.wav");
            sndShoot = AudioEngine.Play2D(srcShoot, false, false, false);
            sndShoot.Stop();

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

            pRedBird = new Azul.Sprite(pText, new Azul.Rect(903.0f, 797.0f, 46.0f, 46.0f), new Azul.Rect(300.0f, 100.0f, 30.0f, 30.0f));
            Debug.Assert(pRedBird != null);

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
            AudioEngine.Update();

            //-----------------------------------------------------------
            // Sound Experiments
            //-----------------------------------------------------------

            // Adjust music theme volume
            if (music.Volume > 0.30f)
            {
                vol_delta = -0.002f;
            }
            else if (music.Volume < 0.00f)
            {
                vol_delta = 0.002f;
            }
            music.Volume += vol_delta;

            //--------------------------------------------------------
            // Rotate Sprite
            //--------------------------------------------------------

            pRedBird.angle = pRedBird.angle + 0.01f;
            pRedBird.Update();

            //--------------------------------------------------------
            // Keyboard Input
            //--------------------------------------------------------

            if ((Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) && keyCount % 20 == 0) ||
                (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) && prevLeftKey == 0))
            {
                prevLeftKey = Azul.AZUL_KEY.KEY_ARROW_LEFT;
                currentBlock.moveLeft(gameGrid);
            }
            else
            {
                if (!Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT))
                {
                    prevLeftKey = 0;
                }
            }

            if ((Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) && keyCount % 20 == 0) ||
                (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) && prevRightKey == 0))
            {
                prevRightKey = Azul.AZUL_KEY.KEY_ARROW_RIGHT;
                currentBlock.moveRight(gameGrid);
            }
            else
            {
                if (!Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT))
                {
                    prevRightKey = 0;
                }
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_UP) && prevUpKey == 0)
            {
                prevUpKey = Azul.AZUL_KEY.KEY_ARROW_UP;
                //ROTATE
                currentBlock.Rotate();
                rotate.play();
            }
            else
            {
                if (!Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_UP))
                {
                    prevUpKey = 0;
                }
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_DOWN) && keyCount % 5 == 0)
            {
                prevDownKey = Azul.AZUL_KEY.KEY_ARROW_DOWN;
                //ROTATE

                currentBlock.moveDown(gameGrid);
                drop.play();

            }
            else
            {
                if (!Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_DOWN))
                {
                    prevDownKey = 0;
                }
            }
            keyCount++;
            /*
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ENTER) && prevEnterKey == 0)
            {
                prevEnterKey = Azul.AZUL_KEY.KEY_ENTER;
                sndShoot = AudioEngine.Play2D(srcShoot, false, false, false);
            }
            else
            {
                if (!Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ENTER))
                {
                    prevEnterKey = 0;
                }
            }*/

            //--------------------------------------------------------
            // Stats test
            //--------------------------------------------------------

        }


        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            // Draw sprite with texture
            pRedBird.Render();

            // Update background
            SOM.drawBackground();
            SOM.drawStrings(stats);

            //draw grid
            drawArray();

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
            if (count == 30)
            {
                count = 0;
                if (!currentBlock.moveDown(gameGrid))
                {
                    printArray();
                    int lines = checkLine(currentBlock);
                    stats.setLineCount(stats.getLineCount() + lines);
                    scoreUpdate(lines);
                    levelUpdate(stats.getLineCount());
                    currentBlock = nextBlock;
                    nextBlock = reGenerateBlock();
                }
            }
            else
            {
                count++;
            }

            /*  THIS PART MAY BE HELPFUL FOR TEAM D
             * 
             * if (count < 2 * 10)
             {

                 Shape.drawLine(Constants.PREVIEW_WINDOW_X, Constants.PREVIEW_WINDOW_Y, Shape.Orientation.ORIENT_0);

                 Shape.drawLine(3, 3, Shape.Orientation.ORIENT_0);
                 Shape.drawL1(5, 8, Shape.Orientation.ORIENT_0);
                 Shape.drawL2(5, 14, Shape.Orientation.ORIENT_0);
                 Shape.drawT(5, 19, Shape.Orientation.ORIENT_0);
                 Shape.drawZ1(5, 23, Shape.Orientation.ORIENT_0);
                 Shape.drawZ2(5, 28, Shape.Orientation.ORIENT_0);
                 Shape.drawSquare(8, 4, Shape.Orientation.ORIENT_0);

             }
             else if (count < 2 * 20)
             {
                 Shape.drawLine(Constants.PREVIEW_WINDOW_X, Constants.PREVIEW_WINDOW_Y, Shape.Orientation.ORIENT_1);

                 Shape.drawLine(3, 3, Shape.Orientation.ORIENT_1);
                 Shape.drawL1(5, 8, Shape.Orientation.ORIENT_1);
                 Shape.drawL2(5, 14, Shape.Orientation.ORIENT_1);
                 Shape.drawT(5, 19, Shape.Orientation.ORIENT_1);
                 Shape.drawZ1(5, 23, Shape.Orientation.ORIENT_1);
                 Shape.drawZ2(5, 28, Shape.Orientation.ORIENT_1);
                 Shape.drawSquare(8, 4, Shape.Orientation.ORIENT_1);

             }
             else if (count < 2 * 30)
             {
                 Shape.drawLine(Constants.PREVIEW_WINDOW_X, Constants.PREVIEW_WINDOW_Y, Shape.Orientation.ORIENT_2);

                 Shape.drawLine(3, 3, Shape.Orientation.ORIENT_2);
                 Shape.drawL1(5, 8, Shape.Orientation.ORIENT_2);
                 Shape.drawL2(5, 14, Shape.Orientation.ORIENT_2);
                 Shape.drawT(5, 19, Shape.Orientation.ORIENT_2);
                 Shape.drawZ1(5, 23, Shape.Orientation.ORIENT_2);
                 Shape.drawZ2(5, 28, Shape.Orientation.ORIENT_2);
                 Shape.drawSquare(8, 4, Shape.Orientation.ORIENT_2);

             }
             else
             {
                 Shape.drawLine(Constants.PREVIEW_WINDOW_X, Constants.PREVIEW_WINDOW_Y, Shape.Orientation.ORIENT_3);

                 Shape.drawLine(3, 3, Shape.Orientation.ORIENT_3);
                 Shape.drawL1(5, 8, Shape.Orientation.ORIENT_3);
                 Shape.drawL2(5, 14, Shape.Orientation.ORIENT_3);
                 Shape.drawT(5, 19, Shape.Orientation.ORIENT_3);
                 Shape.drawZ1(5, 23, Shape.Orientation.ORIENT_3);
                 Shape.drawZ2(5, 28, Shape.Orientation.ORIENT_3);
                 Shape.drawSquare(8, 4, Shape.Orientation.ORIENT_3);

             }

             if (count > 2 * 40)
                 count = 0;
             else
                 count++;*/
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

