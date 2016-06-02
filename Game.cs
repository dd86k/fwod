using System;
using System.Collections.Generic;

/*
    General game mechanics.
*/

namespace fwod
{
    static class Game
    {
        #region Properties
        internal static bool isPlaying = true;
        #endregion

        #region Statistics
        internal class Statistics
        {
            internal static uint StatEnemiesKilled = 0;
            internal static uint StatStepsTaken = 0;
            internal static uint StatMoneyGained = 0;
            internal static uint StatDamageDealt = 0;
            internal static uint StatDamageReceived = 0;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Places Player stuff on screen.
        /// </summary>
        internal static void QuickSetup()
        {
            Console.SetCursorPosition(27, 0);
            Console.Write("|");
            MainPlayer.HP = 10;
            Console.SetCursorPosition(41, 0);
            Console.Write("|");
            MainPlayer.Money = 12;
        }
        #endregion

        #region People
        /// <summary>
        /// List of the enemies.
        /// </summary>
        internal static List<Enemy> EnemyList = new List<Enemy>();

        /// <summary>
        /// List of non-enemy people.
        /// </summary>
        internal static List<Person> PeopleList = new List<Person>();

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
        internal static Person GetPersonObjectAt(int pFutureX, int pFutureY)
        {
            foreach (Person P in EnemyList)
            {
                if (P.X == pFutureX && P.Y == pFutureY)
                    return P;
            }

            return null;
        }
        #endregion

        #region Graphics
        /// <summary>
        /// Graphic characters (char[])
        /// </summary>
        internal class Graphics
        {
            internal class Tiles
            {
                internal readonly static char[] Grades = { '░', '▒', '▓', '█' };
                internal readonly static char[] Half = { '▄', '▌', '▐', '▀' };
            }
            internal class Lines
            {
                internal readonly static char[] Single = { '│', '─' };
                internal readonly static char[] SingleCorner = { '┌', '┐', '┘', '└' };
                internal readonly static char[] SingleConnector = { '┤', '┴', '┬', '├', '┼' };

                internal readonly static char[] Double = { '║', '═' };
                internal readonly static char[] DoubleCorner = { '╔', '╗', '╝', '╚' };
                internal readonly static char[] DoubleConnector = { '╣', '╩', '╦', '╠', '╬' };

                internal readonly static char[] DoubleVerticalCorner = { '╓', '╖', '╜', '╙' };
                internal readonly static char[] DoubleVerticalConnector = { '╢', '╨', '╥', '╟', '╫' };

                internal readonly static char[] DoubleHorizontalCorner = { '╕', '╛', '╘', '╒' };
                internal readonly static char[] DoubleHorizontalConnector = { '╡', '╧', '╤', '╞', '╪' };
            }
            internal class Objects
            {
                internal const char Grass = '.';
                internal const char Ladder = 'H';
                internal const char Chest = 'm';
                internal const char Terminal = 'T';
            }
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
        static internal void GenerateBox(Renderer.Layer pLayer, TypeOfLine pType, int pPosX, int pPosY, int pWidth, int pHeight)
        {
            // Default: TypeOfLine.Single
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
            Renderer.Write(pLayer, CornerTLChar, pPosX, pPosY);
            Utils.GenerateHorizontalLine(pLayer, HorizontalChar, pWidth - 2);
            Renderer.Write(pLayer, CornerTRChar);

            // Side walls
            Console.SetCursorPosition(pPosX, pPosY + 1);
            Utils.GenerateVerticalLine(pLayer, VerticalChar, pHeight - 1);

            Console.SetCursorPosition(pPosX + (pWidth - 1), pPosY + 1);
            Utils.GenerateVerticalLine(pLayer, VerticalChar, pHeight - 1);

            // Bottom wall
            Console.SetCursorPosition(pPosX, pPosY + (pHeight - 1));
            Renderer.Write(pLayer, CornerBLChar);
            Utils.GenerateHorizontalLine(pLayer, HorizontalChar, pWidth - 2);
            Renderer.Write(pLayer, CornerBRChar);
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

        /// <summary>
        /// Display what's going on.
        /// </summary>
        /// <param name="pText">Event entry.</param>
        internal static void UpdateLatestEvent(string pText)
        {
            string[] Lines = new string[] { pText };
            int MaxLength = Utils.WindowWidth - 2;
            string MoreText = " -- More --";

            if (pText.Length > MaxLength)
            {
                int ci = 0;
                int start = 0;
                Lines = new string[pText.Length / (MaxLength - MoreText.Length) + 1];

                do
                {
                    if (start + MaxLength > pText.Length)
                    {
                        Lines[ci] = pText.Substring(start, pText.Length - start);
                        start += MaxLength;
                    }
                    else
                    {
                        Lines[ci] = pText.Substring(start, MaxLength - MoreText.Length) + MoreText;
                        start += MaxLength - MoreText.Length;
                    }
                    ci++;
                } while (start < pText.Length);
            }

            for (int i = 0; i < Lines.Length; i++)
            {
                Console.SetCursorPosition(1, Utils.WindowHeight - 2);
                Console.Write(new string(' ', MaxLength));
                Console.SetCursorPosition(1, Utils.WindowHeight - 2);
                Console.Write(Lines[i]);

                if (i < Lines.Length - 1)
                    Console.ReadKey(true);
            }
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

        /*static internal <StructOfGameData> LoadGame()
        {

        }*/
        #endregion
    }
}