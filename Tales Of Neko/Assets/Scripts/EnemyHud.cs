using UnityEngine;
using UnityEngine.UI;

public class EnemyHud:MonoBehaviour
{
    public Text nameTextBox;
    public Text levelTextBox;
    public Slider HealthSlider;
    public Slider ManaSlider;

    public GameObject enemyGo;
    public Mob Enemy;
    public void Start()
    {
        
        Enemy = enemyGo.GetComponent<Mob>();

        nameTextBox.text = Enemy.name;
        levelTextBox.text =  "  Lv. " + Enemy.Level;
            
        HealthSlider.maxValue = (float) Enemy.MaxHealth;
        HealthSlider.value = (float) Enemy.Health;
        
        ManaSlider.maxValue = (float) Enemy.MaxMana;
        ManaSlider.value = (float) Enemy.Mana;
    }

    public void Set(Mob enemy)
    {
        HealthSlider.value = (float) enemy.Health;
        ManaSlider.value = (float) enemy.Mana;
    }
}