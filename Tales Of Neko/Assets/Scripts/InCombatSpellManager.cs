using System.Collections;
using System.Collections.Generic;
using Tales_of_Neko;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InCombatSpellManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerGo;
    public List<GameObject> spellSlot;
    
    void Start()
    {
        Player Player = playerGo.GetComponent<Player>();
        List<Spell> spells = Player.GetEquippedSpells();
        for(int i=0;i<spells.Count;i++)
        {
            spellSlot[i].AddComponent<SpellSlot>().Spell=spells[i];
            spellSlot[i].SetActive(true);
            
            Button spellSlotButton=spellSlot[i].GetComponent<Button>();
            Text spellSlotTextField = spellSlotButton.GetComponentInChildren<Text>();
            spellSlotTextField.text = spells[i].Name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
