using System;
using System.Collections.Generic;

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
        /// Write at current location. Cursor position unaffected.
        /// </summary>
        /// <param name="layer">Layer to output.</param>
        /// <param name="c">Character.</param>
        public static void Write(char c)
        {
            Map[Console.CursorTop, Console.CursorLeft] = c;
            Console.Write(c);
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
        /// <summary>
        /// Redraw the map from memory including people.
        /// </summary>
        public static unsafe void RedrawMap(int x, int y, int width, int height)
        {
            int lx = x + width;
            int ly = y + height;
            int ox = x;
            char[] p = new char[width];
            
            for (; y < ly; y++, x = ox)
            {
                Console.SetCursorPosition(x, y);
                    
                for (int i = 0; x < lx; ++x, ++i)
                    p[i] = Map[y, x];

                Console.Write(p);
            }

            // Place the people back on screen.
            foreach (Person peep in Game.PeopleList[Game.CurrentFloor])
                peep.Initialize();

            Game.MainPlayer.Initialize();
        }

        public static void GenerateBox(int x, int y, int width, int height)
        {
            // Top wall
            Write('┌', x, y);
            Write(new string('─', width - 2));
            Write('┐');

            // Side walls
            GenerateVerticalLine('│', height - 1, x, y + 1);
            GenerateVerticalLine('│', height - 1, x + (width - 1), y + 1);

            // Bottom wall
            Console.SetCursorPosition(x, y + (height - 1));
            Write('└');
            Write(new string('─', width - 2));
            Write('┘');
        }

        public static void GenerateVerticalLine(char c, int len, int x, int y)
        {
            int l = y + len;
            for (int i = y; i < l; i++)
            {
                Console.SetCursorPosition(x, i);
                Write(c);
            }
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

        #region Generator
        //TODO: Random map generator

        #endregion
    }
}