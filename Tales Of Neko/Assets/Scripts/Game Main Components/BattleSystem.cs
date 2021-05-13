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
    public Image panelTimer;

    public GameObject GameOverGO;

    public Animator TargetedSpellPlayerAnimator;
    public Animator BlastSpellPlayerAnimator;
    
    public Animator TargetedSpellEnemyAnimator;
    public Animator BlastSpellEnemyAnimator;

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
    public Text spellCombo;
    public Text spellMnD;
	public bool ok;
	public GameObject hide;
	public GameObject unhide;
	public Button okButton;
    IEnumerator UseSpell(Spell spell)
    { 	
		spellMnD.text = "You'll use " + spell.Name + ", it will consume "+ spell.ManaUsage.ToString() +" mana "+ " and you'll deal " + spell.AttackDamage.ToString() +" damage.";
        spellCombo.text = "Combo: ";
        List<KeyCode> keyCodes = spell.Combo;
        foreach (KeyCode kcode in keyCodes)
        {
            switch (kcode)
            {
                case KeyCode.UpArrow:
                    spellCombo.text += "⇧ ";
                    break;
                case KeyCode.RightArrow:
                    spellCombo.text += "⇨ ";
                    break;
                case KeyCode.LeftArrow:
                    spellCombo.text += "⇦ ";
                    break;
                case KeyCode.DownArrow:
                    spellCombo.text += "⇩ ";
                    break;
                default:
                    spellCombo.text += kcode.ToString()+" ";
                    break;
        
        	}
		}
		

		var waitForButton = new WaitForUIButtons(okButton);
		yield return waitForButton.Reset();
		if (waitForButton.PressedButton == okButton)
		{
			
			hide.SetActive(false);
			unhide.SetActive(true);
			if (battleState != BattleState.PlayerTurn) yield break;
			bool comboSuceded=false;
			List<KeyCode> pressedKeys = new List<KeyCode>();
			yield return new WaitForSeconds(0.6f);
			panelTimer.enabled = !panelTimer.enabled;
			float startTime = Time.time;
			while (pressedKeys.Count < spell.Combo.Count && Time.time - startTime <= spell.ComboTimer) {
				yield return null;
				panelTimer.fillAmount = (Time.time - startTime) / spell.ComboTimer;
				foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
				{
					if (Input.GetKey(kcode))
					{
						pressedKeys.Add(kcode);
						Input.ResetInputAxes();
					}
				}
			}
			panelTimer.enabled = !panelTimer.enabled;
			if (pressedKeys.SequenceEqual(spell.Combo)) {
				comboSuceded = true;
			}
			
			spellCombo.text = "";
			if (comboSuceded)
			{
				GameManager.Instance.QuestManager.Used(spell.Name);
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
		else
		{
			hide.SetActive(true);
			unhide.SetActive(false);
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
		    
		    
		    gameChat.text = "GO: " + spell.Name+"!!";
		    Animator animator = spell.IsTarget == true? TargetedSpellPlayerAnimator : BlastSpellPlayerAnimator; 
		    animator.Play(spell.AnimationState);
		    yield return new WaitForSeconds(1f);
		    
		    
		    StartCoroutine(UpdatePlayerHud());
		    StartCoroutine(UpdateEnemyHud());

		    
	    
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
			    
			    Animator animator = spell.IsTarget == true? TargetedSpellEnemyAnimator : BlastSpellEnemyAnimator; 
			    animator.Play(spell.AnimationState);
			    yield return new WaitForSeconds(1f);
			    
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
		    GameManager.Instance.QuestManager.EnemyKilled(enemy.name);
		    gameChat.text = "You gained " + enemy.difficulty+" XP";
		    player.AddExperience(enemy.difficulty);
		    
		    StartCoroutine(UpdatePlayerHud());
		    
		    yield return new WaitForSeconds(1f);
		    SceneManager.LoadScene("Map");
	    }
	    else
	    {
		    GameOverGO.SetActive(true);
	    }
	    

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
