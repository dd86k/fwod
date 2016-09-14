/*
    Type extensions
*/

namespace fwod
{
    internal static class TypeExtension
    {
        const string SolidObjects =
            "░▒▓█▄▌▐▀│─┌┐┘└┤┴┬├┼║═╔╗╝╚╣╩╦╠╬╓╖╜╙╢╨╥╟╫╕╛╘╒╡╧╤╞╪";

        internal static bool IsSolidObject(this char c)
        {
            return SolidObjects.Contains(c.ToString());
        }

        /// <summary>
        /// Returns true if the char is an enemy
        /// </summary>
        /// <param name="c">Char</param>
        /// <returns>True if player</returns>
        internal static bool IsPersonObject(this char c)
        {
            foreach (Person Enemy in Game.EnemyList)
            {
                // Return true if we find it in the list
                if (Enemy.CharacterChar == c)
                    return true;
            }

            // Return false if not found
            return false;
        }
        
        /// <summary>
        /// Returns the longuest string in a string array.
        /// </summary>
        /// <param name="a">String array.</param>
        /// <returns>Longuest string.</returns>
        static public int GetLonguestStringLength(this string[] a)
        {
            int max = 0;
            int lastIndex = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].Length > max)
                {
                    max = a[i].Length;
                    lastIndex = i;
                }
            }
            return max;
        }
    }
}