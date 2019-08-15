using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fatty : Enemy
{
    public int maxHpLoss = 15;

    [Header("Fatty Size")]
    public float minSize = 2f;
    public float maxSize = 2.5f;

    [Header("Movement Speed")]
    public float minMovementSpeed = 0.2f;
    public float maxMovementSpeed = 0.4f;
    public float movementSpeedIncrease = 0.1f;

    [Header("Edges")]
    public Transform leftEdge;
    public Transform rightEdge;

    private Vector2 max;
    private Vector2 min;
    private bool movingRight;

    private float fattySize;
    private float movementSpeed;

    private SpriteRenderer sprite;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        fattySize = Random.Range(minSize, maxSize);
        movementSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);
        fattySize = Random.Range(minSize, maxSize);

        transform.localScale = new Vector3(fattySize, fattySize, 1);

        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        transform.eulerAngles = new Vector3(0, 0, 30);

        if (Random.Range(0, 100) < 50)
            movingRight = true;
        else
            movingRight = false;

        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveFatty();
    }

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        
        float newSize = ((float) currentHP / maxHP) * fattySize;

        if (newSize < 1f)
        {
            newSize = 1;
        }        

        transform.localScale = new Vector3(newSize, newSize, 1);

        print(currentHP);   

        if (maxHP - currentHP <= maxHpLoss)
        {
            movementSpeed += movementSpeedIncrease;
        }
    }

    private void moveFatty()
    {
        if (moveRightCheck())
        {
            rb2d.MovePosition(rb2d.position - ((Vector2)transform.up * movementSpeed) * Time.fixedDeltaTime);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 285);
            movingRight = false;
        }
        
        if (moveLeftCheck())
        {
            rb2d.MovePosition(rb2d.position - ((Vector2)transform.up * movementSpeed) * Time.fixedDeltaTime);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 75);
            movingRight = true;
        }
    }

    private bool moveRightCheck()
    {
        return movingRight && (rightEdge.position - (transform.up * movementSpeed) * Time.fixedDeltaTime).x <= max.x;
    }

    private bool moveLeftCheck()
    {
        return !movingRight && (leftEdge.position - (transform.up * movementSpeed) * Time.fixedDeltaTime).x >= min.x;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerDamageable>().TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
