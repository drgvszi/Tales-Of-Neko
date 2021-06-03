using System;
using System.Collections;
using System.Collections.Generic;
using Tales_of_Neko;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject ItemPrefab;
    public ScrollRect ScrollView;

    public GameObject Content;
    // Start is called before the first frame update


    public Text Name;
    public Text Description;
    public Text Price;
    public GameObject StatusPanel;
    public Button Buy;

    void Start()
    {
        foreach (Transform child in Content.transform)
        {
            Destroy(child.gameObject);

        }
        List<Item> items = GameManager.Instance.shop;
        ScrollView.verticalNormalizedPosition = 1;
        for (int i = 0; i < items.Count; i++)
        {
            GameObject spawnedItem = Instantiate(ItemPrefab);
            Toggle toggle = spawnedItem.GetComponent<Toggle>();
            int index = i;
            if (toggle != null)
            {
                toggle.onValueChanged.AddListener(delegate {  ItemListClick(items[index]);});
            }

            spawnedItem.transform.SetParent(Content.transform, false);

            ItemDetails<Item> itemDetails = spawnedItem.GetComponentInChildren<ItemDetails<Item>>();
            itemDetails.Text.text = items[i].Name;
            itemDetails.Class = items[i];
        }
    }
    private void ItemListClick(Item item)
    {
        Name.text = item.Name;
        Description.text=item.Description;
        Price.text = "Price: " + item.Price;
        StatusPanel.SetActive(true);
        if (GameManager.Instance.player.Money >= item.Price)
        {
            Buy.interactable = true;
            Buy.onClick.AddListener(() => BuyItem(item));
        }
        else
        {
            Buy.interactable = false;
        }
    }
    
    private void BuyItem(Item item)
    {
        GameManager.Instance.player.Money -= item.Price;
        GameManager.Instance.player.Inventory.Items.Add(item);
        Description.text = "SOLD";
        Buy.interactable = false;
    }
}
