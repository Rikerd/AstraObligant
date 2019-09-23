using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [Range(0f, 100f)]
    public float dropPercentage = 10f;
    
    [Header("Rates & Items")]
    public int[] dropRates;
    public List<GameObject> pickUps;

    private bool isDrop = false;

    private int total = 0;

    private int randDropNumber;

    private GameObject dropItem;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(1f, 100f) <= dropPercentage)
        {
            isDrop = true;
        }
        else
        {
            return;
        }

        foreach(int dropRate in dropRates)
        {
            total += dropRate;
        }

        randDropNumber = Random.Range(0, total);

        for (int i = 0; i < dropRates.Length; i++)
        {
            if (randDropNumber <= dropRates[i])
            {
                dropItem = pickUps[i];

                return;
            }
            else
            {
                randDropNumber -= dropRates[i];
            }
        }
    }

    public void Drop()
    {
        if (isDrop)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
        }
    }
}
