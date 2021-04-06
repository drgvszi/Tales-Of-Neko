using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Tales_of_Neko
{
    [CreateAssetMenu(fileName ="New Quest",menuName = "Quest")]
    public class Quest:ScriptableObject
    {
        public string Name;
        public string Summary;
        public List<Dialogue> Story;
        public float Reward;


        public static void  SaveQuest(string where,Quest quest)
        {
            using (XmlWriter writer = XmlWriter.Create(where))  
            {  
                writer.WriteStartDocument();  
                writer.WriteStartElement("quest");
                writer.WriteElementString("name",quest.Name);
                writer.WriteElementString("summary",quest.Summary);
                writer.WriteElementString("reward",quest.Reward.ToString());
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
            XmlNode rewardNode = doc.DocumentElement.SelectSingleNode("reward");
            XmlNode storyNode = doc.DocumentElement.SelectSingleNode("story");

            List<Dialogue> story = new List<Dialogue>();

            quest.Name = nameNode.InnerText;
            quest.Summary = summaryNode.InnerText;
            quest.Reward = float.Parse(rewardNode.InnerText);

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