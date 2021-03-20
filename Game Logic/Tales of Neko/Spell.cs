using System;

namespace Tales_of_Neko
{
    public abstract class Spell
    {
        public string Name { get;}
        public Stats Stats { get;}
        public double CurrentCooldown { get;}
        public double MaxCooldown { get;}
        public int ManaUsage { get; }
        
        public ElementType Type { get; }

        public Spell(string name,ElementType type,Stats stats,double maxCooldown,int manaUsage)
        {
            Name = name;
            Type = type;
            Stats = stats;
            MaxCooldown = maxCooldown;
            ManaUsage = manaUsage;
            CurrentCooldown = 0;
        }
        
        public bool IsReady()
        {
            return CurrentCooldown == 0;
        }
        public bool CanUse(Player player)
        {
            if (Player.ThePlayer.GetCurrentMana() > ManaUsage && IsReady())
            {
                return true;
            }

            return false;
        }
    }
}