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
                case WeaponType.Beretta_92_FS:
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
                case WeaponType.Beretta_92_FS:
                    return true;

                default: return false;
            }
        }
    }
}