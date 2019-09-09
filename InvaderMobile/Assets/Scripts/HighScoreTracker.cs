using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTracker: MonoBehaviour
{
    public int highScore = 0;

    public static HighScoreTracker highScoreTracker;

    public List<int> highScoreList;

    private Text highScoreText;

    public void Start()
    {
        highScoreTracker = this;

        highScoreText = GetComponent<Text>();

        TopScoreTracker topScoreTracker = ScoreSaveSystem.LoadScoreList();

        if (topScoreTracker != null)
        {
            highScoreList = topScoreTracker.listOfScores;
        } else
        {
            highScoreList = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                highScoreList.Add(0);
            }
        }
    }

    public void Update()
    {
        updateScoreText();
    }

    private void updateScoreText()
    {
        highScoreText.text = getScoreText();
    }

    public void addScore(int score)
    {
        highScore += score;
    }

    public void SaveScore()
    {
        if (highScore > highScoreList[highScoreList.Count - 1])
        {
            highScoreList.RemoveAt(highScoreList.Count - 1);
            highScoreList.Add(highScore);

            highScoreList.Sort();
            highScoreList.Reverse();

            ScoreSaveSystem.SaveScoreList(this);
        }
    }

    public string getScoreText()
    {
        string result = "";
        int length = highScore.ToString().Length;

        for (int i = length; i < 7; i++)
        {
            result += "0";
        }

        result += highScore;

        return result;
    }

    public List<int> getHighScoreList()
    {
        return highScoreList;
    }
}
