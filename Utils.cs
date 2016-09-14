using System;
using System.Text;

/*
    Various tools for the command prompt and terminal.
*/

namespace fwod
{
    static class Utils
    {
        #region Properties
        /// <summary>
        /// Initial window height. ANSI/ISO screen size.
        /// </summary>
        internal const int WindowHeight = 24;
        /// <summary>
        /// Initial window width. ANSI/ISO screen size.
        /// </summary>
        internal const int WindowWidth = 80;
        #endregion

        #region Center text (Normal)
        /// <summary>
        /// Center text to middle and write
        /// </summary>
        /// <param name="text">Input text</param>
        static internal void WriteAndCenter(string text)
        {
            WriteAndCenter(text, Console.CursorTop);
        }

        /// <summary>
        /// Center text to middle and write to a specific top position
        /// </summary>
        /// <param name="text">Input text</param>
        /// <param name="ypos">Top position</param>
        static internal void WriteAndCenter(string text, int ypos)
        {
            // Calculate the starting position
            int start = (WindowWidth / 2) - (text.Length / 2);

            // If the text is longer than the buffer, set it to 0
            start = start + text.Length > WindowWidth ? 0 : start;

            // Print away at the current cursor height (top)
            Console.SetCursorPosition(start, ypos);
            Console.Write(text);
        }

        /// <summary>
        /// Center text to middle and write, then moves a line foward
        /// </summary>
        /// <param name="text">Input text</param>
        static internal void WriteLineAndCenter(string text)
        {
            WriteLineAndCenter(text, Console.CursorTop);
        }

        /// <summary>
        /// Center text to middle and write, then moves a line foward
        /// </summary>
        /// <param name="text">Input text</param>
        /// <param name="ypos">Top position</param>
        static internal void WriteLineAndCenter(string text, int ypos)
        {
            WriteAndCenter(text, ypos);
            Console.SetCursorPosition(0, ypos + 1);
        }
        #endregion

        #region Center text (Core.cs)
        /// <summary>
        /// Center text to middle and write
        /// </summary>
        /// <param name="text">Input text</param>
        static internal void WriteAndCenterCore(string text)
        {
            WriteAndCenter(text, Console.CursorTop);
        }

        /// <summary>
        /// Center text to middle and write to a specific top position
        /// </summary>
        /// <param name="text">Input text</param>
        /// <param name="ypos">Top position</param>
        static internal void WriteAndCenter(Renderer.Layer layer, string text, int ypos)
        {
            // Calculate the starting position
            int start = (WindowWidth / 2) - (text.Length / 2);

            // If the text is longer than the buffer, set it to 0
            start = start + text.Length > WindowWidth ? 0 : start;

            // Print away at the current cursor height (top)
            Console.SetCursorPosition(start, ypos);
            Renderer.Write(layer, text);
        }

        /// <summary>
        /// Center text to middle and write, then moves a line foward
        /// </summary>
        /// <param name="text">Input text</param>
        static internal void WriteLineAndCenter(Renderer.Layer layer, string text)
        {
            WriteLineAndCenter(layer, text, Console.CursorTop);
        }

        /// <summary>
        /// Center text to middle and write, then moves a line foward
        /// </summary>
        /// <param name="text">Input text</param>
        /// <param name="ypos">Top position</param>
        static internal void WriteLineAndCenter(Renderer.Layer layer, string text, int ypos)
        {
            WriteAndCenter(layer, text, ypos);
            Console.SetCursorPosition(0, ypos + 1);
        }
        #endregion

        #region GenH
        static internal void GenerateHorizontalLine(Renderer.Layer layer, char c, int len)
        {
            GenerateHorizontalLine(layer, c, Console.CursorLeft, Console.CursorTop, len);
        }
        
        static internal void GenerateHorizontalLine(Renderer.Layer layer, char c, int x, int y, int len)
        {
            Renderer.Write(layer, new string(c, len), x, y);
        }
        #endregion

        #region GenV
        static internal void GenerateVerticalLine(Renderer.Layer layer, char c, int len)
        {
            GenerateVerticalLine(layer, c, Console.CursorLeft, Console.CursorTop, len);
        }

        static internal void GenerateVerticalLine(Renderer.Layer layer, char c, int x, int y, int len)
        {
            int l = y + len;
            for (int i = y; i < l; i++)
            {
                Console.SetCursorPosition(x, i);
                Renderer.Write(layer, c);
            }
        }
        #endregion

        #region String
        public static unsafe string CenterString(string text, int width)
        {
            if (text.Length > width)
                text = text.Substring(0, width);
            
            int s = (width / 2) - (text.Length / 2);
            string t = new string(' ', width);
            fixed (char* pt = t)
                for (int i = 0; i < text.Length; i++)
                    pt[s + i] = text[i];

            return t;
        }
        #endregion

        #region Read
        /// <summary>
        /// Readline with a maximum length plus optional password mode.
        /// </summary>
        /// <param name="length">Input size/buffer</param>
        /// <param name="pPassword">Is password</param>
        /// <returns>User's input</returns>
        internal static string ReadLine(int length, bool pPassword = false)
        {
            //TODO: Optimize ReadLine(int, bool)
            // String buffer, pointer, trim at the end.
            // Maybe make multiple lines system.
            StringBuilder Output = new StringBuilder();
            int CurrentIndex = 0;
            bool getting = true;
            int OrigninalLeft = Console.CursorLeft;

            while (getting)
            {
                ConsoleKeyInfo c = Console.ReadKey(true);

                switch (c.Key)
                {
                    /* Ignored characters */
                    case ConsoleKey.Tab:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.DownArrow:
                        break;

                    case ConsoleKey.Enter:
                        getting = false;
                        break;

                    case ConsoleKey.Backspace:
                        if (CurrentIndex > 0)
                        {
                            // Erase whole
                            if (c.Modifiers == ConsoleModifiers.Control)
                            {
                                //TODO: Erase word
                                Output.Clear();
                                CurrentIndex = 0;
                                Console.SetCursorPosition(OrigninalLeft, Console.CursorTop);
                                Console.Write(new string(' ', length));
                                Console.SetCursorPosition(OrigninalLeft, Console.CursorTop);
                            }
                            else // Erase one character
                            {
                                Output = Output.Remove(Output.Length - 1, 1);
                                CurrentIndex--;
                                Console.SetCursorPosition(OrigninalLeft + CurrentIndex, Console.CursorTop);
                                Console.Write(' ');
                                Console.SetCursorPosition(OrigninalLeft + CurrentIndex, Console.CursorTop);
                            }
                        }
                        break;

                    default:
                        if (CurrentIndex < length)
                        {
                            Output.Append(c.KeyChar);

                            CurrentIndex++;

                            Console.Write(pPassword ? '*' : c.KeyChar);
                        }
                        break;
                }
            }

            return Output.Length > 0 ? Output.ToString() : null;
        }
        #endregion
    }
}