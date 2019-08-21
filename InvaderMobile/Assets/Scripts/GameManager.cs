using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Round,
        Boss,
        Setup
    }

    public int currentLevel = 1;

    public float setRoundTimer = 300f;

    public float setBreakTimer = 10f;

    public List<GameObject> normalEnemySpawners;

    public List<GameObject> bossEnemies;

    public List<GameObject> currentEnemySpawners;
    public List<GameObject> currentBosses;

    private float roundTimer;
    private float breakTimer;

    private GameState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Setup;

        roundTimer = setRoundTimer;

        breakTimer = setBreakTimer;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.Setup)
        {
            breakTimer -= Time.deltaTime;

            if (breakTimer <= 0f)
            {
                roundTimer = setRoundTimer;
                currentState = GameState.Round;
            }
        }
        else if (currentState == GameState.Round)
        {
            roundTimer -= Time.deltaTime;

            if (roundTimer <= 0f)
            {
                currentState = GameState.Boss;
            }
        }
        else if (currentState == GameState.Boss)
        {

        }
    }

    public void clearBasicEnemySpawners()
    {
        foreach (GameObject currentEnemySpawner in currentEnemySpawners)
        {
            currentEnemySpawner.SetActive(false);
        }

        currentEnemySpawners.Clear();
    }

    public void clearBosses()
    {
        foreach (GameObject currentBoss in currentBosses)
        {
            currentBoss.SetActive(false);
        }

        currentBosses.Clear();
    }

    public void enemySpawnerPicker()
    {
        // Disable and clears all previous enemies spawners
        clearBasicEnemySpawners();

        if (currentLevel <= 1 && currentLevel >= 5)
        {
            currentEnemySpawners.Add(normalEnemySpawners[0]);
            currentEnemySpawners.Add(normalEnemySpawners[currentLevel]);
        }
        else
        {
            int firstSpawner = Random.Range(0, normalEnemySpawners.Count);

            int secondSpawner;

            do
            {
                secondSpawner = Random.Range(0, normalEnemySpawners.Count);
            } while (firstSpawner == secondSpawner);

            currentEnemySpawners.Add(normalEnemySpawners[firstSpawner]);
            currentEnemySpawners.Add(normalEnemySpawners[secondSpawner]);
        }

        foreach (GameObject currentEnemySpawner in currentEnemySpawners)
        {
            currentEnemySpawner.SetActive(true);
        }
    }

    public void bossSpawnerPicker()
    {
        // Disable and clears all previous enemies spawners
        clearBasicEnemySpawners();

        // Adds Asteroids to queue
        currentEnemySpawners.Add(normalEnemySpawners[0]);

        // Disable and clears all previous boss spawners
        clearBosses();

        if (currentLevel <= 1 && currentLevel >= 5)
        {
            currentBosses.Add(bossEnemies[0]);
            currentBosses.Add(bossEnemies[currentLevel]);
        }
        else
        {
            int spawner = Random.Range(0, bossEnemies.Count);

            currentEnemySpawners.Add(bossEnemies[spawner]);
        }

        foreach (GameObject currentBoss in currentBosses)
        {
            currentBoss.SetActive(true);
        }
    }

    public void nextLevel()
    {
        // Do next level prompt
        // Set up new current spawner;

        currentLevel++;
    }
}
