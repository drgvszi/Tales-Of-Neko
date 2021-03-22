using System;
using System.Collections.Generic;
using Tales_of_Neko;
using UnityEngine;
using Random = System.Random;

public class Player:Character
{
    public CharacterClass Class;
    public Inventory Inventory;
    

    public double Experience;

    public bool canLevelUp = false;
    private Player(string name) : base(name)
    {
        Experience = 0;
        Inventory = new Inventory();
        Spells= new List<Spell>();
    }
    public Player(string name, CharacterClass characterClass, float health, float mana):base(name)
    {
        Class = characterClass;
        Health = health;
        Mana = mana;
        Level = 1;
        Inventory = new Inventory();
        Spells= new List<Spell>();
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
        if (canLevelUp == false)
        {
            if (Experience >= NextLevelXp())
            {
                canLevelUp = true;
            }
        }
    }

    public double NextLevelXp()
    {
        return Math.Round(0.04 * (Level ^ 3) + 0.8 * (Level ^ 2) + 2 * Level);
    }

    public override string ToString()
    {
        return base.ToString();
    }
}