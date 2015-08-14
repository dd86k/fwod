#if DEBUG
using System;
using System.Diagnostics;

/*
    Used for debugging purposes.
*/

namespace fwod
{
    internal class Debug
    {
        internal static void StartTests(ref int pReturnInt)
        {
            int testnum = 0;
            try
            {
                Console.Clear();

                /* ==== 1 ===== */

                Console.Write("Test #" + testnum + ": Show what's in Graphics");
                
                Console.WriteLine();
                Console.WriteLine("Grades                   : " + new string(Game.Graphics.Tiles.Grades));
                Console.WriteLine("Half                     : " + new string(Game.Graphics.Tiles.Half));
                Console.WriteLine("Single                   : " + new string(Game.Graphics.Lines.Single));
                Console.WriteLine("SingleConnector          : " + new string(Game.Graphics.Lines.SingleConnector));
                Console.WriteLine("SingleCorner             : " + new string(Game.Graphics.Lines.SingleCorner));
                Console.WriteLine("Double                   : " + new string(Game.Graphics.Lines.Double));
                Console.WriteLine("DoubleConnector          : " + new string(Game.Graphics.Lines.DoubleConnector));
                Console.WriteLine("DoubleCorner             : " + new string(Game.Graphics.Lines.DoubleCorner));
                Console.WriteLine("DoubleHorizontalConnector: " + new string(Game.Graphics.Lines.DoubleHorizontalConnector));
                Console.WriteLine("DoubleHorizontalCorner   : " + new string(Game.Graphics.Lines.DoubleHorizontalCorner));
                Console.WriteLine("DoubleVerticalConnector  : " + new string(Game.Graphics.Lines.DoubleVerticalConnector));
                Console.WriteLine("DoubleVerticalCorner     : " + new string(Game.Graphics.Lines.DoubleVerticalCorner));

                Pause(ref testnum);

                /* ==== 2 ===== */
                Console.Write("Test #" + testnum + ": Generate 2 boxes of 4x4");

                Game.GenerateBox(Core.Layer.Game, Game.TypeOfLine.Single, 0, 2, 4, 4);
                Game.GenerateBox(Core.Layer.Game, Game.TypeOfLine.Double, 5, 2, 4, 4);

                Pause(ref testnum);

                /* ==== 3 ===== */
                Console.Write("Test #" + testnum + ": Generate 2 boxes of 3x3");

                Game.GenerateBox(Core.Layer.Game, Game.TypeOfLine.Single, 0, 2, 3, 3);
                Game.GenerateBox(Core.Layer.Game, Game.TypeOfLine.Double, 5, 2, 3, 3);

                Pause(ref testnum);

                /* ==== 4 ===== */
                Console.Write("Test #" + testnum + ": Generate 2 boxes of 2x2 (Minimum)");

                Game.GenerateBox(Core.Layer.Game, Game.TypeOfLine.Single, 0, 2, 2, 2);
                Game.GenerateBox(Core.Layer.Game, Game.TypeOfLine.Double, 5, 2, 2, 2);

                Pause(ref testnum);

                /* ==== 5 ===== */
                Console.Write("Test #" + testnum + ": Generate 2 boxes inside of each other");

                Game.GenerateBox(Core.Layer.Game, Game.TypeOfLine.Double, 1, 2, 4, 4);
                Game.GenerateBox(Core.Layer.Game, Game.TypeOfLine.Single, 2, 3, 2, 2);

                Pause(ref testnum);

                /* ==== 6 ===== */
                Console.Write("Test #" + testnum + ": Create Player with custom position");

                Player p = new Player(ConsoleTools.WindowWidth / 2, ConsoleTools.WindowHeight - 3);

                p.Initialize();

                Pause(ref testnum);

                /* ==== 7 ===== */
                Console.Write("Test #" + testnum + ": Make the Player answer something");

                p.Initialize(); // Pause() clears the buffer

                ConsoleTools.WriteAndCenter(Core.Layer.People, "What is your favorite food?", p.PosY - 5);

                string tt = p.GetAnswer();

                Pause(ref testnum);

                /* ==== 8 ===== */
                Console.Write("Test #" + testnum + ": Make the Player say what was asked ");

                p.Initialize(); // Pause() clears the buffer

                p.Say(tt == null ? "Nothing!" : tt);

                Pause(ref testnum);

                /* ==== 9 ===== */
                Console.Write("Test #" + testnum + ": Make the Player say some text (== 25 chars)");

                p.Initialize(); // Pause() clears the buffer

                p.Say("ddddddddddddddddddddddddd");

                Pause(ref testnum);

                /* ==== 10 ===== */
                Console.Write("Test #" + testnum + ": Make the Player say some text (> 25 chars)");

                p.Initialize(); // Pause() clears the buffer

                p.Say("Hey dere utube im here 2 show u my minecraft tutaliral we gonna have fun!!1");

                Pause(ref testnum);

                /* ==== 11 ===== */
                Console.Write("Test #" + testnum + ": Make the Player say a lot of text");

                p.Initialize(); // Pause() clears the buffer

                p.Say("A constant member is defined at compile time and cannot be changed at runtime.");

                Pause(ref testnum);
                
                /* ==== 12 ===== */
                Console.Write("Test #" + testnum + ": Say ascii art");

                string[] t =
                {
                    "   -  -::   --                               ",
                    "  /:/: -:: -::-                              ",
                    "   -+ /::-:-: :       :::::::      -::::::-  ",
                    "     /: //-::-:      :       :/-  --      -/:",
                    "   :///- -  - -:      -/////:       //////   ",
                    " -/ ://:       :     +ooo/  -/    -oooo:  :: ",
                    "---/    /-      :   /oooooo-  :   oooooo+  : ",
                    "/ /     :-       :  sssssss+  :  /sssssss-  /",
                    "-o/////:-           +ssssso-  /  -ssssss/  --",
                    " ://///:----        -ossso:  :-   /ssss+  -: ",
                    "       -----:///::    /+/::/:-     -++/::/-  ",
                    "                 -       -            --     "
                };

                p.Initialize();
                p.Say(t, true);

                Pause(ref testnum);

                /* ==== 13 ===== */
                Console.Write("Test #" + testnum + ": Recreate player at X:3 and make him talk");

                p = new Player(3, ConsoleTools.WindowHeight - 4);

                p.Initialize(); // Pause() clears the buffer

                p.Say("Didn't you knew that I was there all along?");

                Pause(ref testnum);

                /* ==== 14 ===== */
                Console.Write("Test #" + testnum + ": Recreate player at X:3|Y:1 and make him talk a bit more");

                p = new Player(10, 1);

                p.Initialize(); // Pause() clears the buffer

                p.Say("Haha! TELEPORTER B O Y S. Okay no I'll stop I swear!");

                Pause(ref testnum);

                /* ==== 15 ===== */
                Console.Write("Test #" + testnum + ": Recreate player at the right and make him talk");

                p = new Player(ConsoleTools.WindowWidth - 3, ConsoleTools.WindowHeight - 2);

                p.Initialize(); // Pause() clears the buffer

                p.Say("I will eat all of your pizza if you don't write me this documentation.");

                Pause(ref testnum);

                /* ==== 16 ===== */
                Console.Write("Test #" + testnum + ": Multi-layer test");

                // Write
                Core.Write(Core.Layer.Game, "Test", 0, 2);
                // Wait
                Console.ReadKey(true);
                // Erase
                Console.SetCursorPosition(0, 2);
                Console.Write("    ");
                // Wait
                Console.ReadKey(true);
                // Retrieve info
                string tmp = string.Empty;
                for (int i = 0; i < 4; i++)
                {
                    tmp += Core.GetCharAt(Core.Layer.Game, i, 2);
                }
                // Print back
                Console.SetCursorPosition(0, 2);
                Console.Write(tmp);

                Pause(ref testnum);

                /* ==== 17 ===== */
                Console.Write("Test #" + testnum + ": Multi-layer bubble test");

                Console.ReadKey(true);
                // Fill buffer
                Core.FillScreen(Core.Layer.Game, '.');
                // Wait
                Console.ReadKey(true);
                // Place player

                p.Initialize();
                p.Say("Woah!");

                Pause(ref testnum);

                /* ==== 18 ===== */
                Console.Write("Test #" + testnum + ": Multi-layer player test");

                Console.ReadKey(true);

                Core.FillScreen(Core.Layer.Game, '.');

                p.Initialize();

                bool isTesting = true;
                do
                {
                    fwod.MainClass.Entry();
                } while (isTesting);

                Pause(ref testnum);

                /* ==== 19 ===== */
                Console.Write("Test #" + testnum + ": Menu test");

                DisplayMenu.Show(DisplayMenu.MainMenuItems);

                Pause(ref testnum);
                
                Console.Write("All tests passed without any exceptions");

                pReturnInt = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("    !! An error occurred during test #" + testnum + " !!");
                Console.WriteLine("Exception: " + string.Format("{0}", ex.GetType()) + " - " + string.Format("0x{0:X8}", ex.HResult));
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine("    -- Stack --");
                Console.WriteLine(ex.StackTrace);

                pReturnInt = ex.HResult;
            }
        }

        static void Pause(ref int pTestNum)
        {
            pTestNum++;
            Console.ReadKey(true);
            Core.ClearAllLayers();
        }

        static internal void TalkTest(string pText)
        {
            Console.Clear();
            Person p = new Person(ConsoleTools.WindowWidth / 2, ConsoleTools.WindowHeight - 3);
            p.CharacterChar = '@';
            p.Initialize();
            p.Say(pText);
            Console.Clear();
        }

        static internal void SpeedTalkTest()
        {
            Console.Clear();
            Person p = new Person(ConsoleTools.WindowWidth / 2, ConsoleTools.WindowHeight - 3);
            p.CharacterChar = '@';
            p.Initialize();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 25000; i++)
            {
                p.Say(string.Format("{0:00000}", i), false);
            }
            sw.Stop();
            p.Say(string.Format("{0}", sw.Elapsed));
            Console.Clear();
        }
    }
}
#endif