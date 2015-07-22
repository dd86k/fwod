using System;
using System.Collections.Generic;

/*
    General game mechanics.
*/

namespace fwod
{
    static class Game
    {
        #region Constants
        const string SaveFilenameModel = "fwod@.sg";
        const string ScreenshotFileNamePrefix = "screenshot-";
        #endregion

        #region Players
        /// <summary>
        /// List of the enemies.
        /// </summary>
        internal static List<Player> EnemyList = new List<Player>();

        /// <summary>
        /// Main player
        /// </summary>
        internal static Player MainPlayer = new Player();

        /// <summary>
        /// Determine the Player with position
        /// </summary>
        /// <param name="pFutureX">Future left position</param>
        /// <param name="pFutureY">Future top position</param>
        /// <returns>Enemy, null if no found</returns>
        internal static Player GetEnemyObjectAt(int pFutureX, int pFutureY)
        {
            foreach (Player Enemy in Game.EnemyList)
            {
                if (Enemy.PosX == pFutureX && Enemy.PosY == pFutureY)
                    return Enemy;
            }

            return null;
        }
        #endregion

        #region Graphics
        /// <summary>
        /// Graphic characters (char[])
        /// </summary>
        internal struct Graphics
        {
            internal struct Tiles
            {
                internal static char[] Grades = {'░', '▒', '▓', '█'};
                internal static char[] Half = {'▄', '▌', '▐', '▀'};
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
            /*
            internal struct Objects
            {
                internal static char StairsUp = '';
                internal static char StairsDown = '';
                internal static char Grass = '.';
            }
            */
        }
        #endregion

        #region Box generation
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
        {
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
        #endregion

        #region Events
        /*
        internal static void TakeTurn()
        {
            foreach (Player Enemy in EnemyList)
            {

            }
        }
        */

        internal static void UpdateEvent(string pText)
        {
            Console.SetCursorPosition(1, ConsoleTools.BufferHeight - 2);
            Console.Write(new string(' ', ConsoleTools.BufferWidth - 2));
            Console.SetCursorPosition(1, ConsoleTools.BufferHeight - 2);
            Console.Write(pText);
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