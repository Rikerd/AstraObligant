using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    [Header("Speed Values")]
    public float patrolSpeed = 3f;
    public float bulletSpeed = 1f;

    [Header("Spawn Movement Variables")]
    public float minInitialMovementTimer = 0.8f;
    public float maxInitialMovementTimer = 1.2f;
    public float fowardMovement = 2f;

    [Header("Shooting Variables")]
    public GameObject bullet;
    public float setShootTimer = 2f;

    [Header("Edges")]
    public Transform leftEdge;
    public Transform rightEdge;

    [Header("Barrels")]
    public Transform[] barrels;

    private float shootTimer;

    private float initialMovementTimer;

    private Vector2 max;
    private Vector2 min;

    private Rigidbody2D rb2d;

    private Vector2 movement;

    private bool movingRight = true;

    private bool initialMovement;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        shootTimer = setShootTimer;

        initialMovementTimer = Random.Range(minInitialMovementTimer, maxInitialMovementTimer);

        int rand = Random.Range(0, 100);
        
        if (rand < 50)
        {
            movingRight = true;
        }
        else
        {
            movingRight = false;
        }

        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        rb2d = GetComponent<Rigidbody2D>();

        movement = new Vector2(patrolSpeed, 0);

        initialMovement = true;
    }

    public void Update()
    {
        if (!isDead)
        {
            if (!initialMovement)
            {
                if (shootTimer > 0f)
                {
                    shootTimer -= Time.deltaTime;
                }
                else
                {
                    Shoot();
                }
            }
            else
            {
                initialMovementTimer -= Time.deltaTime;

                if (initialMovementTimer <= 0f)
                {
                    initialMovement = false;
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
            else
            {
                // Patrol Right Movement
                if (moveRightCheck())
                {
                    rb2d.MovePosition(rb2d.position + movement * Time.fixedDeltaTime);
                }
                else
                {
                    movingRight = false;
                }

                // Patrol Left Movement
                if (moveLeftCheck())
                {
                    rb2d.MovePosition(rb2d.position - movement * Time.fixedDeltaTime);
                }
                else
                {
                    movingRight = true;
                }
            }
        }
    }

    private bool moveRightCheck()
    {
        return movingRight && ((Vector2) rightEdge.position + movement * Time.fixedDeltaTime).x <= max.x;
    }

    private bool moveLeftCheck()
    {
        return !movingRight && ((Vector2) leftEdge.position - movement * Time.fixedDeltaTime).x >= min.x;
    }

    private void Shoot()
    {

        foreach (Transform barrel in barrels)
        {
            GameObject newBullet = Instantiate(bullet, barrel.position, Quaternion.identity);
            newBullet.GetComponent<EnemyBullet>().setDamage(damage);
            newBullet.GetComponent<EnemyBullet>().setMovementSpeed(bulletSpeed);
        }

        shootTimer = setShootTimer;
    }
}
