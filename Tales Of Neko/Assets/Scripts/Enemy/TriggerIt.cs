using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerIt : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyGo;

    private void Update()
    {
        Mob LastAttackedEnemy = GameManager.Instance.enemies[GameManager.Instance.enemyAttacked];
        if (LastAttackedEnemy.Name == enemy.GetComponent<Mob>().Name && LastAttackedEnemy.Health <= 0)
        {
            enemyGo.SetActive(false);
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
       if (Vector2.Distance(transform.position, other.transform.position) > 0.3f)
       {
           List<Mob> enemies = GameManager.Instance.enemies;
           for (int i = 0; i < enemies.Count; i++)
           {
               if (enemies[i].Name == enemy.GetComponent<Mob>().Name)
               {
                   GameManager.Instance.enemyAttacked = i;
               }
           }
           
           SceneManager.LoadScene("BattleGround");
           
       }

     
    }
}
