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

        internal static bool IsPlayerObject(this char pChar)
        {
            

            return false;
        }
    }
}