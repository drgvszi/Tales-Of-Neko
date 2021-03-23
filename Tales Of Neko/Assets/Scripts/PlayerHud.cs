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
    
    public GameObject playerGo;
    public Player Player;
    public void Start()
    {
        
        Player = playerGo.GetComponent<Player>();
        print(1);

        nameTextBox.text = Player.name;
        levelTextBox.text =  "  Lv. " + Player.Level;
        
        print(Player.MaxHealth+" "+Player.Health);
            
        HealthSlider.maxValue = (float) Player.MaxHealth;
        HealthSlider.value = (float) Player.Health;
        
        ManaSlider.maxValue = (float) Player.MaxMana;
        ManaSlider.value = (float) Player.Mana;

        XpBar.maxValue = (float) Player.NextLevelXp();
        XpBar.value = (float) Player.Experience;
        
        print(HealthSlider.value);
    }

    public void Set(Player player)
    {
        HealthSlider.value = (float) player.Health;
        ManaSlider.value = (float) player.Mana;
        XpBar.value = (float) Player.Experience;
        
    }
}