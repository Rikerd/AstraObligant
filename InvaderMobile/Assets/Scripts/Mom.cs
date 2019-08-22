using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mom : Enemy
{
    [Header("Spawn Movement Variables")]
    public float initialMovementTimer = 1f;
    public float fowardMovement = 2f;

    [Header("Spawning Variables")]
    public GameObject babyShip;
    public float setBabySpawnTimer = 2f;

    private float babySpawnTimer;

    private Rigidbody2D rb2d;

    private bool initialMovement;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        babySpawnTimer = setBabySpawnTimer;

        rb2d = GetComponent<Rigidbody2D>();

        initialMovement = true;
    }

    private void Update()
    {
        if (!isDead)
        {
            if (initialMovement)
            {
                initialMovementTimer -= Time.deltaTime;

                if (initialMovementTimer <= 0f)
                {
                    initialMovement = false;
                }
            }
            else
            {
                if (babySpawnTimer > 0f)
                {
                    babySpawnTimer -= Time.deltaTime;
                }
                else
                {
                    Spawn();
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
            if (initialMovement)
            {
                rb2d.MovePosition(rb2d.position - new Vector2(0f, fowardMovement) * Time.fixedDeltaTime);
            }
        }
    }

    private void Spawn()
    {
        Instantiate(babyShip, transform.position + new Vector3(0f, -0.2f, 0f), Quaternion.identity);

        babySpawnTimer = setBabySpawnTimer; 
    }
}
