using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectClass : MonoBehaviour
{
    public GameObject SceneGameObject;

    public ChangeScene SceneChanger;
    // Start is called before the first frame update
    void Start()
    {
        SceneChanger = SceneGameObject.GetComponent<ChangeScene>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectMage()
    {   Debug.Log("Warrior");
        PlayerPrefs.SetString("class","mage");
        SceneChanger.ChangerScene();
    }

    public void selectWarrior()
    {
        Debug.Log("Warrior");
        PlayerPrefs.SetString("class","warrior");
        SceneChanger.ChangerScene();
    }

    public void selectRogue()
    {
        Debug.Log("Rogue");
        PlayerPrefs.SetString("class","rogue");
        SceneChanger.ChangerScene();
    }
}
