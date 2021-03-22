using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum BattleState{Start,PlayerTurn,EnemyTurn,Win,Lose}
public enum AttackType{Mana,Physic}
public class BattleSystem: MonoBehaviour
{
    public GameObject playerGameObject;
    public GameObject enemyGameObject;
    // Start is called before the first frame update

    public Transform playerStation;
    public Transform enemyStation;

    public BattleState battleState;

    public Player player;
    public Mob enemy;

    [FormerlySerializedAs("PlayerHud")] public PlayerHud playerHud;
    [FormerlySerializedAs("EnemyHud")] public EnemyHud enemyHud;
    void Start()
    {
        battleState = BattleState.Start;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
	    playerGameObject.GetComponent<Renderer>().sortingOrder = 1;
	    enemyGameObject.GetComponent<Renderer>().sortingOrder = 1;
        GameObject playerGo = Instantiate(playerGameObject, playerStation);
        GameObject enemyGo = Instantiate(enemyGameObject, enemyStation);
        
        player = playerGo.GetComponent<Player>();
        enemy = enemyGo.GetComponent<Mob>();

        yield return new WaitForSeconds(2f);
        battleState = BattleState.PlayerTurn;

        playerHud.textBox.text = player.ToString();
        enemyHud.textBox.text = enemy.ToString();
        
    
    }

    IEnumerator PlayerTurn()
    {
	    playerHud.textBox.text = "";
	    playerHud.textBox.text += "Player turn\n";
	    playerHud.textBox.text += player.ToString();
	    yield return new WaitForSeconds(1f);
    }

    
    public void UseSpell(Spell spell)
    { 
	    if (battleState != BattleState.PlayerTurn) return;
	    playerHud.textBox.text += "use spell "+spell.Name+"\n";
	    StartCoroutine(ManaAttack(spell));

    }
    public void UseBasicAttack()
    {
	    if (battleState != BattleState.PlayerTurn)
		    return;
	    playerHud.textBox.text += "use basic attack\n";
	    StartCoroutine(BasicAttack());
	    
    }

    IEnumerator ManaAttack(Spell spell)
    {
	    if (player.CanUse(spell))
	    {
		    double playerWisdom = player.GetComplessiveStats().Wisdom ;
		    double playerStrength = player.GetComplessiveStats().Strength;

		    player.UseMana(spell.ManaUsage);
		    enemy.TakeDamage(playerWisdom * 0.8 + playerStrength * 0.3 + spell.AttackDamage);
		    
		    bool isDead = !enemy.IsAlive();
	    
		    yield return new WaitForSeconds(2f);
	    
		    if(isDead)
		    {
			    battleState = BattleState.Win;
			    StartCoroutine(EndBattle());
		    } else
		    {
			    battleState= BattleState.EnemyTurn;
			    StartCoroutine(EnemyTurn());
		    }
		    
	    }
	   
    }
    
    IEnumerator BasicAttack()
    {
	    double playerDexterity = player.GetComplessiveStats().Dexterity;
	    double playerStrength = player.GetComplessiveStats().Strength;
	    
	    enemy.TakeDamage(playerStrength + 0.2 * playerDexterity);
	    
	    bool isDead = !enemy.IsAlive();
	    
	    yield return new WaitForSeconds(1f);
	    
	    if(isDead)
	    {
		    battleState = BattleState.Win;
		    StartCoroutine(EndBattle());
	    } else
	    {
		    battleState= BattleState.EnemyTurn;
		    StartCoroutine(EnemyTurn());
	    }
	    
    }
    
    IEnumerator EnemyTurn()
    {
	    
	    bool isDone=false;
	    enemyHud.textBox.text = "";
	    enemyHud.textBox.text += "Enemy turn\n";
	    enemyHud.textBox.text += enemy.ToString();
	    
	    double randomNumber = Random.value;
	    if (randomNumber <= 0.3)
	    {
		    player.TakeDamage(enemy.Stats.Strength + 0.2 * enemy.Stats.Dexterity);
		    enemyHud.textBox.text += "Use basic attack\n";
	    }
	    else
	    {
		    List<Spell> availableSpells = enemy.GetAvailableSpells();
		    if (availableSpells.Count == 0)
		    {
			    player.TakeDamage(enemy.Stats.Strength + 0.2 * enemy.Stats.Dexterity);
			    enemyHud.textBox.text += "Use basic attack\n";
		    }
		    else
		    {
			    int wichSpell = Random.Range(0, availableSpells.Count);
			    Spell spell = availableSpells[wichSpell];
			    
			    double enemyWisdom = enemy.Stats.Wisdom;
			    double enemyStrength = enemy.Stats.Strength;

			    enemyHud.textBox.text += "Use spell " + spell.Name + "\n";
			    enemy.UseMana(spell.ManaUsage);
			    player.TakeDamage(enemyWisdom * 0.8 + enemyStrength * 0.3 + spell.AttackDamage);
			    
		    }
	    }
	    bool isDead = !player.IsAlive();
	    
	    yield return new WaitForSeconds(3f);
	    
	    if(isDead)
	    {
		    battleState = BattleState.Lose;
		    StartCoroutine(EndBattle());
	    } else
	    {
		    battleState= BattleState.PlayerTurn;
		    StartCoroutine(PlayerTurn());
	    }
    }

    IEnumerator EndBattle()
    {
	    playerHud.textBox.text = "";
	    playerHud.textBox.text += battleState == BattleState.Win ? "Player won!" : "Player lost!";
	    yield return new WaitForSeconds(3f);
	    SceneManager.LoadScene("Map");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
