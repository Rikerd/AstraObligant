using System.Collections;
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
    public List<GameObject> temporarySpawners;
    public List<GameObject> currentBosses;
    public List<Enemy> currentBossScripts;

    public GameObject blockerSpawners;

    public float setScoreMultiplierTimer = 7f;

    public Text scoreMultiplierText;

    private int scoreMultiplier = 1;
    private float scoreMultiplierTimer;
    private int baseMultiplier = 1;

    public static GameManager gm;

    private GameObject levelPrompt;
    private Text levelPromptText;

    private GameObject damageUpPrompt;
    private GameObject tripleShotPrompt;
    private GameObject scoreUpPrompt;
    private GameObject healthUpPrompt;

    private float roundTimer;
    private float breakTimer;

    private GameState currentState;

    private bool coroutineStarted;

    private ShipHealth playerHp;

    private bool dropped;
    private BossDropSystem bossDropSystem;

    private TransitionController transitionController;

    private void Awake()
    {
        gm = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Setup;

        roundTimer = setRoundTimer;

        breakTimer = 0;

        levelPrompt = GameObject.Find("Level Prompt");
        levelPromptText = levelPrompt.GetComponent<Text>();
        levelPrompt.SetActive(false);

        damageUpPrompt = GameObject.Find("Damage Up Prompt");
        damageUpPrompt.SetActive(false);

        tripleShotPrompt = GameObject.Find("Triple Shot Prompt");
        tripleShotPrompt.SetActive(false);

        scoreUpPrompt = GameObject.Find("Score Up Prompt");
        scoreUpPrompt.SetActive(false);

        healthUpPrompt = GameObject.Find("Health Up Prompt");
        healthUpPrompt.SetActive(false);

        coroutineStarted = false;

        scoreMultiplier = 1;

        scoreMultiplierText.text = scoreMultiplier.ToString() + "x";

        baseMultiplier = 1;

        playerHp = GameObject.Find("Ship").GetComponentInChildren<ShipHealth>();

        dropped = false;
        bossDropSystem = GetComponent<BossDropSystem>();

        transitionController = Camera.main.GetComponent<TransitionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHp.isPlayerDead())
        {
            return;
        }

        if (scoreMultiplierTimer > 0f)
        {
            scoreMultiplierTimer -= Time.deltaTime;

            if (scoreMultiplierTimer <= 0f)
            {
                scoreMultiplier = baseMultiplier;
                scoreMultiplierText.text = scoreMultiplier.ToString() + "x";
            }
        }

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

            foreach (GameObject boss in currentBosses)
            {
                Destroy(boss.gameObject);
            }

            clearBasicEnemySpawners();

            clearBosses();

            clearField();

            determineDrop();

            breakTimer = setBreakTimer;

            blockerSpawners.SetActive(false);

            currentState = GameState.Setup;
        }
    }

    public void clearBasicEnemySpawners()
    {
        foreach (GameObject currentEnemySpawner in currentEnemySpawners)
        {
            currentEnemySpawner.SetActive(false);
        }

        foreach (GameObject temporarySpawner in temporarySpawners)
        {
            Destroy(temporarySpawner);
        }

        currentEnemySpawners.Clear();
        temporarySpawners.Clear();
    }

    public void clearBosses()
    {
        foreach (GameObject boss in currentBosses)
        {
            Destroy(boss.gameObject);
        }

        currentBosses.Clear();
        currentBossScripts.Clear();
    }

    public void enemySpawnerPicker()
    {
        // Disable and clears all previous enemies spawners
        clearBasicEnemySpawners();

        if (currentLevel <= 4)
        {
            for (int i = 0; i < currentLevel; i++)
            {
                currentEnemySpawners.Add(normalEnemySpawners[i]);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                currentEnemySpawners.Add(normalEnemySpawners[i]);
            }

            int additionalSpawners = currentLevel / 5;

            for (int j = 0; j < additionalSpawners; j++)
            {
                GameObject spawner = normalEnemySpawners[Random.Range(0, 4)];

                temporarySpawners.Add(Instantiate(spawner));
            }
        }

        foreach (GameObject currentEnemySpawner in currentEnemySpawners)
        {
            currentEnemySpawner.SetActive(true);
        }

        foreach (GameObject temporarySpawner in temporarySpawners)
        {
            temporarySpawner.SetActive(true);
        }
    }

    public void bossPicker()
    {
        // Disable and clears all previous enemies spawners
        clearBasicEnemySpawners();

        currentEnemySpawners.Add(normalEnemySpawners[0]);

        if (currentLevel == 1)
        {
            return;
        }
        else if (currentLevel == 2)
        {
            GameObject boss = Instantiate(bossEnemies[0]);

            currentBosses.Add(boss);
            currentBossScripts.Add(boss.GetComponent<Enemy>());
        }
        else if (currentLevel == 3)
        {
            GameObject boss = Instantiate(bossEnemies[1]);

            currentBosses.Add(boss);
            currentBossScripts.Add(boss.GetComponent<Enemy>());
        }
        else if (currentLevel == 4)
        {
            GameObject boss = Instantiate(bossEnemies[2]);

            currentBosses.Add(boss);
            currentBossScripts.Add(boss.GetComponent<Enemy>());
        }
        else
        {
            int numOfSpawners = currentLevel / 5;

            for (int i = 0; i < numOfSpawners; i++)
            {
                int spawner = Random.Range(0, bossEnemies.Count);

                GameObject boss = Instantiate(bossEnemies[spawner]);
                
                currentBosses.Add(boss);
                currentBossScripts.Add(boss.GetComponent<Enemy>());
            }
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
        blockerSpawners.SetActive(true);

        coroutineStarted = false;
    }

    public void clearField()
    {
        transitionController.startFlash();

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

    public void increaseBaseMultiplier(int scoreMultiply)
    {
        scoreMultiplier *= scoreMultiply;
        baseMultiplier *= scoreMultiply;
        scoreMultiplierText.text = scoreMultiplier.ToString() + "x";
    }

    public void tempIncreaseMultiplier(int scoreMultiply)
    {
        scoreMultiplier *= scoreMultiply;

        scoreMultiplierTimer = setScoreMultiplierTimer;

        scoreMultiplierText.text = scoreMultiplier.ToString() + "x";
    }

    public int getMultiplier()
    {
        return scoreMultiplier;
    }

    public float getCurrentMultiplierTimer()
    {
        return scoreMultiplierTimer;
    }

    public void determineDrop()
    {
        if (currentLevel == 1)
        {
            return;
        }

        if (currentLevel % 5 == 0 && !dropped)
        {
            bossDropSystem.ChooseDrop();
            bossDropSystem.Drop(Vector3.zero);
        } 
        else if (!dropped)
        {
            int rand = Random.Range(0, 100);

            if (rand < 10)
            {
                dropped = true;

                bossDropSystem.ChooseDrop();
                bossDropSystem.Drop(Vector3.zero);
            }
        }
        else
        {
            bossDropSystem.DefaultDrop(Vector3.zero);
        }
        
    }

    public void StartPrompt(string promptType)
    {
        if (levelPrompt.activeSelf)
        {
            return;
        }

        if (promptType == "Damage")
        {
            StartCoroutine(ShowPrompt(damageUpPrompt));
        }
        else if (promptType == "Health")
        {
            StartCoroutine(ShowPrompt(healthUpPrompt));
        }
        else if (promptType == "Score")
        {
            StartCoroutine(ShowPrompt(scoreUpPrompt));
        }
        else if (promptType == "Triple Shot")
        {
            StartCoroutine(ShowPrompt(tripleShotPrompt));
        }
    }

    IEnumerator ShowPrompt(GameObject prompt)
    {
        prompt.SetActive(true);

        yield return new WaitForSeconds(1f);

        prompt.SetActive(false);
    }

    public int getEnemyHpMultiplier()
    {
        int multiplier = (currentLevel / 5) - 2;

        if (multiplier < 0)
        {
            return 0;
        }
        else
        {
            return multiplier;
        }
    }
}
