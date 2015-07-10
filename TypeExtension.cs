using System.Collections.Generic;

namespace fwod
{
    internal static class TypeExtension
    {
        const string SolidObjects = "░▒▓█▄█▀│─┤┴┬├┼┌┐┘└║═╣╩╦╠╬╔╗╝╚╣╩╦╠╬╗╝╚╔╣╩╦╠╬╔╗╝╚";

        internal static bool IsSolidObject(this char pChar)
        {
            //TODO: Collisions also includes an enemy
            // .Contains can't take char and Mono doesn't like ToString()
            return SolidObjects.Contains(string.Format("{0}", pChar));
        }

        /// <summary>
        /// Returns true if the char is an enemy
        /// </summary>
        /// <param name="pChar">Char</param>
        /// <returns>True if player</returns>
        internal static bool IsEnemyObject(this char pChar)
        {
            foreach (Player Enemy in Game.EnemyList)
            {
                // Return true if we find it in the list
                if (Enemy.CharacterChar == pChar)
                    return true;
            }

            // Return false if Count == 0 or not found
            return false;
        }
    }
}