using System.Collections;
using System.Collections.Generic;
using SaveLoadSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Load()
    {
        SaveSystem.Instance.Load();
        SceneManager.LoadScene("Map");
    }
}
