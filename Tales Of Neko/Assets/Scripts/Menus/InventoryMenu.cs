using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Tales_of_Neko;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    public GameObject ItemPrefab;
    public ScrollRect ScrollView;
    public GameObject Content;


    public GameObject Head;
    public GameObject Body;
    public GameObject Leggins;
    public GameObject Boots;
    public GameObject PrimaryWeapon;
    public GameObject SecondaryWeapon;

    public GameObject EquipMenu;

    public Text Stat1;
    public Text Stat2;
    public Text Stat3;
    public Text Stat4;
    void Start()
    {

        foreach (Transform child in Content.transform)
        {
            Destroy(child.gameObject);

        }


        Player player = GameManager.Instance.player;
        Inventory inventory = player.Inventory;

        List<Item> unequippedItems = inventory.Items;


        ScrollView.verticalNormalizedPosition = 1;
        for (int i = 0; i < unequippedItems.Count; i++)
        {
            GameObject spawnedItem = Instantiate(ItemPrefab);
            spawnedItem.transform.SetParent(Content.transform, false);

            ItemDetails<Item> itemDetails = spawnedItem.GetComponentInChildren<ItemDetails<Item>>();
            itemDetails.Text.text = unequippedItems[i].Name;
            itemDetails.Class = unequippedItems[i];
            
            
            
            Toggle toggle = spawnedItem.GetComponent<Toggle>();
            var index = i;
            toggle.onValueChanged.AddListener(delegate { ShowEquippedMenu(unequippedItems[index],spawnedItem); });
            
        }

        Stats stats = player.GetComplessiveStats();
        Stat1.text = stats.Strength.ToString();
        Stat2.text = stats.Constitution.ToString();
        Stat3.text = stats.Dexterity.ToString();
        Stat4.text = stats.Wisdom.ToString();



        Head.GetComponentInChildren<Text>().text=player.Inventory.EquippedItems.Helmet.Name;
        Body.GetComponentInChildren<Text>().text=player.Inventory.EquippedItems.Chestplate.Name;
        Leggins.GetComponentInChildren<Text>().text=player.Inventory.EquippedItems.Leggings.Name;
        Boots.GetComponentInChildren<Text>().text=player.Inventory.EquippedItems.Boots.Name;
        PrimaryWeapon.GetComponentInChildren<Text>().text=player.Inventory.EquippedItems.Weapon.Name;
        //SecondaryWeapon.GetComponentInChildren<Text>().text=player.Inventory.EquippedItems.H.Name;
    }
    

    public void EquippItem(Item item, GameObject ItemListElement)
    {
        Text itemSlot;
        Player player = GameManager.Instance.player;
        Item oldItem = null;
        print(item.Name);
        
        switch (item.ItemType)
        {
            case ItemType.Helmet:
                itemSlot = Head.GetComponentInChildren<Text>();
                player.Inventory.Items.Add(player.Inventory.EquippedItems.Helmet);
                oldItem = player.Inventory.EquippedItems.Helmet;
                player.Inventory.EquippedItems.ReplaceHelmet(item);
                itemSlot.text = item.Name;
                break;
            case ItemType.Chestplate:
                itemSlot = Body.GetComponentInChildren<Text>();
                player.Inventory.Items.Add(player.Inventory.EquippedItems.Chestplate);
                oldItem = player.Inventory.EquippedItems.Chestplate;
                player.Inventory.EquippedItems.ReplaceChestplate(item);
                itemSlot.text = item.Name;
                break;
            case ItemType.Leggings:
                itemSlot = Leggins.GetComponentInChildren<Text>();

                player.Inventory.Items.Add(player.Inventory.EquippedItems.Leggings);
                oldItem = player.Inventory.EquippedItems.Leggings;
                player.Inventory.EquippedItems.ReplaceLeggings(item);
                itemSlot.text = item.Name;
                break;
            case ItemType.Boots:
                itemSlot = Boots.GetComponentInChildren<Text>();
                player.Inventory.Items.Add(player.Inventory.EquippedItems.Boots);
                oldItem = player.Inventory.EquippedItems.Boots;
                player.Inventory.EquippedItems.ReplaceBoots(item);
                itemSlot.text = item.Name;
                break;
            case ItemType.Weapon:
                itemSlot = PrimaryWeapon.GetComponentInChildren<Text>();
                itemSlot.text = item.Name;
                
                player.Inventory.Items.Add(player.Inventory.EquippedItems.Weapon);
                oldItem = player.Inventory.EquippedItems.Weapon;
                player.Inventory.EquippedItems.ReplaceWeapon(item);
                break;
        }
        
        ItemDetails<Item> itemDetails = ItemListElement.GetComponentInChildren<ItemDetails<Item>>();

        itemDetails.Class = oldItem;
        itemDetails.Text.text = oldItem.Name;
        
        Stats stats = player.GetComplessiveStats();
        Stat1.text = stats.Strength.ToString();
        Stat2.text = stats.Constitution.ToString();
        Stat3.text = stats.Dexterity.ToString();
        Stat4.text = stats.Wisdom.ToString();

    }

    public void ShowEquippedMenu(Item unequippedItem,GameObject spawnedItem)
    {
        EquipMenu.SetActive(!EquipMenu.activeSelf);
        if (EquipMenu.activeSelf)
        {
            Text[] components = EquipMenu.GetComponentsInChildren<Text>();
            components[0].text = unequippedItem.Name;
            components[1].text = "TYPE : " + unequippedItem.ItemType.ToString() + "\n" + unequippedItem.Description;
            
            Button button = EquipMenu.GetComponentInChildren<Button>();
            if(button != null) {
                button.onClick.AddListener(() =>EquippItem(unequippedItem,spawnedItem));
            }
            
        }
       
        
        
    }
}
