using System;

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
            int start = (WindowWidth / 2) - (pText.Length / 2);

            // If the text is longer than the buffer, set it to 0
            start = start + pText.Length > WindowWidth ? 0 : start;

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
        static internal void WriteAndCenter(Renderer.Layer pLayer, string pText, int pTopPosition)
        {
            // Calculate the starting position
            int start = (WindowWidth / 2) - (pText.Length / 2);

            // If the text is longer than the buffer, set it to 0
            start = start + pText.Length > WindowWidth ? 0 : start;

            // Print away at the current cursor height (top)
            Console.SetCursorPosition(start, pTopPosition);
            Renderer.Write(pLayer, pText);
        }

        /// <summary>
        /// Center text to middle and write, then moves a line foward
        /// </summary>
        /// <param name="pText">Input text</param>
        static internal void WriteLineAndCenter(Renderer.Layer pLayer, string pText)
        {
            WriteLineAndCenter(pLayer, pText, Console.CursorTop);
        }

        /// <summary>
        /// Center text to middle and write, then moves a line foward
        /// </summary>
        /// <param name="pText">Input text</param>
        /// <param name="pTopPosition">Top position</param>
        static internal void WriteLineAndCenter(Renderer.Layer pLayer, string pText, int pTopPosition)
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
        static internal void GenerateHorizontalLine(Renderer.Layer pLayer, char pChar, int pLenght)
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
        static internal void GenerateHorizontalLine(Renderer.Layer pLayer, char pChar, int pPosX, int pPosY, int pLenght)
        {
            Renderer.Write(pLayer, new string(pChar, pLenght));
        }
        #endregion

        #region GenV
        /// <summary>
        /// Generates a vertical line on screen
        /// </summary>
        /// <param name="pChar">Character to use</param>
        /// <param name="pLenght">Length</param>
        static internal void GenerateVerticalLine(Renderer.Layer pLayer, char pChar, int pLenght)
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
        static internal void GenerateVerticalLine(Renderer.Layer pLayer, char pChar, int pPosX, int pPosY, int pLenght)
        {
            int len = pPosY + pLenght;
            for (int i = pPosY; i < len; i++)
            {
                Console.SetCursorPosition(pPosX, i);
                Renderer.Write(pLayer, pChar);
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
            System.Text.StringBuilder Output = new System.Text.StringBuilder();
            int CurrentIndex = 0;
            bool GotAnswer = false;
            int OrigninalLeft = Console.CursorLeft;

            while (!GotAnswer)
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
                        GotAnswer = true;
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
                                Console.Write(new string(' ', pLimit));
                                Console.SetCursorPosition(OrigninalLeft, Console.CursorTop);
                            }
                            // Erase one character
                            else
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
                        if (CurrentIndex < pLimit)
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