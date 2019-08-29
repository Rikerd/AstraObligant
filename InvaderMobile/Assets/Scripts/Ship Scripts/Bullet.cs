using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float movementSpeed;

    private Rigidbody2D rb2d;
    private bool hit;
    private bool reflected = false;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        hit = false;
        reflected = false;
    }

    private void FixedUpdate()
    {
        if (reflected)
        {
            Vector2 move = -transform.up * movementSpeed * Time.deltaTime;
            rb2d.MovePosition(rb2d.position + move);
            return;
        }

        if (!hit)
        {
            Vector2 move = transform.up * movementSpeed * Time.deltaTime;
            rb2d.MovePosition(rb2d.position + move);
        }
    }

    public bool isReflected()
    {
        return reflected;
    }

    public void Reflect(float refSpeed)
    {
        reflected = true;

        movementSpeed = refSpeed;

        GetComponent<SpriteRenderer>().color = Color.red;

        gameObject.tag = "Untagged";
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
        if (!reflected)
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<Enemy>().TakeDamage(ShootController.shootController.GetDamage());
                Destroy(gameObject);
            }

            if (collision.tag == "Pick Up")
            {
                if (collision.GetComponent<PickUpItems>().UsePickUp())
                {
                    Destroy(gameObject);
                }
            }
        }
        
        if (reflected && collision.tag == "Player")
        {
            collision.GetComponent<PlayerDamageable>().TakeDamage(ShootController.shootController.GetDamage());
            Destroy(gameObject);
        }

        /*
        if (collision.tag == "Boss")
        {
            hit = true;
            collision.GetComponentInParent<BossController>().takeDamage(damage);
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            GetComponent<ParticleSystem>().Play();

            Destroy(gameObject, 0.5f);
        }*/
    }
}
