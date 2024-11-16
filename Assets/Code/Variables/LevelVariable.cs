using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelVariable", menuName = "Scriptable Objects/LevelVariable")]
public class LevelVariable : ScriptableObject
{
    #if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
    #endif
    public List<int> target;
    public int currentLevel;
    public int currentScore;
    public int currentTarget => target[currentLevel-1];
    public bool isWin => currentScore == currentTarget;
    public bool scored()
    {
        currentScore++;
        return currentScore == currentTarget;
    }
    public void Init()
    {
        currentLevel = 1;
        currentScore = 0;
    }
    public void nextLevel()
    {
        currentLevel++;
        currentScore = 0;
    }
}
