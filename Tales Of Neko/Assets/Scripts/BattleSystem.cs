using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Tales_of_Neko;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum BattleState{Start,PlayerTurn,EnemyTurn,Win,Lose,UsedAbility}
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
    public Text panelText;
    public Image panelTimer;

    [FormerlySerializedAs("SpellsButtons")] public List<GameObject> spellsButtons;
    
    void Start()
    {
        battleState = BattleState.Start;
        StartCoroutine(SetupBattle());
        gameChat.text = "Battle start!";
    }

    public void Run()
    {
	    player.GetRawStats();
	    if (player.CanEscape(enemy))
	    {
		    gameChat.text = "You escaped!";
	    }
	    else
	    {
		    gameChat.text = "Can't escape!";
	    }
	    new WaitForSeconds(1f);
	    SceneManager.LoadScene("Map");
    }
    IEnumerator SetupBattle()
    {
	    player = GameManager.Instance.player;
	    enemy = GameManager.Instance.enemies[GameManager.Instance.enemyAttacked];

	    playerGameObject = Resources.Load<GameObject>( "Player\\Player");
	    enemyGameObject = Resources.Load<GameObject>( "Enemies\\"+enemy.Name);
	    
	    
	    playerGameObject.GetComponent<Renderer>().sortingOrder = 1;
	    enemyGameObject.GetComponent<Renderer>().sortingOrder = 1;
        Instantiate(playerGameObject, playerStation);
        Instantiate(enemyGameObject, enemyStation);

        yield return new WaitForSeconds(1f);

    }

    void StartFight()
    {
	    if (player.GetComplessiveStats().Dexterity > enemy.Stats.Dexterity)
	    { 
		    battleState = BattleState.PlayerTurn;
		    gameChat.text = "You are faster than your enemy, it is your turn!";
		    
	    }
	    else
	    {
		    battleState = BattleState.EnemyTurn;
		    gameChat.text = "The enemy is faster than you, it is his turn!";
		    StartCoroutine(EnemyTurn(true));
	    }
    }

    IEnumerator PlayerTurn()
    {
	    gameChat.text = "It is your turn";
	    yield return new WaitForSeconds(0);
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
		    StartCoroutine(UseSpell(spellSlot.Spell));
	    }
    }
    IEnumerator UseSpell(Spell spell)
    { 
	    if (battleState != BattleState.PlayerTurn) yield break;
	    bool comboSuceded=false;

	    List<KeyCode> pressedKeys = new List<KeyCode>();

	    panelText.text = "Press the combo keys!";
	    yield return new WaitForSeconds(0.6f);
	    panelTimer.enabled = !panelTimer.enabled;
	    
	    
	    float startTime = Time.time;
	    int i = 0;
	    while (pressedKeys.Count < spell.Combo.Count && Time.time - startTime <= spell.ComboTimer) {
		    yield return null;
		    panelTimer.fillAmount = (Time.time - startTime) / spell.ComboTimer;
		    foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
		    {
			    if (Input.GetKey(kcode))
			    {
				    panelText.text = kcode.ToString();
				    pressedKeys.Add(kcode);
					Input.ResetInputAxes();
			    }
		    }
	    }

	    panelText.text = "";
	    panelTimer.enabled = !panelTimer.enabled;
	    if (pressedKeys.SequenceEqual(spell.Combo)) {
		    comboSuceded = true;
	    }
	    
	    
	    if (comboSuceded) {
		    StartCoroutine(ManaAttack(spell));
	    }
	    else
	    {
		    gameChat.text = spell.Name+" failed!";
	    
		    yield return new WaitForSeconds(1f);
		    
		    battleState= BattleState.EnemyTurn;
			    StartCoroutine(EnemyTurn(false));
	    }
	    

    }
    public void UseBasicAttack(double addedAttack)
    {
	    if (battleState != BattleState.PlayerTurn)
		    return;
	    StartCoroutine(BasicAttack(addedAttack));
	    
    }

    IEnumerator ManaAttack(Spell spell)
    {
	    battleState = BattleState.UsedAbility;
	    if (player.CanUse(spell))
	    {
		    double playerWisdom = player.GetComplessiveStats().Wisdom ;
		    double playerStrength = player.GetComplessiveStats().Strength;

		    player.UseMana(spell.ManaUsage);
		    enemy.TakeDamage(playerWisdom * 0.8 + playerStrength * 0.3 + spell.AttackDamage);
		    
		    bool isDead = !enemy.IsAlive();
		    
		    StartCoroutine(UpdatePlayerHud());
		    StartCoroutine(UpdateEnemyHud());

		    gameChat.text = "GO: " + spell.Name+"!!";
	    
		    yield return new WaitForSeconds(1f);
	    
		    if(isDead)
		    {
			    battleState = BattleState.Win;
			    StartCoroutine(EndBattle());
		    } else
		    {
			    battleState= BattleState.EnemyTurn;
			    StartCoroutine(EnemyTurn(false));
		    }
		    
	    }
	   
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator BasicAttack(double addedAttack)
    {
	    battleState = BattleState.UsedAbility;
	    double playerDexterity = player.GetComplessiveStats().Dexterity;
	    double playerStrength = player.GetComplessiveStats().Strength;
	    
	    enemy.TakeDamage(playerStrength + 0.2 * playerDexterity +addedAttack);
	    
	    bool isDead = !enemy.IsAlive();
	    
	    StartCoroutine(UpdatePlayerHud());
	    StartCoroutine(UpdateEnemyHud());

	    gameChat.text = "ATTACK!";
	    yield return new WaitForSeconds(1f);
	    
	    if(isDead)
	    {
		    battleState = BattleState.Win;
		    StartCoroutine(EndBattle());
	    } else
	    {
		    battleState= BattleState.EnemyTurn;
		    StartCoroutine(EnemyTurn(false));
	    }
	    
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator EnemyTurn(bool willWait)
    {
	    Color oldColor = new Color(1,1,1);
	    int spellNumber = player.GetEquippedSpells().Count;
	    for (int i = 0; i < spellNumber; i++)
	    {
		    oldColor = spellsButtons[i].GetComponent<Image>().color;
		    spellsButtons[i].GetComponent<Image>().color=new Color(0.6f,0.6f,0.6f);
	    }
	    
	    if(willWait)
			yield return new WaitForSeconds(1f);
	    gameChat.text="It is the enemy turn";

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
	    
	    StartCoroutine(UpdatePlayerHud());
	    StartCoroutine(UpdateEnemyHud());
	    
	    yield return new WaitForSeconds(1f);
	    
	    for (int i = 0; i < spellNumber; i++)
	    {
		    spellsButtons[i].GetComponent<Image>().color=oldColor;
	    }
	    
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

    IEnumerator UpdateEnemyHud()
    {
	    playerHud.Set(player);
	    yield return new WaitForSeconds(0.7f);
	    playerHud.SetTransparent(player);
    }

    IEnumerator UpdatePlayerHud()
    {
	    enemyHud.Set(enemy);
	    yield return new WaitForSeconds(0.7f);
	    enemyHud.SetTransparent(enemy);
    }

    IEnumerator EndBattle()
    {
	    gameChat.text= battleState==BattleState.Win?"You won!":"You lost!";
	    yield return new WaitForSeconds(1f);
	    if (battleState == BattleState.Win)
	    {
		    gameChat.text = "You gained " + enemy.difficulty+" XP";
		    player.AddExperience(enemy.difficulty);
		    
		    StartCoroutine(UpdatePlayerHud());
		    
		    yield return new WaitForSeconds(1f);
	    }
	    SceneManager.LoadScene("Map");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
