using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float movementSpeed;

    private int damage;

    private Rigidbody2D rb2d;
    private bool hit;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        hit = false;
    }

    private void FixedUpdate()
    {
        if (!hit)
        {
            Vector2 move = -transform.up * movementSpeed * Time.deltaTime;
            rb2d.MovePosition(rb2d.position + move);
        }
    }

    public void OnBecameInvisible()
    {
        if (!hit)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<ShipHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void setMovementSpeed(float ms)
    {
        movementSpeed = ms;
    }

    public void setDamage(int dmg)
    {
        damage = dmg;
    }
}
