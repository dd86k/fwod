namespace fwod
{
    #region Item base class
    class Item
    {
        #region Constructors
        internal Item(string pName)
        {
            Name = pName;
        }
        #endregion

        #region Object properties
        internal string Name
        {
            get; private set;
        }
        #endregion
    }
    #endregion

    #region Weapons
    class Weapon : Item
    {
        #region Construction
        internal Weapon(string pName, int pBaseDamage)
            : this(pName, pBaseDamage, Prefix.Normal)
        {

        }

        internal Weapon(string pName, int pBaseDamage, Prefix pWeaponPrefix)
            : base(pName)
        {
            _basedamage = pBaseDamage;
            Modifier = pWeaponPrefix;
        }
        #endregion

        #region Object properties
        internal new string Name
        {
            get
            {
                return $"{Modifier} {base.Name}";
            }
        }

        int _basedamage;
        internal double BaseDamage
        {
            get
            {
                switch (Modifier)
                {
                    case Prefix.Broken:
                        return _basedamage * 0.5;

                    case Prefix.Rusty:
                        return _basedamage * 0.7;

                    case Prefix.Sharp:
                        return _basedamage * 1.2;

                    case Prefix.Godly:
                        return _basedamage * 2.5;

                    case Prefix.OP_AF:
                        return _basedamage * 10;

                    default: // Normal
                        return _basedamage;
                }
            }
        }

        internal enum Prefix : byte
        {
            Normal, Broken, Rusty, Sharp, Godly, OP_AF
        }
        
        internal Prefix Modifier
        {
            get;
            set;
        }
        #endregion
    }

    class Sword : Weapon
    {
        #region Construction
        internal Sword(string pName, int pBaseDamage)
            : base(pName, pBaseDamage)
        {

        }
        #endregion
    }
    #endregion

    #region Armor
    class Armor : Item
    {
        #region Construstion
        internal Armor(string pName, int pArmorPoints)
            : base(pName)
        {
            ArmorPoints = pArmorPoints;
        }
        #endregion

        #region Properties
        internal int ArmorPoints
        {
            get;
            private set;
        }
        #endregion
    }
    #endregion

    #region Drinks
    class Food : Item
    {
        internal Food(string pName, int pRestorePoints)
            : base(pName)
        {
            RestorePoints = pRestorePoints;
        }
        
        internal int RestorePoints
        {
            get;
            private set;
        }
    }
    #endregion
}