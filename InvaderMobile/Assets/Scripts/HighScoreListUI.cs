using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreListUI : MonoBehaviour
{
    public List<Text> highScoreTexts;

    public void Awake()
    {
        TopScoreTracker data = ScoreSaveSystem.LoadScoreList();

        if (data != null)
        {
            for (int i = 0; i < 5; i++)
            {
                string result = "";
                int length = data.listOfScores[i].ToString().Length;

                for (int j = length; j < 7; j++)
                {
                    result += "0";
                }

                result += data.listOfScores[i];

                highScoreTexts[i].text = (i + 1).ToString() + ". " + result;
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                highScoreTexts[i].text = (i + 1).ToString() + ". 000000";
            }
        }
    }
}
