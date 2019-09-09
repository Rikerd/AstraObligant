using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TopScoreTracker
{
    public List<int> listOfScores;

    public TopScoreTracker (HighScoreTracker scoreTracker)
    {
        listOfScores = scoreTracker.getHighScoreList();
    }
}
