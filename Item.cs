namespace fwod
{
    public enum Modifier : byte
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
        public Weapon(WeaponType weapon, Modifier mod = Modifier.Normal)
        {
            Type = weapon;
            Damage = (int)mod.GetModificationValue(weapon.GetBaseDamage());
            Modifier = mod;
        }
        
        public int Damage { get; }
        public WeaponType Type { get; }
        public Modifier Modifier { get; }
        public string Name => $"{Modifier} {Type}";
    }
    #endregion

    #region Armor
    class Armor : Item
    {
        public Armor(ArmorType armor, Modifier mod = Modifier.Normal)
        {
            Type = armor;
            Modifier = mod;
            ArmorPoints = (int)mod.GetModificationValue(armor.GetBaseDefense());
        }

        public ArmorType Type { get; }
        public Modifier Modifier { get; }
        public int ArmorPoints { get; }
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