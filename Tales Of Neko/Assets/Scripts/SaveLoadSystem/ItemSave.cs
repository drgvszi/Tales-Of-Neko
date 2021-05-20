using System;
using Tales_of_Neko;

namespace SaveLoadSystem
{
    [Serializable]
    public class ItemSave
    {
        public string Name;
        public ItemType ItemType;
        public string Description;
        public Stats Stats;

        public static ItemSave Save(Item item)
        {
            ItemSave save = new ItemSave();
            save.Name = item.Name;
            save.ItemType = item.ItemType;
            save.Description = item.Description;
            save.Stats = item.Stats;
            return save;
        }

        public static Item Load(ItemSave save)
        {
            Item item = new Item();
            item.Name = save.Name;
            item.ItemType = save.ItemType;
            item.Description = save.Description;
            item.Stats = save.Stats;
            return item;
        }
    }
}