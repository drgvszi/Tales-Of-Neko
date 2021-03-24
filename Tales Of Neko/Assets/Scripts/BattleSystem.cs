using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Tales_of_Neko;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
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

    public Text gameChat;
    
    void Start()
    {
        battleState = BattleState.Start;
        StartCoroutine(SetupBattle());
        gameChat.text = "Battle start!";
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
        if (player.GetComplessiveStats().Dexterity > enemy.Stats.Dexterity)
        { 
	        battleState = BattleState.PlayerTurn;
	        gameChat.text = "You are faster than your enemy, it is your turn!";
        }
        else
        {
	        battleState = BattleState.EnemyTurn;
	        gameChat.text = "The enemy is faster than you, it is his turn!";
	        StartCoroutine(EnemyTurn());
        }
        

    }

    IEnumerator PlayerTurn()
    {
	    gameChat.text = "It is your turn";
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
	    gameChat.text="It is the enemy turn";
	    
	    bool isDone=false;

	    double randomNumber = Random.value;
	    if (randomNumber <= 0.3)
	    {
		    gameChat.text=enemy.name+" uses basic attack";
		    player.TakeDamage(enemy.Stats.Strength + 0.2 * enemy.Stats.Dexterity);
	    }
	    else
	    {
		    List<Spell> availableSpells = enemy.GetAvailableSpells();
		    if (availableSpells.Count == 0)
		    {
			    gameChat.text=enemy.name+" uses basic attack";
			    player.TakeDamage(enemy.Stats.Strength + 0.2 * enemy.Stats.Dexterity);
		    }
		    else
		    {
			    int wichSpell = Random.Range(0, availableSpells.Count);
			    Spell spell = availableSpells[wichSpell];
			    
			    double enemyWisdom = enemy.Stats.Wisdom;
			    double enemyStrength = enemy.Stats.Strength;
			    
			    gameChat.text=enemy.name+" uses "+spell.Name;
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
	    gameChat.text= battleState==BattleState.Win?"You won!":"You lost!";
	    yield return new WaitForSeconds(1f);
	    if (battleState == BattleState.Win)
	    {
		    gameChat.text = "You gained " + enemy.difficulty+" XP";
		    player.AddExperience(enemy.difficulty);
		    yield return new WaitForSeconds(1f);
	    }
	    SceneManager.LoadScene("Map");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
