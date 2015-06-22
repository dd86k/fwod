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
            ConsoleTools.GenerateHorizontalLine(Graphics.Walls.Thick[1], lenW);
            Console.Write(Graphics.Walls.Thick[3]);

            // Side walls

            Console.SetCursorPosition(1, 2);

            int lenH = Console.BufferHeight - 2;
            Console.SetCursorPosition(1, 2);
            ConsoleTools.GenerateVerticalLine(Graphics.Walls.Thick[0], lenH);

            lenW += 2;
            Console.SetCursorPosition(lenW, 2);
            ConsoleTools.GenerateVerticalLine(Graphics.Walls.Thick[0], lenH);

            // Bottom wall

            Console.SetCursorPosition(1, lenH);
            Console.Write(Graphics.Walls.Thick[5]);

            lenW -= 2;
            Console.SetCursorPosition(2, lenH);
            ConsoleTools.GenerateHorizontalLine(Graphics.Walls.Thick[1], lenW);

            Console.Write(Graphics.Walls.Thick[4]);
        }
    }
}