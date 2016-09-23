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
        Pistol
    }

    public enum ArmorType : byte
    {
        No_Armor,
        Body_Armor
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
            Name = Type.GetName();
        }
        
        public int Damage { get; }
        public WeaponType Type { get; }
        public Modifier Modifier { get; }
        public string FullName =>
            Type == WeaponType.Unarmed ?
            Name : $"{Modifier} {Name}";
        public string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }

    class Armor : Item
    {
        public Armor(ArmorType armor, Modifier mod = Modifier.Normal)
        {
            Type = armor;
            Modifier = mod;
            ArmorPoints = (int)mod.GetModificationValue(armor.GetBaseDefense());
            Name = Type.GetName();
        }

        public ArmorType Type { get; }
        public Modifier Modifier { get; }
        public int ArmorPoints { get; }
        public string FullName => $"{Modifier} {Name}";
        public string Name { get; }

        public override string ToString()
        {
            return Name;
        }
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