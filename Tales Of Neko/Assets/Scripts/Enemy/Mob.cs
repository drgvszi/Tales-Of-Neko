using System;
using Tales_of_Neko;
using UnityEngine;
using UnityEngine.Serialization;

public class Mob: Character
{
    [FormerlySerializedAs("Difficulty")] public float difficulty;
    
    public Mob(string name, float difficulty, Stats stats) : base(name)
    {
        this.difficulty = difficulty;
        Stats = stats;
    }
    public override string ToString()
    {
        return base.ToString();
    }
    
    

}