using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollision : PlayerDamageable
{
    private ShipHealth hp;

    public void Start()
    {
        hp = GameObject.Find("Ship Sprite").GetComponent<ShipHealth>();
    }

    public override void TakeDamage(int dmg)
    {
        hp.TakeDamage(dmg);
    }
}
