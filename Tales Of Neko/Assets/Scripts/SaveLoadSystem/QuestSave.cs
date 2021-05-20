using System;
using System.Collections.Generic;
using Tales_of_Neko;
using UnityEngine;
using UnityEngine.Serialization;

namespace SaveLoadSystem
{
    [Serializable]
    public class QuestSave
    {
        public string Name;
        public string Summary;
        public List<Dialogue> Story;
        [FormerlySerializedAs("Reward")] public float MoneyReward;
        public float ExperienceReward;
        
        public QuestGoal QuestGoal;


        public static Quest Load(QuestSave save)
        {
            Quest quest = ScriptableObject.CreateInstance<Quest>();
            quest.Name = save.Name;
            quest.Summary = save.Summary;
            quest.Story = new List<Dialogue>(save.Story);
            quest.MoneyReward = save.MoneyReward;
            quest.ExperienceReward = save.ExperienceReward;
            quest.QuestGoal = save.QuestGoal;

            return quest;

        }

        public static QuestSave Save(Quest quest)
        {
            QuestSave questSave = new QuestSave();
            questSave.Name = quest.Name;
            questSave.Summary = quest.Summary;
            questSave.Story = new List<Dialogue>(quest.Story);
            questSave.MoneyReward = quest.MoneyReward;
            questSave.ExperienceReward = quest.ExperienceReward;
            questSave.QuestGoal = quest.QuestGoal;
            return questSave;
        }
    }
}