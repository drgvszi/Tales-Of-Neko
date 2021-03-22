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
    [Header("Spell stats")] public double AttackDamage;
    public double CurrentCooldown;
    public double MaxCooldown;
    public int ManaUsage;

   

    public Spell(string name,ElementType type,double attackDamage,double maxCooldown,int manaUsage)
    {
        Name = name;
        Type = type;
        AttackDamage = attackDamage;
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
        if (player.GetCurrentMana() > ManaUsage && IsReady())
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
