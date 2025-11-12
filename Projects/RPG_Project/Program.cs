// Import the System namespace to use Console.WriteLine and other built-in features
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

// Creating my main program, this needs to be Asyncronhous as i will be using Await for text typing 
class Program
{   // Testing my narrator 
    static async Task Main() // Allows the use of await and other async commands 
    {
        // await RPGMethods.Narrator("Hello", 50); // The string is what's said and the delay is 50ms
        // Creating a start up system in teh game, like a tutorial in which key features are initiallised 
        // Testing my GetPlayerName method
        var playerName = await RPGMethods.GetPlayerName(); // Pulling the player name from input
        // Creating the instance of the player 
        Player player = new Player(playerName, 100, 20, 10, 5);
        // Adding basic starting items 
        player.Inventory.Add("Health Potion");
        player.Inventory.Add("Knife");
        // Begining basic narration 
        await RPGMethods.Narrator("You dont remember a thing do you.. Lucky for you I guess ");
        await RPGMethods.Narrator("Youre on Promethues.. a small asteroid dedicated to genetic experementation... cmon' you must remember ");
        await RPGMethods.Narrator("You are the main biologist running this damn thing ! ");
        await RPGMethods.Narrator($"Im sorry, {playerName}, this is your fault, you made these things...  ");
        await RPGMethods.Narrator("Lets take a look at your inventory and see what might be useful");
        await player.ShowInventory();// Displays the inventory 
        // Contuning the story using my narator method
        await RPGMethods.Narrator("Equip that knife.. youll need it.. ");
        bool ValidChoice = false;
        while (!ValidChoice)
        {
            await RPGMethods.Narrator("Choose your action: 1. Equip Knife", 10);
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    await player.EquipWeapon("Knife", 5);
                    ValidChoice = true;
                    break;
                default:
                    await RPGMethods.Narrator("Hmm that didnt work, try typing just a number 1/2/3");
                    break;
            }
        }
        // Displaying current stats 
        await RPGMethods.Narrator("Great, at least your brain is still working, lets check your stats");
        await RPGMethods.Narrator("We need to hurry, I can hear something in the corridor  ");
        await RPGMethods.Narrator("Choose your action: 1. Check stats", 10);
        bool StatsValidChoice = false;
        while (!StatsValidChoice)
        {
            string Statsinput = Console.ReadLine();
            switch (Statsinput)
            {
                case "1":
                    await player.DisplayStats();
                    StatsValidChoice = true;
                    break;
                default:
                    await RPGMethods.Narrator("Hmm that didnt work, try typing just a number 1/2/3");
                    break;
            }
        }
        await RPGMethods.Narrator("Ahh its a mutated rat! hurry and kill it before is calls for more !");
        // Starting tutorial battle - manually initiated
        BaseEnemy enemy = new BaseEnemy("Mutant Rat", 30, 10, 2);
        await StartBattle(player, enemy);
        // after the forced tutorial battle, begin the dungeon
        await ExploreDungeon(player);
        

    }

    // AI used to help testing BaseEnemy 
    public static async Task StartBattle(Player player, BaseEnemy enemy)
    {
        await RPGMethods.Narrator($"A {enemy.Name} appears!");

        while (enemy.Health > 0 && player.Health > 0)
        {
            await RPGMethods.Narrator($"\nEnemy Health: {enemy.Health}");
            await RPGMethods.Narrator($"Your Health: {player.Health}");
            await RPGMethods.Narrator("Choose your action: 1. Attack  2. Heal 3. Show Inventory", 10);

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    await player.AttackEnemy(enemy);
                    break;
                case "2":
                    await player.UsePotion(20);
                    break;
                case "3":
                    await player.ShowInventory();
                    break;
                default:
                    await RPGMethods.Narrator("You fumbled your choice and the enemy attacks!");
                    break;
            }
            if (enemy.Health > 0)
            {
                await enemy.AttackPlayer(player);
            }
            if (player.Health <= 0)
            {
                
                await RPGMethods.Narrator("You were defeated...");
                await RPGMethods.Narrator("The corridors are filled with laughter");
                await RPGMethods.Narrator("GAME OVER", 100);
                Environment.Exit(0);
                return;
            }
        }
        await RPGMethods.Narrator($"You defeated the {enemy.Name}! Well done adventure, lets move on");
    }
