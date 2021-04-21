using System;
using System.Collections;
using System.Collections.Generic;
using Tales_of_Neko;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSpells : MonoBehaviour
{
    public List<GameObject> SpellSlots = new List<GameObject>(6);

    public GameObject ItemPrefab;
    public ScrollRect ScrollView;
    public GameObject Content;

    public int _PressedSpellSlot=-1;
    public GameObject _SelectedSpellGO=null;
    
    void Start()
    {
        
        foreach (Transform  child in Content.transform)
        {
            Destroy(child.gameObject);

        }


        Player player = GameManager.Instance.player;
        List<Spell> spells = player.Spells;
        List<Spell> equippedSpells = player.Spells.FindAll(delegate(Spell spell) { return spell.IsEquipped; });
        List<Spell> unequippedSpells = player.Spells.FindAll(delegate(Spell spell) { return !spell.IsEquipped; });
        for (int i = 0; i < Math.Min(equippedSpells.Count, SpellSlots.Count); i++)
        {
            SpellSlots[i].GetComponentInChildren<Text>().text = equippedSpells[i].Name;
            SpellSlot SpellSlot = SpellSlots[i].GetComponent<SpellSlot>();
            SpellSlot.Spell=equippedSpells[i];
            SpellSlot.IsSet = true;
        }
        
        ScrollView.verticalNormalizedPosition = 1;
        for (int i = 0; i < unequippedSpells.Count; i++)
        {
            GameObject spawnedItem = Instantiate(ItemPrefab);
            Button button = spawnedItem.GetComponent<Button>();
            if(button != null) {
                button.onClick.AddListener(() =>SpellListClick(spawnedItem));
            }
            else
            {
                print("ceeee?");
            }
            spawnedItem.transform.SetParent(Content.transform,false);

            ItemDetails<Spell> itemDetails = spawnedItem.GetComponentInChildren<ItemDetails<Spell>>();
            itemDetails.Text.text = unequippedSpells[i].Name + "  TYPE: " + unequippedSpells[i].Type.ToString() + 
                                    "   ATK: " + unequippedSpells[i].AttackDamage;
            itemDetails.Class = unequippedSpells[i];
        } 
        
        

    }
    

    private void Update()
    {
        if(_SelectedSpellGO!=null){
            ItemDetails<Spell> itemDetails = _SelectedSpellGO.GetComponentInChildren<ItemDetails<Spell>>();
            if ( itemDetails != null)
            {
                if (_PressedSpellSlot != -1)
                {
                    SpellSlots[_PressedSpellSlot].GetComponentInChildren<Text>().text = itemDetails.Class.Name;
                    itemDetails.Class.IsEquipped = true;
                    SpellSlot spellSlot = SpellSlots[_PressedSpellSlot].GetComponent<SpellSlot>();
                    if (spellSlot.Spell)
                    {
                        spellSlot.Spell.IsEquipped = false;
                    }
                    spellSlot.Spell = itemDetails.Class;
                    spellSlot.IsSet = true;
                    
                    
                    Start();
                    _PressedSpellSlot = -1;
                    _SelectedSpellGO = null;

                }
            }
        }
    }

    public void SpellListClick(GameObject gameObject)
    {
        _SelectedSpellGO = gameObject;
    }
    public void ClickedSpellSlot1()
    {
        _PressedSpellSlot = 0;
    }
    public void ClickedSpellSlot2()
    {
        _PressedSpellSlot = 1;
    }

    public void ClickedSpellSlot3()
    {
        _PressedSpellSlot = 2;
    }

    public void ClickedSpellSlot4()
    {
        _PressedSpellSlot = 3;
    }

    public void ClickedSpellSlot5()
    {
        _PressedSpellSlot = 4;
    }

    public void ClickedSpellSlot6()
    {
        _PressedSpellSlot = 5;
    }
}

