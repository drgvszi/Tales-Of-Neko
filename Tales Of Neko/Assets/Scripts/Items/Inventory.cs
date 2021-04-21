using System;
using System.Collections.Generic;
using Tales_of_Neko;

[Serializable] 
public class Inventory
{
    
    public EquippedItems EquippedItems;
    public List<Item> Items;

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