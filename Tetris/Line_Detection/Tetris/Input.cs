using System;
using System.Diagnostics;

namespace Tetris
{
    public class Input
    {
        public static void KeyboardInput()
        {

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT))
            {
                //MOVE OBJECT RIGHT
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT))
            {
                //MOVE OBJECT LEFT
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_DOWN))
            {
                //MOVE OBJECT DOWN
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_UP))
            {
                //ROTATE OBJECT
            }

        }
    }
}
