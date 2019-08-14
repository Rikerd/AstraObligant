using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : Enemy
{
    public enum MovementType
    {
        Left,
        Right,
        Down
    }

    [Header("Speed Values")]
    public float patrolSpeed = 3f;
    public float bulletSpeed = 1f;
    public float setDownwardTimer = 1f;

    [Header("Spawn Movement Variables")]
    public float minInitialMovementTimer = 0.5f;
    public float maxInitialMovementTimer = 0.8f;
    public float fowardMovement = 2f;

    [Header("Spawning Variables")]
    public GameObject bullet;
    public float setShootTimer = 2f;

    private float initialMovementTimer;

    private float shootTimer;

    private float downwardTimer = 0f;

    private Vector2 max;
    private Vector2 min;

    private Rigidbody2D rb2d;

    private Vector2 patrolMovement;
    private Vector2 downMovement;

    private bool movingRight = true;

    private bool initialMovement;

    private MovementType currentMovement;
    private MovementType previousMovement;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        shootTimer = setShootTimer;

        initialMovementTimer = Random.Range(minInitialMovementTimer, maxInitialMovementTimer);

        currentMovement = MovementType.Right;

        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        rb2d = GetComponent<Rigidbody2D>();

        patrolMovement = new Vector2(patrolSpeed, 0);
        downMovement = new Vector2(0, patrolSpeed);

        initialMovement = true;
    }

    // Update is called once per frame
    void Update()
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

            if (currentMovement == MovementType.Down && downwardTimer > 0f)
            {
                downwardTimer -= Time.deltaTime;
            }
            else if (currentMovement == MovementType.Down && downwardTimer <= 0f)
            {
                if (previousMovement == MovementType.Left)
                {
                    currentMovement = MovementType.Right;
                }
                else if (previousMovement == MovementType.Right)
                {
                    currentMovement = MovementType.Left;
                }
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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (initialMovement)
        {
            rb2d.MovePosition(rb2d.position - new Vector2(0f, fowardMovement) * Time.fixedDeltaTime);
        }
        else
        {
            if (moveRightCheck())
            {
                rb2d.MovePosition(rb2d.position + patrolMovement * Time.fixedDeltaTime);
            }
            else if (moveLeftCheck())
            {
                rb2d.MovePosition(rb2d.position - patrolMovement * Time.fixedDeltaTime);
            }
            else
            {
                moveDown();
            }

        }
    }

    private bool moveRightCheck()
    {
        return currentMovement == MovementType.Right && (rb2d.position + patrolMovement * Time.fixedDeltaTime).x <= max.x - 0.41f;
    }

    private bool moveLeftCheck()
    {
        return currentMovement == MovementType.Left && (rb2d.position - patrolMovement * Time.fixedDeltaTime).x >= min.x + 0.41f;
    }

    private void moveDown()
    {
        if (currentMovement != MovementType.Down)
        {
            previousMovement = currentMovement;

            currentMovement = MovementType.Down;

            downwardTimer = setDownwardTimer;
        }

        rb2d.MovePosition(rb2d.position - downMovement * Time.fixedDeltaTime);

    }

    private void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, transform.position + new Vector3(0f, -0.2f, 0f), Quaternion.identity);
        newBullet.GetComponent<EnemyBullet>().setDamage(damage);
        newBullet.GetComponent<EnemyBullet>().setMovementSpeed(bulletSpeed);

        shootTimer = setShootTimer;
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
