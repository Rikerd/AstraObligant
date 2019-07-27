using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject bullet;

    public float setShotDelay = 0.3f;

    private ShieldController shield;

    private float shotDelay;
    
    // Start is called before the first frame update
    void Start()
    {
        shield = GetComponent<ShieldController>();

        shotDelay = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (shotDelay > 0f)
        {
            shotDelay -= Time.deltaTime;
        }
        
        /*
        if (Input.touchCount > 0 && shotDelay <= 0f)
        {
            Shoot();
        }
        else if (Input.touchCount <= 0)
        {
            shield.StartShield();
        }
        */

        #region Keyboard Controls
        // Keyboard Controls for Debugging
        if (Input.GetKey(KeyCode.Space) && shotDelay <= 0f)
        {
            Shoot();
        }
        else
        {
            shield.StartShield();
        }
        #endregion Keyboard Controls
    }

    private void Shoot()
    {
        Instantiate(bullet, transform.position + new Vector3(0f, 0.3f), Quaternion.identity);

        shotDelay = setShotDelay;

        shield.StopShield();
    }
}
