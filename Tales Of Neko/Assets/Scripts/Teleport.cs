using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Teleport : MonoBehaviour
{
    public GameObject obj;
    public GameObject obj1;

    
    private void OnTriggerEnter2D(Collider2D other)
    {   

        if (Vector2.Distance(transform.position, other.transform.position) > 0.3f)
        {   
            Thread.Sleep(300);
            obj.SetActive(false);
            obj1.SetActive(true);
        }
    }
    
}

