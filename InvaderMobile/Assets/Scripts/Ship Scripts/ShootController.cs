using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject bullet;

    public float setShotDelay = 0.3f;

    private float shotDelay;
    
    // Start is called before the first frame update
    void Start()
    {
        shotDelay = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (shotDelay > 0f)
        {
            shotDelay -= Time.deltaTime;
        }

        if (Input.touchCount > 0 && shotDelay <= 0f)
        {
            Shoot();
        }

        #region Keyboard Controls
        // Keyboard Controls for Debugging
        if (Input.GetKey(KeyCode.Space) && shotDelay <= 0f)
        {
            Shoot();
        }
        #endregion Keyboard Controls
    }

    private void Shoot()
    {
        Instantiate(bullet, transform.position + new Vector3(0f, 0.3f), Quaternion.identity);

        shotDelay = setShotDelay;
    }
}
