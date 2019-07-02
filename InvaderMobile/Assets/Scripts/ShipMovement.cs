using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float movementSpeed = 2;

    public float tiltLimit = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tilt = Input.acceleration;

        print(tilt);

        if (tilt.x > tiltLimit)
        {
            transform.Translate(new Vector3(movementSpeed, 0, 0) * Time.deltaTime);
        } else if (tilt.x < -tiltLimit)
        {
            transform.Translate(new Vector3(-movementSpeed, 0, 0) * Time.deltaTime);
        }
    }
}
