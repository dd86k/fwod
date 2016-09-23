/*
    Type extensions
*/

namespace fwod
{
    public static class TypeExtensions
    {
        const string SolidObjects =
            "░▒▓█▄▌▐▀│─┌┐└┘┤┴┬├┼║═╔╗╚╝╣╩╦╠╬╓╖╜╙╢╨╥╟╫╕╛╘╒╡╧╤╞╪";

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

        /// <summary>
        /// Centers text in padding, guarantees provided length.
        /// </summary>
        /// <param name="text">Text to center.</param>
        /// <param name="width">Length of the new string.</param>
        /// <returns>Padded string.</returns>
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

        public static float GetModificationValue(this Modifier c, int b)
        {
            switch (c)
            {
                case Modifier.Broken:
                    return b * 0.5f;

                case Modifier.Rusty:
                    return b * 0.7f;

                case Modifier.Sharp:
                    return b * 1.2f;

                case Modifier.Godly:
                    return b * 2.5f;

                case Modifier.PleaseNerf:
                    return b * 10;

                default: // Normal
                    return b;
            }
        }

        /*
         * This section is only used for Item.cs
         */

        public static int GetBaseDamage(this WeaponType w)
        {
            switch (w)
            {
                case WeaponType.Pistol:
                    return 3;

                case WeaponType.Cutlass:
                    return 6;

                default: return 1;
            }
        }

        public static int GetBaseDefense(this ArmorType a)
        {
            switch (a)
            {
                case ArmorType.No_Armor: return 0;
                case ArmorType.Body_Armor: return 15;

                default: return 1;
            }
        }

        public static int GetBaseRecovery(this FoodType t)
        {
            switch (t)
            {
                case FoodType.Energy_Drink:
                    return 10;

                default: return 1;
            }
        }
        
        public static string GetName(this WeaponType t)
            => t.ToString().Replace('_', ' ');
        public static string GetName(this ArmorType t)
            => t.ToString().Replace('_', ' ');
        public static string GetName(this FoodType t)
            => t.ToString().Replace('_', ' ');

        public static bool IsGun(this WeaponType t)
        {
            switch (t)
            {
                case WeaponType.Pistol:
                    return true;

                default: return false;
            }
        }
    }
}