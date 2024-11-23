using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelVariable", menuName = "Scriptable Objects/LevelVariable")]
public class LevelVariable : ScriptableObject
{
    #if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
    #endif

    public float currentMaxTime;
    public int currentScore;
    public int currentTarget;
    public bool isWin => currentScore == currentTarget;
    public bool scored()
    {
        currentScore++;
        return currentScore == currentTarget;
    }
    public void Init()
    {
        currentMaxTime = 60;
        currentScore = 0;
        currentTarget = 3;
    }
    public void nextLevel()
    {
        currentMaxTime+=15;
        currentScore = 0;
        currentTarget++;
    }
}
