namespace Tales_of_Neko
{
    public class Mob: Character
    {
        public float Difficulty;

        public float GetAttackDamage()
        {
            return Stats.Strength;
        }
        public Mob(string name, float difficulty, Stats stats) : base(name)
        {
            Difficulty = difficulty;
            Stats = stats;
        }
        
        

    }
}