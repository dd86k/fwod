/*
    Type extensions
*/

namespace fwod
{
    internal static class TypeExtension
    {
        /// <summary>
        /// Solid objects which the player can't pass through
        /// </summary>
        static readonly string SolidObjects =
            Game.Graphics.Lines.Double.GetString() +
            Game.Graphics.Lines.DoubleConnector.GetString() +
            Game.Graphics.Lines.DoubleCorner.GetString() +
            Game.Graphics.Lines.DoubleHorizontalConnector.GetString() +
            Game.Graphics.Lines.DoubleHorizontalCorner.GetString() +
            Game.Graphics.Lines.DoubleVerticalConnector.GetString() +
            Game.Graphics.Lines.DoubleVerticalCorner.GetString() +
            Game.Graphics.Lines.Single.GetString() +
            Game.Graphics.Lines.SingleConnector.GetString() +
            Game.Graphics.Lines.SingleCorner.GetString() +
            Game.Graphics.Tiles.Grades.GetString() +
            Game.Graphics.Tiles.Half.GetString();

        internal static bool IsSolidObject(this char pChar)
        {
            return SolidObjects.Contains($"{pChar}");
        }

        /// <summary>
        /// Returns true if the char is an enemy
        /// </summary>
        /// <param name="pChar">Char</param>
        /// <returns>True if player</returns>
        internal static bool IsPersonObject(this char pChar)
        {
            foreach (Person Enemy in Game.EnemyList)
            {
                // Return true if we find it in the list
                if (Enemy.CharacterChar == pChar)
                    return true;
            }

            // Return false if not found
            return false;
        }
        
        /// <summary>
        /// Returns the longuest string in a string array.
        /// </summary>
        /// <param name="pArray">String array.</param>
        /// <returns>Longuest string.</returns>
        static public string GetLonguestString(this string[] pArray)
        {
            int max = 0;
            int lastIndex = 0;
            for (int i = 0; i < pArray.Length; i++)
            {
                if (pArray[i].Length > max)
                {
                    max = pArray[i].Length;
                    lastIndex = i;
                }
            }
            return pArray[lastIndex];
        }

        /// <summary>
        /// Returns a string out of a character array.
        /// </summary>
        /// <param name="x">Char array.</param>
        /// <returns>String.</returns>
        static public string GetString(this char[] x) => new string(x);

        static public string Repeat(this char c, int pNumberOfTimes) => new string(c, pNumberOfTimes);
    }
}