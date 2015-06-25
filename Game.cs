using System;

/*
    General game mechanics.
*/

namespace FWoD
{
    internal class Game
    {
        const string SaveFilename = "fwod.sg";

        internal struct Graphics
        {
            internal struct Tiles
            {
                internal static char[] Grades = new char[] {'░', '▒', '▓', '█'};
                internal static char[] Half = new char[] {'▄', '▌', '▐', '▀'};
            }
            internal struct Lines
            {
                internal static char[] Single = {'│', '─'};
                internal static char[] SingleCorner = {'┌', '┐', '┘', '└'};
                internal static char[] SingleConnector = {'┤', '┴', '┬', '├', '┼'};

                internal static char[] Double = {'║', '═'};
                internal static char[] DoubleCorner = {'╔', '╗', '╝', '╚'};
                internal static char[] DoubleConnector = {'╣', '╩', '╦', '╠', '╬'};


                internal static char[] InnerSingleCorner = {'╒', '╖', '╜', '╙'};
                internal static char[] InnerSingleConnector = {'╢', '╧', '╤', '╟', '╫'};

                internal static char[] InnerDoubleCorner = {'╕', '╛', '╘', '╓'};
                internal static char[] InnerDoubleConnector = {'╡', '╨', '╥', '╞', '╪'};
            }
        }

        //TODO: Find a better solution than this
        internal enum TypeOfLine
        {
            Single, Double
        }

        /// <summary>
        /// Generates a box.
        /// </summary>
        /// <param name="pType">Type of line.</param>
        /// <param name="pPosX">Top position.</param>
        /// <param name="pPosY">Left position.</param>
        /// <param name="pWidth">Width.</param>
        /// <param name="pHeight">Height.</param>
        static internal void GenerateBox(TypeOfLine pType, int pPosX, int pPosY, int pWidth, int pHeight)
        { //TODO: Enum for opening (like if we want to open the box).
          // ! Maybe I should move this to ConsoleTools !
          // Thinking about making those actual objects (for later manipulation instead of recalling this)


            int Width = pWidth < 2 ? 1 : pWidth - 2; // Minimum: 2
            int Height = pHeight < 2 ? 1 : pHeight - 1;

            // Default is single lines
            char CornerTLChar = Graphics.Lines.SingleCorner[0]; // Top Left
            char CornerTRChar = Graphics.Lines.SingleCorner[1]; // Top Right
            char CornerBLChar = Graphics.Lines.SingleCorner[3]; // Bottom Left
            char CornerBRChar = Graphics.Lines.SingleCorner[2]; // Bottom Right
            char HorizontalChar = Graphics.Lines.Single[1]; // Horizontal
            char VerticalChar = Graphics.Lines.Single[0]; // Vertical

            switch (pType)
            { // By default TypeOfLine.Single is already defined as above.
                case TypeOfLine.Double:
                    CornerTLChar = Graphics.Lines.DoubleCorner[0];
                    CornerTRChar = Graphics.Lines.DoubleCorner[1];
                    CornerBLChar = Graphics.Lines.DoubleCorner[3];
                    CornerBRChar = Graphics.Lines.DoubleCorner[2];
                    HorizontalChar = Graphics.Lines.Double[1];
                    VerticalChar = Graphics.Lines.Double[0];
                    break;
            }

            // Top wall
            Console.SetCursorPosition(pPosX, pPosY);
            Console.Write(CornerTLChar);
            ConsoleTools.GenerateHorizontalLine(HorizontalChar, Width);
            Console.Write(CornerTRChar);

            // Side walls
            Console.SetCursorPosition(pPosX, pPosY + 1);
            ConsoleTools.GenerateVerticalLine(VerticalChar, Height);

            Console.SetCursorPosition(pPosX + Width + 1, pPosY + 1);
            ConsoleTools.GenerateVerticalLine(VerticalChar, Height);

            // Bottom wall
            Console.SetCursorPosition(pPosX, pPosY + Height);
            Console.Write(CornerBLChar);
            ConsoleTools.GenerateHorizontalLine(HorizontalChar, Width);
            Console.Write(CornerBRChar);
        }

        /* "UI" For save/load game
                [ Save/Load game ]         <- Center text
        +--------------------------------+
        | <SavegameFile1> - <PlayerName> | <- other colors when selected
        +--------------------------------+
        | <SavegameFile2> - <PlayerName> |
        +--------------------------------+
        | [...] 5 Items in total         |
        */

        /*static internal bool SaveProgress() // Return true is saved properly
        { //TODO: Find a way to convert to binary blob and encode it (basE91?)
            using (TextWriter tw = 
        }*/

        /*static internal <StructOfGameData> LoadProgress()
        {

        }*/
    }
}