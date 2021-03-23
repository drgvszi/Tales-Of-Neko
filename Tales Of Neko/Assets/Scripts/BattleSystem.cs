using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Tales_of_Neko;
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

    public PlayerHud playerHud;
    public EnemyHud enemyHud;

    public BattleState battleState;

    public Player player;
    public Mob enemy;
    
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

        yield return new WaitForSeconds(1f);
        battleState = BattleState.PlayerTurn;

    }

    IEnumerator PlayerTurn()
    {
	    yield return new WaitForSeconds(0f);
    }


    public void UseSpellOnClick(GameObject SpellSlot)
    {
	    SpellSlot spellSlot = SpellSlot.GetComponent<SpellSlot>();
	    if (spellSlot.Spell.IsBasicAttack)
	    {
		    UseBasicAttack(spellSlot.Spell.AttackDamage);
	    }
	    else
	    {
		    UseSpell(spellSlot.Spell);
	    }
    }
    public void UseSpell(Spell spell)
    { 
	    if (battleState != BattleState.PlayerTurn) return;
	    StartCoroutine(ManaAttack(spell));

    }
    public void UseBasicAttack(double addedAttack)
    {
	    if (battleState != BattleState.PlayerTurn)
		    return;
	    StartCoroutine(BasicAttack(addedAttack));
	    
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

		    UpdatePlayerHud();
		    UpdateEnemyHud();
	    
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
	   
    }
    
    IEnumerator BasicAttack(double addedAttack)
    {
	    double playerDexterity = player.GetComplessiveStats().Dexterity;
	    double playerStrength = player.GetComplessiveStats().Strength;
	    
	    enemy.TakeDamage(playerStrength + 0.2 * playerDexterity +addedAttack);
	    
	    bool isDead = !enemy.IsAlive();
	    
	    UpdatePlayerHud();
	    UpdateEnemyHud();
	    
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

	    double randomNumber = Random.value;
	    if (randomNumber <= 0.3)
	    {
		    player.TakeDamage(enemy.Stats.Strength + 0.2 * enemy.Stats.Dexterity);
	    }
	    else
	    {
		    List<Spell> availableSpells = enemy.GetAvailableSpells();
		    if (availableSpells.Count == 0)
		    {
			    player.TakeDamage(enemy.Stats.Strength + 0.2 * enemy.Stats.Dexterity);
		    }
		    else
		    {
			    int wichSpell = Random.Range(0, availableSpells.Count);
			    Spell spell = availableSpells[wichSpell];
			    
			    double enemyWisdom = enemy.Stats.Wisdom;
			    double enemyStrength = enemy.Stats.Strength;
			    enemy.UseMana(spell.ManaUsage);
			    player.TakeDamage(enemyWisdom * 0.8 + enemyStrength * 0.3 + spell.AttackDamage);
			    
		    }
	    }
	    bool isDead = !player.IsAlive();
	    
	    UpdatePlayerHud();
	    UpdateEnemyHud();
	    
	    yield return new WaitForSeconds(1f);
	    
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

    private void UpdateEnemyHud()
    {
	    playerHud.Set(player);
    }

    private void UpdatePlayerHud()
    {
	    enemyHud.Set(enemy);
    }

    IEnumerator EndBattle()
    {
	    yield return new WaitForSeconds(2f);
	    SceneManager.LoadScene("Map");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
