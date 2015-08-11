using System;

/*
    Various tools for the command prompt and terminal.
*/

namespace fwod
{
    static class ConsoleTools
    {
        #region Properties
        /// <summary>
        /// Initial buffer height. ANSI/ISO screen size.
        /// </summary>
        internal const int BufferHeight = 24;
        /// <summary>
        /// Initial buffer width. ANSI/ISO screen size.
        /// </summary>
        internal const int BufferWidth = 80;
        /// <summary>
        /// Original foreground color
        /// </summary>
        static internal readonly ConsoleColor OriginalForegroundColor = Console.ForegroundColor;
        /// <summary>
        /// Original background color
        /// </summary>
        static internal readonly ConsoleColor OriginalBackgroundColor = Console.BackgroundColor;
        #endregion

        #region Center text (Normal)
        /// <summary>
        /// Center text to middle and write
        /// </summary>
        /// <param name="pText">Input text</param>
        static internal void WriteAndCenter(string pText)
        {
            WriteAndCenter(pText, Console.CursorTop);
        }

        /// <summary>
        /// Center text to middle and write to a specific top position
        /// </summary>
        /// <param name="pText">Input text</param>
        /// <param name="pTopPosition">Top position</param>
        static internal void WriteAndCenter(string pText, int pTopPosition)
        {
            // Calculate the starting position
            int start = (BufferWidth / 2) - (pText.Length / 2);

            // If the text is longer than the buffer, set it to 0
            start = start + pText.Length > BufferWidth ? 0 : start;

            // Print away at the current cursor height (top)
            Console.SetCursorPosition(start, pTopPosition);
            Console.Write(pText);
        }

        /// <summary>
        /// Center text to middle and write, then moves a line foward
        /// </summary>
        /// <param name="pText">Input text</param>
        static internal void WriteLineAndCenter(string pText)
        {
            WriteLineAndCenter(pText, Console.CursorTop);
        }

        /// <summary>
        /// Center text to middle and write, then moves a line foward
        /// </summary>
        /// <param name="pText">Input text</param>
        /// <param name="pTopPosition">Top position</param>
        static internal void WriteLineAndCenter(string pText, int pTopPosition)
        {
            WriteAndCenter(pText, pTopPosition);
            Console.SetCursorPosition(0, pTopPosition + 1);
        }
        #endregion

        #region Center text (Core.cs)
        /// <summary>
        /// Center text to middle and write
        /// </summary>
        /// <param name="pText">Input text</param>
        static internal void WriteAndCenterCore(string pText)
        {
            WriteAndCenter(pText, Console.CursorTop);
        }

        /// <summary>
        /// Center text to middle and write to a specific top position
        /// </summary>
        /// <param name="pText">Input text</param>
        /// <param name="pTopPosition">Top position</param>
        static internal void WriteAndCenter(Core.Layer pLayer, string pText, int pTopPosition)
        {
            // Calculate the starting position
            int start = (BufferWidth / 2) - (pText.Length / 2);

            // If the text is longer than the buffer, set it to 0
            start = start + pText.Length > BufferWidth ? 0 : start;

            // Print away at the current cursor height (top)
            Console.SetCursorPosition(start, pTopPosition);
            Core.Write(pLayer, pText);
        }

        /// <summary>
        /// Center text to middle and write, then moves a line foward
        /// </summary>
        /// <param name="pText">Input text</param>
        static internal void WriteLineAndCenter(Core.Layer pLayer, string pText)
        {
            WriteLineAndCenter(pLayer, pText, Console.CursorTop);
        }

        /// <summary>
        /// Center text to middle and write, then moves a line foward
        /// </summary>
        /// <param name="pText">Input text</param>
        /// <param name="pTopPosition">Top position</param>
        static internal void WriteLineAndCenter(Core.Layer pLayer, string pText, int pTopPosition)
        {
            WriteAndCenter(pLayer, pText, pTopPosition);
            Console.SetCursorPosition(0, pTopPosition + 1);
        }
        #endregion

        #region GenH
        /// <summary>
        /// Generates a horizontal line on screen
        /// </summary>
        /// <param name="pChar">Character to use</param>
        /// <param name="pLenght">Length</param>
        static internal void GenerateHorizontalLine(Core.Layer pLayer, char pChar, int pLenght)
        {
            GenerateHorizontalLine(pLayer, pChar, Console.CursorLeft, Console.CursorTop, pLenght);
        }

