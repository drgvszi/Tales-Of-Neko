using System;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHud:MonoBehaviour
{
    public Text nameTextBox;
    public Text levelTextBox;
    public Slider HealthSlider;
    public Slider ManaSlider;
    public Slider XpBar;
    
    public Player Player;
    public void Start()
    {

        Player = GameManager.Instance.player;
        print(1);

        if (nameTextBox!=null)
        {
            nameTextBox.text = Player.name;
            levelTextBox.text = "  Lv. " + Player.Level;
        }

        HealthSlider.maxValue = (float) Player.MaxHealth;
        HealthSlider.value = (float) Player.Health;
        
        ManaSlider.maxValue = (float) Player.MaxMana;
        ManaSlider.value = (float) Player.Mana;

        if (XpBar != null)
        {
            XpBar.maxValue = (float) Player.NextLevelXp();
            XpBar.value = (float) Player.Experience;
        }
    }

    public void Set(Player player)
    {
        
        HealthSlider.value = (float) player.Health;
        ManaSlider.value = (float) player.Mana;
        if (XpBar != null)
        {
            XpBar.value = (float) Player.Experience;
        }
    }
}