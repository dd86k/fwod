//using System.Collections.Generic;

namespace fwod
{
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
            get { return _name; }
            set { _name = value; }
        }
        #endregion
    }

    static class Items
    {
        internal static void Init()
        {
            Item[] items =
            {
                new Sword("LOL DEV OP PLS NERF", 9001)
            };

            //ItemsList.AddRange(items);
        }

        //internal static List<Item> ItemsList = new List<Item>();
    }

    class Sword : Item
    {
        #region Constructors
        internal Sword(string pName, int pBaseDamage)
            : base(pName)
        {
            _basedamage = pBaseDamage;
        }
        #endregion

        #region Object properties
        int _basedamage;
        internal int BaseDamage
        {
            get { return _basedamage; }
        }
        #endregion
    }
}