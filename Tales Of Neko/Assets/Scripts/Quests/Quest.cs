using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tales_of_Neko
{
    [Serializable]
    [CreateAssetMenu(fileName ="New Quest",menuName = "Quest")]
    public class Quest:ScriptableObject
    {
        public string Name;
        public string Summary;
        public List<Dialogue> Story;
        [FormerlySerializedAs("Reward")] public float MoneyReward;
        public float ExperienceReward;
        
        public QuestGoal QuestGoal;


        public static void  SaveQuest(string where,Quest quest)
        {
            using (XmlWriter writer = XmlWriter.Create(where))  
            {  
                writer.WriteStartDocument();  
                writer.WriteStartElement("quest");
                writer.WriteElementString("name",quest.Name);
                writer.WriteElementString("summary",quest.Summary);
                writer.WriteElementString("money_reward",quest.MoneyReward.ToString());
                writer.WriteElementString("experience_reward",quest.ExperienceReward.ToString());
                writer.WriteStartElement("story");
                foreach (Dialogue dialog in quest.Story)
                {
                    writer.WriteElementString(dialog.Who,dialog.What);
                }
                writer.WriteEndElement();  
                writer.WriteEndElement();  
                writer.WriteEndDocument();  
            }  
            
        }

        public static Quest LoadQuest(string from)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(from);
            Quest quest = new Quest();

            XmlNode summaryNode = doc.DocumentElement.SelectSingleNode("summary");
            XmlNode nameNode = doc.DocumentElement.SelectSingleNode("name");
            XmlNode moneyRewardNode = doc.DocumentElement.SelectSingleNode("money_reward");
            XmlNode expRewardNode = doc.DocumentElement.SelectSingleNode("money_reward");
            XmlNode storyNode = doc.DocumentElement.SelectSingleNode("story");

            List<Dialogue> story = new List<Dialogue>();

            quest.Name = nameNode.InnerText;
            quest.Summary = summaryNode.InnerText;
            quest.MoneyReward = float.Parse(moneyRewardNode.InnerText);
            quest.ExperienceReward = float.Parse(expRewardNode.InnerText);

            foreach (XmlNode node in storyNode.ChildNodes)
            {
                Dialogue dialogue = new Dialogue(node.Attributes["me"] != null ? "me" : "npc", 
                    node.InnerText);
                story.Add(dialogue);

            }

            quest.Story = story;
            return quest;

        }
    }
}