using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Creating a small map that the player will interact with.

public class Room
{
    // Base info required in all rooms, keeping uniformity.
    public string Name { get; set; }  // Name for the room
    public string Description { get; set; } // Description of the room
    public Dictionary<string, Room> Exits { get; set; } = new(); // A string named dictionary of all room.exits 

    // Adding in some markers that i can use later for triggering events

    public bool HasEnemy { get; set; } = false;
    public bool HasLoot { get; set; } = false;
    public bool IsBossRoom { get; set; } = false;
    // all three above are defaulted to false, as in no, no enemy, loot or boss 

    // creating the room constructor 
    public Room(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public async Task Describe() // Creating my describe method
    {
        await RPGMethods.Narrator($"\n=== {Name} ===", 10);
        await RPGMethods.Narrator(Description, 10);

        await RPGMethods.Narrator("\nExits", 10);
        foreach (var exit in Exits.Keys)
            await RPGMethods.Narrator($" - {exit}", 10);
    }
};

