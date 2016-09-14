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

        #region Box generation
        public static void GenerateBox(int x, int y, int width, int height)
        {
            // Top wall
            Console.SetCursorPosition(x, y);
            Console.Write('┌');
            Console.Write(new string('─', width - 2));
            Console.Write('┐');

            // Side walls
            Console.SetCursorPosition(x, y + 1);
            GenerateVerticalLine('│', height - 1);
            Console.SetCursorPosition(x + (width - 1), y + 1);
            GenerateVerticalLine('│', height - 1);

            // Bottom wall
            Console.SetCursorPosition(x, y + (height - 1));
            Console.Write('└');
            Console.Write(new string('─', width - 2));
            Console.Write('┘');
        }
        #endregion

        #region GenH
        static internal void GenerateHorizontalLine(char c, int len)
        {
            GenerateHorizontalLine(c, Console.CursorLeft, Console.CursorTop, len);
        }

        static internal void GenerateHorizontalLine(char c, int x, int y, int len)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(new string(c, len));
        }

        static internal void GenerateHorizontalLineMap(char c, int len)
        {
            GenerateHorizontalLineMap(c, Console.CursorLeft, Console.CursorTop, len);
        }
        
        static internal void GenerateHorizontalLineMap(char c, int x, int y, int len)
        {
            MapManager.Write(new string(c, len), x, y);
        }
        #endregion

        #region GenV
        static internal void GenerateVerticalLine(char c, int len)
        {
            GenerateVerticalLine(c, Console.CursorLeft, Console.CursorTop, len);
        }

        static internal void GenerateVerticalLine(char c, int x, int y, int len)
        {
            int l = y + len;
            for (int i = y; i < l; i++)
            {
                Console.SetCursorPosition(x, i);
                Console.Write(c);
            }
        }

        static internal void GenerateVerticalLineMap(char c, int len)
        {
            GenerateVerticalLineMap(c, Console.CursorLeft, Console.CursorTop, len);
        }

        static internal void GenerateVerticalLineMap(char c, int x, int y, int len)
        {
            int l = y + len;
            for (int i = y; i < l; i++)
            {
                Console.SetCursorPosition(x, i);
                MapManager.Write(c);
            }
        }
        #endregion

        #region Centering
        public static unsafe string Center(this string text, int width)
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

        public static void CenterAndWrite(string text)
        {
            Console.SetCursorPosition((WindowWidth / 2) - (text.Length / 2), Console.CursorTop);
            Console.Write(text);
        }

        public static void CenterAndWriteLine(string text)
        {
            Console.SetCursorPosition((WindowWidth / 2) - (text.Length / 2), Console.CursorTop);
            Console.WriteLine(text);
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