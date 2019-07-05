using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroid;

    public float setSpawnTimer = 3f;

    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = setSpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            Instantiate(asteroid, transform.position, Quaternion.identity);
            spawnTimer = setSpawnTimer;
        }
    }
}
