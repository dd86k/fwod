namespace fwod
{
    public enum WeaponModifier : byte
    {
        Normal, Broken, Rusty, Sharp, Godly, PleaseNerf
    }

    public enum WeaponType : byte
    {
        Unarmed,
        Cutlass,
        Beretta92FS
    }

    public enum ArmorType : byte
    {
        Shirt,

    }

    public enum ArmorModifier : byte
    {
        Normal,
    }

    static class WeaponHelper
    {
        public static float GetDamage(this WeaponModifier c, int baseDamage)
        {
            switch (c)
            {
                case WeaponModifier.Broken:
                    return baseDamage * 0.5f;

                case WeaponModifier.Rusty:
                    return baseDamage * 0.7f;

                case WeaponModifier.Sharp:
                    return baseDamage * 1.2f;

                case WeaponModifier.Godly:
                    return baseDamage * 2.5f;

                case WeaponModifier.PleaseNerf:
                    return baseDamage * 10;

                default: // Normal
                    return baseDamage;
            }
        }
        /*
        public static int GetBaseDamage(this GunType gun)
        {
        }
        */
    }

    #region Item base
    class Item
    {

    }
    #endregion

    #region Weapons
    /// <summary>
    /// Base class for a weapon.
    /// </summary>
    class Weapon : Item
    {
        public Weapon(WeaponType weapon, int baseDamage,
            WeaponModifier weaponMod = WeaponModifier.Normal)
        {
            BaseDamage = baseDamage;
            Modifier = weaponMod;
        }
        
        public int BaseDamage { get; }
        public ushort Damage => (ushort)Modifier.GetDamage(BaseDamage);
        public WeaponType Type { get; }
        public WeaponModifier Modifier { get; }
        public string Name => $"{Modifier} {Type}";
    }
    #endregion

    #region Armor
    class Armor : Item
    {
        public Armor(ArmorType armor, ushort armorPoints,
            ArmorModifier mod = ArmorModifier.Normal)
        {
            Type = armor;
            Modifier = mod;
            ArmorPoints = armorPoints;
        }

        public ArmorType Type { get; }
        public ArmorModifier Modifier { get; }
        //TODO: ap * mod
        public ushort ArmorPoints { get; }
        public string Name => $"{Modifier} {Type}";
    }
    #endregion

    #region Nutrition
    class Food : Item
    {
        public Food(string name, int restorePoints)
        {
            RestorePoints = restorePoints;
        }
        
        public int RestorePoints { get; }
    }
    #endregion
}