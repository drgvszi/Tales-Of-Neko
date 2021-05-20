using System;
using System.Collections;
using System.Collections.Generic;
using Tales_of_Neko;
using UnityEngine;

[Serializable]
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
    public Animator spellanim;
    public bool IsEquipped = false;
    public bool IsBasicAttack = false;

    public List<KeyCode> Combo;
    public float ComboTimer;

    public string AnimationState;
    public bool IsTarget;

    public Spell(string name,ElementType type,double attackDamage,double maxCooldown,int manaUsage)
    {
        
        Name = name;
        Type = type;
        AttackDamage = attackDamage;
        MaxCooldown = maxCooldown;
        ManaUsage = manaUsage;
        CurrentCooldown = 0;
        Combo = new List<KeyCode>();
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

    public static string TypeToString(Spell spell)
    {
        if (spell.IsBasicAttack)
        {
            return "👊";
        }
        switch (spell.Type)
        {
            case ElementType.Fire:
                return "🔥";
            case ElementType.Air:
                return "🌀";
            case ElementType.Water:
                return "🌊";
            case ElementType.Earth:
                return "⛰️";
            case ElementType.Light:
                return "✨";
            case ElementType.Dark:
                return "🌑";
            default:
                return "🔮";
        }
        
    }
}
