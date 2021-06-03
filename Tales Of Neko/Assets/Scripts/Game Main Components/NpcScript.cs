using System;
using System.Collections;
using System.Collections.Generic;
using Tales_of_Neko;
using UnityEngine;
using UnityEngine.UI;

public class NpcScript : MonoBehaviour
{
    public List<Quest> Quests;
    
    public GameObject DialogueMenu;
    public Button AcceptButton;
    public Button DeclineButton;
    
    public Text DialogueChat;
    public Text EnterChat;
    public PlayerMovement PlayerMovement;
    
    private List<KeyCode> keyCodes = new List<KeyCode>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        keyCodes.Add(KeyCode.KeypadEnter);
        keyCodes.Add(KeyCode.A);
        keyCodes.Add(KeyCode.Space);

        for (int index = 0; index < GameManager.Instance.toDelete; index++)
        {
            Quests.Remove(Quests[index]);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        DialogueMenu.SetActive(true);
        
        AcceptButton.gameObject.SetActive(false);
        DeclineButton.gameObject.SetActive(false);
        
        StartCoroutine(Story());
    }

    public void Accept()
    {
        PlayerMovement.canMove = true;
        foreach (var goal in Quests[0].QuestGoal.Goals)
        {
            goal.currentAmount = 0;
        }

        GameManager.Instance.player.Quests.Add(Quests[0]);
        Quests.Remove(Quests[0]);
        GameManager.Instance.toDelete += 1;
        GameManager.Instance.player.questUpdate = true;

        DialogueMenu.SetActive(false);
    }

    public void Decline()
    {
        PlayerMovement.canMove = true;
        DialogueMenu.SetActive(false);
    }

    public IEnumerator Story()
    {
        PlayerMovement.canMove = false;
        EnterChat.text = "Space to continue...";
        foreach (Dialogue dialogue in Quests[0].Story)
        {
            DialogueChat.text = dialogue.Who + " : " + dialogue.What;
            
            bool next = false;
            while (!next)
            {
                if (Input.GetKeyDown(KeyCode.A) || 
                    Input.GetKeyDown(KeyCode.Space) || 
                    Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    Console.Out.WriteLine("AICIII");
                    next = true;
                }
                yield return null;
            }
            Input.ResetInputAxes();
        }

        EnterChat.text = "";
        AcceptButton.gameObject.SetActive(true);
        DeclineButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(0f);
    }
}
