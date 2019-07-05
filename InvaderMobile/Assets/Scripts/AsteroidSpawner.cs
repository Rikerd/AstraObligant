using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroid;

    public float setMinSpawnTimer = 3f;
    public float setMaxSpawnTimer = 5f;


    private Transform playerTransform;
    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Random.Range(setMaxSpawnTimer, setMaxSpawnTimer);

        playerTransform = GameObject.Find("TestShip").transform;
    }

    // Update is called once per frame
    void Update()
    {

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            Instantiate(asteroid, new Vector2(playerTransform.position.x, transform.position.y) , Quaternion.identity);
            spawnTimer = Random.Range(setMaxSpawnTimer, setMaxSpawnTimer);
        }
    }
}
