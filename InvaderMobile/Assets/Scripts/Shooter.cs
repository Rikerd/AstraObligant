using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    public float patrolSpeed = 3f;
    
    [Header("Shooting Variables")]
    public GameObject bullet;
    public float setShootTimer = 2f;

    private float shootTimer;

    private Vector2 max;
    private Vector2 min;

    private Rigidbody2D rb2d;

    private Vector2 movement;

    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        shootTimer = setShootTimer;

        movingRight = true;

        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        rb2d = GetComponent<Rigidbody2D>();

        movement = new Vector2(patrolSpeed, 0);
    }

    public void Update()
    {
        if (shootTimer > 0f)
        {
            shootTimer -= Time.deltaTime;
        } else
        {
            Shoot();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
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

    private bool moveRightCheck()
    {
        return movingRight && (rb2d.position + movement * Time.fixedDeltaTime).x <= max.x - 0.28f;
    }

    private bool moveLeftCheck()
    {
        return !movingRight && (rb2d.position - movement * Time.fixedDeltaTime).x >= min.x + 0.28f;
    }

    private void Shoot()
    {
        Instantiate(bullet, transform.position + new Vector3(0.15f, 0f, 0f), Quaternion.identity);
        Instantiate(bullet, transform.position - new Vector3(0.15f, 0f, 0f), Quaternion.identity);

        shootTimer = setShootTimer;
    }
}
