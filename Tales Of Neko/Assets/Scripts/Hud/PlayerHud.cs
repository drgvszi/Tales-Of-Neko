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

    public Slider HealthSliderTransparent;
    public Slider ManaSliderTransparent;
    public Slider XpBarTransparent;
    
    public Player Player;
    public void Start()
    {
        //Debug.Log(GameManager.Instance.enemies);
        //Debug.Log(GameManager.Instance.player);
        
        Player = GameManager.Instance.player;

        if (nameTextBox!=null)
        {
            nameTextBox.text = Player.Name;
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

        if (HealthSliderTransparent !=null)
        {
            HealthSliderTransparent.maxValue = (float) Player.MaxHealth;
            HealthSliderTransparent.value = (float) Player.Health;

            ManaSliderTransparent.maxValue = (float) Player.MaxMana;
            ManaSliderTransparent.value = (float) Player.Mana;
            
            XpBarTransparent.maxValue = (float) Player.NextLevelXp();
            XpBarTransparent.value = (float) Player.Experience;
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

    public void SetTransparent(Player player)
    { 
        HealthSliderTransparent.value = (float) player.Health;
        ManaSliderTransparent.value = (float) player.Mana;
        if (XpBarTransparent != null)
        {
            XpBarTransparent.value = (float) Player.Experience;
        }
        
    }
}