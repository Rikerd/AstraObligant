using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int maxHP;

    public int damage;

    public int scoreValue;

    protected int currentHP;

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            addScore();

            GetComponent<DropSystem>().Drop();
            Destroy(gameObject);
        }
    }

    public void addScore()
    {
        HighScoreTracker.highScoreTracker.addScore(scoreValue);
    }
}
