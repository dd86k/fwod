using System;
using System.Text;

/*
    Various tools for the command prompt and terminal.
*/

namespace fwod
{
    [Flags]
    public enum Borders : byte
    {
        Top = 1, Right = 2, Bottom = 4, Left = 8,
        All = Top | Right | Bottom | Left
    }

    [Flags]
    public enum Corners : byte
    {
        TopLeft = 1, TopRight = 2, BottomRight = 4, BottomLeft = 8,
        All = TopLeft | TopRight | BottomRight | BottomLeft
    }

    static class Utils
    {
        #region Properties
        /// <summary>
        /// Initial window height. ANSI/ISO screen size.
        /// </summary>
        public const int WindowHeight = 24;
        /// <summary>
        /// Initial window width. ANSI/ISO screen size.
        /// </summary>
        public const int WindowWidth = 80;
        #endregion

        #region Box generation
        /// <summary>
        /// Generates a simple box.
        /// </summary>
        /// <param name="x">Left position.</param>
        /// <param name="y">Top position.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public static void GenerateBox(int x, int y, int width, int height)
        {
            // Top wall
            Console.SetCursorPosition(x, y);
            Console.Write($"┌{new string('─', width - 2)}┐");

            // Side walls
            GenerateVerticalLine('│', height - 2, x, y + 1);
            GenerateVerticalLine('│', height - 2, x + (width - 1), y + 1);

            // Bottom wall
            Console.SetCursorPosition(x, y + (height - 1));
            Console.Write($"└{new string('─', width - 2)}┘");
        }

        /// <summary>
        /// Generates a custom box, might be a bit slower.
        /// </summary>
        /// <param name="x">Left position.</param>
        /// <param name="y">Top position.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="borders"></param>
        /// <param name="corners"></param>
        public static void GenerateCustomBox(int x, int y, int width, int height,
            Borders borders = Borders.All, Corners corners = Corners.All)
        {
            if (corners.HasFlag(Corners.TopLeft))
            {
                Console.SetCursorPosition(x, y);
                Console.Write('┌');
            }

            if (borders.HasFlag(Borders.Top))
            {
                Console.SetCursorPosition(x + 1, y);
                Console.Write(new string('─', width - 2));
            }

            if (corners.HasFlag(Corners.TopRight))
            {
                Console.SetCursorPosition(x + width - 1, y);
                Console.Write('┐');
            }

            // Side walls
            if (borders.HasFlag(Borders.Left))
                GenerateVerticalLine('│', height - 2, x, y + 1);
            if (borders.HasFlag(Borders.Right))
                GenerateVerticalLine('│', height - 2, x + (width - 1), y + 1);

            // Bottom wall
            int ym = y + (height - 1);

            if (corners.HasFlag(Corners.BottomLeft))
            {
                Console.SetCursorPosition(x, ym);
                Console.Write('└');
            }

            if (borders.HasFlag(Borders.Bottom))
            {
                Console.SetCursorPosition(x + 1, ym);
                Console.Write(new string('─', width - 2));
            }
            
            if (corners.HasFlag(Corners.BottomRight))
            {
                Console.SetCursorPosition(x + width - 1, ym);
                Console.Write('┘');
            }
        }

        /// <summary>
        /// Generate a box with a grill in it.
        /// </summary>
        /// <param name="x">Left position.</param>
        /// <param name="y">Top position.</param>
        /// <param name="columns">Columns.</param>
        /// <param name="rows">Rows.</param>
        public static unsafe void GenerateGrill(int x, int y,
            int columns, int rows)
        {
            // Width and height
            int w = (columns * 2) + 1;
            int h = (rows * 2);
            // Top, horizontal, vertical, and bottom lines/buffers.
            string tbuf = $"┌{new string('┬', w - 2)}┐";
            string hbuf = $"├{new string('┼', w - 2)}┤";
            string vbuf = $"│{new string('│', w - 2)}│";
            string bbuf = $"└{new string('┴', w - 2)}┘";

            fixed (char* pt = tbuf)
            fixed (char* pv = vbuf)
            fixed (char* ph = hbuf)
            fixed (char* pb = bbuf)
            {
                for (int i = 1; i < hbuf.Length; i += 2)
                {
                    pt[i] = '─';
                    ph[i] = '─';
                    pv[i] = ' ';
                    pb[i] = '─';
                }
            }

            bool b = y % 2 == 0;

            int ly = y + h;
            int iy = y;
            Console.SetCursorPosition(x, iy++);
            Console.Write(tbuf);
            if (b)
                for (; iy < ly; iy++)
                {
                    Console.SetCursorPosition(x, iy);

                    if (iy % 2 == 0)
                        Console.Write(hbuf);
                    else
                        Console.Write(vbuf);
                }
            else
                for (; iy < ly; iy++)
                {
                    Console.SetCursorPosition(x, iy);

                    if (iy % 2 == 0)
                        Console.Write(vbuf);
                    else
                        Console.Write(hbuf);
                }
            Console.SetCursorPosition(x, iy);
            Console.Write(bbuf);
        }
        #endregion

        #region GenH
        static public void GenerateHorizontalLine(char c, int len)
        {
            GenerateHorizontalLine(c, Console.CursorLeft, Console.CursorTop, len);
        }

