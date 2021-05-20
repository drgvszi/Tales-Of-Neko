using System;
using System.Collections.Generic;
using Tales_of_Neko;
using UnityEngine;

namespace SaveLoadSystem
{
    [Serializable]
    public class SpellSave
    {
        public string Name;
        public ElementType Type;
        [Header("Spell stats")] public double AttackDamage;
        public double CurrentCooldown;
        public double MaxCooldown;
        public int ManaUsage;
        //public Animator spellanim;
        public bool IsEquipped = false;
        public bool IsBasicAttack = false;

        public List<KeyCode> Combo;
        public float ComboTimer;

        public string AnimationState;
        public bool IsTarget;

        public SpellSave(Spell spell)
        {
            Name = spell.name;
            Type = spell.Type;
            CurrentCooldown = spell.CurrentCooldown;
            MaxCooldown = spell.MaxCooldown;
            ManaUsage = spell.ManaUsage;
            //spellanim = spell.spellanim;
            IsEquipped = spell.IsEquipped;
            IsBasicAttack = spell.IsBasicAttack;
            Combo = spell.Combo;
            ComboTimer = spell.ComboTimer;
            AnimationState = spell.AnimationState;
            IsTarget = spell.IsTarget;
            AttackDamage = spell.AttackDamage;
        }

        public static SpellSave Save(Spell spell)
        {
            SpellSave save = new SpellSave(spell);
            return save;
        }

        public static Spell Load(SpellSave spellSave)
        {
            Spell spell = new Spell(spellSave.Name,spellSave.Type,spellSave.AttackDamage,
                spellSave.MaxCooldown,spellSave.ManaUsage);
            spell.CurrentCooldown = spellSave.CurrentCooldown;
            spell.Combo = new List<KeyCode>(spellSave.Combo);
            spell.AnimationState = spellSave.AnimationState;
            //spell.spellanim = spellSave.spellanim;
            spell.ComboTimer = spellSave.ComboTimer;
            spell.IsEquipped = spellSave.IsEquipped;
            spell.IsTarget = spellSave.IsTarget;
            spell.IsBasicAttack = spellSave.IsBasicAttack;

            return spell;
        }
        
    }
}