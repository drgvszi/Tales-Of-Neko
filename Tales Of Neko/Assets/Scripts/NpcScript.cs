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
    public Rigidbody2D PlayerRigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
        PlayerRigidBody.constraints = RigidbodyConstraints2D.None;
        GameManager.Instance.player.Quests.Add(Quest);
        
        DialogueMenu.SetActive(false);
    }

    public void Decline()
    {
        PlayerRigidBody.constraints = RigidbodyConstraints2D.None;
        DialogueMenu.SetActive(false);
    }

    public IEnumerator Story()
    {
        PlayerRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        foreach (Dialogue dialogue in Quest.Story)
        {
            DialogueChat.text = dialogue.Who + " : " + dialogue.What;
            yield return new WaitForSeconds(2f);
        }
        
        AcceptButton.gameObject.SetActive(true);
        DeclineButton.gameObject.SetActive(true);
        
        
    }
}
