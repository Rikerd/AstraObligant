using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplier : PickUpItems
{
    public bool permMultiplier;

    public int multiplier = 2;

    public void Start()
    {
        BaseStart();
    }

    // Update is called once per frame
    void Update()
    {
        updateAndCheckTimer();
    }

    public override bool UsePickUp()
    {
        if (permMultiplier)
        {
            GameManager.gm.increaseBaseMultiplier(multiplier);
        }
        else
        {
            GameManager.gm.tempIncreaseMultiplier(multiplier);
        }

        Destroy(gameObject);

        return true;
    }

}
