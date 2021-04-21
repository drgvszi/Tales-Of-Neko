using UnityEngine;

namespace Tales_of_Neko
{
    [System.Serializable]
    public class QuestGoal
    {
        public Goal[] Goals;
        public bool isDone()
        {
            foreach (Goal goal in Goals)
            {
                if (!goal.isDone())
                {
                    return false;
                }
            }

            return true;
        }
    }
    [System.Serializable]
    public class Goal
    {
        public TypeOfGoal Type;
        public string What;
        public float currentAmount;
        public float requiredAmount;

        public bool isDone()
        {
            return currentAmount >= requiredAmount;
        }

        public bool EnemyKilled(string monsterName)
        {
            if (What == monsterName && Type == TypeOfGoal.Kill)
            {
                currentAmount++;
                return true;
            }

            return false;
        }
        public bool Used(string what)
        {
            if (What == what && Type == TypeOfGoal.Use)
            {
                currentAmount++;
                return true;
            }

            return false;
        }
        public bool Gathered(string what)
        {
            if (What == what && Type == TypeOfGoal.Gather)
            {
                currentAmount++;
                return true;
            }

            return false;
        }
        public bool Move(string how)
        {
            if (What == how && Type == TypeOfGoal.Move)
            {
                currentAmount++;
                return true;
            }
            return false;
        }

        public bool Talk(string whom)
        {
            if (What == whom && TypeOfGoal.Talk == Type)
            {
                currentAmount++;
                return true;
            }
            return false;
        }
        
    }

    public enum TypeOfGoal
    {
        Kill,Gather,Move,Use,Talk
    }
}