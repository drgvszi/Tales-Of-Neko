using System;

namespace Tales_of_Neko
{
    public class Combat
    {
        public static bool IsFirst(Player player, Mob mob)
        {
            return player.GetComplessiveStats().Dexterity >= mob.Stats.Dexterity;
        }

        public static void BasicAttack(ref Player player, ref Mob mob)
        {
            Stats allStats = player.GetComplessiveStats();
            mob.TakeDamage(Math.Abs(mob.Stats.Constitution-allStats.Strength));
        }

        public static void ManaAttack(ref Player player, ref Mob mob)
        {
            Stats allStats = player.GetComplessiveStats();
            player.UseMana(10);
            mob.TakeDamage(Math.Abs(mob.Stats.Constitution-allStats.Wisdom));
        }

        public static void Attack(ref Mob mob, ref Player player)
        {
            Stats allStats = player.GetComplessiveStats();
            player.TakeDamage(Math.Abs(allStats.Constitution - mob.Stats.Strength));
        }

        public static bool CanFight(Character character1, Character character2)
        {
            return character1.IsAlive() && character2.IsAlive();
        }
    }
}