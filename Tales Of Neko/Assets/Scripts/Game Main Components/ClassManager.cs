using System.Collections;
using System.Collections.Generic;
using Tales_of_Neko;
using UnityEditor.Animations;
using UnityEngine;

public class ClassManager : MonoBehaviour
{
    public GameObject PlayerCamera;
    public AnimatorController mageController;
    public AnimatorController warriorController;
    public AnimatorController rogueController;
    public AnimatorController bastedController;

    private bool first = true;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCamera.GetComponent<Animator>().runtimeAnimatorController = bastedController;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.player.deaths == 1&&first)
        {
            setAnimator(GameManager.Instance.player.Class);
            first = false;
        }
        
    }

    public void setAnimator(CharacterClass playerClass)
    {
        if (playerClass == CharacterClass.Mage)
        {
            PlayerCamera.GetComponent<Animator>().runtimeAnimatorController = mageController;
        }
        else if (playerClass == CharacterClass.Warrior)
        {
            PlayerCamera.GetComponent<Animator>().runtimeAnimatorController = warriorController;
        }
        else if(playerClass == CharacterClass.Rogue)
        {
            PlayerCamera.GetComponent<Animator>().runtimeAnimatorController = rogueController;
        }
    }
}
