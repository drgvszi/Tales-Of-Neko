using System;
using UnityEngine;

namespace Tales_of_Neko
{
    [Serializable] 
    public class Stats
    {
        public float Strength;
        public float Constitution;
        public float Dexterity;
        public float Intelligence;
        public float Wisdom;

        public Stats()
        {
            Strength = 0;
            Constitution = 0;
            Dexterity = 0;
            Intelligence = 0;
            Wisdom = 0;
        }
        public Stats(float strength,float constitution,float dexterity,float intelligence,float wisdom)
        {
            Strength = strength;
            Constitution = constitution;
            Dexterity = dexterity;
            Intelligence = intelligence;
            Wisdom = wisdom;
        }

        public static Stats operator +(Stats stat1,Stats stat2)
        {
            Stats stats=new Stats();
            if (stat1 == null) {
                stat1=new Stats(0,0,0,0,0);
            }

            if (stat2 == null)
            {
                stat2=new Stats(0,0,0,0,0);
            }
            
            stats.Strength = stat1.Strength + stat2.Strength;
            stats.Constitution = stat1.Constitution + stat2.Constitution;
            stats.Dexterity = stat1.Dexterity + stat2.Dexterity;
            stats.Intelligence = stat1.Intelligence + stat2.Intelligence;
            stats.Wisdom = stat1.Wisdom + stat2.Wisdom;
            return stats;
        }
    }
}