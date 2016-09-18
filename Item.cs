namespace fwod
{
    public enum Modifier : byte
    {
        Normal, Broken, Rusty, Sharp, Godly, PleaseNerf
    }

    public enum WeaponType : byte
    {
        Fist,
        Cutlass,
        Beretta_92_FS
    }

    public enum ArmorType : byte
    {
        Shirt,

    }

    public enum FoodType : byte
    {
        Energy_Drink,
    }
    
    class Item
    {

    }
    
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
            Name = $"{Modifier} {Type.GetName()}";
        }
        
        public int Damage { get; }
        public WeaponType Type { get; }
        public Modifier Modifier { get; }
        public string Name { get; }
    }

    class Armor : Item
    {
        public Armor(ArmorType armor, Modifier mod = Modifier.Normal)
        {
            Type = armor;
            Modifier = mod;
            ArmorPoints = (int)mod.GetModificationValue(armor.GetBaseDefense());
            Name = $"{Modifier} {Type.GetName()}";
        }

        public ArmorType Type { get; }
        public Modifier Modifier { get; }
        public int ArmorPoints { get; }
        public string Name { get; }
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
    }
}