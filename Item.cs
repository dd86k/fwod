/*
 * Items.
 */

namespace fwod
{
    /// <summary>
    /// Item modifier (Weapon and Armor).
    /// </summary>
    /// <remarks>Does not apply to Food.</remarks>
    public enum ItemModifier : byte
    {
        Normal, Broken, Rusty, Sharp, Godly, PleaseNerf
    }

    public enum WeaponType : byte
    {
        // Ranged
        Pistol,

        // Swords
        Cutlass,
        Katana
    }

    public enum ArmorType : byte
    {
        Body_Armor
    }

    public enum FoodType : byte
    {
        Energy_Drink,
    }
    
    class Item
    {

        public char this[int i]
        {
            get
            {
                return ToString()[i];
            }
        }

        public static implicit operator string(Item i)
        {
            return i.ToString();
        }
    }
    
    /// <summary>
    /// Base class for a weapon.
    /// </summary>
    class Weapon : Item
    {
        public Weapon(WeaponType weapon, ItemModifier mod = ItemModifier.Normal)
        {
            Type = weapon;
            Damage = mod.GetModdedValue(weapon.GetBaseDamage());
            Modifier = mod;
            Name = Type.GetName();
        }

        public int Damage { get; }
        public WeaponType Type { get; }
        public ItemModifier Modifier { get; }
        public string FullName =>
            Modifier == ItemModifier.Normal ? Name : $"{Modifier} {Name}";
        public string Name { get; }

        public bool IsRanged
        {
            get
            {
                switch (Type)
                {
                    case WeaponType.Pistol:
                        return true;
                    default: return false;
                }
            }
        }

        public override string ToString() => Name;
    }

    class Armor : Item
    {
        public Armor(ArmorType armor, ItemModifier mod = ItemModifier.Normal)
        {
            Type = armor;
            Modifier = mod;
            ArmorPoints = mod.GetModdedValue(armor.GetBaseDefense());
            Name = Type.GetName();
        }

        public ArmorType Type { get; }
        public ItemModifier Modifier { get; }
        public int ArmorPoints { get; }
        public string FullName =>
            Modifier == ItemModifier.Normal ? Name : $"{Modifier} {Name}";
        public string Name { get; }

        public override string ToString() => Name;
    }

    class Food : Item
    {
        public Food(FoodType food)
        {
            Type = food;
            RestorePoints = food.GetBaseRecovery();
            Name = food.GetName();
        }
        
        public FoodType Type { get; }
        public int RestorePoints { get; }
        public string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}