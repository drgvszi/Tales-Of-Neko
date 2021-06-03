using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.outOFBattle)
        {
            player.transform.position = GameManager.Instance.PlayerPos;
            GameManager.Instance.outOFBattle = false;
        }

    }
}
