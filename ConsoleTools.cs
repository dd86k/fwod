using System;

/*
    Various tools for the command prompt and terminal.
*/

namespace FWoD
{
    internal class ConsoleTools
    {
        static internal readonly int BufferHeight = 25;
        static internal readonly int BufferWidth = 80;

        // Center
        static internal void WriteAndCenter(string pText)
        {
            WriteAndCenter(pText, Console.CursorTop);
        }

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

        static internal void WriteLineAndCenter(string pText)
        {
            WriteLineAndCenter(pText, Console.CursorTop);
        }

        static internal void WriteLineAndCenter(string pText, int pTopPosition)
        {
            WriteAndCenter(pText, pTopPosition);
            Console.SetCursorPosition(0, pTopPosition + 1);
        }

        // Generate lines
        static internal void GenerateHorizontalLine(char pChar, int pLenght)
        {
            GenerateHorizontalLine(pChar, Console.CursorLeft, Console.CursorTop, pLenght);
        }

        static internal void GenerateHorizontalLine(char pChar, int pPosX, int pPosY, int pLenght)
        {
            Console.SetCursorPosition(pPosX, pPosY);
            for (int i = 0; i < pLenght; i++)
            {
                Console.Write(pChar);
            }
        }

        static internal void GenerateVerticalLine(char pChar, int pLenght)
        {
            GenerateVerticalLine(pChar, Console.CursorLeft, Console.CursorTop, pLenght);
        }

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

        // Generate string
        // Same at the GenerateHorizontalLine but returns a string instead
        // GenerateHorizontalLine is prefered because it directly prints out
        static internal string RepeatChar(char pChar, int pLenght)
        {
            string Out = string.Empty;
            for (int i = 0; i < pLenght; i++)
            {
                Out += pChar;
            }
            return Out;
        }

        // If Console.OpenStandardOutput() fails on Linux, I'll just use this in the future
        // ..Or use #if
        //System.IO.TextWriter OriginalOut = new System.IO.TextWriter();
        static internal void ResetConsoleOut()
        {
            using (System.IO.StreamWriter tw = new System.IO.StreamWriter(Console.OpenStandardOutput()))
            {
                tw.AutoFlush = true;
                Console.SetOut(tw);
            }
        }
    }
}