﻿using System;
using System.Collections.Generic;

/*
    General game mechanics.
*/

namespace fwod
{
    class Game
    {
        #region Properties
        public static bool isPlaying = true;
        public static Menu MainMenu = Menu.GetMainMenu();
        public static int CurrentFloor = 0;
        #endregion

        #region Statistics
        internal class Statistics
        {
            internal static ulong StatEnemiesKilled = 0;
            internal static ulong StatStepsTaken = 0;
            internal static ulong StatMoneyGained = 0;
            internal static ulong StatDamageDealt = 0;
            internal static ulong StatDamageReceived = 0;
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
        /// List of non-enemy people.
        /// </summary>
        public static List<List<Person>> PeopleList = new List<List<Person>>();

        /// <summary>
        /// Main player
        /// </summary>
        public static Player MainPlayer;

        /// <summary>
        /// Determine the Player with position
        /// </summary>
        /// <param name="x">Future left position</param>
        /// <param name="y">Future top position</param>
        /// <returns>Enemy, null if no found</returns>
        internal static Person GetPersonObjectAt(int floor, int x, int y)
        {
            //TODO: Add per level searching. Since the dictionary now exists.
            foreach (Person P in PeopleList[floor])
            {
                if (P.X == x && P.Y == y)
                    return P;
            }

            return null;
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
                internal const char Grass = '.';
                internal const char Ladder = 'H';
                internal const char Chest = 'm';
                internal const char Terminal = 'T';
            }
        }
        #endregion

        #region Box generation
        static internal void GenerateBox(Renderer.Layer layer, int x, int y, int width, int height)
        {
            // Top wall
            Renderer.Write(layer, '┌', x, y);
            Utils.GenerateHorizontalLine(layer, '─', width - 2);
            Renderer.Write(layer, '┐');

            // Side walls
            Console.SetCursorPosition(x, y + 1);
            Utils.GenerateVerticalLine(layer, '│', height - 1);

            Console.SetCursorPosition(x + (width - 1), y + 1);
            Utils.GenerateVerticalLine(layer, '│', height - 1);

            // Bottom wall
            Console.SetCursorPosition(x, y + (height - 1));
            Renderer.Write(layer, '└');
            Utils.GenerateHorizontalLine(layer, '─', width - 2);
            Renderer.Write(layer, '┘');
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
        /// <param name="text">Event entry.</param>
        internal static void Log(string text)
        {
            //TODO: Clean this clutter.
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
        //TODO: New menu for Save/Load function.

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