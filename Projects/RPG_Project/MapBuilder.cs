
using System;
using System.Collections.Generic;

    public static class MapBuilder
    {
        public static Dictionary<string, Room> BuildMap()
        {
            // Room setup - AI Generated descriptions for the rooms 
            var Entrance = new Room("Entrance Hall", "Cold stone walls surround you. A faint torch flickers to life.");
            var Training = new Room("Training Chamber", "A wide room filled with the squeaks of mutant rats. To the west you see an old wooden sign \n MONSTERPIT BEWARE.. You hear rats fighting in the distance.");
            var Storage = new Room("Storage Room", "Old crates and barrels lie scattered. You might find something useful.");
            var Corridor = new Room("Corridor", "A long, damp hallway connects the rooms. You feel watched...");
            var GuardRoom = new Room("Guard Room", "Broken armor and blood stains mark a violent struggle here.");
            var Lab = new Room("Laboratory", "The walls are lined with tubes and failed experiments. The air hums ominously.");
            var ExitTunnel = new Room("Exit Tunnel", "A faint light glows ahead — freedom, at last.");
            // adding in new rooms for combat
            var MonsterPit = new Room("MonsterPit", "Dim torches reveal a grimy pit. The squeaks and growls of mutant rats echo all around.");

            // Adding flags into the rooms for triggers 
            MonsterPit.HasEnemy = true; // Training room has enemies to train on 
            GuardRoom.HasEnemy = true;
            Lab.IsBossRoom = true; // Making the lab my boss room
            Storage.HasLoot = true; // Storage holds items 

            // Connecting the rooms together, creating a full loop so its enclosed
            Entrance.Exits["North"] = Training;
            Training.Exits["South"] = Entrance;
            Training.Exits["East"] = Storage;
            Storage.Exits["West"] = Training;
            Training.Exits["North"] = Corridor;
            Corridor.Exits["South"] = Training;
            Corridor.Exits["North"] = GuardRoom;
            GuardRoom.Exits["South"] = Corridor;
            GuardRoom.Exits["North"] = Lab;
            Lab.Exits["South"] = GuardRoom;
            Lab.Exits["North"] = ExitTunnel;
            ExitTunnel.Exits["South"] = Lab;
            // Simple loop made between the room so player can move thgreough but not option to "Fall out"
            // Got to return the string named Dictionary with the room names
            // adding in moster fighting room, so the player can choose rather than forced in training room.
            Training.Exits["West"] = MonsterPit;
            MonsterPit.Exits["East"] = Training;
            
            return new Dictionary<string, Room>
        {
            {"Entrance", Entrance},
            {"Training", Training},
            {"Storage", Storage},
            {"Corridor", Corridor},
            {"GuardRoom", GuardRoom},
            {"Lab", Lab},
            {"ExitTunnel", ExitTunnel},
            {"MonsterPit", MonsterPit },
        };
        }
    }