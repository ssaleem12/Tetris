using System;

namespace Tetris
{
    static class SOM  // SpriteObjecManager i.e. SOM
    {
        static public void drawStrings(GameStats stats)
        {
            int levels = stats.getLevelNum();
            int lines = stats.getLineCount();
            int score = stats.getScore();

            SpriteFont LevelLabel = new SpriteFont("Level " + levels, 280, 300);
            SpriteFont LineslLabel = new SpriteFont("Lines " + lines, 280, 275);
            SpriteFont ScoreLabel = new SpriteFont("Score " + score, 280, 250);
            SpriteFont PauseLabel = new SpriteFont("Press P to Pause", 280, 200);
            SpriteFont QuitLabel = new SpriteFont("Press Q to Exit", 280, 180);

            PauseLabel.Draw();
            LevelLabel.Draw();
            LineslLabel.Draw();
            ScoreLabel.Draw();
            QuitLabel.Draw();
        }

        static public void drawInternal(int xPos, int yPos, DrawColor.Shade inColor)
        {
            // This is draw in painted order
            // Draw the color big box first, then the inside..
            Azul.SpriteSolidBox smallBlock = new Azul.SpriteSolidBox(new Azul.Rect(xPos, yPos, Constants.BOX_SIZE - 4, Constants.BOX_SIZE - 4),
                                                                     DrawColor.getColor(inColor));
            smallBlock.Update();

            Azul.SpriteSolidBox bigBlock = new Azul.SpriteSolidBox(new Azul.Rect(xPos, yPos, Constants.BOX_SIZE, Constants.BOX_SIZE),
                                                                   DrawColor.getColor(DrawColor.Shade.COLOR_GREY));
            bigBlock.Update();

            // Draw
            bigBlock.Render();
            smallBlock.Render();
        }

        public static void drawBox(int xPos, int yPos, DrawColor.Shade inColor)
        {
            // This is draw in painted order
            // Draw the color big box first, then the inside.
            int x = (xPos + 1) * Constants.BOX_SIZE + Constants.BOX_SIZE_HALF;
            int y = (yPos + 1) * Constants.BOX_SIZE + Constants.BOX_SIZE_HALF;

            drawInternal(x, y, inColor);
        }

        static public void drawPreviewWindow(int xPos, int yPos, int sizeX, int sizeY, DrawColor.Shade inColor, DrawColor.Shade outColor)
        {
            // This is draw in painted order
            // Draw the color big box first, then the inside..

            Azul.SpriteSolidBox smallBlock = new Azul.SpriteSolidBox(new Azul.Rect(xPos, yPos, sizeX - 4, sizeY - 4),
                                                                     DrawColor.getColor(inColor));
            smallBlock.Update();

            Azul.SpriteSolidBox bigBlock = new Azul.SpriteSolidBox(new Azul.Rect(xPos, yPos, sizeX, sizeY),
                                                                   DrawColor.getColor(DrawColor.Shade.COLOR_GREY));
            bigBlock.Update();

            // draw
            bigBlock.Render();
            smallBlock.Render();
        }

        static public void drawBackground()
        {
            int i;

            // Draw the bottom Bar
            int start_x = Constants.BOX_SIZE_HALF;

            for (i = 0; i < 12; i++)
            {
                drawInternal(start_x + i * Constants.BOX_SIZE, Constants.BOX_SIZE_HALF, DrawColor.Shade.COLOR_DK_GREY);
            }

            // Draw the left and right bar
            start_x = 11 * Constants.BOX_SIZE + Constants.BOX_SIZE_HALF;

            for (i = 0; i < 31; i++)
            {
                drawInternal(start_x, Constants.BOX_SIZE_HALF + i * Constants.BOX_SIZE, DrawColor.Shade.COLOR_DK_GREY);
                drawInternal(Constants.BOX_SIZE_HALF, Constants.BOX_SIZE_HALF + i * Constants.BOX_SIZE, DrawColor.Shade.COLOR_DK_GREY);
            }

            // preview window
            drawPreviewWindow((Constants.PREVIEW_WINDOW_X + 1) * Constants.BOX_SIZE + Constants.BOX_SIZE_HALF,
                               (Constants.PREVIEW_WINDOW_Y + 1) * Constants.BOX_SIZE + Constants.BOX_SIZE_HALF,
                               9 * Constants.BOX_SIZE, 7 * Constants.BOX_SIZE,
                               DrawColor.Shade.COLOR_BACKGROUND_CUSTOM,
                               DrawColor.Shade.COLOR_DK_GREY);
        }