        static public void GenerateHorizontalLine(char c, int x, int y, int len)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(new string(c, len));
        }
        #endregion

        #region GenV
        static public void GenerateVerticalLine(char c, int len, int x, int y)
        {
            int l = y + len;
            for (int i = y; i < l; i++)
            {
                Console.SetCursorPosition(x, i);
                Console.Write(c);
            }
        }
        #endregion

        #region Centering
        public static unsafe string Center(this string text, int width)
        {
            if (text.Length > width)
                text = text.Substring(0, width);
            
            int s = (width / 2) - (text.Length / 2);
            string t = new string(' ', width);
            fixed (char* pt = t)
                for (int i = 0; i < text.Length; i++)
                    pt[s + i] = text[i];

            return t;
        }

        public static void CenterAndWrite(string text)
        {
            Console.SetCursorPosition((WindowWidth / 2) - (text.Length / 2), Console.CursorTop);
            Console.Write(text);
        }

        public static void CenterAndWriteLine(string text)
        {
            Console.SetCursorPosition((WindowWidth / 2) - (text.Length / 2), Console.CursorTop);
            Console.WriteLine(text);
        }
        #endregion

        #region Read
        /// <summary>
        /// Readline with a maximum length plus optional password mode.
        /// </summary>
        /// <param name="length">Input size/buffer</param>
        /// <param name="pPassword">Is password</param>
        /// <returns>User's input</returns>
        public static unsafe string ReadLine(int length, bool pPassword = false)
        {
            StringBuilder o = new StringBuilder();
            int index = 0;
            bool c = true;
            int oleft = Console.CursorLeft; // Origninal Left Position
            int otop = Console.CursorTop; // Origninal Top Position

            Console.CursorVisible = true;

            while (c)
            {
                ConsoleKeyInfo k = Console.ReadKey(true);

                switch (k.Key)
                {
                    // Ignore keys
                    case ConsoleKey.Tab:
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                        break;

                    // Returns the string
                    case ConsoleKey.Enter:
                        c = false;
                        break;

                    // Navigation
                    case ConsoleKey.LeftArrow:
                        if (index > 0)
                        {
                            Console.SetCursorPosition(oleft + --index, otop);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (index < o.Length)
                        {
                            Console.SetCursorPosition(oleft + ++index, otop);
                        }
                        break;
                    case ConsoleKey.Home:
                        if (index > 0)
                        {
                            index = 0;
                            Console.SetCursorPosition(oleft, otop);
                        }
                        break;
                    case ConsoleKey.End:
                        if (index < o.Length)
                        {
                            index = o.Length;
                            Console.SetCursorPosition(oleft + index, otop);
                        }
                        break;

                    case ConsoleKey.Delete:
                        if (index < o.Length)
                        {
                            // Erase whole from index
                            if (k.Modifiers == ConsoleModifiers.Control)
                            {
                                o = o.Remove(index, o.Length - index);
                                Console.SetCursorPosition(oleft, otop);
                                Console.Write(new string(' ', length));
                                Console.SetCursorPosition(oleft, otop);
                                Console.Write(pPassword ? new string('*', o.Length) : o.ToString());
                                Console.SetCursorPosition(oleft + index, otop);
                            }
                            else // Erase one character
                            {
                                o = o.Remove(index, 1);
                                Console.SetCursorPosition(oleft, otop);
                                Console.Write(new string(' ', length));
                                Console.SetCursorPosition(oleft, otop);
                                Console.Write(pPassword ? new string('*', o.Length) : o.ToString());
                                Console.SetCursorPosition(oleft + index, otop);
                            }
                        }
                        break;

                    case ConsoleKey.Backspace:
                        if (index > 0)
                        {
                            // Erase whole from index
                            if (k.Modifiers == ConsoleModifiers.Control)
                            {
                                o = o.Remove(0, index);
                                index = 0;
                                Console.SetCursorPosition(oleft, otop);
                                Console.Write(new string(' ', length));
                                Console.SetCursorPosition(oleft, otop);
                                Console.Write(pPassword ? new string('*', o.Length) : o.ToString());
                                Console.SetCursorPosition(oleft + index, otop);
                            }
                            else // Erase one character
                            {
                                o = o.Remove(--index, 1);
                                Console.SetCursorPosition(oleft, otop);
                                Console.Write(new string(' ', length));
                                Console.SetCursorPosition(oleft, otop);
                                Console.Write(pPassword ? new string('*', o.Length) : o.ToString());
                                Console.SetCursorPosition(oleft + index, otop);
                            }
                        }
                        break;

                    default:
                        if (o.Length < length)
                        {
                            char h = k.KeyChar;

                            if (char.IsLetterOrDigit(h) || char.IsPunctuation(h) || char.IsSymbol(h) || char.IsWhiteSpace(h))
                            {
                                o.Insert(index++, h);
                                Console.SetCursorPosition(oleft, otop);
                                Console.Write(new string(' ', length));
                                Console.SetCursorPosition(oleft, otop);
                                Console.Write(pPassword ? new string('*', o.Length) : o.ToString());
                                Console.SetCursorPosition(oleft + index, otop);
                            }
                        }
                        break;
                }
            }

            try
            {
                Console.CursorVisible = false;
            }
            catch {}

            return o.ToString();
        }
        #endregion
    }
}