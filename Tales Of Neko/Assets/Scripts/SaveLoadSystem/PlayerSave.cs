using System;
using System.Collections.Generic;
using Tales_of_Neko;
using UnityEngine.Serialization;

namespace SaveLoadSystem
{
    [Serializable]
    public class PlayerSave
    {

        public string Name;

        public Stats Stats;

        public double Health;
        public double Mana;

        public double MaxHealth;
        public double MaxMana;
    
        public List<SpellSave> Spells;
        public int Level;
        
        public CharacterClass Class;
        public InventorySave Inventory;
        public double Experience;
        public double Money;
        
        [FormerlySerializedAs("canLevelUp")] public int levelStatsUp = 0;
        public List<QuestSave> Quests;


        public static PlayerSave Save(Player player)
        {
            PlayerSave save = new PlayerSave();
            save.Name = player.name;
            save.Stats = player.Stats;
            save.Health = player.Health;
            save.Mana = player.Mana;
            save.MaxHealth = player.MaxHealth;
            save.MaxMana = player.MaxMana;
            save.Spells = new List<SpellSave>();
            foreach (Spell spell in player.Spells) {
                save.Spells.Add(SpellSave.Save(spell));
            }
            
            save.Level = player.Level;
            save.Class = player.Class;
            save.Inventory = InventorySave.Save(player.Inventory);
            save.Experience = player.Experience;
            save.Money = player.Money;
            save.levelStatsUp = player.levelStatsUp;
            save.Quests = new List<QuestSave>();
            foreach (Quest quest in player.Quests)
            {
                save.Quests.Add(QuestSave.Save(quest));
            }
            return save;
        }

        public static Player Load(PlayerSave playerSave)
        {
            Player player = GameManager.Instance.gameObject.AddComponent<Player>();
            
            //TO DO
            player.Class = playerSave.Class;
            player.Experience = playerSave.Experience;
            player.Inventory = InventorySave.Load(playerSave.Inventory);
            player.Money= playerSave.Money;
            player.Quests=new List<Quest>();
            foreach (QuestSave questSave in playerSave.Quests)
            {
                player.Quests.Add(QuestSave.Load(questSave));
                
            }
            player.levelStatsUp=playerSave.levelStatsUp;
            player.Health=playerSave.Health;
            player.Mana=playerSave.Mana;
            player.Level=playerSave.Level;
            player.Name=playerSave.Name;
            player.Spells=new List<Spell>();
            foreach (SpellSave spellSave in playerSave.Spells)
            {
                player.Spells.Add(SpellSave.Load(spellSave));
            }
            player.Stats=playerSave.Stats;
            player.MaxHealth=playerSave.MaxHealth;
            player.MaxMana=playerSave.MaxMana;
            
            return player;

        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Stats)}: {Stats}, {nameof(Health)}: {Health}, {nameof(Mana)}: {Mana}, {nameof(MaxHealth)}: {MaxHealth}, {nameof(MaxMana)}: {MaxMana}, {nameof(Spells)}: {Spells}, {nameof(Level)}: {Level}, {nameof(Class)}: {Class}, {nameof(Inventory)}: {Inventory}, {nameof(Experience)}: {Experience}, {nameof(Money)}: {Money}, {nameof(levelStatsUp)}: {levelStatsUp}, {nameof(Quests)}: {Quests}";
        }
    }
}