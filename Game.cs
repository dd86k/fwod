using System;
using System.Collections.Generic;

/*
    General game mechanics.
*/

namespace fwod
{
    internal class Game
    {
        const string SaveFilenameModel = "fwod#.sg";

        //internal static Dictionary<int, Player> PlayerList = new Dictionary<int, Player>();

        internal static List<Player> PlayerList = new List<Player>();

        /// <summary>
        /// Graphic characters (char[])
        /// </summary>
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

        /// <summary>
        /// Type of line to use.
        /// </summary>
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
        static internal void GenerateBox(Core.Layer pLayer, TypeOfLine pType, int pPosX, int pPosY, int pWidth, int pHeight)
        { //IDEA: Move all the playersay stuff back to Player.cs?
            // Minimum value must be at least 2
            pWidth = pWidth < 2 ? 1 : pWidth - 2;
            pHeight = pHeight < 2 ? 1 : pHeight - 1;

            // Verify that values are within bounds
            if (pPosX < 0)
            {
                pPosX = 0;
            }

            if (pPosX + pWidth > ConsoleTools.BufferWidth)
            {
                pPosX = ConsoleTools.BufferWidth - pWidth;
            }

            if (pPosY < 0)
            {
                pPosY = 0;
            }

            if (pPosY + pHeight > ConsoleTools.BufferWidth)
            {
                pPosY = ConsoleTools.BufferWidth - pHeight;
            }

            // Default is single lines
            char CornerTLChar = Graphics.Lines.SingleCorner[0]; // Top Left
            char CornerTRChar = Graphics.Lines.SingleCorner[1]; // Top Right
            char CornerBLChar = Graphics.Lines.SingleCorner[3]; // Bottom Left
            char CornerBRChar = Graphics.Lines.SingleCorner[2]; // Bottom Right
            char HorizontalChar = Graphics.Lines.Single[1];     // Horizontal
            char VerticalChar = Graphics.Lines.Single[0];       // Vertical

            switch (pType)
            {
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
            Core.Write(pLayer, CornerTLChar, pPosX, pPosY);
            ConsoleTools.GenerateHorizontalLine(pLayer, HorizontalChar, pWidth);
            Core.Write(pLayer, CornerTRChar);

            // Side walls
            Console.SetCursorPosition(pPosX, pPosY + 1);
            ConsoleTools.GenerateVerticalLine(pLayer, VerticalChar, pHeight);

            Console.SetCursorPosition(pPosX + pWidth + 1, pPosY + 1);
            ConsoleTools.GenerateVerticalLine(pLayer, VerticalChar, pHeight);

            // Bottom wall
            Console.SetCursorPosition(pPosX, pPosY + pHeight);
            Core.Write(pLayer, CornerBLChar);
            ConsoleTools.GenerateHorizontalLine(pLayer, HorizontalChar, pWidth);
            Core.Write(pLayer, CornerBRChar);
        }

        #region Events
        internal static void UpdateEvent(string pText)
        {
            Console.SetCursorPosition(1, ConsoleTools.BufferHeight - 2);
            Console.Write(pText);
        }
        #endregion

        #region Player specific stuff, centralized
        static internal void CharacterSays(Player pPlayer, string pText)
        {
            string[] Lines = new string[] { pText };
            int ci = 0;
            int start = 0;

            // This block seperates the input into 25 characters each lines equaly.
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
            // Minimum text so the bubble doesn't look too thin
            else Lines = new string[] { " " };

            //TODO: The verification is already done via GenerateBox, so
                 // I'm wondering if I should just modify those values
                 // with a ref or out

            // X/Left bubble starting position
            int StartX = pPlayer.PosX - (Lines[0].Length / 2) - 1;
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
            int StartY = pPlayer.PosY - (Lines.Length) - 3;
            // Re-places StartY if it goes further than the display buffer
            if (StartY > ConsoleTools.BufferWidth)
            {
                StartY = ConsoleTools.BufferWidth - (Lines[0].Length - 2);
            }

            if (StartY < 0)
            {
                StartY = 3;
            }

            // Define the position of the text
            int TextStartX = StartX + 1;
            int TextStartY = StartY + 1;

            // Generate the bubble
            GenerateBubble(pPlayer,
                Lines[0].Length,
                Lines.Length,
                StartX,
                StartY);

            // Insert Text
            for (int i = 0; i < Lines.Length; i++)
            {
                Core.Write(Core.Layer.Player, Lines[i], TextStartX, TextStartY + i);
            }

            // Waiting for keypress
            Console.ReadKey(true);

            // Clear bubble
            //Console.SetCursorPosition(StartX, StartY);
            int lenH = StartX + Lines[0].Length + 2;
            int lenV = StartY + Lines.Length + 2;
            for (int row = StartY; row < lenV; row++)
            {
                for (int col = StartX; col < lenH; col++)
                {
                    // Write back what was at Game layer before
                    Core.Write(Core.Layer.Game, Core.GetCharAt(Core.Layer.Game, col, row), col, row);
                }
            }
        }

        static internal string GetAnswerFromCharacter(Player pPlayer)
        {
            // Generates temporary text for spacer
            string tmp = ConsoleTools.RepeatChar(' ', 25);

            // Determine the starting position of the bubble
            int StartX = pPlayer.PosX - (tmp.Length / 2) - 1;
            int StartY = pPlayer.PosY - 4;

            // Generate the bubble
            GenerateBubble(pPlayer, tmp.Length, 1, StartX, StartY);

            // Read input from player
            string Out = Console.ReadLine();

            // Clear bubble
            int lenH = StartX + tmp.Length + 2;
            int lenV = StartY + 3;
            for (int row = StartY; row < lenV; row++)
            {
                for (int col = StartX; col < lenH; col++)
                {
                    // Write back what was at Game layer before
                    Core.Write(Core.Layer.Game, Core.GetCharAt(Core.Layer.Game, col, row), col, row);
                }
            }

            return Out;
        }

        /// <summary>
        /// Generates the bubble for a player.
        /// </summary>
        /// <param name="pPlayer">Player</param>
        /// <param name="pTextLength">Lenght of the text (Width)</param>
        /// <param name="pLines">Length of the text (Height)</param>
        /// <param name="pPosX">Top position</param>
        /// <param name="pPosY">Left position</param>
        static void GenerateBubble(Player pPlayer, int pTextLength, int pLines, int pPosX, int pPosY)
        {
            Game.GenerateBox(Core.Layer.Player, Game.TypeOfLine.Single, pPosX, pPosY, pTextLength + 2, pLines + 2);

            // Bubble chat "connector"
            if (pPosY < pPlayer.PosY) // Over player
            {
                Console.SetCursorPosition(pPlayer.PosX, pPlayer.PosY - 2);
                Core.Write(Core.Layer.Player, Game.Graphics.Lines.SingleConnector[2]);
            }
            else // Under player
            {
                Console.SetCursorPosition(pPlayer.PosX, pPlayer.PosY + 2);
                Core.Write(Core.Layer.Player, Game.Graphics.Lines.SingleConnector[1]);
            }

            // Prepare to insert text
            Console.SetCursorPosition(pPosX + 1, pPosY + 1);
        }
        #endregion

        #region Save/Load
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
        #endregion
    }
}