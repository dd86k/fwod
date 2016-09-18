using System;

/*
    General game mechanics.
*/

namespace fwod
{
    class Game
    {
        #region Properties
        public static bool IsPlaying = true;
        public static Menu MainMenu = Menu.GetMainMenu();
        public static byte CurrentFloor = 0;
        #endregion

        #region Statistics
        public class Statistics
        {
            public static ulong
                EnemiesKilled = 0,
                StepsTaken = 0,
                MoneyGained = 0,
                DamageDealt = 0,
                DamageReceived = 0;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Places Player stuff on screen.
        /// </summary>
        public static void QuickInitialize()
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
        /// Main player
        /// </summary>
        public static Player MainPlayer;

        public static PeopleManager PeopleList;

        /// <summary>
        /// Determine the Player with position
        /// </summary>
        /// <param name="x">Future left position</param>
        /// <param name="y">Future top position</param>
        /// <returns>Enemy, null if no found</returns>
        public static Person GetPersonObjectAt(int floor, int x, int y)
        {
            foreach (Person P in PeopleList[floor])
            {
                if (P.X == x && P.Y == y)
                    return P;
            }

            return null;
        }
        
        public static bool IsSomeonePresentAt(int floor, int x, int y)
        {
            foreach (Person P in PeopleList[floor])
            {
                if (P.X == x && P.Y == y)
                    return true;
            }

            return false;
        }
        #endregion

        #region Graphics
        /// <summary>
        /// Graphic characters (char[])
        /// </summary>
        public class Graphics
        {
            public class Objects
            {
                public const char
                    Grass = '.',
                    Ladder = 'H',
                    Chest = 'm',
                    Terminal = 'T',
                    ATM = '$';
            }
        }
        #endregion

        #region Events
        /*
        public static void TakeTurn()
        {
            foreach (Player Enemy in EnemyList)
            {

            }
        }
        */

        /// <summary>
        /// Display what's going on.
        /// </summary>
        /// <param name="text">Event entry.</param>
        public static void Log(string text)
        {
            //TODO: Clean this clutter.
            // Idea: Remove the array and use the substrings directly.
            string[] Lines = new string[] { text };
            int MaxLength = Utils.WindowWidth - 2;
            string MoreText = " -- More --";

            if (text.Length > MaxLength)
            {
                int ci = 0;
                int start = 0;
                Lines = new string[text.Length / (MaxLength - MoreText.Length) + 1];

                do
                {
                    if (start + MaxLength > text.Length)
                    {
                        Lines[ci] = text.Substring(start, text.Length - start);
                        start += MaxLength;
                    }
                    else
                    {
                        Lines[ci] = text.Substring(start, MaxLength - MoreText.Length) + MoreText;
                        start += MaxLength - MoreText.Length;
                    }
                    ci++;
                } while (start < text.Length);
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
        //TODO: Save method
        //TODO: Load method

        /*static public bool SaveProgress() // Return true is saved properly
        { //TODO: Find a way to convert to binary blob and encode it (basE91?)
            using (TextWriter tw = 
        }*/

        /*static public <StructOfGameData> LoadGame()
        {

        }*/
        #endregion
    }
}