using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public LevelVariable levelVariable;
    public TMP_Text levelText;
    public TMP_Text timeText;
    public TMP_Text scoredText;
    public UnityEvent GameOver;
    public UnityEvent NextLevel;
    private float time;
    private bool isPlaying;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }
    public void OnLevelStart()
    {
       isPlaying = true;
        time = 90;
    }
    public void Update()
    {
        if (isPlaying)
        {
            time -= Time.deltaTime;
            if (time <= 0) OnGameOver();
            else timeText.text = "Time: "+time.ToString("F2");

        }
    }
    public void Init()
    {
        levelVariable.Init();
        isPlaying =false;
        timeText.text = "Time: 90s";
        levelText.text ="Level: "+ levelVariable.currentLevel.ToString();
        scoredText.text = "Score: 0/" + levelVariable.currentTarget.ToString();
    }
    private void OnGameOver()
    {
        Init();
        GameOver.Invoke();
    }
    public void OnScored()
    {
        levelVariable.scored();
        if(levelVariable.isWin)
        {
            levelVariable.nextLevel();
            NextLevel.Invoke();
            isPlaying = false;
            timeText.text = "Time: 90s";
            levelText.text  = "Level: " + levelVariable.currentLevel.ToString();
            scoredText.text = "Score: 0/" + levelVariable.currentTarget.ToString();
        }
        else
        {
            scoredText.text = "Score: "+levelVariable.currentScore.ToString()+"/" + levelVariable.currentTarget.ToString();
        }
    }

}