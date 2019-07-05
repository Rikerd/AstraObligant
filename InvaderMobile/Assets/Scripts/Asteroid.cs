using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Enemy
{
    public float movementSpeed = 0.3f;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;

        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.MovePosition(transform.position - (transform.up * movementSpeed) * Time.deltaTime);
    }
}
