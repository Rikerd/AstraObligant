using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealth : PlayerDamageable
{
    public int maxHP = 3;

    public float maxShield = 100;
    
    public float shieldRechargeRate = 2f;

    public float setInvincibleTimer = 1f;

    public int currentHP;
    public float currentShield;

    public GameObject shieldObject;
    
    private HpBar hpUIController;

    private ScreenShake shake;

    private SpriteRenderer sprite;

    private Collider2D shipCollidier;

    private bool invincible;
    private float invincibleTimer;

    #region Old Shield Stuff
    //private Slider shieldSlider;

    //private ShieldController shieldController;

    //private bool shielded;

    //private bool recharging;
    #endregion Old Shield Stuff

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        currentShield = maxShield;

        hpUIController = GameObject.Find("Health Bar").GetComponent<HpBar>();

        shake = Camera.main.GetComponent<ScreenShake>();

        sprite = GetComponent<SpriteRenderer>();

        shipCollidier = GetComponent<Collider2D>();

        invincible = false;
        invincibleTimer = setInvincibleTimer;

        #region Old Shield Stuff
        //shieldSlider = GameObject.Find("Shield Bar").GetComponent<Slider>();

        //shieldController = GetComponentInParent<ShieldController>();

        //shieldSlider.maxValue = maxShield;
        //shieldSlider.value = currentShield;

        //shielded = false;
        //recharging = false;
        #endregion Old Shield Stuff
    }

    private void Update()
    {
        // Recharge();

        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreaseMaxHp();
        }

        if (invincible)
        {
            invincibleTimer -= Time.deltaTime;

            sprite.enabled = !sprite.enabled;

            if (invincibleTimer <= 0f)
            {
                invincible = false;

                sprite.enabled = true;
            }
        }
    }

    public override void TakeDamage(int dmg)
    {
        if (!invincible)
        {
            currentHP -= dmg;
            hpUIController.damageHealth(dmg);
            shake.StartShake();
        }

        #region Old Shield Stuff
        /*
        if (!shielded)
        {
            currentHP -= dmg;
            hpUIController.damageHealth(dmg);
        }
        else
        {
            currentShield -= dmg * 10;

            if (currentShield < 0f)
            {
                currentShield = 0;
            }

            if (currentShield == 0f)
            {
                shieldController.StopShield();
            }

            shieldSlider.value = currentShield;
        }
        */
        #endregion Old Shield Stuff

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            invincible = true;

            invincibleTimer = setInvincibleTimer;
        }
    }

    #region Old Shield Stuff
    /*
    private void Recharge()
    {
        if (recharging)
        {
            if (currentShield < maxShield)
            {
                currentShield += shieldRechargeRate * Time.deltaTime;
            }

            if (currentShield > maxShield)
            {
                currentShield = maxShield;
            }

            shieldSlider.value = currentShield;
        }
    }

    public void ActivateShield()
    {
        shielded = true;

        shieldObject.SetActive(true);
    }

    public void DeactivateShield()
    {
        shielded = false;

        shieldObject.SetActive(false);
    }

    public void ActivateRecharge()
    {
        recharging = true;
    }

    public void DeactivateRecharge()
    {
        recharging = false;
    }

    public float getCurrentShieldPercent()
    {
        return (currentShield / maxShield) * 100;
    }
    */
    #endregion Old Shield Stuff

    public int getCurrentHp()
    {
        return currentHP;
    }

    public int getMaxHp()
    {
        return maxHP;
    }

    public void Heal(int amt)
    {
        currentHP += amt;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        hpUIController.healHealth(amt);
    }

    public void IncreaseMaxHp()
    {
        maxHP += 3;

        StartCoroutine(FullHeal());
    }

    IEnumerator FullHeal()
    {
        while (currentHP < maxHP)
        {
            Heal(1);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
