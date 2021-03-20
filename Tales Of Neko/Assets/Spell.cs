using System.Collections;
using System.Collections.Generic;
using Tales_of_Neko;
using UnityEngine;

[CreateAssetMenu(fileName ="New Spell",menuName = "Spell")]
public class Spell : ScriptableObject
{
    [Header("Spell characteristic")]
    public string Name;
    public ElementType Type;
    [Header("Spell stats")] 
    public Stats Stats;
    public double CurrentCooldown;
    public double MaxCooldown;
    public int ManaUsage;

   

    public Spell(string name,ElementType type,Stats stats,double maxCooldown,int manaUsage)
    {
        Name = name;
        Type = type;
        Stats = stats;
        MaxCooldown = maxCooldown;
        ManaUsage = manaUsage;
        CurrentCooldown = 0;
    }
        
    public bool IsReady()
    {
        return CurrentCooldown == 0;
    }
    public bool CanUse(Player player)
    {
        if (Player.ThePlayer.GetCurrentMana() > ManaUsage && IsReady())
        {
            return true;
        }

        return false;
    }
    public void PutOnCooldown()
    {
        CooldownManager.Instance.StartCoolDown(this);    
    }
}
