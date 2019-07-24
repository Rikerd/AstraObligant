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

    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        rb2d = GetComponent<Rigidbody2D>();

        movement = new Vector2(movementSpeed, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 tilt = Input.acceleration;

        #region Keyboard Controls
        // Keyboard Controls for Debugging
        if (Input.GetKey(KeyCode.D) && (rb2d.position + movement * Time.fixedDeltaTime).x <= max.x - 0.28f)
        {
            rb2d.MovePosition(rb2d.position + movement * Time.fixedDeltaTime);

        } else if (Input.GetKey(KeyCode.A) && (rb2d.position - movement * Time.fixedDeltaTime).x >= min.x + 0.28f)
        {
            rb2d.MovePosition(rb2d.position - movement * Time.fixedDeltaTime);
        }
        #endregion Keyboard Controls

        if (moveRightCheck(tilt.x))
        {
            rb2d.MovePosition(rb2d.position + movement * Time.fixedDeltaTime);
            // transform.Translate(movement * Time.fixedDeltaTime);
        } else if (moveLeftCheck(tilt.x))
        {
            rb2d.MovePosition(rb2d.position - movement * Time.fixedDeltaTime);
            //transform.Translate(new Vector3(-movementSpeed, 0, 0) * Time.fixedDeltaTime);
        }
    }

    private bool moveRightCheck(float direction)
    {
        return (direction >= tiltLimit && (rb2d.position + movement * Time.fixedDeltaTime).x <= max.x - 0.28f);
    }

    private bool moveLeftCheck(float direction)
    {
        return (direction <= -tiltLimit && (rb2d.position - movement * Time.fixedDeltaTime).x >= min.x + 0.28f);
    }
}
