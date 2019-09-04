using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public static ShootController shootController;

    public GameObject bullet;

    public int damage = 1;

    public float setShotDelay = 0.3f;

    [Header("Spawn Positions")]
    public Transform centralShot;
    public Transform leftShot;
    public Transform rightShot;

    private bool tripleShot = false;

    private AudioSource audioSource;

    //private ShieldController shield;

    //private float shotDelay;
    
    // Start is called before the first frame update
    void Start()
    {
        //shield = GetComponent<ShieldController>();

        //shotDelay = 0f;

        shootController = this;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (shotDelay > 0f)
        {
            shotDelay -= Time.deltaTime;
        }
        */
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Shoot();
            }
        }
        #region Old Shield Stuff
        /*else if (Input.touchCount <= 0)
        {
            shield.StartShield();
        }
        */
        #endregion Old Shield Stuff

        #region Keyboard Controls
        // Keyboard Controls for Debugging
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        #region Old Shield Stuff
        /*
        else
        {
            shield.StartShield();
        }
        */
        #endregion Old Shield Stuff

        #endregion Keyboard Controls
    }

    private void Shoot()
    {
        audioSource.pitch = Random.Range(0.85f, 1.15f);
        audioSource.Play();

        Instantiate(bullet, centralShot.position, Quaternion.identity);

        if (tripleShot)
        {
            Instantiate(bullet, leftShot.position, Quaternion.identity);
            Instantiate(bullet, rightShot.position, Quaternion.identity);
        }

        //shotDelay = setShotDelay;

        //shield.StopShield();
    }
    
    public void ActivateTripleShot()
    {
        tripleShot = true;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void DamageMultiplier(int multiplier)
    {
        damage *= multiplier;
    }

    
}
