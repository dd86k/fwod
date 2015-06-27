using System;

/*
    General game mechanics.
*/

namespace FWoD
{
    internal class Game
    {
        const string SaveFilenameModel = "fwod#.sg";

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


                internal static char[] DoubleVerticalCorner = { '╓', '╖', '╜', '╙' };
                internal static char[] DoubleVerticalConnector = { '╢', '╨', '╥', '╟', '╫' };

                internal static char[] DoubleHorizontalCorner = {'╕', '╛', '╘', '╒'};
                internal static char[] DoubleHorizontalConnector = { '╡', '╧', '╤', '╞', '╪' };
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
          // Thinking about making those actual objects (for later manipulation instead of recalling this)

            pWidth = pWidth < 2 ? 1 : pWidth - 2; // Minimum: 2
            pHeight = pHeight < 2 ? 1 : pHeight - 1;

            if (pPosX < 0)
            {
                pPosX = 0;
            }
            if (pPosX + pWidth > ConsoleTools.BufferWidth)
            {
                pPosX = ConsoleTools.BufferWidth - pWidth;
            }

            // Default is single lines
            char CornerTLChar = Graphics.Lines.SingleCorner[0]; // Top Left
            char CornerTRChar = Graphics.Lines.SingleCorner[1]; // Top Right
            char CornerBLChar = Graphics.Lines.SingleCorner[3]; // Bottom Left
            char CornerBRChar = Graphics.Lines.SingleCorner[2]; // Bottom Right
            char HorizontalChar = Graphics.Lines.Single[1];     // Horizontal
            char VerticalChar = Graphics.Lines.Single[0];       // Vertical

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
            ConsoleTools.GenerateHorizontalLine(HorizontalChar, pWidth);
            Console.Write(CornerTRChar);

            // Side walls
            Console.SetCursorPosition(pPosX, pPosY + 1);
            ConsoleTools.GenerateVerticalLine(VerticalChar, pHeight);

            Console.SetCursorPosition(pPosX + pWidth + 1, pPosY + 1);
            ConsoleTools.GenerateVerticalLine(VerticalChar, pHeight);

            // Bottom wall
            Console.SetCursorPosition(pPosX, pPosY + pHeight);
            Console.Write(CornerBLChar);
            ConsoleTools.GenerateHorizontalLine(HorizontalChar, pWidth);
            Console.Write(CornerBRChar);
        }

        internal static string GetAnswerFromEntity(Player pPlayer)
        {

        }

        internal static void MakeEntityTalk(Player pPlayer, string pText)
        {

        }

        internal static void MakeEntityTalk(Enemy pEnemy, string pText)
        {

        }

        void PlayerSays(string pText)
        {
            string[] Lines = new string[] { pText };
            int ci = 0;
            int start = 0;
            // Seperates the text in blocks of 25 letters in case
            if (pText.Length != 0)
            {
                if (pText.Length > 25)
                {
                    Lines = new string[(pText.Length / 26) + 1];
                    do
                    {
                        if (start + 25 > pText.Length)
                            Lines[ci] = pText.Substring(start, pText.Length - start);
                        else
                            Lines[ci] = pText.Substring(start, 25);
                        ci++;
                        start += 25;
                    } while (start < pText.Length);
                }
            }
            else Lines = new string[] { " " }; // At least the bubble won't look squished out

            // X/Left bubble starting position
            int StartX = this.PosX - (Lines[0].Length / 2) - 1;
            // Re-places StartX if it goes further than the display buffer
            if (StartX + (Lines[0].Length + 2) > ConsoleTools.BufferWidth)
            {
                StartX = ConsoleTools.BufferWidth - (Lines[0].Length + 2);
            }

            if (StartX < 0)
            {
                StartX = 0;
            }

            // Y/Top bubble starting position
            int StartY = this.PosY - (Lines.Length) - 3;
            // Re-places StartY if it goes further than the display buffer
            if (StartY > ConsoleTools.BufferWidth)
            {
                StartY = ConsoleTools.BufferWidth - (Lines[0].Length - 2);
            }

            if (StartY < 0)
            {
                StartY = 3;
            }

            int TextStartX = StartX + 1;
            int TextStartY = StartY + 1;

            GenerateBubble(Lines[0].Length,
                Lines.Length,
                StartX,
                StartY);

            // Insert Text
            for (int i = 0; i < Lines.Length; i++)
            {
                Console.SetCursorPosition(TextStartX, TextStartY + i);
                Console.Write(Lines[i]);
            }

            // Waiting for keypress
            Console.SetCursorPosition(0, 0);
            Console.ReadKey(true);

            // Clear bubble
            //TODO: Put older chars back
            Console.SetCursorPosition(StartX, StartY);
            int len = Lines[0].Length + 4;
            for (int i = StartY; i < this.PosY; i++)
            {
                ConsoleTools.GenerateHorizontalLine(' ', len);
                Console.SetCursorPosition(StartX, i);
            }
        }

        string PlayerAnswer()
        {
            string tmp = ConsoleTools.RepeatChar(' ', 25);

            // determine the starting position of the bubble
            // lol copy paste 
            int StartX = this.PosX - (tmp.Length / 2) - 1;
            int StartY = this.PosY - 4;

            GenerateBubble(tmp.Length, 1, StartX, StartY);

            string Out = Console.ReadLine();

            // Clear bubble
            //TODO: Put older chars back
            Console.SetCursorPosition(StartX, StartY);
            int len = tmp.Length + 4;
            for (int i = StartY; i < this.PosY; i++)
            {
                ConsoleTools.GenerateHorizontalLine(' ', len);
                Console.SetCursorPosition(StartX, i);
            }

            return Out;
        }

        void GenerateBubble(int pTextLength, int pLines, int pPosX, int pPosY)
        {
            Game.GenerateBox(Game.TypeOfLine.Single, pPosX, pPosY, pTextLength + 2, pLines + 2);

            // Bubble chat "connector"
            if (pPosY < this.PosY) // Over player
            {
                Console.SetCursorPosition(this.PosX, this.PosY - 2);
                Console.Write(Game.Graphics.Lines.SingleConnector[2]);
            }
            else // Under player
            {
                Console.SetCursorPosition(this.PosX, this.PosY + 2);
                Console.Write(Game.Graphics.Lines.SingleConnector[1]);
            }

            // Prepare to insert text
            Console.SetCursorPosition(pPosX + 1, pPosY + 1);
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