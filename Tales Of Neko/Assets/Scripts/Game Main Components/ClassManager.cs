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
    // Start is called before the first frame update
    void Start()
    {
        setAnimator(GameManager.Instance.player.Class);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void setAnimator(CharacterClass playerClass)
    {
        Debug.Log("AAA");
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
