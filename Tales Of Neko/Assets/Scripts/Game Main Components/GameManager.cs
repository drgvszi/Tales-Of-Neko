using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SaveLoadSystem;
using Tales_of_Neko;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameObject;
    private static GameManager _instance;
    
    public Player player;
    public List<Mob> enemies;

    public int enemyAttacked;
    public QuestManager QuestManager;
    public List<Item> shop;


    public void Awake()
    {
        if (_instance == null) {
            _instance = Instance;
            _instance.gameObject = new GameObject();

            if (File.Exists(Application.persistentDataPath + "/GameManager.save")) {
                
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/GameManager.save", FileMode.Open);
                GameManagerSave save = (GameManagerSave)bf.Deserialize(file);
                file.Close();
  
                GameManagerData data = GameManagerSave.Load(save);
                _instance.player=_instance.gameObject.GetComponent<Player>();
                _instance.enemies = new List<Mob>(_instance.gameObject.GetComponents<Mob>());
                _instance.enemyAttacked = data.enemyAttacked;
                _instance.QuestManager = new QuestManager();
            }
            else
            {
                _instance.player = Resources.Load<GameObject>("Player\\Player").GetComponent<Player>();
                _instance.shop = new List<Item>(Resources.Load<GameObject>("Shop\\Shop").GetComponent<ShopItems>().Items);
                player.Class = getClass();
                GameObject[] enemiesGo = Resources.LoadAll<GameObject>("Enemies");
                _instance.enemies=new List<Mob>(enemiesGo.Length);
                QuestManager = new QuestManager();
            
                for (int i = 0; i < enemiesGo.Length; i++)
                {
                    _instance.enemies.Add(enemiesGo[i].GetComponent<Mob>());
                }
            }
            DontDestroyOnLoad(_instance.gameObject);
            DontDestroyOnLoad (_instance);
        } else if (this != _instance) {
            Destroy (this);
 
        }
    }
    
    private GameManager()
    {
        QuestManager questManager = new QuestManager();
        // initialize your game manager here. Do not reference to GameObjects here (i.e. GameObject.Find etc.)
        // because the game manager will be created before the objects
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager> ();
            }

            return _instance;
        }
    }

    public CharacterClass getClass()
    {
        if (PlayerPrefs.GetString("class") == "mage")
        {
            return CharacterClass.Mage;
        }
        else if (PlayerPrefs.GetString("class") == "warrior")
        {
            return CharacterClass.Warrior;
        }

        return CharacterClass.Rogue;
    }
    

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/GameManager.save", FileMode.Open);
        GameManagerSave save = (GameManagerSave)bf.Deserialize(file);
        file.Close();

        GameManagerData data = GameManagerSave.Load(save);

        _instance.player=_instance.gameObject.GetComponent<Player>();
        _instance.enemies = new List<Mob>(_instance.gameObject.GetComponents<Mob>());
        _instance.enemyAttacked = data.enemyAttacked;
        _instance.QuestManager = new QuestManager();

    }

    public void Save()
    {
        GameManagerSave save = GameManagerSave.Save(GameManager._instance);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/GameManager.save");
        bf.Serialize(file, save);
        file.Close();
    }
}