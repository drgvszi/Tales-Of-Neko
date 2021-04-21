using System;
using System.Collections;
using System.Collections.Generic;
using Tales_of_Neko;
using UnityEngine;
using UnityEngine.UI;

public class NpcScript : MonoBehaviour
{
    public Quest Quest;
    
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
        GameManager.Instance.player.Quests.Add(Quest);

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
        foreach (Dialogue dialogue in Quest.Story)
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
