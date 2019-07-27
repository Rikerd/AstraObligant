using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public float setShieldDelay = 0.2f;
    public float setRechargeDelay = 0.2f;

    private float shieldDelayTimer;

    private bool shielding;

    private ShipHealth hpController;

    // Start is called before the first frame update
    void Start()
    {
        shielding = false;

        shieldDelayTimer = setShieldDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (shielding)
        {
            if (shieldDelayTimer > 0f)
            {
                shieldDelayTimer -= Time.deltaTime;
                return;
            }

            hpController.ActivateShield();
        }
        else
        {
            shieldDelayTimer = setShieldDelay;

            hpController.DeactivateShield();
        }
    }

    public void StartShield()
    {
        shielding = true;
    }

    public void StopShield()
    {
        shielding = false;
    }
}
