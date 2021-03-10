namespace Tales_of_Neko
{
    public class Item
    {
        public Stats Stats { get; set; }
        public string Name { get; set; }
        public ItemType ItemType { get; set; }

        public Item()
        {
            Stats = new Stats();
        }

        public Item(string name, ItemType type)
        {
            Name = name;
            ItemType = type;
            Stats = new Stats();
        }
        public Item(string name, ItemType type,Stats stats)
        {
            Name = name;
            ItemType = type;
            Stats = stats;
        }
    }
}