using System;
using System.Collections.Generic;

namespace Tales_of_Neko
{
    public class Player:Character
    {
        public CharacterClass Class { get; }
        public Inventory Inventory { get; set; }

        public static Player ThePlayer;

        public double Experience { get; set; }
        
        public List<Spell> Spells { get; set; }


        private Player(string name) : base(name)
        {
            Experience = 0;
            Inventory = new Inventory();
            Spells= new List<Spell>();
        }
        private Player(string name, CharacterClass characterClass, float health, float mana):base(name)
        {
            Class = characterClass;
            Health = health;
            Mana = mana;
            Inventory = new Inventory();
            Spells= new List<Spell>();
            Experience = 0;

        }
        private Player(string name, CharacterClass characterClass, float health, float mana,Stats stats):base(name)
        {
            Class = characterClass;
            Health = health;
            Mana = mana;
            Stats = stats;
            Inventory = new Inventory();
            Spells= new List<Spell>();
            Experience = 0;

        }

        public static Player CreateNewPlayer(string name, CharacterClass characterClass, float health, float mana)
        {
            if (ThePlayer == null) {
                ThePlayer=new Player(name,characterClass,health,mana);
            }

            return ThePlayer;
        }
        public static Player CreateNewPlayer(string name, CharacterClass characterClass, float health, float mana,Stats stats)
        {
            if (ThePlayer == null) {
                ThePlayer=new Player(name,characterClass,health,mana,stats);
            }

            return ThePlayer;
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
            if (random.NextDouble() > mob.Difficulty/100) {
                if (allStats.Intelligence >= mob.GetRawStats().Intelligence &&
                    allStats.Dexterity >= mob.GetRawStats().Dexterity) {
                    return true;
                }
            }

            return false;

        }

        public void AddExperience(double experience)
        {
            Experience += experience / 100.0;
        }
        public int GetCurrentLevel()
        {
            return ((int) (Experience + 1));
        }
        
    }
}