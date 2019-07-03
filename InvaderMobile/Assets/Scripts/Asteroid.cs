using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float movementSpeed = 0.3f;

    public int setHP = 1;

    private Rigidbody2D rb2d;
    private int hp;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.MovePosition(transform.position - (transform.up * movementSpeed) * Time.deltaTime);
    }
}
