using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTracker: MonoBehaviour
{
    public int highScore = 0;

    public static HighScoreTracker highScoreTracker;

    private Text highScoreText;

    public void Start()
    {
        highScoreTracker = this;

        highScoreText = GetComponent<Text>();
    }

    public void Update()
    {
        updateScoreText();
    }

    private void updateScoreText()
    {
        string result = "";
        int length = highScore.ToString().Length;

        for (int i = length; i < 7; i++)
        {
            result += "0";
        }

        result += highScore;
        highScoreText.text = result;
    }

    public void addScore(int score)
    {
        highScore += score;
    }
}
