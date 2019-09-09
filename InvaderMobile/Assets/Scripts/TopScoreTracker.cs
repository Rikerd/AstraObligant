using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopScoreTracker
{
    public List<int> listOfScores;

    public TopScoreTracker (HighScoreTracker scoreTracker)
    {
        if (scoreTracker.highScore > listOfScores[listOfScores.Count - 1])
        {
            listOfScores.RemoveAt(listOfScores.Count - 1);
            listOfScores.Add(scoreTracker.highScore);

            listOfScores.Sort();
        }
    }
}
