using System.Collections;
using System.Collections.Generic;
using Tales_of_Neko;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SpellsMenu : MonoBehaviour
{
    public GameObject ItemPrefab;
    public ScrollRect ScrollView;
    public GameObject Content;
    // Start is called before the first frame update


    public Text spellName;
    public Text spellCombo;
    public Text spellType;
    void Start()
    {
        foreach (Transform  child in Content.transform)
        {
            Destroy(child.gameObject);

        }


        Spell[] spells = Resources.LoadAll<Spell>("Spells");

        ScrollView.verticalNormalizedPosition = 1;
        for (int i = 0; i < spells.Length; i++)
        {
            GameObject spawnedItem = Instantiate(ItemPrefab);
            Button button = spawnedItem.GetComponent<Button>();
            int index = i;
            if(button != null) {
                button.onClick.AddListener(() =>SpellListClick(spells[index]));
            }
            spawnedItem.transform.SetParent(Content.transform,false);

            ItemDetails<Spell> itemDetails = spawnedItem.GetComponentInChildren<ItemDetails<Spell>>();
            itemDetails.Text.text = spells[i].Name;
            itemDetails.Class = spells[i];
        }
    }

    private void SpellListClick(Spell spell)
    {
        print(spell.Name);
        spellName.text = "Name: " + spell.Name;
        spellType.text = "Type: " + spell.Type.ToString();
        spellCombo.text = "Combo: ";
        List<KeyCode> keyCodes = spell.Combo;
        foreach (KeyCode kcode in keyCodes)
        {
            switch (kcode)
            {
                case KeyCode.UpArrow:
                    spellCombo.text += "⇧ ";
                    break;
                case KeyCode.RightArrow:
                    spellCombo.text += "⇨ ";
                    break;
                case KeyCode.LeftArrow:
                    spellCombo.text += "⇦ ";
                    break;
                case KeyCode.DownArrow:
                    spellCombo.text += "⇩ ";
                    break;
                default:
                    spellCombo.text += kcode.ToString()+" ";
                    break;

            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
