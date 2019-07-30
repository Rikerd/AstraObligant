using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPickUp : PickUpItems
{
    public int hpRecovery = 1;

    private ShipHealth hp;

    public void Start()
    {
        BaseStart();

        hp = GameObject.Find("Ship Sprite").GetComponent<ShipHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        updateAndCheckTimer();
    }

    public override bool UsePickUp()
    {
        if (hp.getCurrentHp() < hp.getMaxHp())
        {
            hp.Heal(hpRecovery);

            Destroy(gameObject);

            return true;
        }

        return false;
    }
}