        static public void GameOverScreen(GameStats stats)
        {
            
            SpriteFont goLabel = new SpriteFont("GAME OVER!", 140, 500);
            SpriteFont taLabel = new SpriteFont(" PRESS BACKSPACE ", 100, 470);
            SpriteFont taLabel2 = new SpriteFont("  TO TRY AGAIN! ", 100, 450);
            SpriteFont exLabel = new SpriteFont(" PRESS Q TO EXIT! ", 100, 420);



            drawPreviewWindow(200, 380, 300, 500, DrawColor.Shade.COLOR_BLUE, DrawColor.Shade.COLOR_RED);

            drawHighScores(stats);
            goLabel.Draw();
            taLabel.Draw();
            taLabel2.Draw();
            exLabel.Draw();
        }
        static public void PausedScreen()
        {

            SpriteFont sLabel = new SpriteFont("GAME PAUSED!", 130, 340);
            SpriteFont bLabel = new SpriteFont(" PRESS P TO CONTINUE!", 65, 280);
           // SpriteFont b2Label2 = new SpriteFont("TO CONTINUE! ", 100, 280);

            drawPreviewWindow(200, 300, 300, 150, DrawColor.Shade.COLOR_BACKGROUND_CUSTOM, DrawColor.Shade.COLOR_RED);

            sLabel.Draw();
            bLabel.Draw();
           // b2Label2.Draw();

        }
        static public void StartScreen()
        {

            SpriteFont sLabel = new SpriteFont("TETRIS!", 160, 340);
            SpriteFont bLabel = new SpriteFont(" PRESS ENTER TO BEGIN!", 60, 280);
            //SpriteFont b2Label2 = new SpriteFont("TO BEGIN! ", 100, 280);

            drawPreviewWindow(200, 300, 300, 150, DrawColor.Shade.COLOR_BACKGROUND_CUSTOM, DrawColor.Shade.COLOR_RED);

            sLabel.Draw();
            bLabel.Draw();
           // b2Label2.Draw();

        }

        static public void drawHighScores(GameStats stats)
        {
            SpriteFont hsLabel1 = new SpriteFont("  HIGH SCORES ", 100, 380);
            SpriteFont hsLabel2 = new SpriteFont("NAME        SCORE ", 100, 350);
            Tuple<String, int>[] hScores = stats.getHighScores();


            SpriteFont hsc1 = new SpriteFont("#1 " + hScores[0].Item1, 70, 320);
            SpriteFont hsc2 = new SpriteFont(" " + hScores[0].Item2, 240, 320);
            SpriteFont hsc3 = new SpriteFont("#2 " + hScores[1].Item1, 70, 300);
            SpriteFont hsc4 = new SpriteFont(" " + hScores[1].Item2, 240, 300);
            SpriteFont hsc5 = new SpriteFont("#3 " + hScores[2].Item1, 70, 280);
            SpriteFont hsc6 = new SpriteFont(" " + hScores[2].Item2, 240, 280);
            SpriteFont hsc7 = new SpriteFont("#4 " + hScores[3].Item1, 70, 260);
            SpriteFont hsc8 = new SpriteFont(" " + hScores[3].Item2, 240, 260);
            SpriteFont hsc9 = new SpriteFont("#5 " + hScores[4].Item1, 70, 240);
            SpriteFont hsc10 = new SpriteFont(" " + hScores[4].Item2, 240, 240);

            hsLabel1.Draw();
            hsLabel2.Draw();
            hsc1.Draw();
            hsc6.Draw();
            hsc2.Draw();
            hsc3.Draw();
            hsc4.Draw();
            hsc5.Draw();
            hsc6.Draw();
            hsc7.Draw();
            hsc8.Draw();
            hsc9.Draw();
            hsc10.Draw();
        }
    }
}
