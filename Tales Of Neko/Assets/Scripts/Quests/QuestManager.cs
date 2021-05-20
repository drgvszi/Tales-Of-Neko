using System.Collections;
using System.Collections.Generic;
using Tales_of_Neko;
using UnityEngine;

public class QuestManager
{
    // Update is called once per frame
    
    public bool EnemyKilled(string enemy)
    {
        foreach (Quest quest in GameManager.Instance.player.Quests)
        {
            if (!quest.QuestGoal.isDone())
            {
                foreach (Goal goal in quest.QuestGoal.Goals)
                {
                    if (!goal.isDone())
                    {
                        if (goal.EnemyKilled(enemy))
                        {
                            return true;
                        }
                    }
                }
            }
            
        }

        return false;
    }
    public bool Gathered(string what)
    {
        foreach (Quest quest in GameManager.Instance.player.Quests)
        {
            if (!quest.QuestGoal.isDone())
            {
                foreach (Goal goal in quest.QuestGoal.Goals)
                {
                    if (!goal.isDone())
                    {
                        if (goal.Gathered(what))
                        {
                            return true;
                        }
                    }
                }
            }
            
        }

        return false;
    }
    public bool Used(string what)
    {
        foreach (Quest quest in GameManager.Instance.player.Quests)
        {
            if (!quest.QuestGoal.isDone())
            {
                foreach (Goal goal in quest.QuestGoal.Goals)
                {
                    if (!goal.isDone())
                    {
                        if (goal.Used(what))
                        {
                            return true;
                        }
                    }
                }
            }
            
        }

        return false;
    }
    public bool Moved(string how)
    {
        foreach (Quest quest in GameManager.Instance.player.Quests)
        {
            if (!quest.QuestGoal.isDone())
            {
                foreach (Goal goal in quest.QuestGoal.Goals)
                {
                    if (!goal.isDone())
                    {
                        if (goal.Move(how))
                        {
                            return true;
                        }
                    }
                }
            }
            
        }

        return false;
    }
    public bool Talked(string whom)
    {
        foreach (Quest quest in GameManager.Instance.player.Quests)
        {
            if (!quest.QuestGoal.isDone())
            {
                foreach (Goal goal in quest.QuestGoal.Goals)
                {
                    if (!goal.isDone())
                    {
                        if (goal.Talk(whom))
                        {
                            return true;
                        }
                    }
                }
            }
            
        }

        return false;
    }
}
