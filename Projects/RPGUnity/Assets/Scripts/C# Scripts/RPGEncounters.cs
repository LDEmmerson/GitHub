// Import the System namespace to use Console.WriteLine and other built-in features
using System;
using System.Configuration.Assemblies;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Xml.Serialization;

// Creating a basic Enemy profile i will use later to make custom instances // Times ive revisited to change code:(4)
public class BaseEnemy
{
    // Standard stats for each enemy, easily use get set methods for name and enemy status 
    public string Name { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Armour { get; set; }
    public int BaseAttack { get; set; }
    public int WeaponAttack { get; set; }
    public int XPReward { get; set; } = 50; //  XP rewards 

    // Keeping my Total Attack consistent 
    public int TotalAttack => BaseAttack + WeaponAttack;

    public BaseEnemy(string name, int maxHealth, int baseAttack, int armour)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth; // Defaults to full health when creating an instance 
        BaseAttack = baseAttack;
        Armour = armour;
        WeaponAttack = 0; // Default for now
        XPReward = 50; // Default 50XP 
    }
    // take damage method to be added to the base class 
    public async Task TakeDamage(int IncomingDamage)
    {
        Health = await RPGMethods.DealDamage(Name, Health, Armour, IncomingDamage);
        if (Health <= 0)
            await RPGMethods.Narrator($"{Name} has been defeated!", 20);
    }
    // Enemy attacking the player method
    public async Task AttackPlayer(Player player)
    {
        await player.TakeDamage(TotalAttack);
    }
};
































// TO add 

// Mad Scientist / Lab Monsters

// The Shrieking Test Subject
// A failed human experiment, limbs elongated and skin patchy.
// Makes horrifying screams that disorient the player.
// Weak individually, but swarms can overwhelm.

// Mutant Rodents
// Rats or mice the scientist experimented on.
// Gigantic, glowing eyes, razor-sharp teeth.
// Can bite to inflict poison or infection.

// Bio-Gel Slime
// Transparent blob with visible organs inside.
// Absorbs and slows movement; splits into smaller slimes when hit.
// Can ooze through vents to ambush the player.

// Cyber-Enhanced Guard
// Human or animal fused with mechanical limbs or weapons.
// Quick and heavily armored, requires strategy to defeat.

// Flesh Puppets
// Corpses stitched together into nightmarish forms.
// Move unpredictably, attack in groups.
// Weak individually but frightening.

// Abomination Experiment #13
// Tentacles, extra eyes, and multiple mouths.
// Capable of grabbing or attacking from a distance.
// Could be a boss for one part of the lab.

// The Gas-Phantom
// A creature formed from chemical fumes or experimental gas.
// Invisible until it attacks; can poison or confuse.

// Clockwork Chimera
// A failed animal experiment with mechanical implants.
// Makes grinding noises, attacks in predictable patterns.

// Living Shadow
// Created by a failed light/energy experiment.
// Can merge with shadows, teleport, or mimic movements.

// The Scientist’s Prototype
// Humanoid but unnervingly perfect or twisted.
// Intelligent and can track the player through the lab.