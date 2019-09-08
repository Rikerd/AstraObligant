using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopScoreTracker
{
    public List<int> listOfScores;

    public TopScoreTracker (HighScoreTracker scoreTracker)
    {
        // CHECK IF HIGHER THAN LOWEST SCORE

        // REMOVE LOWEST SCORE

        listOfScores.Add(scoreTracker.highScore);

        // SORT LIST
    }
}
