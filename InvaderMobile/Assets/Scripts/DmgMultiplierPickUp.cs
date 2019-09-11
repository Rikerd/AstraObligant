using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgMultiplierPickUp : PickUpItems
{
    public int multiplier = 2;

    public void Start()
    {
        BaseStart();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override bool UsePickUp()
    {
        ShootController.shootController.DamageMultiplier(multiplier);

        Destroy(gameObject);

        GameManager.gm.StartPrompt("Damage");

        Instantiate(audioSource, transform.position, Quaternion.identity);

        Instantiate(particle, transform.position, Quaternion.identity);

        return true;
    }

}
