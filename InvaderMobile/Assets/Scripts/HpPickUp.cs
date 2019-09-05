using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPickUp : PickUpItems
{
    public int hpRecovery = 1;

    private ShipHealth hp;

    private bool isColliding = false;

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
        if (isColliding)
        {
            return false;
        }
        if (hp.getCurrentHp() < hp.getMaxHp())
        {
            isColliding = true;

            hp.Heal(hpRecovery);

            Destroy(gameObject);

            isColliding = true;

            Instantiate(audioSource, transform.position, Quaternion.identity);

            return true;
        }

        return false;
    }
}
