using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    private string Sname = "Map";
    // Update is called once per frame
    public void ChangerScene()
    {
        
        SceneManager.LoadScene(Sname);
    }
}
