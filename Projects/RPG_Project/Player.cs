// New class for the player to be defined in seperatle

// importing system commands
using System;

// Re-Wrote (3) times so far 
// New class
public class Player
{
    // Base Player info
    public string Name { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Mana { get; set; }
    public int Armour { get; set; }
    public int BaseAttack { get; set; }
    public int WeaponAttack { get; set; }
    // creating a inventory
    public List<string> Inventory { get; set; } = new();

    // XP System

    public int Level { get; private set; } = 1;
    public int CurrentXP { get; private set; } = 0;
    public int XPToNextLevel { get; private set; } = 100;

    // Adding weapon system into it - Basic but working 
    public int TotalAttack => BaseAttack + WeaponAttack;


    // Building the constructor for this class
    public Player(string name, int maxHealth, int mana, int baseAttack, int armour)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth; // Defaults to full health when creating an instance 
        Mana = mana;
        BaseAttack = baseAttack;
        Armour = armour;
        WeaponAttack = 0;
    }
    // take damage method to be added to the base class 
    public async Task TakeDamage(int IncomingDamage)
    {
        Health = await RPGMethods.DealDamage(Name, Health, Armour, IncomingDamage);
        if (Health <= 0)
            await RPGMethods.Narrator($"{Name} has been defeated!", 20);
    }
    // Enemy attacking the player method
    public async Task AttackEnemy(BaseEnemy Enemy)
    {
        Enemy.Health = await RPGMethods.DealDamage(Enemy.Name, Enemy.Health, Enemy.Armour, TotalAttack);

        // check for if player has killed the enemy 
        if (Enemy.Health <= 0)
        {
            await RPGMethods.Narrator($"{Enemy.Name} has been defeated", 20);
            if (Enemy.XPReward > 0) // only grant if the enemy has it defined
                await GainXP(Enemy.XPReward);
        }
    }
    // Adding Health potion options 
    public async Task UsePotion(int HealAmount, string PotionName = "Health Potion")
    {
        Health = await RPGMethods.Heal(Name, Health, MaxHealth, HealAmount, Inventory, PotionName);
    }
    
    //Gain XP system
    public async Task GainXP(int Amount)
    {
        CurrentXP += Amount;
        await RPGMethods.Narrator($"{Name} gained {Amount} XP!", 20);
        if (CurrentXP >= XPToNextLevel)
            await LevelUp();
    }
    public async Task LevelUp()
    {
        CurrentXP -= XPToNextLevel;
        Level++; // Level increase
        MaxHealth += 10; // 10 More max health
        BaseAttack += 3; // 3 more attack 
        Health = MaxHealth; // refreshes health to max 

        await RPGMethods.Narrator($"{Name} has leveled up! Now level {Level}", 20);
    }

    // Equip weapon ( AI ASSISTED )
    public async Task EquipWeapon(string weaponName, int attackBonus)
    {
        WeaponAttack = attackBonus;
        if (!Inventory.Contains(weaponName))
            Inventory.Add(weaponName);
        await RPGMethods.Narrator($"{Name} equipped {weaponName} (+{attackBonus} attack)", 20);
    }

    // adding in my display inventory method within the class to keep it together.
    public async Task ShowInventory()
    {
        await RPGMethods.Narrator("\n----Inventory----", 20);
        if (Inventory.Count == 0)  // If inventory is equal to 0
        {
            await RPGMethods.Narrator("You have nothing in your inventory", 20); // display error message 
        }
        else
        {
            for (int i = 0; i < Inventory.Count; i++)
                await RPGMethods.Narrator($"{i + 1}. {Inventory[i]}", 20);
        }
        await RPGMethods.Narrator("\n-----------------", 20);
    }

    // Stats system
    public async Task DisplayStats()
    {
        await RPGMethods.Narrator("\n---Stats---", 10);
        await RPGMethods.Narrator($"Health:         {Health}/{MaxHealth}", 10);
        await RPGMethods.Narrator($"XP:             {CurrentXP}/{XPToNextLevel}", 10);
        await RPGMethods.Narrator($"Level:          {Level}", 10);
        await RPGMethods.Narrator($"TotalAttack:    {TotalAttack}", 10);
        await RPGMethods.Narrator($"Weapon Attack:  {WeaponAttack}", 10);
        await RPGMethods.Narrator($"BaseAtack:      {BaseAttack}", 10);
        await RPGMethods.Narrator($"Armour:         {Armour}", 10);
        await RPGMethods.Narrator("------------\n", 10);
    }
};