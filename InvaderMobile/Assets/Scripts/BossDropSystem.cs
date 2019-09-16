using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDropSystem : MonoBehaviour
{
    [Header("Rates & Items")]
    public int[] dropRates;
    public List<GameObject> pickUps;
    public GameObject defaultDrop;

    private List<bool> droppedList;
    
    private List<GameObject> unusedPickUps;

    private int total = 0;

    private int randDropNumber;

    private GameObject dropItem;

    // Start is called before the first frame update
    void Start()
    {
        droppedList = new List<bool>();

        for (int i = 0; i < pickUps.Count; i++)
        {
            droppedList.Add(false);
        }
        unusedPickUps = new List<GameObject>();
    }

    public void ChooseDrop()
    {
        total = 0;
        
        unusedPickUps.Clear();

        for (int i = 0; i < dropRates.Length; i++)
        {
            if (!droppedList[i])
            {
                if (i == 5 && GameManager.gm.currentLevel <= 15)
                {
                    continue;
                }

                total += dropRates[i];
                
                unusedPickUps.Add(pickUps[i]);
            }
        }

        randDropNumber = Random.Range(0, total);

        for (int i = 0; i < dropRates.Length; i++)
        {
            if (droppedList[i])
            {
                continue;
            }

            if (i == 5 && GameManager.gm.currentLevel <= 15)
            {
                continue;
            }

            if (randDropNumber <= dropRates[i])
            {
                dropItem = unusedPickUps[i];

                if (i == 0 || i == 1 || i == 5)
                {
                    droppedList[i] = true;
                }

                return;
            }
            else
            {
                randDropNumber -= dropRates[i];
            }
        }
    }

    public void Drop(Vector3 pos)
    {
        Instantiate(dropItem, pos, Quaternion.identity);
    }

    public void DefaultDrop(Vector3 pos)
    {
        Instantiate(defaultDrop, pos, Quaternion.identity);
    }
}
