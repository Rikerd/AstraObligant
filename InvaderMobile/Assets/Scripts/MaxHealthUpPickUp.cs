﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthUpPickUp : PickUpItems
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
        GameObject.Find("Ship").GetComponentInChildren<ShipHealth>().IncreaseMaxHp();

        Destroy(gameObject);

        GameManager.gm.StartPrompt("Health");

        Instantiate(audioSource, transform.position, Quaternion.identity);

        Instantiate(particle, transform.position, Quaternion.identity);

        return true;
    }
}
