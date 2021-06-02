using System;
using UnityEngine;
using Tales_of_Neko;

[CreateAssetMenu(fileName ="New Item",menuName = "Item")]
public class Item:ScriptableObject
{   
    [Header("Item characteristics")]
    public string Name;
    public ItemType ItemType;
    public string Description;
    public int Price;
        
    [Header("Item stats")]
    public Stats Stats;
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