using System;

namespace SaveLoadSystem
{
    [Serializable]
    public class EquippedItemsSave
    {
        public ItemSave Helmet;
        public ItemSave Chestplate;
        public ItemSave Leggings;
        public ItemSave Boots;

        public ItemSave Weapon;


        public static EquippedItemsSave Save(EquippedItems items)
        {
            EquippedItemsSave save = new EquippedItemsSave();
            save.Helmet=ItemSave.Save(items.Helmet);
            save.Chestplate=ItemSave.Save(items.Chestplate);
            save.Leggings=ItemSave.Save(items.Leggings);
            save.Boots=ItemSave.Save(items.Boots);
            save.Weapon=ItemSave.Save(items.Weapon);

            return save;
        }

        public static EquippedItems Load(EquippedItemsSave save)
        {
            EquippedItems items = new EquippedItems();
            items.Helmet = ItemSave.Load(save.Helmet);
            items.Chestplate = ItemSave.Load(save.Chestplate);
            items.Leggings = ItemSave.Load(save.Leggings);
            items.Boots = ItemSave.Load(save.Boots);
            items.Weapon = ItemSave.Load(save.Weapon);
            return items;

        }
    }
}