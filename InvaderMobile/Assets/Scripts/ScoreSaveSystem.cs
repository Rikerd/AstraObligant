using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class ScoreSaveSystem
{
    public static void SaveScoreList(HighScoreTracker scoreTracker)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/score.lol";

        FileStream stream = new FileStream(path, FileMode.Create);

        TopScoreTracker topScoreTracker = new TopScoreTracker(scoreTracker);

        formatter.Serialize(stream, topScoreTracker);
        stream.Close();
    }

    public static TopScoreTracker LoadScoreList()
    {
        string path = Application.persistentDataPath + "/score.lol";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            TopScoreTracker topScoreTracker = formatter.Deserialize(stream) as TopScoreTracker;

            stream.Close();

            return topScoreTracker;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);

            return null;
        }
    }
}
