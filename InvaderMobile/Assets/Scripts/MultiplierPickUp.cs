using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierPickUp : PickUpItems
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

        return true;
    }
}
