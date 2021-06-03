using UnityEngine;
using UnityEngine.UI;

public class EnemyHud:MonoBehaviour
{
    public Text nameTextBox;
    public Text levelTextBox;
    public Slider HealthSlider;
    public Slider ManaSlider;
    
    public Slider HealthSliderTransparent;
    public Slider ManaSliderTransparent;
    
    public Mob Enemy;
    public void Start()
    {
        

        Enemy = GameManager.Instance.enemies
            [GameManager.Instance.enemyAttacked[GameManager.Instance.enemyAttacked.Count-1]];

        nameTextBox.text = Enemy.Name;
        levelTextBox.text =  "  Lv. " + Enemy.Level;
            
        HealthSlider.maxValue = (float) Enemy.MaxHealth;
        HealthSlider.value = (float) Enemy.Health;
        
        ManaSlider.maxValue = (float) Enemy.MaxMana;
        ManaSlider.value = (float) Enemy.Mana;
        
        HealthSliderTransparent.maxValue=HealthSlider.maxValue;
        HealthSliderTransparent.value=HealthSlider.value;
        
        ManaSliderTransparent.maxValue = (float) Enemy.MaxMana;
        ManaSliderTransparent.value = (float) Enemy.Mana;
    }

    public void Set(Mob enemy)
    {
        HealthSlider.value = (float) enemy.Health;
        ManaSlider.value = (float) enemy.Mana;
    }
    
    public void SetTransparent(Mob enemy)
    { 
        HealthSliderTransparent.value = (float) enemy.Health;
        ManaSliderTransparent.value = (float) enemy.Mana;

    }
}