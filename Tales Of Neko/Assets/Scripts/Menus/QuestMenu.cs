using System.Collections;
using System.Collections.Generic;
using Tales_of_Neko;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenu : MonoBehaviour
{
    public GameObject ItemPrefab;
     public ScrollRect ScrollView;
     public GameObject Content;
     // Start is called before the first frame update


     public Text QuestText;
     public GameObject StatusPanel;

     void Start()
     {
       
         foreach (Transform child in Content.transform)
         {
             Destroy(child.gameObject);

         }


         List<Quest> quests = GameManager.Instance.player.Quests;

         ScrollView.verticalNormalizedPosition = 1;
         for (int i = 0; i < quests.Count; i++)
         {
             GameObject spawnedItem = Instantiate(ItemPrefab);
             Button button = spawnedItem.GetComponent<Button>();
             int index = i;
             if (button != null)
             {
                 button.onClick.AddListener(() => QuestListClick(quests[index]));
             }

             spawnedItem.transform.SetParent(Content.transform, false);

             ItemDetails<Quest> itemDetails = spawnedItem.GetComponentInChildren<ItemDetails<Quest>>();
             itemDetails.Text.text = quests[i].Name;
             itemDetails.Class = quests[i];
         }
     }
     

     private void QuestListClick(Quest quest)
     {
         bool allDone = true;
         QuestText.text = quest.Summary+"\n\n";
         foreach (Goal goal in quest.QuestGoal.Goals)
         {
             QuestText.text += goal.Type.ToString() +" "+goal.What+" "
                               +goal.currentAmount+"/"+goal.requiredAmount+"\n";
             allDone = allDone && goal.isDone();
         }

         if (allDone)
         {
             StatusPanel.SetActive(true);
             Button button = StatusPanel.GetComponentInChildren<Button>();
             if(button != null) {
                 button.onClick.AddListener(() =>QuestDone(quest));
             }
             
         }
         else
         {
             StatusPanel.SetActive(false);
         }

         QuestText.text += "Reward: "+quest.MoneyReward + "\t Exp :" + quest.ExperienceReward;
     }

     private void QuestDone(Quest quest)
     {
         GameManager.Instance.player.Experience += quest.ExperienceReward;
         GameManager.Instance.player.Money += quest.MoneyReward;
         GameManager.Instance.player.Quests.Remove(quest);

         QuestText.text = "";
         StatusPanel.SetActive(false);
         Start();
     }

     // Update is called once per frame
     void Update()
     {
         if (GameManager.Instance.player.questUpdate)
         {
             Start();
             GameManager.Instance.player.questUpdate = false;
         }

     }
}
