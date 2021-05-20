using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string Sname;
    // Update is called once per frame
    public void ChangerScene()
    {
        
        SceneManager.LoadScene(Sname);
    }
}
