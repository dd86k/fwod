using System;

/*
    Rendering for the multi-layer system.
*/

namespace fwod
{
    static class MapManager
    {
        public static char[,] Map;

        #region Write
        /// <summary>
        /// Write at current location.
        /// </summary>
        /// <param name="layer">Layer to output.</param>
        /// <param name="c">Character.</param>
        public static void Write(char c)
        {
            Write(c, Console.CursorLeft, Console.CursorTop);
        }

        /// <summary>
        /// Write at specific location.
        /// </summary>
        /// <param name="layer">Layer to output.</param>
        /// <param name="c">Character.</param>
        /// <param name="x">Left position.</param>
        /// <param name="y">Top position.</param>
        public static void Write(char c, int x, int y)
        {
            Map[y, x] = c;
            Console.SetCursorPosition(x, y);
            Console.Write(c);
        }

        /// <summary>
        /// Write at current location.
        /// </summary>
        /// <param name="layer">Layer to output.</param>
        /// <param name="text">String.</param>
        public static void Write(string text)
        {
            Write(text, Console.CursorLeft, Console.CursorTop);
        }

        /// <summary>
        /// Write at specific location.
        /// </summary>
        /// <param name="layer">Layer to output.</param>
        /// <param name="text">String.</param>
        /// <param name="x">Left position.</param>
        /// <param name="y">Top position.</param>
        public static unsafe void Write(string text, int x, int y)
        {
            fixed (char* pt = text)
            {
                for (int i = 0; i < text.Length; i++)
                    Map[y, x + i] = *(pt + i);
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
        public static void WriteLine(char c)
        {
            WriteLine(c, Console.CursorLeft, Console.CursorTop);
        }

        public static void WriteLine(char c, int x, int y)
        {
            Console.WriteLine(Map[y, x] = c);
        }

        public static void WriteLine(string text)
        {
            WriteLine(text, Console.CursorLeft, Console.CursorTop);
        }

        public static unsafe void WriteLine(string text, int x, int y)
        {
            fixed (char* pt = text)
            {
                for (int i = 0; i < text.Length; i++)
                    Map[y, x + i] = *(pt + i);
            }

            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
        }
        #endregion

        #region Helpers
        public static unsafe void RedrawMap(int x, int y, int width, int height)
        {
            int lx = x + width;
            int ly = y + height;
            int ox = x;
            string buffer = new string('\0', width);

            fixed (char* p = buffer)
            {
                for (; y < ly; y++)
                {
                    Console.SetCursorPosition(x, y);
                    
                    for (; x < lx; x++)
                        p[x] = Map[y, x];
                    x = ox;

                    Console.Write(buffer);
                }
            }

            // Place the people back on screen.
            foreach (Person p in Game.PeopleList[Game.CurrentFloor])
                p.Initialize();

            Game.MainPlayer.Initialize();
        }

        public static void GenerateBox(int x, int y, int width, int height)
        {
            // Top wall
            Write('┌', x, y);
            Utils.GenerateHorizontalLineMap('─', width - 2);
            Write('┐');

            // Side walls
            Console.SetCursorPosition(x, y + 1);
            Utils.GenerateVerticalLineMap('│', height - 1);
            Console.SetCursorPosition(x + (width - 1), y + 1);
            Utils.GenerateVerticalLineMap('│', height - 1);

            // Bottom wall
            Console.SetCursorPosition(x, y + (height - 1));
            Write('└');
            Utils.GenerateHorizontalLineMap('─', width - 2);
            Write('┘');
        }
        #endregion

        #region Clear
        /// <summary>
        /// Clears the current map.
        /// </summary>
        /// <param name="clear">Update display buffer</param>
        public static void ClearMap(bool clear = true)
        {
            Map = new char[Utils.WindowHeight, Utils.WindowWidth];

            if (clear)
                Console.Clear();
        }
        #endregion
    }
}