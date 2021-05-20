using System;
using System.Collections.Generic;
using Tales_of_Neko;

namespace SaveLoadSystem
{
    [Serializable]
    public class EnemySave
    {
        public float Difficulty;
        
        public string Name;

        public Stats Stats;

        public double Health;
        public double Mana;

        public double MaxHealth;
        public double MaxMana;
    
        public List<SpellSave> Spells;
        public int Level;


        public static EnemySave Save(Mob enemy)
        {
            EnemySave save = new EnemySave();
            save.Difficulty = enemy.difficulty;
            save.Name = enemy.name;
            save.Stats = enemy.Stats;
            save.Health = enemy.Health;
            save.Mana = enemy.Mana;
            save.MaxHealth = enemy.MaxHealth;
            save.MaxMana = enemy.MaxMana;
            save.Spells = new List<SpellSave>();
            foreach (Spell spell in enemy.Spells)
            {
                save.Spells.Add(SpellSave.Save(spell));
                
            }
            save.Level = enemy.Level;
            return save;
        }

        public static Mob Load(EnemySave enemySave)
        {
            Mob enemy = GameManager.Instance.gameObject.AddComponent<Mob>();

            enemy.Name = enemySave.Name;
            enemy.difficulty = enemySave.Difficulty;
            enemy.Stats = enemySave.Stats;
            enemy.Health = enemySave.Health;
            enemy.Mana = enemySave.Mana;
            enemy.Spells = new List<Spell>();
            foreach (SpellSave spellSave in enemySave.Spells)
            {
                enemy.Spells.Add(SpellSave.Load(spellSave));
            }
            enemy.Level = enemySave.Level;
            enemy.MaxHealth = enemySave.MaxHealth;
            enemy.MaxMana = enemySave.MaxMana;
            return enemy;
        }
    }
}