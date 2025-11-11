// Import the System namespace to use Console.WriteLine and other built-in features
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

// Using this file to keep my methods seperated and clean where possible
// Creating several small methods that focus on a particular thing.
// Using async methods so code can run simultaniously 


public static class RPGMethods
{
    // Narrator / Typer 
    public static async Task Narrator(string text, int delay) // Narrator method created to make my text appear slower not instantly
    {
        // Preventing input 
        Console.CursorVisible = false;
        foreach (char c in text)
        {
            Console.Write(c);
            await Task.Delay(delay); // For each character written, add a delay of (delay)
        }
        Console.WriteLine();
        Console.CursorVisible = true;

        // catching any accidental key strokes
        while(Console.KeyAvailable)
        Console.ReadKey(true);
    }


    // Player set up 
    public static async Task<string> GetPlayerName()
    {
        await RPGMethods.Narrator("Finally youre awake, What is your name ?", 50);
        string namePlayer = Console.ReadLine(); // Taking the players input

        while (string.IsNullOrEmpty(namePlayer))
        {
            await RPGMethods.Narrator("You must have a name.. surely..", 50);
            namePlayer = Console.ReadLine();
        }
        await RPGMethods.Narrator($"Ahh great, glad to see youre still alive {namePlayer}", 50);
        return namePlayer;
    }

    // Adding to the inventory 
    public static async Task AddItem(List<string> inventory, string item) // created a task called AddItem, 
    {
        
            inventory.Add(item);
            await RPGMethods.Narrator($"{item} has been added to your inventory", 10);
            return;
    }

    // Removing and using an item
    public static async Task UseItem(Player player)
    {
        await player.ShowInventory();
        await RPGMethods.Narrator("Enter the number of the item you would like to use.", 10);

        string input = Console.ReadLine();
        if (!int.TryParse(input, out int Choice)) // Parsing here is done 
        {
            await RPGMethods.Narrator("Invalid input, Please enter a number", 10);
            return;
        }
        if (Choice < 1 || Choice > player.Inventory.Count)// This is checking that the choice is at least 1 or bigger, not bigger than the actual inventory size or null
        {
            await RPGMethods.Narrator("Invalid Choice", 10);
            return;
        }
        player.Inventory.RemoveAt(Choice -1);
        await RPGMethods.Narrator($"Youve used {Choice}.", 10);
    }




    // Health And damage system

    public static async Task<int> DealDamage(string TargetName, int TargetHealth, int TargetArmour, int Damage) // Changed 3 Times 
    {
        int FinalDamage = Damage - TargetArmour;
        if (FinalDamage < 0) FinalDamage = 0; // Preventing negative damage this time 

        TargetHealth -= FinalDamage;
        if (TargetHealth < 0) TargetHealth = 0; // Preventing negative health 

        await RPGMethods.Narrator($"{TargetName} took {FinalDamage} damage! Current Health: {TargetHealth}", 10);

        return TargetHealth;
    }

    public static async Task<int> Heal(string TargetName, int CurrentHealth, int MaxHealth, int HealAmount,
    List<string>? Inventory = null, // Should only be for player inventory
    string PotionName = "Health Potion") // Default healing potion
    {
        // Checking if an inventory is given from the class 
        if (Inventory != null)
        {
            int PotionIndex = Inventory.IndexOf(PotionName); // Calling for the first inctance of PotionName 
            if (PotionIndex == -1)
            {   // Checking if the potion index goes to -1 in which case no potion
                await RPGMethods.Narrator($"{TargetName} has no {PotionName} left!", 10);
                return CurrentHealth;
                // Cant heal if you havent got a Heal Potion present
            }
            else
            {
                // potion exists, remove it 
                Inventory.RemoveAt(PotionIndex); // Remove the potion after use
                await RPGMethods.Narrator($"{TargetName} uses a {PotionName}!", 10);
            }
        }
        // If all above has checked and passed and a potion is present and removed then progress to this
        CurrentHealth += HealAmount;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth; // If the current health is equal to max health , then set to the max health, preventing going above

        await RPGMethods.Narrator($"{TargetName} healed {HealAmount} of health points! Current Health: {CurrentHealth}", 10);

        return CurrentHealth;
    }

    // Menu of sorts 

    public static async Task<int> ShowMenu()
    {
        await RPGMethods.Narrator("Choose an option", 10);
        await RPGMethods.Narrator("1. View Inventory", 10);
        await RPGMethods.Narrator("2. Use Item", 10);
        await RPGMethods.Narrator("3. View stats", 10);
        await RPGMethods.Narrator("4. Exit the game", 10);

        string input = Console.ReadLine();
        if (!int.TryParse(input, out int Choice))
        {
            await RPGMethods.Narrator("Invalid input, defaulting to 0", 10);
            Choice = 0;
        }
        return Choice;
    }
    // Game Menu

    // Navigation

    // Item(s)
}