        /// <summary>
        /// Generates a horizontal line on screen
        /// </summary>
        /// <param name="pChar">Character to use</param>
        /// <param name="pPosX">Left position</param>
        /// <param name="pPosY">Top position</param>
        /// <param name="pLenght">Length</param>
        static internal void GenerateHorizontalLine(Core.Layer pLayer, char pChar, int pPosX, int pPosY, int pLenght)
        {
            Console.SetCursorPosition(pPosX, pPosY);
            for (int i = 0; i < pLenght; i++)
            {
                Core.Write(pLayer, pChar);
            }
        }
        #endregion

        #region GenV
        /// <summary>
        /// Generates a vertical line on screen
        /// </summary>
        /// <param name="pChar">Character to use</param>
        /// <param name="pLenght">Length</param>
        static internal void GenerateVerticalLine(Core.Layer pLayer, char pChar, int pLenght)
        {
            GenerateVerticalLine(pLayer, pChar, Console.CursorLeft, Console.CursorTop, pLenght);
        }

        /// <summary>
        /// Generates a vertical line on screen
        /// </summary>
        /// <param name="pChar">Character to use</param>
        /// <param name="pPosX">Left position</param>
        /// <param name="pPosY">Top position</param>
        /// <param name="pLenght">Length</param>
        static internal void GenerateVerticalLine(Core.Layer pLayer, char pChar, int pPosX, int pPosY, int pLenght)
        {
            Console.SetCursorPosition(pPosX, pPosY);
            int len = pPosY + pLenght;
            for (int i = pPosY; i < len; i++)
            {
                Core.Write(pLayer, pChar);
                Console.SetCursorPosition(pPosX, i);
            }
        }
        #endregion

        #region String
        /// <summary>
        /// Generates a string with margins.
        /// </summary>
        /// <param name="pText"></param>
        /// <param name="pWidth"></param>
        /// <returns></returns>
        static internal string CenterString(string pText, int pWidth)
        {
            string stmp = new string(' ', pWidth);
            int start = (pWidth / 2) - (pText.Length / 2);
            stmp = stmp.Insert(start, pText);
            stmp = stmp.Remove(start + pText.Length, pText.Length);
            return stmp;
        }

        static internal int GetLonguestString(string[] pArray)
        {
            int max = 0;
            for (int i = 0; i < pArray.Length; i++)
            {
                if (pArray[i].Length > max)
                    max = pArray[i].Length;
            }
            return max;
        }
        #endregion

        #region Read
        /// <summary>
        /// Readline with a maximum length.
        /// </summary>
        /// <param name="pLimit">Limit in characters</param>
        /// <returns>User's input</returns>
        internal static string ReadLine(int pLimit)
        {
            return ReadLine(pLimit, false);
        }

        /// <summary>
        /// Readline with a maximum length plus optional password mode.
        /// </summary>
        /// <param name="pLimit">Character limit</param>
        /// <param name="pPassword">Is password</param>
        /// <returns>User's input</returns>
        internal static string ReadLine(int pLimit, bool pPassword)
        {
            System.Text.StringBuilder _out = new System.Text.StringBuilder();
            int _index = 0;
            bool _get = true;
            int OrigninalLeft = Console.CursorLeft;

            while (_get)
            {
                ConsoleKeyInfo c = Console.ReadKey(true);

                switch (c.Key)
                {
                    case ConsoleKey.Tab:
                        break;

                    case ConsoleKey.Enter:
                        _get = false;
                        break;

                    case ConsoleKey.Backspace:
                        if (_index > 0)
                        {
                            _out = _out.Remove(_out.Length - 1, 1);
                            _index--;
                            Console.SetCursorPosition(OrigninalLeft + _index, Console.CursorTop);
                            Console.Write(' ');
                            Console.SetCursorPosition(OrigninalLeft + _index, Console.CursorTop);
                        }
                        break;

                    default:
                        if (_index < pLimit)
                        {
                            _out.Append(c.KeyChar);
                            _index++;

                            if (pPassword)
                                Console.Write('*');
                            else
                                Console.Write(c.KeyChar);
                        }
                        break;
                }
            }

            if (_out.Length > 0) return _out.ToString();
            return null;
        }
        #endregion
    }
}