using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    public int maxHP = 3;

    public int currentHP;

    private HpBar hpUIController;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;

        hpUIController = GameObject.Find("Health Bar").GetComponent<HpBar>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        hpUIController.updateHealth(dmg);

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
