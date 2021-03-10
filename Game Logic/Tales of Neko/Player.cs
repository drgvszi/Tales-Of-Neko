using System;

namespace Tales_of_Neko
{
    public class Player:Character
    {
        public CharacterClass Class { get; }
        public Inventory Inventory { get; set; }

        public static Player ThePlayer;
        

        private float Health { get; set; }
        private float Mana { get; set; }
        
        

        private Player(string name) : base(name)
        {
            Inventory = new Inventory();
        }
        private Player(string name, CharacterClass characterClass, float health, float mana):base(name)
        {
            Class = characterClass;
            Health = health;
            Mana = mana;
            Inventory = new Inventory();

        }
        private Player(string name, CharacterClass characterClass, float health, float mana,Stats stats):base(name)
        {
            Class = characterClass;
            Health = health;
            Mana = mana;
            Stats = stats;
            Inventory = new Inventory();

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
        
    }
}