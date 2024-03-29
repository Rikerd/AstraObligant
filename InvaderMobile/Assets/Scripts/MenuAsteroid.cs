﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAsteroid : MonoBehaviour
{
    public GameObject particle;
    
    [Header("Asteroid Size")]
    public float minSize = 0.7f;
    public float maxSize = 0.9f;

    [Header("Movement Speed")]
    public float minMovementSpeed = 0.1f;
    public float maxMovementSpeed = 0.3f;
    public Transform parentObject;

    [Header("Rotation Speed")]
    public float minRotationSpeed = 25f;
    public float maxRotationSpeed = 45f;

    private float asteroidSize;
    private float movementSpeed;
    private float rotationSpeed;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    public void Start()
    {
        asteroidSize = Random.Range(minSize, maxSize);
        movementSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

        transform.localScale = new Vector3(asteroidSize, asteroidSize, 1);

        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotateAsteroid();

        //rb2d.MovePosition(transform.position - (Vector3.up * movementSpeed) * Time.fixedDeltaTime);
        parentObject.Translate(-(Vector3.up * movementSpeed) * Time.fixedDeltaTime);
    }

    private void rotateAsteroid()
    {
        rb2d.MoveRotation(rb2d.rotation + rotationSpeed * Time.fixedDeltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            spawnParticle();

            Destroy(parentObject.gameObject);
        }
    }

    public void spawnParticle()
    {
        if (particle != null)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
