﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotPickUp : PickUpItems
{
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
        ShootController.shootController.ActivateTripleShot();

        Destroy(gameObject);

        GameManager.gm.StartPrompt("Triple Shot");

        Instantiate(audioSource, transform.position, Quaternion.identity);

        Instantiate(particle, transform.position, Quaternion.identity);

        return true;
    }
}
