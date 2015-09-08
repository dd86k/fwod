namespace fwod
{
    #region Item base class
    class Item
    {
        #region Constructors
        internal Item(string pName)
        {
            _name = pName;
        }
        #endregion

        #region Object properties
        string _name;
        internal string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
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
            _prefix = pWeaponPrefix;
        }
        #endregion

        #region Object properties
        int _basedamage;
        internal double BaseDamage
        {
            get
            {
                switch (_prefix)
                {
                    case Prefix.Broken:
                        return _basedamage * 0.5;
                    case Prefix.Rusty:
                        return _basedamage * 0.7;
                    case Prefix.Sharp:
                        return _basedamage * 1.2;
                    case Prefix.Godly:
                        return _basedamage * 1.8;
                    case Prefix.OPPLSNERF:
                        return _basedamage * 4.5;
                    default:
                        return _basedamage;
                }
            }
        }

        internal enum Prefix
        {
            Normal, Broken, Rusty, Sharp, Godly, OPPLSNERF
        }

        Prefix _prefix;
        internal Prefix Modifier
        {
            get { return _prefix; }
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
            _armorpoints = pArmorPoints;
        }
        #endregion

        #region Properties
        int _armorpoints;
        internal int ArmorPoints
        {
            get { return _armorpoints; }
        }
        #endregion
    }
    #endregion

    #region Drinks
    class Drink : Item
    {
        internal Drink(string pName, int pRestorePoints)
            : base(pName)
        {
            _restorepoints = pRestorePoints;
        }

        int _restorepoints;
        internal int RestorePoints
        {
            get { return _restorepoints; }
        }
    }
    #endregion
}