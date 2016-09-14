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
        internal static void Write(char c)
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
        internal static void Write(char c, int x, int y)
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
        internal static void Write(string text)
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
        internal static void WriteLine(char c)
        {
            WriteLine(c, Console.CursorLeft, Console.CursorTop);
        }

        internal static void WriteLine(char c, int x, int y)
        {
            Console.WriteLine(Map[y, x] = c);
        }

        internal static void WriteLine(string text)
        {
            WriteLine(text, Console.CursorLeft, Console.CursorTop);
        }

        internal static unsafe void WriteLine(string text, int x, int y)
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
        /// Clears a layer
        /// </summary>
        /// <param name="layer">Layer to clear</param>
        /// <param name="clear">Update display buffer</param>
        internal static void ClearMap(bool clear = true)
        {
            for (int h = 0; h < Utils.WindowHeight; h++)
                for (int w = 0; w < Utils.WindowWidth; w++)
                    Map[h, w] = '\0';

            if (clear)
                Console.Clear();
        }
        #endregion
    }
}