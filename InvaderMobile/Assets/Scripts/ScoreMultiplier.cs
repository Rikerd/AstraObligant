using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplier : PickUpItems
{
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
        GameManager.gm.increaseMultiplier(multiplier);

        Destroy(gameObject);

        return true;
    }

}
