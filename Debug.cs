using System;

/*
    Used for debugging purposes.
*/

namespace FWoD
{
    internal class Debug
    {
        internal static void StartTests(ref int pReturnInt)
        {
            int testnum = 1;
            try
            {
                Console.Clear();
                #if WINDOWS
                    Console.SetBufferSize(80, 25);
                    Console.SetWindowSize(80, 25);
                #endif

                #region Test #1
                Console.Write("Test #" + testnum + ": Generate 2 boxes of 4x4");

                Game.GenerateBox(Game.TypeOfLine.Single, 0, 2, 4, 4);
                Game.GenerateBox(Game.TypeOfLine.Double, 5, 2, 4, 4);

                Pause(ref testnum);
                #endregion

                #region Test #2
                Console.Write("Test #" + testnum + ": Generate 2 boxes of 3x3");

                Game.GenerateBox(Game.TypeOfLine.Single, 0, 2, 3, 3);
                Game.GenerateBox(Game.TypeOfLine.Double, 5, 2, 3, 3);

                Pause(ref testnum);
                #endregion

                #region Test #3
                Console.Write("Test #" + testnum + ": Generate 2 boxes of 2x2 (Minimum)");

                Game.GenerateBox(Game.TypeOfLine.Single, 0, 2, 2, 2);
                Game.GenerateBox(Game.TypeOfLine.Double, 5, 2, 2, 2);

                Pause(ref testnum);
                #endregion

                #region Test #4
                Console.Write("Test #" + testnum + ": Generate 2 boxes inside of each other");
                
                Game.GenerateBox(Game.TypeOfLine.Double, 1, 2, 4, 4);
                Game.GenerateBox(Game.TypeOfLine.Single, 2, 3, 2, 2);

                Pause(ref testnum);
                #endregion

                #region Test #5
                Console.Write("Test #" + testnum + ": Trying to generate a box with -1, -1 as width and height");

                Game.GenerateBox(Game.TypeOfLine.Double, 1, 1, -1, -1);

                Pause(ref testnum);
                #endregion

                #region Test #6
                Console.Write("Test #" + testnum + ": Create Player with custom position");

                Player p = new Player(Console.BufferWidth / 2, Console.BufferHeight - 3);

                p.Initialize();

                Pause(ref testnum);
                #endregion

                #region Test #7
                Console.Write("Test #" + testnum + ": Make the Player answer something");

                p.Initialize(); // Pause() clears the buffer

                string tt = p.PlayerAnswer();

                Pause(ref testnum);
                #endregion

                #region Test #8
                Console.Write("Test #" + testnum + ": Make the Player say what was asked ");

                p.Initialize(); // Pause() clears the buffer

                p.PlayerSays(tt);

                Pause(ref testnum);
                #endregion

                #region Test #9
                Console.Write("Test #" + testnum + ": Make the Player say some text (== 25 chars)");

                p.Initialize(); // Pause() clears the buffer

                p.PlayerSays("ddddddddddddddddddddddddd");

                Pause(ref testnum);
                #endregion

                #region Test #10
                Console.Write("Test #" + testnum + ": Make the Player say some text (> 25 chars)");

                p.Initialize(); // Pause() clears the buffer

                p.PlayerSays("Hey dere utube im here 2 show u my minecraft tutaliral we gonna have fun!!1");

                Pause(ref testnum);
                #endregion

                #region Test #11
                Console.Write("Test #" + testnum + ": Make the Player say a lot of text");

                p.Initialize(); // Pause() clears the buffer

                p.PlayerSays("A constant member is defined at compile time and cannot be changed at runtime.");

                Pause(ref testnum);
                #endregion

                #region Test #12
                Console.Write("Test #" + testnum + ": Recreate player at X:3 and make him talk");

                p = new Player(3, Console.BufferHeight - 4);

                p.Initialize(); // Pause() clears the buffer

                p.PlayerSays("Didn't you knew that I was there all along?");

                Pause(ref testnum);
                #endregion

                #region Test #13
                Console.Write("Test #" + testnum + ": Recreate player at X:3|Y:1 and make him talk a bit more");

                p = new Player(10, 1);

                p.Initialize(); // Pause() clears the buffer

                p.PlayerSays("Haha! TELEPORTER B O Y S. Okay no I'll stop I swear!");

                Pause(ref testnum);
                #endregion

                #region Test #14
                Console.Write("Test #" + testnum + ": Recreate player at the right and make him talk");

                p = new Player(Console.BufferWidth - 3, Console.BufferHeight - 2);

                p.Initialize(); // Pause() clears the buffer

                p.PlayerSays("I will eat all of your pizza if you don't write me this documentation.");

                Pause(ref testnum);
                #endregion

                Console.Write("All tests passed without any exceptions");

                pReturnInt = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(" !! An error occurred during test #" + testnum + " !!");
                Console.WriteLine("Here's some debugging information:");
                Console.WriteLine("Exception: " + ex.GetType().ToString() + " " + string.Format("(0x{0:X8})", ex.HResult));
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine(" -- Stack --" );
                Console.WriteLine(ex.StackTrace);

                pReturnInt = ex.HResult;
            }
        }

        static void Pause(ref int pTestNum)
        {
            Console.ReadKey(true);
            pTestNum++;
            Console.Clear();
        }
    }
}