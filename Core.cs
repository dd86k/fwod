using System;

/*
    Core system for the multi-layer system.
*/

namespace FWoD
{
    class Core
    {
        //IDEA: Three jagged arrays (char[][][]) instead of a 2d jagged array (char[][,])?
        // Reason: Performance
        // Drawback: More memory
        /// <summary>
        /// Multi-layered char buffer
        /// </summary>
        internal static char[][,] Layers = new char[3][,]
        { // 3 layers of 25 row and 80 rolumns each
          // 2D Arrays work like this: [Y, X]
            new char[ConsoleTools.BufferHeight, ConsoleTools.BufferWidth], // Menu
            new char[ConsoleTools.BufferHeight, ConsoleTools.BufferWidth], // Bubbles
            new char[ConsoleTools.BufferHeight, ConsoleTools.BufferWidth]  // Game
        };

        /// <summary>
        /// Layer to output at. Menu=0, etc.
        /// </summary>
        internal enum Layer
        {
            Menu, Bubble, Game
        }

        //internal Layer CurrentLayer = Layer.Game;

        #region Write
        /// <summary>
        /// Write at current location.
        /// </summary>
        /// <param name="pLayer">Layer to output.</param>
        /// <param name="pInput">Character.</param>
        internal static void Write(Layer pLayer, char pInput)
        {
            Write(pLayer, pInput, Console.CursorLeft, Console.CursorTop);
        }

        /// <summary>
        /// Write at specific location.
        /// </summary>
        /// <param name="pLayer">Layer to output.</param>
        /// <param name="pInput">Character.</param>
        /// <param name="pPosX">Left position.</param>
        /// <param name="pPosY">Top position.</param>
        internal static void Write(Layer pLayer, char pInput, int pPosX, int pPosY)
        {
            Layers[(int)pLayer][pPosY, pPosX] = pInput;
            Console.SetCursorPosition(pPosX, pPosY);
            Console.Write(pInput);
        }

        /// <summary>
        /// Write at current location.
        /// </summary>
        /// <param name="pLayer">Layer to output.</param>
        /// <param name="pInput">String.</param>
        internal static void Write(Layer pLayer, string pInput)
        {
            Write(pLayer, pInput, Console.CursorLeft, Console.CursorTop);
        }

        /// <summary>
        /// Write at specific location.
        /// </summary>
        /// <param name="pLayer">Layer to output.</param>
        /// <param name="pInput">String.</param>
        /// <param name="pPosX">Left position.</param>
        /// <param name="pPosY">Top position.</param>
        internal static void Write(Layer pLayer, string pInput, int pPosX, int pPosY)
        {
            for (int i = 0; i < pInput.Length; i++)
            {
                Layers[(int)pLayer][pPosY, pPosX + i] = pInput[i];
            }

            Console.SetCursorPosition(pPosX, pPosY);
            Console.Write(pInput);
        }
        #endregion

        #region WriteLine
        /// <summary>
        /// Write at current location with newline.
        /// </summary>
        /// <param name="pLayer">Layer to output.</param>
        /// <param name="pInput">Character.</param>
        internal static void WriteLine(Layer pLayer, char pInput)
        {
            WriteLine(pLayer, pInput, Console.CursorLeft, Console.CursorTop);
        }

        internal static void WriteLine(Layer pLayer, char pInput, int pPosX, int pPosY)
        {
            Layers[(int)pLayer][pPosY, pPosX] = pInput;
            Console.WriteLine(pInput);
        }

        internal static void WriteLine(Layer pLayer, string pInput)
        {
            WriteLine(pLayer, pInput, Console.CursorLeft, Console.CursorTop);
        }

        internal static void WriteLine(Layer pLayer, string pInput, int pPosX, int pPosY)
        {
            for (int i = 0; i < pInput.Length; i++)
            {
                Layers[(int)pLayer][Console.CursorTop, Console.CursorLeft + i] = pInput[i];
            }

            Console.WriteLine(pInput);
        }
        #endregion

        #region GetInfo
        /// <summary>
        /// Get a characters from a layer at a position.
        /// </summary>
        /// <param name="pLayer">Layer.</param>
        /// <param name="pPosX">Left position.</param>
        /// <param name="pPosY">Top position.</param>
        /// <returns>Stored character.</returns>
        internal static char GetCharAt(Layer pLayer, int pPosX, int pPosY)
        {
            return Layers[(int)pLayer][pPosY, pPosX];
        }
        #endregion

        #region Clear
        internal static void ClearLayer(Layer pLayer)
        {
            for (int h = 0; h < ConsoleTools.BufferHeight; h++)
            {
                for (int w = 0; w < ConsoleTools.BufferWidth; w++)
                {
                    Layers[(int)pLayer][h, w] = '\0';
                }
            }
        }

        internal static void ClearAllLayers()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int h = 0; h < ConsoleTools.BufferHeight; h++)
                {
                    for (int w = 0; w < ConsoleTools.BufferWidth; w++)
                    {
                        Layers[i][h, w] = '\0';
                    }
                }
            }
        }
        #endregion
    }
}