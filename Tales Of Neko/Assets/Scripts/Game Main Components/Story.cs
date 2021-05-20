using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/GameManager.save")) {
            SceneManager.LoadScene("Map");
            Debug.Log("CE?");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
