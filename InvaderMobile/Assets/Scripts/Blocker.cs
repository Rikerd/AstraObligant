using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    public float movementSpeed = 1f;

    public float reflectSpeed = 2f;

    private Rigidbody2D rb2d;
    private bool movingRight = true;



    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player Bullet")
        {
            collision.GetComponent<Bullet>().Reflect(reflectSpeed);
        }
    }
}
