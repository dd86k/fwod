/*
    Type extensions
*/

namespace fwod
{
    public static class TypeExtension
    {
        const string SolidObjects =
            "░▒▓█▄▌▐▀│─┌┐┘└┤┴┬├┼║═╔╗╝╚╣╩╦╠╬╓╖╜╙╢╨╥╟╫╕╛╘╒╡╧╤╞╪";

        public static bool IsSolidObject(this char c)
        {
            return SolidObjects.Contains(c.ToString());
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