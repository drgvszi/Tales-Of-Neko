using System;
using System.Collections.Generic;

namespace SaveLoadSystem
{
    [Serializable]
    public class InventorySave
    {
        public EquippedItemsSave EquippedItems;
        public List<ItemSave> Items;

        public static InventorySave Save(Inventory inventory)
        {
            InventorySave inventorySave = new InventorySave();
            inventorySave.Items = new List<ItemSave>();
            foreach (Item item in inventory.Items)
            {
                inventorySave.Items.Add(ItemSave.Save(item));
            }

            inventorySave.EquippedItems = EquippedItemsSave.Save(inventory.EquippedItems);

            return inventorySave;

        }

        public static Inventory Load(InventorySave save)
        {
            Inventory inventory = new Inventory();
            inventory.Items = new List<Item>();
            foreach (ItemSave itemSave in save.Items)
            {
                inventory.Items.Add(ItemSave.Load(itemSave));
            }
            inventory.EquippedItems = EquippedItemsSave.Load(save.EquippedItems);
            return inventory;
        }
        
    }
}