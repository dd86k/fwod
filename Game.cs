using System;

namespace Play
{
    internal class Game
    {
        internal struct Graphics
        {
            internal struct Tiles
            {
                internal static char[] Grades = new char[] {'░', '▒', '▓', '█'};
                internal static char[] Half = new char[] {'▄', '▌', '▐', '▀'};
            }
            internal struct Walls
            {
                internal static char[] Thin = {'│', '─', '┌', '┐', '┘', '└', '┤', '┴', '┬', '├', '┼'};
                internal static char[] Thick = {'║', '═', '╔', '╗', '╝', '╚', '╣', '╩', '╦', '╠', '╬'};
                internal static char[] InnerThin = {'╢', '╟', '╖', '╜', '╧', '╤', '╙', '╒', '╫'};
                internal static char[] InnerThick = {'╡', '╞', '╕', '╛', '╨', '╥', '╘', '╓', '╪'};
            }
        }

        /// <summary>
        /// Generates the master room (outer walls).
        /// </summary>
        static internal void GenerateMasterRoom()
        {
            // Top wall
            Console.SetCursorPosition(1, 1);
            Console.Write(Graphics.Walls.Thick[2]);

            int lenW = Console.BufferWidth - 4;
            for (int i = 0; i < lenW; i++)
            {
                Console.Write(Graphics.Walls.Thick[1]);
            }

            Console.Write(Graphics.Walls.Thick[3]);

            // Side walls

            Console.SetCursorPosition(1, 2);

            int lenH = Console.BufferHeight - 2;
            for (int i = 2; i < lenH; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write(Graphics.Walls.Thick[0]);
            }
            lenW += 2;
            for (int i = 2; i < lenH; i++)
            {
                Console.SetCursorPosition(lenW, i);
                Console.Write(Graphics.Walls.Thick[0]);
            }

            // Bottom wall

            Console.SetCursorPosition(1, lenH);
            Console.Write(Graphics.Walls.Thick[5]);

            lenW -= 2;
            Console.SetCursorPosition(2, lenH);
            for (int i = 0; i < lenW; i++)
            {
                Console.Write(Graphics.Walls.Thick[1]);
            }

            Console.Write(Graphics.Walls.Thick[4]);
        }
    }
}