using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject bullet;

    public float setShotDelay = 0.3f;

    [Header("Shield Properties")]
    public float setShieldDelay = 0.2f;


    private float shotDelay;

    private float shieldDelay;
    
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
        else if (Input.touchCount <= 0)
        {
            Shield();
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
        shieldDelay = setShieldDelay;

        Instantiate(bullet, transform.position + new Vector3(0f, 0.3f), Quaternion.identity);

        shotDelay = setShotDelay;
    }

    private void Shield()
    {
        if (shieldDelay >= 0f)
        {
            shieldDelay -= Time.deltaTime;
        }
        else
        {
            return;
        }
    }
}