// initiating the dungeon crawl
    static async Task ExploreDungeon(Player player)
    {

        var map = MapBuilder.BuildMap(); // Builds my map
        Room CurrentRoom = map["Entrance"]; // Sets the starting location
        bool Playing = true; // Sets the game as "Playing"

        while (Playing)
        {
            // As the game starts and is marked as playing, begin the start sequence
            await CurrentRoom.Describe(); // Describe where you are 
            await RPGMethods.Narrator("What do you do?", 10);
            string exits = string.Join("/", CurrentRoom.Exits.Keys);
            await RPGMethods.Narrator($"Move {exits}, Search, Fight, Quit", 10);
            // making the users input lowercase to be easier to match
            string UserChoice =  Console.ReadLine().ToLower(); // Reads the entered text and matches it to lower case

            switch (UserChoice)
            {
                case "north":
                case "south":
                case "east":
                case "west":
                    if (CurrentRoom.Exits.ContainsKey(UserChoice))
                    {
                        CurrentRoom = CurrentRoom.Exits[UserChoice];
                        Console.Clear();
                        await RPGMethods.Narrator($"You moved {UserChoice}...\n", 10);
                        // Calling upon the flags i added earlier to the rooms
                        if (CurrentRoom.HasEnemy)
                        {
                            await RPGMethods.Narrator("Something stirs in the shadows, you feel eyes watching you.", 10);
                            BaseEnemy enemy = BaseEnemy.GenerateEnemyForRoom(CurrentRoom);
                            await StartBattle(player, enemy);
                            CurrentRoom.HasEnemy = true;
                        }
                        if (CurrentRoom.IsBossRoom)
                        {
                            await RPGMethods.Narrator("You sense a monsterous presence nearby...", 10);
                        BaseEnemy enemy = BaseEnemy.GenerateEnemyForRoom(CurrentRoom);
                            await StartBattle(player, enemy);
                            if (enemy.Health <= 0)
                            {
                            CurrentRoom.IsBossRoom = false;
                            await RPGMethods.Narrator($"You have slain {enemy.Name}, the final creature of Prometheus...");
                            await RPGMethods.Narrator("The corridors fall silent. You’ve survived — for now.");
                            await RPGMethods.Narrator("GAME OVER", 100);
                            Playing = false; 
                            }
                        }
                    }
                    else
                    {
                        await RPGMethods.Narrator("Youre unable to go that way", 10);
                    }
                    break;
                case "search":
                    if (CurrentRoom.HasLoot)
                    {
                        await RPGMethods.Narrator("You have found a small potion hidden under the rubble ", 10);
                        player.Inventory.Add("Health Potion"); // places the potion into the inventory.
                        CurrentRoom.HasLoot = true; // Setting the room back to true to keep a supply of potions
                    }
                    else
                    {
                        await CurrentRoom.Describe();
                    }
                    break;
                case "fight":
                    if (CurrentRoom.HasEnemy)
                    {
                        BaseEnemy enemy = BaseEnemy.GenerateEnemyForRoom(CurrentRoom);
                        await StartBattle(player, enemy);
                    }
                    else
                    {
                        await RPGMethods.Narrator("Thers nothing to fight here!");
                    }
                    break;
                case "quit":
                    Playing = false;
                    await RPGMethods.Narrator("You decide to rest for now...", 10);
                    break;
                default:
                    await RPGMethods.Narrator("Hmm that didnt work.. Try again?", 10); break;
            }
        }
    } 
}
