using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    public float horizontalMovementSpeed = 1f;
    public float verticalMovementSpeed = 0.5f;
    public float setVerticalMoveTimer = 1f;

    public float reflectSpeed = 2f;

    public GameObject particle;

    private float verticalMoveTimer;

    private Rigidbody2D rb2d;
    private bool movingRight = true;

    private Vector2 verticalMovement;
    private Vector2 horizontalMovement;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        if (movingRight)
        {
            horizontalMovement = new Vector2(horizontalMovementSpeed, 0);
        }
        else
        {
            horizontalMovement = new Vector2(-horizontalMovementSpeed, 0);
        }

        verticalMoveTimer = setVerticalMoveTimer;

        verticalMovement = new Vector2(0, verticalMovementSpeed);

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (verticalMoveTimer > 0f)
        {
            verticalMoveTimer -= Time.deltaTime;
        }
        else
        {
            verticalMoveTimer = setVerticalMoveTimer;
            verticalMovement = -verticalMovement;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + (horizontalMovement * Time.fixedDeltaTime) + (verticalMovement * Time.fixedDeltaTime));
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player Bullet")
        {
            audioSource.Play();
            collision.GetComponent<Bullet>().Reflect(reflectSpeed);
        }
    }

    public void setMovementDirection(bool rightDirection)
    {
        movingRight = rightDirection;
    }

    public void killKey()
    {
        Instantiate(particle, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
