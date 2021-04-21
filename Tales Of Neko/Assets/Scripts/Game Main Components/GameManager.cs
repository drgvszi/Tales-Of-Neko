using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    public Player player;
    public List<Mob> enemies;

    public int enemyAttacked;
    public QuestManager QuestManager;
    
    
    public void Awake()
    {
        if (instance == null) {
            instance = Instance;
            instance.player = Resources.Load<GameObject>("Player\\Player").GetComponent<Player>();
            GameObject[] enemiesGo = Resources.LoadAll<GameObject>("Enemies");
            instance.enemies=new List<Mob>(enemiesGo.Length);
            QuestManager = new QuestManager();
            
            for (int i = 0; i < enemiesGo.Length; i++)
            {
                instance.enemies.Add(enemiesGo[i].GetComponent<Mob>());
            }
            DontDestroyOnLoad (instance);
        } else if (this != instance) {
            Destroy (this.gameObject);
 
        }
    }
    

    private GameManager()
    {
        
        // initialize your game manager here. Do not reference to GameObjects here (i.e. GameObject.Find etc.)
        // because the game manager will be created before the objects
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager> ();
                DontDestroyOnLoad (instance.gameObject);
            }

            return instance;
        }
    }
}