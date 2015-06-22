using System;

namespace Play
{
    internal class ConsoleTools
    {
        // Center
        static internal void WriteAndCenter(string pText)
        {
            // Calculate the starting position
            int start = (Console.BufferWidth / 2) - (pText.Length / 2);
            // If the text is longer than the buffer, set it to 0
            start = start + pText.Length > Console.BufferWidth ? 0 : start;
            // Print away at the current cursor height (top)
            Console.SetCursorPosition(start, Console.CursorTop);
            Console.Write(pText);
        }

        static internal void WriteLineAndCenter(string pText)
        {
            WriteAndCenter(pText);
            Console.SetCursorPosition(0, Console.CursorTop + 1);
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
            }
        }

        // Generate string
        static internal string RepeatChar(char pChar, int pLenght)
        {
            string Out = string.Empty;
            for (int i = 0; i < pLenght; i++)
            {
                Out += pChar;
            }
            return Out;
        }
    }
}