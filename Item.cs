namespace fwod
{
    public enum WeaponModifier : byte
    {
        Normal, Broken, Rusty, Sharp, Godly, PleaseNerf
    }

    static class ModifierHelper
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
    }

    #region Item base class
    class Item
    {
        #region Constructors
        internal Item(string name)
        {
            Name = name;
        }
        #endregion

        #region Object properties
        internal string Name
        {
            get;
        }
        #endregion
    }
    #endregion

    #region Weapons
    class Weapon : Item
    {
        #region Construction
        internal Weapon(string name, int baseDamage)
            : this(name, baseDamage, WeaponModifier.Normal)
        {

        }

        internal Weapon(string name, int baseDamage, WeaponModifier weaponmod)
            : base(name)
        {
            BaseDamage = baseDamage;
            Enhancement = weaponmod;
        }
        #endregion

        #region Object properties
        public new string Name => $"{Enhancement} {base.Name}";
        public int BaseDamage { get; }
        public float Damage => Enhancement.GetDamage(BaseDamage);
        public WeaponModifier Enhancement { get; }
        #endregion
    }

    class Sword : Weapon
    {
        #region Construction
        internal Sword(string name, int baseDamage)
            : base(name, baseDamage)
        {

        }
        #endregion
    }
    #endregion

    #region Armor
    class Armor : Item
    {
        #region Construstion
        //TODO: Expand armor to include boots, leggings, etc.
        internal Armor(string name, int armorPoints)
            : base(name)
        {
            ArmorPoints = armorPoints;
        }
        #endregion

        #region Properties
        internal int ArmorPoints { get; }
        #endregion
    }
    #endregion

    #region Drinks
    class Food : Item
    {
        internal Food(string name, int restorePoints)
            : base(name)
        {
            RestorePoints = restorePoints;
        }
        
        internal int RestorePoints { get; }
    }
    #endregion
}