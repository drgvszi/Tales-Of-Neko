using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcShop : MonoBehaviour
{
    public GameObject Shop;
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        Shop.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
