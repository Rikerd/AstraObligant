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

    public float setMinSpawnTimer = 3f;
    public float setMaxSpawnTimer = 5f;

    public bool spawnOnPlayer = false;

    private float spawnRange;
    private Transform playerTransform;
    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Random.Range(setMaxSpawnTimer, setMaxSpawnTimer);

        playerTransform = GameObject.Find("TestShip").transform;

        if (!spawnOnPlayer)
        {
            spawnRange = Random.Range(transform.position.x - 2.5f, transform.position.x + 2.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            if (spawnOnPlayer)
                Instantiate(spawnObject, new Vector2(playerTransform.position.x, transform.position.y), Quaternion.identity);
            else
                Instantiate(spawnObject, new Vector2(spawnRange, transform.position.y), Quaternion.identity);

            spawnTimer = Random.Range(setMinSpawnTimer, setMaxSpawnTimer);
        }
    }
}
