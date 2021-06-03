using System;
using System.Collections.Generic;
using SaveLoadSystem;
using Tales_of_Neko;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class Player:Character
{
    public CharacterClass Class;
    public Inventory Inventory;
    

    public double Experience;
    public double Money;

    [FormerlySerializedAs("canLevelUp")] public int levelStatsUp = 0;

    public List<Quest> Quests;
    public QuestManager QuestManager;

    public bool questUpdate;

    public int deaths = 0;
    public Player(string name) : base(name)
    {
        Experience = 0;
        Inventory = new Inventory();
        Spells= new List<Spell>();
        Quests = new List<Quest>();
    }
    public Player(string name, CharacterClass characterClass, float health, float mana):base(name)
    {
        Class = characterClass;
        Health = health;
        Mana = mana;
        Level = 1;
        Inventory = new Inventory();
        Spells= new List<Spell>();
        Quests = new List<Quest>();
        Experience = 0;

    }
    public Player(string name, CharacterClass characterClass, float health, float mana,Stats stats):base(name)
    {
        Class = characterClass;
        Health = health;
        Mana = mana;
        Stats = stats;
        Level = 1;
        Inventory = new Inventory();
        Spells= new List<Spell>();
        Quests = new List<Quest>();
        Experience = 0;

    }
    public Stats GetComplessiveStats()
    {
        return Stats + Inventory.EquippedItems.GetBonusStats();
    }
    public void AddToInventory(Item item)
    {
        Inventory.Add(item);
    }

    public bool CanEscape(Mob mob)
    {
        Stats allStats = GetComplessiveStats();
        Random random=new Random();
        if (random.NextDouble() > mob.difficulty/100) {
            if (allStats.Intelligence >= mob.GetRawStats().Intelligence &&
                allStats.Dexterity >= mob.GetRawStats().Dexterity) {
                return true;
            }
        }

        return false;

    }

    public void AddExperience(double experience)
    {
        Experience += experience;
        if (Experience >= NextLevelXp()) {
                levelStatsUp +=1;
                Level += 1;
                Experience = 0;
        }
    }

    public double NextLevelXp()
    {
        return Math.Round(0.04 * (Level ^ 3) + 0.8 * (Level ^ 2) + 2 * Level);
    }

    public List<Spell> GetEquippedSpells()
    {
        List<Spell> spells = new List<Spell>();
        foreach (Spell spell in Spells)
        {
            if (spell.IsEquipped)
            {
                spells.Add(spell);
            }
            
        }

        return spells;
    }

    public override string ToString()
    {
        return base.ToString();
    }
    
}