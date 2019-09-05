using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUpItems : MonoBehaviour
{
    public GameObject audioSource;

    public float setLastingDuration = 4f;

    protected float lastingDuration;

    protected void BaseStart()
    {
        lastingDuration = setLastingDuration;
    }

    public void updateAndCheckTimer()
    {
        lastingDuration -= Time.deltaTime;

        if (lastingDuration <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public abstract bool UsePickUp();
}
