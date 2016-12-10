using System;

/*
 * Game mechanics.
 */

//TODO: PlayIntro() -- Intro scene, etc.

namespace fwod
{
    class Game
    {
        #region Properties
        public static byte CurrentFloor = 0;
        #endregion

        #region Statistics
        public class Statistics
        {
            public static decimal
                EnemiesKilled = 0,
                StepsTaken = 0,
                MoneyGained = 0,
                DamageDealt = 0,
                DamageReceived = 0;
        }
        #endregion

        #region Methods
        public static void QuickIntro()
        {
            Console.SetCursorPosition(27, 0);
            Console.Write("|");
            MainPlayer.HP = 10;
            Console.SetCursorPosition(43, 0);
            Console.Write("|");
            MainPlayer.Money = 12;
            MainPlayer.Name = "Player";
        }
        #endregion

        #region People
        /// <summary>
        /// Main player
        /// </summary>
        public static Player MainPlayer;

        public static PeopleManager People;
        #endregion

        #region Graphics
        public class Graphics
        {
            public class Objects
            {
                public const char
                    Grass = '.',
                    Chest = '±',
                    Terminal = '#',
                    ATM = '$',
                    Trap = '+';
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

        public static void ClearMessage()
        {
            Console.SetCursorPosition(1, Utils.WindowHeight - 1);
            Console.Write(new string(' ', Utils.WindowWidth - 2));
        }

        /// <summary>
        /// Display what's going on.
        /// </summary>
        /// <param name="text">Event entry.</param>
        public static void Message(string text)
        {
            //TODO: Clean this clutter.
            // Idea: Remove the array and use the substrings directly.

            string[] lines = new string[] { text };
            int length = Utils.WindowWidth - 2;
            const string MoreText = " -- More --";

            if (text.Length > length)
            {
                int ci = 0;
                int start = 0;
                lines = new string[text.Length / (length - MoreText.Length) + 1];

                do
                {
                    if (start + length > text.Length)
                    {
                        lines[ci] = text.Substring(start, text.Length - start);
                        start += length;
                    }
                    else
                    {
                        lines[ci] = text.Substring(start, length - MoreText.Length) + MoreText;
                        start += length - MoreText.Length;
                    }
                    ci++;
                } while (start < text.Length);
            }

            for (int i = 0; i < lines.Length; i++)
            {
                ClearMessage();
                Console.SetCursorPosition(1, Utils.WindowHeight - 1);
                Console.Write(lines[i]);

                if (i < lines.Length - 1)
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