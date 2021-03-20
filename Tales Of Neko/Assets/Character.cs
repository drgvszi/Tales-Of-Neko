namespace Tales_of_Neko
{
    public class Character
    {
        public string Name { get; }

        public Stats Stats { get; set; }

        protected float Health { get; set; }
        protected float Mana { get; set; }

        public Character(string name)
        {
            Name = name;
            Health = 100.0f;
            Mana = 100.0f;
            Stats = new Stats();
        }
        
        
        public float GetCurrentMana() {
            return Mana;
        }
        public float GetCurrentHealth()
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

        
        public float TakeDamage(float damage)
        {
            Health -= damage;
            return Health;
        }
        public float UseMana(float mana)
        {
            Mana -= mana;
            return Mana;
        }

        public override string ToString()
        {
            return Name + "\nHEALTH: " + Health + "\nMANA: " + Mana;
        }
    }
}