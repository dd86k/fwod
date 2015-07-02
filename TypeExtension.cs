namespace fwod
{
    internal static class TypeExtension
    {
        const string SolidObjects = "░▒▓█▄█▀│─┤┴┬├┼┌┐┘└║═╣╩╦╠╬╔╗╝╚╣╩╦╠╬╗╝╚╔╣╩╦╠╬╔╗╝╚";

        internal static bool IsSolidObject(this char pChar)
        {
            // .Contains can't take char and Mono doesn't like ToString()
            return SolidObjects.Contains(string.Format("{0}", pChar));
        }
    }
}