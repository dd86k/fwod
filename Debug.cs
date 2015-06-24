using System;

/*
    Used for debugging purposes.
*/

namespace FWoD
{
    internal class Debug
    {
        internal static void StartTests(out int pReturnInt)
        {
            int testnum = 1;
            try
            {
                Console.Clear();

                Console.Write("Generate 2 boxes of 4x4");

                Game.GenerateBox(Game.TypeOfLine.Single, 0, 1, 4, 4);
                Game.GenerateBox(Game.TypeOfLine.Double, 5, 1, 4, 4);

                Console.ReadKey(true);
                testnum++;
                Console.Clear();

                Console.Write("Generate 2 boxes of 2x2 (Minimum)");

                Game.GenerateBox(Game.TypeOfLine.Single, 0, 1, 2, 2);
                Game.GenerateBox(Game.TypeOfLine.Double, 5, 1, 2, 2);

                Console.ReadKey(true);
                testnum++;
                Console.Clear();

                Console.Write("Generate 2 boxes inside of each other");
                
                Game.GenerateBox(Game.TypeOfLine.Double, 1, 1, 4, 4);
                Game.GenerateBox(Game.TypeOfLine.Single, 2, 2, 2, 2);

                Console.ReadKey(true);
                testnum++;
                Console.Clear();

                Console.Write("Trying to generate a box with -1, -1 as width and height");

                Game.GenerateBox(Game.TypeOfLine.Double, 1, 1, -1, -1);

                Console.ReadKey(true);
                testnum++;
                Console.Clear();

                // Future tests...

                pReturnInt = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("An error occurred during test #" + testnum + ".");
                Console.WriteLine("Here's some debugging information:");
                Console.WriteLine("Exception: " + ex.GetType().ToString());
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine(" -- Stack --" );
                Console.WriteLine(ex.StackTrace);

                pReturnInt = 1; // Return exception number/hash instead?
            }
        }
    }
}