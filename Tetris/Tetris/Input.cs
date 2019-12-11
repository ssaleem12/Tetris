using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    static class Input
    {
        public static void checkInput(ref Azul.AZUL_KEY pause
            , ref bool startMenu
            , ref bool gamePaused
            , ref Sound theme
            , ref bool isGameDone
            , ref bool canPressSpace
            , ref MainBlock currentBlock
            , ref MainBlock nextBlock
            , ref Azul.AZUL_KEY prevLeftKey
            , ref int keyCount
            , ref int movementSpeed
            , ref Box[,] gameGrid
            , ref Azul.AZUL_KEY prevRightKey
            , ref Azul.AZUL_KEY prevUpKey
            , ref Azul.AZUL_KEY prevDownKey
            , ref Azul.AZUL_KEY BackSpaceKey
            , ref Azul.AZUL_KEY SpaceKey
            , ref Sound rotate
            , ref bool dropPlayed
            , ref int downSpeed
            , ref Sound drop
            , ref bool landedBlock
            , Action resetGame)
        {
            if (((Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_P)) && pause == 0) && startMenu == false)
            {
                pause = Azul.AZUL_KEY.KEY_P;
                if (gamePaused)
                {
                    gamePaused = false;
                    theme.unpause();
                }
                else
                {
                    gamePaused = true;
                    theme.pause();
                }
            }
            else
            {
                if (!Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_P))
                {
                    pause = 0;
                }
            }

            if (((Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ENTER)) && startMenu))
            {
                startMenu = false;
            }

            if (!gamePaused && !isGameDone && !startMenu)
            {
                if (canPressSpace == true)
                {
                    if ((Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) && prevLeftKey == 0) ||
                    (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) && keyCount % movementSpeed == 0))
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

                    if ((Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) && prevRightKey == 0) ||
                        (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) && keyCount % movementSpeed == 0))
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
                        currentBlock.Rotate(gameGrid);
                        rotate.play();
                    }
                    else
                    {
                        if (!Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_UP))
                        {
                            prevUpKey = 0;
                        }
                    }
                }

                if ((Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_DOWN) && prevDownKey == 0) ||
                    (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_DOWN) && keyCount % downSpeed == 0))
                {
                    prevDownKey = Azul.AZUL_KEY.KEY_ARROW_DOWN;
                    //ROTATE

                    currentBlock.moveDown(gameGrid);

                    if (dropPlayed == false)
                    {
                        drop.play();
                        dropPlayed = true;
                    }

                }
                else
                {
                    if (!Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_DOWN))
                    {
                        prevDownKey = 0;
                        dropPlayed = false;
                    }
                }

                if (landedBlock == false && canPressSpace == true)
                {

                    if (((Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE)) && SpaceKey == 0))
                    {
                        SpaceKey = Azul.AZUL_KEY.KEY_SPACE;
                        currentBlock.instantDown(gameGrid);
                        canPressSpace = false;

                    }
                    else
                    {
                        if (!Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE))
                        {
                            SpaceKey = 0;
                        }
                    }

                }
            }
            if (isGameDone == true)
            {
                if ((Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_BACKSPACE) && BackSpaceKey == 0))
                {
                    prevDownKey = Azul.AZUL_KEY.KEY_BACKSPACE;
                    isGameDone = false;
                    resetGame();

                }
                else
                {
                    if (!Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_BACKSPACE))
                    {
                        BackSpaceKey = 0;
                    }
                }

            }
            if ((Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_Q)))
            {
                System.Environment.Exit(0);
            }
            keyCount++;

        }
        }
    }