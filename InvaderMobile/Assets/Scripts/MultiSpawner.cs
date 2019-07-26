using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSpawner : MonoBehaviour
{
    public GameObject spawnObject;

    public int spawnAmount = 3;
    public float spawnTimeSeparation = 0.5f;

    public float setMinSpawnTimer = 6f;
    public float setMaxSpawnTimer = 10f;

    public Spawner.SpawnerPosition position;

    [Header("Spawn Range")]
    public Transform minPosition;
    public Transform maxPosition;

    private float spawnRangeX;
    private float spawnRangeY;
    private Transform playerTransform;
    private float spawnTimer;
    private bool spawning;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Random.Range(setMinSpawnTimer, setMaxSpawnTimer);

        playerTransform = GameObject.Find("TestShip").transform;

        spawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer <= 0f && !spawning)
        {
            spawnRangeX = Random.Range(minPosition.position.x, maxPosition.position.x);
            spawnRangeY = Random.Range(minPosition.position.y, maxPosition.position.y);

            spawning = true;

            StartCoroutine(Spawn());
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject newObject = Instantiate(spawnObject, new Vector2(spawnRangeX, spawnRangeY), Quaternion.identity);

            if (position == Spawner.SpawnerPosition.Left)
            {
                newObject.GetComponent<Blocker>().setMovementDirection(true);
            }
            else if (position == Spawner.SpawnerPosition.Right)
            {
                newObject.GetComponent<Blocker>().setMovementDirection(false);
            }

            yield return new WaitForSeconds(spawnTimeSeparation);
        }

        spawnTimer = Random.Range(setMinSpawnTimer, setMaxSpawnTimer);
        spawning = false;
    }
}
