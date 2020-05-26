using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass

{
    public static int[] carrySpells;
    public static int Score;
    
    public static void SetCarrySpells(int[] givenSpells)
    {
        carrySpells = new int[2];
        carrySpells[0] = givenSpells[0];
        carrySpells[1] = givenSpells[1];
        
    }  

    public static int[] GetCarrySpells()
    {
        return carrySpells;
    }

    public static void SetScore(int score)
    {
        Score = score;
    }

    public static int GetScore()
    {
        return Score;
    }
}
