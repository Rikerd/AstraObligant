using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float movementSpeed;

    public GameObject particle;
    public GameObject audioSource;

    private TrailRenderer trail;
    private Rigidbody2D rb2d;
    private bool hit;
    private bool reflected = false;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        hit = false;
        reflected = false;

        trail = GetComponent<TrailRenderer>();
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

        trail.startColor = new Color(255, 0, 0);
        trail.endColor = new Color(255, 91, 0);

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
                Instantiate(particle, transform.position, Quaternion.identity);
                Instantiate(audioSource, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            if (collision.tag == "Pick Up")
            {
                if (collision.GetComponent<PickUpItems>().UsePickUp())
                {
                    Instantiate(particle, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
        
        if (reflected && collision.tag == "Player")
        {
            // ADD PARTICLE FOR REFLECTED
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
