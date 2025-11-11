// Import the System namespace to use Console.WriteLine and other built-in features
using System;
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
        await RPGMethods.Narrator("You dont remember a thing do you.. Lucky for you I guess ", 50);
        await RPGMethods.Narrator("Youre on Promethues.. a small asteroid dedicated to genetic experementation... cmon' you must remember ", 50);
        await RPGMethods.Narrator("You are the main biologist running this damn thing ! ", 50);
        await RPGMethods.Narrator($"Im sorry, {playerName}, this is your fault, you made these things...  ", 50);
        await RPGMethods.Narrator("Lets take a look at your inventory and see what might be useful", 50);
        await player.ShowInventory();// Displays the inventory 
        // Contuning the story using my narator method
        await RPGMethods.Narrator("Equip that knife.. youll need it.. ", 50);

        await RPGMethods.Narrator("Choose your action: 1. Show Inventory", 10);
        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                await player.ShowInventory();
                break;
            case "2":
                await player.EquipWeapon("Knife", 5);
                break;
            default:
                await RPGMethods.Narrator("Hmm that didnt work, try typing just a number 1/2/3", 50);
                break;
        }
        // Displaying current stats 
        await player.DisplayStats();
        // Starting tutorial battle - manually initiated 
        // await StartBattle(player);
    }
}
        // AI used to help testing BaseEnemy 
    //     public static async Task StartBattle(Player player)
    //     {
    //         BaseEnemy enemy = new BaseEnemy("Mutant Rat", 30, 10,2 );
    //         await RPGMethods.Narrator($"A wild {enemy.Name} appears!", 50);
    //         while (enemy.Health > 0 && player.Health > 0) 
    //         {
    //             await RPGMethods.Narrator($"\nEnemy Health: {enemy.Health}", 50);
    //             await RPGMethods.Narrator($"Your Health: {player.Health}", 50);
    //             await RPGMethods.Narrator("Choose your action: 1. Attack  2. Heal 3. Show Inventory", 10);
    //             string input = Console.ReadLine();
    //             switch (input)
    //             {
    //             case "1":
    //                 await player.AttackEnemy(enemy);
    //                 break;
    //             case "2":
    //                 await player.UsePotion(20);
    //                 break;
    //             case "3": 
    //                 await player.ShowInventory();
    //                 break;
    //             default:
    //                 await RPGMethods.Narrator("You fumbled your choice and the enemy attacks!", 50);
    //                 break;
    //             }
    //             if (enemy.Health > 0)
    //             {
    //                 await enemy.AttackPlayer(player);
    //             }
    //             if (player.Health <= 0)
    //             {
    //                 await RPGMethods.Narrator("You were defeated...", 50);
    //                 return;
    //             }
    //         }
    //     await RPGMethods.Narrator($"You defeated the {enemy.Name}!", 50);
    //     }
    // };

    // For later implimentation

// initiating the dungeon crawl

//     static async Task ExploreDungeon(Player player)
//     {
//         var map = MapBuilder.BuildMap(); // Builds my map
//         Room CurrentRoom = map["Enterance"]; // Sets the starting location
//         bool Playing = true; // Sets the game as "Playing"

//         while (Playing)
//         {
//             // As the game starts and is marked as playing, begin the start sequence
//             CurrentRoom.Describe(); // Describe where you are 
//             await RPGMethods.Narrator("\n What do you do?", 10);
//             await RPGMethods.Narrator("\n Move [North/South/East/West], [Explore] [Quit]", 10);
//             // making the users input lowercase to be easier to match
//             string UserChoice = Console.ReadLine().ToLower(); // Reads the entered text and matches it to lower case

//             switch (UserChoice)
//             {
//                 case "north":
//                 case "south":
//                 case "east":
//                 case "west":
//                     if (CurrentRoom.Exits.ContainsKey(UserChoice))
//                     {
//                         CurrentRoom = CurrentRoom.Exits[UserChoice];
//                         Console.Clear();
//                         await RPGMethods.Narrator($"You moved {UserChoice}...\n", 10);
//                         // Calling upon the flags i added earlier to the rooms
//                         if (CurrentRoom.HasEnemy)
//                         {
//                             await RPGMethods.Narrator("Something stirs in the shadows, you feel eyes watching you.", 10);
//                             // Begin battle here 
//                         }
//                         if (CurrentRoom.HasLoot)
//                         {
//                             await RPGMethods.Narrator("You have found a small potion hidden under the rubble ", 10);
//                             player.Inventory.add("HealthPotion"); // places the potion into the inventory.
//                             CurrentRoom.HasLoot = false; // Setting the room back to no potions
//                         }
//                         if (CurrentRoom.IsBossRoom)
//                         {
//                             await RPGMethods.Narrator("You sense a monsterous presence nearby...", 10);
//                             // Initiate my boss battle 
//                         }
//                     }
//                     else
//                     {
//                         await RPGMethods.Narrator("Youre unable to go that way", 10);
//                     }
//                     break;
//                 case "Quit":
//                     Playing = false;
//                     await RPGMethods.Narrator("Youd decide to rest for now...", 10);
//                     break;
//                 default:
//                     await RPGMethods.Narrator("Invalid Command...", 10); break;
//             }
//         }
//     }
// }
