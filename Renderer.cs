using System;

/*
    Rendering for the multi-layer system.
*/

namespace fwod
{
    static class Renderer
    {
        #region Layers
        /// <summary>
        /// Multi-layered char buffer
        /// </summary>
        /// <remarks>
        /// Default: 3 layers of 24 row x 80 rolumns each
        /// 2D Arrays work like this: [ROW, COL]
        /// </remarks>
        internal static char[][,] Layers = new char[3][,]
        {
            new char[Utils.WindowHeight, Utils.WindowWidth], // Menu
            new char[Utils.WindowHeight, Utils.WindowWidth], // People
            new char[Utils.WindowHeight, Utils.WindowWidth]  // Game
        };

        /// <summary>
        /// Layer.
        /// </summary>
        /// <remarks>
        /// The higher the layer in the enumeration,
        /// the higher it will display on the console.
        /// e.g. Menu is at layer 0, which is the highest layer.
        /// People layer will display under menu characters at render
        /// </remarks>
        internal enum Layer : byte
        {
            /// <summary>
            /// Menu layer.
            /// </summary>
            Menu,
            /// <summary>
            /// Players, monsters.
            /// </summary>
            People,
            /// <summary>
            /// Walls, objects.
            /// </summary>
            Game,
            /// <summary>
            /// Output to no layers, print on screen only.
            /// </summary>
            None
        }
        #endregion

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
            if (pLayer != Layer.None)
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
            if (pLayer != Layer.None)
            {
                for (int i = 0; i < pInput.Length; i++)
                {
                    Layers[(int)pLayer][pPosY, pPosX + i] = pInput[i];
                }
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
            if (pLayer != Layer.None)
                Layers[(int)pLayer][pPosY, pPosX] = pInput;

            Console.WriteLine(pInput);
        }

        internal static void WriteLine(Layer pLayer, string pInput)
        {
            WriteLine(pLayer, pInput, Console.CursorLeft, Console.CursorTop);
        }

        internal static void WriteLine(Layer pLayer, string pInput, int pPosX, int pPosY)
        {
            if (pLayer != Layer.None)
            {
                for (int i = 0; i < pInput.Length; i++)
                {
                    Layers[(int)pLayer][pPosY, pPosX + i] = pInput[i];
                }
            }

            Console.WriteLine(pInput);
        }
        #endregion

        #region Get info
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
        /// <summary>
        /// Clears a layer and prints to console
        /// </summary>
        /// <param name="pLayer">Layer to clear</param>
        internal static void ClearLayer(Layer pLayer)
        {
            ClearLayer(pLayer, true);
        }

        /// <summary>
        /// Clears a layer
        /// </summary>
        /// <param name="pLayer">Layer to clear</param>
        /// <param name="pClearConsole">Update console</param>
        internal static void ClearLayer(Layer pLayer, bool pClearConsole)
        {
            for (int h = 0; h < Utils.WindowHeight; h++)
            {
                for (int w = 0; w < Utils.WindowWidth; w++)
                {
                    Layers[(int)pLayer][h, w] = '\0';
                }
            }

            if (pClearConsole)
                Console.Clear();
        }

        /// <summary>
        /// Clears all layers and prints to console
        /// </summary>
        internal static void ClearAllLayers()
        {
            for (int i = 0; i < Layers.Length; i++)
            {
                for (int h = 0; h < Utils.WindowHeight; h++)
                {
                    for (int w = 0; w < Utils.WindowWidth; w++)
                    {
                        Layers[i][h, w] = '\0';
                    }
                }
            }
            Console.Clear();
        }
        #endregion
    }
}