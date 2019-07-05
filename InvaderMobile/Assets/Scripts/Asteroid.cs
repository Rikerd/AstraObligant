using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Enemy
{
    public float minMovementSpeed = 0.1f;
    public float maxMovementSpeed = 0.3f;

    private float movementSpeed;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);

        currentHP = maxHP;

        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.MovePosition(transform.position - (transform.up * movementSpeed) * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<ShipHealth>().TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
