﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Round,
        Boss,
        Setup
    }

    public int currentLevel = 0;

    public float setRoundTimer = 300f;

    public float setBreakTimer = 10f;

    public List<GameObject> normalEnemySpawners;

    public List<GameObject> bossEnemies;

    public List<GameObject> currentEnemySpawners;
    public List<GameObject> currentBosses;
    public List<Enemy> currentBossScripts;

    private GameObject levelPrompt;
    private Text levelPromptText;

    private float roundTimer;
    private float breakTimer;

    private GameState currentState;

    private bool coroutineStarted;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Setup;

        roundTimer = setRoundTimer;

        breakTimer = 0;

        levelPrompt = GameObject.Find("Level Prompt");
        levelPromptText = levelPrompt.GetComponent<Text>();
        levelPrompt.SetActive(false);

        coroutineStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.Setup)
        {
            breakTimer -= Time.deltaTime;

            if (breakTimer <= 0f && !coroutineStarted)
            {
                StartCoroutine(NextLevel());
            }
        }
        else if (currentState == GameState.Round)
        {
            roundTimer -= Time.deltaTime;

            if (roundTimer <= 0f)
            {
                clearField();
                currentState = GameState.Boss;
                bossPicker();
            }
        }
        else if (currentState == GameState.Boss)
        {
            foreach (Enemy boss in currentBossScripts)
            {
                if (!boss.checkDeath())
                {
                    return;
                }
            }

            clearField();
            currentState = GameState.Setup;
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
        currentBossScripts.Clear();
    }

    public void enemySpawnerPicker()
    {
        // Disable and clears all previous enemies spawners
        clearBasicEnemySpawners();

        if (currentLevel >= 1 && currentLevel <= 5)
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

    public void bossPicker()
    {
        // Disable and clears all previous enemies spawners
        clearBasicEnemySpawners();

        // Disable and clears all previous boss spawners
        clearBosses();

        if (currentLevel == 1)
        {
            currentEnemySpawners.Add(normalEnemySpawners[0]);

            GameObject newObject = Instantiate(bossEnemies[currentLevel - 1], bossEnemies[currentLevel - 1].transform.position, Quaternion.identity);

            currentBosses.Add(newObject);
            currentBossScripts.Add(newObject.GetComponent<Enemy>());
        }
        else if (currentLevel == 2)
        {
            currentEnemySpawners.Add(normalEnemySpawners[2]);

            GameObject newObject = Instantiate(bossEnemies[0], bossEnemies[0].transform.position, Quaternion.identity);

            currentBosses.Add(newObject);
            currentBossScripts.Add(newObject.GetComponent<Enemy>());
        }
        else if (currentLevel == 3 || currentLevel == 4)
        {
            currentEnemySpawners.Add(normalEnemySpawners[0]);

            GameObject newObject = Instantiate(bossEnemies[currentLevel - 2], bossEnemies[currentLevel - 2].transform.position, Quaternion.identity);

            currentBosses.Add(newObject);
            currentBossScripts.Add(newObject.GetComponent<Enemy>());
        }
        else
        {
            int rand = Random.Range(0, 100);

            if (rand < 50)
            {
                currentEnemySpawners.Add(normalEnemySpawners[0]);
            }
            else
            {
                currentEnemySpawners.Add(normalEnemySpawners[2]);
            }

            int spawner = Random.Range(0, bossEnemies.Count);

            currentEnemySpawners.Add(bossEnemies[spawner]);
        }

        foreach (GameObject currentEnemySpawner in currentEnemySpawners)
        {
            currentEnemySpawner.SetActive(true);
        }

        foreach (GameObject currentBoss in currentBosses)
        {
            currentBoss.SetActive(true);
        }
    }

    IEnumerator NextLevel()
    {
        // Do next level prompt
        // Set up new current spawner;
        coroutineStarted = true;
        currentLevel++;

        string result = "";
        int length = currentLevel.ToString().Length;

        for (int i = length; i < 2; i++)
        {
            result += "0";
        }

        result += currentLevel;

        levelPromptText.text = "LEVEL - " + result;
        levelPrompt.SetActive(true);

        yield return new WaitForSeconds(1f);

        levelPrompt.SetActive(false);


        roundTimer = setRoundTimer;
        currentState = GameState.Round;
        enemySpawnerPicker();

        coroutineStarted = false;
    }

    public void clearField()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] blockers = GameObject.FindGameObjectsWithTag("Blocker");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().killKey();
        }

        foreach(GameObject blocker in blockers)
        {
            blocker.GetComponent<Blocker>().killKey();
        }
    }
}