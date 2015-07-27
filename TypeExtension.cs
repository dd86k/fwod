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
        static readonly string SolidObjects = new string(Game.Graphics.Lines.Double) +
            new string(Game.Graphics.Lines.DoubleConnector) +
            new string(Game.Graphics.Lines.DoubleCorner) +
            new string(Game.Graphics.Lines.DoubleHorizontalConnector) +
            new string(Game.Graphics.Lines.DoubleHorizontalCorner) +
            new string(Game.Graphics.Lines.DoubleVerticalConnector) +
            new string(Game.Graphics.Lines.DoubleVerticalCorner) +
            new string(Game.Graphics.Lines.Single) +
            new string(Game.Graphics.Lines.SingleConnector) +
            new string(Game.Graphics.Lines.SingleCorner) +
            new string(Game.Graphics.Tiles.Grades) +
            new string(Game.Graphics.Tiles.Half);

        internal static bool IsSolidObject(this char pChar)
        {
            // Note: .Contains can't take char and Mono doesn't like ToString()
            return SolidObjects.Contains(string.Format("{0}", pChar));
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
    }
}