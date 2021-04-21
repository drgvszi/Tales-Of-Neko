using System;
using System.Collections.Generic;
using Tales_of_Neko;
using UnityEngine;


public class Character: MonoBehaviour
{
    public string Name;

    public Stats Stats;

    public double Health;
    public double Mana;

    public double MaxHealth;
    public double MaxMana;
    
    public List<Spell> Spells;
    public int Level;

    public Character(string name)
    {
        Name = name;
        Health = 100.0f;
        Mana = 100.0f;
        Stats = new Stats();
    }

    public void Awake()
    {
    }

    public double GetCurrentMana() {
        return Mana;
    }
    public double GetCurrentHealth()
    {
        return Health;
    }
    public Stats GetRawStats()
    {
        return Stats;
    }

    public bool IsAlive()
    {
        return Health > 0;
    }

    
    public double TakeDamage(double damage)
    {
        double rate = damage / Stats.Constitution;
        Health -= (rate * damage);
        if (Health < 0)
        {
            Health = 0;
        }
        return Health;
    }
    public double UseMana(double mana)
    {
        Mana -= mana;
        if (Mana < 0)
        {
            Mana = 0;
        }
        return Mana;
    }

    public bool CanUse(Spell spell)
    {
        if(Spells.Contains(spell))
        {
            if (GetCurrentMana() >= spell.ManaUsage)
            {
                return true;
            }
            
        }

        return false;
    }
    public List<Spell> GetAvailableSpells()
    {
        List<Spell> availableSpells=new List<Spell>();
        foreach (Spell spell in Spells) 
        {
            if (CanUse(spell))
            {
                availableSpells.Add(spell);
            }
        }

        return availableSpells;
    }

    public override string ToString()
    {
        return Name + "  HEALTH: " + Health + "   MANA: " + Mana+"\n";
    }
}