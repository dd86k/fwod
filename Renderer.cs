using System;

/*
    Rendering for the multi-layer system.
*/

namespace fwod
{
    static class Renderer
    {
        #region Layers
        /*
         * TODO!! Remove the People and Menu layers.
         * 
         * The idea is just to have an array for the map. (Called char[,] Map)
         * The menu and people layers are useless since the menu is directly
         * drawn on the screen buffer while the people (player, enemies, etc.)
         * already have their own dictionary depending on the level.
         * 
         * Basically, removing the layer feature for simplicity. Which includes:
         * - Renaming Layers to Map, removing the two other "layers".
         * - Remove the Layer enumeration.
         * - Use the Core/Rendeder for map stuff only.
         * 
         * Will take a while to do.
         */
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
            /// Output to no layers, print directly on screen.
            /// </summary>
            None
        }
        #endregion

        #region Write
        /// <summary>
        /// Write at current location.
        /// </summary>
        /// <param name="layer">Layer to output.</param>
        /// <param name="c">Character.</param>
        internal static void Write(Layer layer, char c)
        {
            Write(layer, c, Console.CursorLeft, Console.CursorTop);
        }

        /// <summary>
        /// Write at specific location.
        /// </summary>
        /// <param name="layer">Layer to output.</param>
        /// <param name="c">Character.</param>
        /// <param name="x">Left position.</param>
        /// <param name="y">Top position.</param>
        internal static void Write(Layer layer, char c, int x, int y)
        {
            if (layer != Layer.None)
                Layers[(int)layer][y, x] = c;
            Console.SetCursorPosition(x, y);
            Console.Write(c);
        }

        /// <summary>
        /// Write at current location.
        /// </summary>
        /// <param name="layer">Layer to output.</param>
        /// <param name="text">String.</param>
        internal static void Write(Layer layer, string text)
        {
            Write(layer, text, Console.CursorLeft, Console.CursorTop);
        }

        /// <summary>
        /// Write at specific location.
        /// </summary>
        /// <param name="layer">Layer to output.</param>
        /// <param name="text">String.</param>
        /// <param name="x">Left position.</param>
        /// <param name="y">Top position.</param>
        internal static void Write(Layer layer, string text, int x, int y)
        {
            if (layer != Layer.None)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    Layers[(int)layer][y, x + i] = text[i];
                }
            }

            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }
        #endregion

        #region WriteLine
        /// <summary>
        /// Write at current location with newline.
        /// </summary>
        /// <param name="layer">Layer to output.</param>
        /// <param name="c">Character.</param>
        internal static void WriteLine(Layer layer, char c)
        {
            WriteLine(layer, c, Console.CursorLeft, Console.CursorTop);
        }

        internal static void WriteLine(Layer layer, char c, int x, int y)
        {
            if (layer != Layer.None)
                Layers[(int)layer][y, x] = c;

            Console.WriteLine(c);
        }

        internal static void WriteLine(Layer layer, string text)
        {
            WriteLine(layer, text, Console.CursorLeft, Console.CursorTop);
        }

        internal static void WriteLine(Layer layer, string text, int x, int y)
        {
            if (layer != Layer.None)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    Layers[(int)layer][y, x + i] = text[i];
                }
            }

            Console.WriteLine(text);
        }
        #endregion

        #region Get info
        /// <summary>
        /// Get a characters from a layer at a position.
        /// </summary>
        /// <param name="layer">Layer.</param>
        /// <param name="x">Left position.</param>
        /// <param name="y">Top position.</param>
        /// <returns>Stored character.</returns>
        internal static char GetCharAt(Layer layer, int x, int y)
        {
            return Layers[(int)layer][y, x];
        }
        #endregion

        #region Clear
        /// <summary>
        /// Clears a layer and prints to console
        /// </summary>
        /// <param name="layer">Layer to clear</param>
        internal static void ClearLayer(Layer layer)
        {
            ClearLayer(layer, true);
        }

        /// <summary>
        /// Clears a layer
        /// </summary>
        /// <param name="layer">Layer to clear</param>
        /// <param name="clear">Update display buffer</param>
        internal static void ClearLayer(Layer layer, bool clear)
        {
            for (int h = 0; h < Utils.WindowHeight; h++)
            {
                for (int w = 0; w < Utils.WindowWidth; w++)
                {
                    Layers[(int)layer][h, w] = '\0';
                }
            }

            if (clear)
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