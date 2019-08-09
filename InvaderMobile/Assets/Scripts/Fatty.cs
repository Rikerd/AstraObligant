using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fatty : Enemy
{
    [Header("Fatty Size")]
    public float minSize = 2f;
    public float maxSize = 2.5f;

    [Header("Movement Speed")]
    public float minMovementSpeed = 0.2f;
    public float maxMovementSpeed = 0.4f;

    private Vector2 max;
    private Vector2 min;
    private bool movingRight;

    private float fattySize;
    private float movementSpeed;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        fattySize = Random.Range(minSize, maxSize);
        movementSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);

        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        transform.eulerAngles = new Vector3(0, 0, 30);

        movingRight = true;

        currentHP = maxHP;

        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveFatty();
    }

    private void moveFatty()
    {
        if (moveRightCheck())
        {
            rb2d.MovePosition(rb2d.position - ((Vector2)transform.up * movementSpeed) * Time.fixedDeltaTime);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 300);
            movingRight = false;
        }
        
        if (moveLeftCheck())
        {
            rb2d.MovePosition(rb2d.position - ((Vector2)transform.up * movementSpeed) * Time.fixedDeltaTime);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 60);
            movingRight = true;
        }
    }

    private bool moveRightCheck()
    {
        print(max.x - (0.5f));
        return movingRight && (rb2d.position - ((Vector2)transform.up * movementSpeed) * Time.fixedDeltaTime).x <= max.x - 0.5f;
    }

    private bool moveLeftCheck()
    {
        return !movingRight && (rb2d.position - ((Vector2)transform.up * movementSpeed) * Time.fixedDeltaTime).x >= min.x + 0.5f;
    }
}
