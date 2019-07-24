using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    public float patrolSpeed = 3f;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveRightCheck())
        {
            rb2d.MovePosition(rb2d.position + movement * Time.fixedDeltaTime);
        }
        else
        {
            movingRight = false;
        }

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
}
