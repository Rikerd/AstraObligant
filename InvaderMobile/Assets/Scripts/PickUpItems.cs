﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUpItems : MonoBehaviour
{
    public GameObject audioSource;

    public GameObject particle;

    public float setLastingDuration = 4f;

    protected float lastingDuration;

    protected Animator anim;

    protected void BaseStart()
    {
        lastingDuration = setLastingDuration;

        anim = GetComponent<Animator>();
    }

    public void updateAndCheckTimer()
    {
        lastingDuration -= Time.deltaTime;

        if (lastingDuration <= 4f && !anim.GetBool("fade"))
        {
            anim.SetBool("fade", true);
        }

        if (lastingDuration <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public abstract bool UsePickUp();
}
