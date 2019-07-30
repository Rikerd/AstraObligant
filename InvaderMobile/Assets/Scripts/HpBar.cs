using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour {
    private Transform[] hearts;
    private int heartIndexToDisable;

    // Use this for initialization
    void Start () {
        hearts = transform.GetComponentsInChildren<Transform>();
        heartIndexToDisable = hearts.Length - 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void damageHealth(int dmgTaken)
    {
        for (int i = 0; i < dmgTaken; i++)
        {
            if (heartIndexToDisable < 0)
            {
                return;
            }

            hearts[heartIndexToDisable].gameObject.SetActive(false);
            heartIndexToDisable--;
        }
    }

    public void healHealth(int hpHeal)
    {
        for (int i = 0; i < hpHeal; i++)
        {
            // ADD CONDITION FOR MAX HP IF NEEDED

            heartIndexToDisable++; // HEART INDEX TO ACTIVATE
            hearts[heartIndexToDisable].gameObject.SetActive(true);
        }
    }
}
