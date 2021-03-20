using System.Collections.Generic;

namespace Tales_of_Neko
{
    public class Inventory
    {
        public EquippedItems EquippedItems { get; set; }
        public List<Item> Items { get;}

        public Inventory()
        {
            EquippedItems=new EquippedItems();
            Items=new List<Item>();
        }
        public void Add(Item item)
        {
            Items.Add(item);
        }
    }
}