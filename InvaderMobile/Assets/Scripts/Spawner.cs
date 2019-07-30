using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum SpawnerPosition
    {
        Top,
        Left,
        Right
    }

    public GameObject spawnObject;
    
    public bool spawnOnPlayer = false;

    [Header("Spawn Timer Variation")]
    public float setMinSpawnTimer = 3f;
    public float setMaxSpawnTimer = 5f;

    [Header("Spawn Range")]
    public Transform minPosition;
    public Transform maxPosition;

    private float spawnRangeX;
    private float spawnRangeY;
    private Transform playerTransform;
    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Random.Range(setMaxSpawnTimer, setMaxSpawnTimer);

        playerTransform = GameObject.Find("TestShip").transform;

        if (!spawnOnPlayer)
        {
            spawnRangeX = Random.Range(minPosition.position.x, maxPosition.position.x);
        }

        spawnRangeY = Random.Range(minPosition.position.y, maxPosition.position.y);

    }

    // Update is called once per frame
    void Update()
    {

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            if (spawnOnPlayer)
                Instantiate(spawnObject, new Vector2(playerTransform.position.x, spawnRangeY), Quaternion.identity);
            else
                Instantiate(spawnObject, new Vector2(spawnRangeX, spawnRangeY), Quaternion.identity);

            spawnTimer = Random.Range(setMinSpawnTimer, setMaxSpawnTimer);
        }
    }
}
