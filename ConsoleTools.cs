using System;

/*
    Various tools for the command prompt and terminal.
*/

namespace FWoD
{
    internal class ConsoleTools
    {
        /// <summary>
        /// Initial buffer height
        /// </summary>
        static internal readonly int BufferHeight = 25;
        /// <summary>
        /// Initial buffer width
        /// </summary>
        static internal readonly int BufferWidth = 80;

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

        /// <summary>
        /// Generates a horizontal line on screen
        /// </summary>
        /// <param name="pChar">Character to use</param>
        /// <param name="pLenght">Length</param>
        static internal void GenerateHorizontalLine(char pChar, int pLenght)
        {
            GenerateHorizontalLine(pChar, Console.CursorLeft, Console.CursorTop, pLenght);
        }

        /// <summary>
        /// Generates a horizontal line on screen
        /// </summary>
        /// <param name="pChar">Character to use</param>
        /// <param name="pPosX">Left position</param>
        /// <param name="pPosY">Top position</param>
        /// <param name="pLenght">Length</param>
        static internal void GenerateHorizontalLine(char pChar, int pPosX, int pPosY, int pLenght)
        {
            Console.SetCursorPosition(pPosX, pPosY);
            for (int i = 0; i < pLenght; i++)
            {
                Console.Write(pChar);
            }
        }

        /// <summary>
        /// Generates a vertical line on screen
        /// </summary>
        /// <param name="pChar">Character to use</param>
        /// <param name="pLenght">Length</param>
        static internal void GenerateVerticalLine(char pChar, int pLenght)
        {
            GenerateVerticalLine(pChar, Console.CursorLeft, Console.CursorTop, pLenght);
        }

        /// <summary>
        /// Generates a vertical line on screen
        /// </summary>
        /// <param name="pChar">Character to use</param>
        /// <param name="pPosX">Left position</param>
        /// <param name="pPosY">Top position</param>
        /// <param name="pLenght">Length</param>
        static internal void GenerateVerticalLine(char pChar, int pPosX, int pPosY, int pLenght)
        {
            Console.SetCursorPosition(pPosX, pPosY);
            for (int i = 0; i < pLenght; i++)
            {
                Console.Write(pChar);
                Console.SetCursorPosition(pPosX, pPosY++);
                /* -- REASONS WHY I DO IT THIS WAY --
                 * NEWLINE FUCKS UP SHIT
                 * ADDING SPACE (for padding) OVERWRITES [anything in between] BUFFER
                 * "hurr durr ur cpu cycles!!!11"
                 */
            }
        }

        /// <summary>
        /// Generates a string out of a character
        /// </summary>
        /// <param name="pChar">Input char</param>
        /// <param name="pLenght">Output string</param>
        /// <returns></returns>
        static internal string RepeatChar(char pChar, int pLenght)
        {
            string Out = string.Empty;
            for (int i = 0; i < pLenght; i++)
            {
                Out += pChar;
            }
            return Out;
        }

        /// <summary>
        /// Resets the Console's Out to the original one
        /// </summary>
        static internal void ResetConsoleOut()
        {
            // If Console.OpenStandardOutput() fails on Linux, I'll just use this in the future
            // ..Or use #if
            //System.IO.TextWriter OriginalOut = new System.IO.TextWriter();
            using (System.IO.StreamWriter tw = new System.IO.StreamWriter(Console.OpenStandardOutput()))
            {
                tw.AutoFlush = true;
                Console.SetOut(tw);
            }
        }
    }
}