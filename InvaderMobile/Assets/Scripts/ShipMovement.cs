using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float movementSpeed = 2;

    public float tiltLimit = 0.3f;

    private Vector2 max;
    private Vector2 min;

    private Rigidbody2D rb2d;

    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        rb2d = GetComponent<Rigidbody2D>();

        movement = new Vector3(movementSpeed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tilt = Input.acceleration;

        if (moveRightCheck(tilt.x))
        {
            transform.Translate(new Vector3(movementSpeed, 0, 0) * Time.deltaTime);
        } else if (moveLeftCheck(tilt.x))
        {
            transform.Translate(new Vector3(-movementSpeed, 0, 0) * Time.deltaTime);
        }
    }

    private bool moveRightCheck(float direction)
    {
        return (direction >= tiltLimit && (rb2d.position + new Vector2(movementSpeed, 0) * Time.deltaTime).x <= max.x - 0.28f);
    }

    private bool moveLeftCheck(float direction)
    {
        return (direction <= -tiltLimit && (rb2d.position + new Vector2(-movementSpeed, 0) * Time.deltaTime).x >= min.x + 0.28f);
    }
}
