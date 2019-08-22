using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int maxHP;

    public int damage;

    public int scoreValue;

    public GameObject scorePrompt;

    public bool isBoss = false;

    protected int currentHP;

    protected bool isDead = false;

    public virtual void Start()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(int dmg)
    {
        if (!isDead)
        {
            currentHP -= dmg;

            if (currentHP <= 0)
            {
                addScore();

                GetComponent<DropSystem>().Drop();
                createScorePrompt();

                if (!isBoss)
                {
                    Destroy(gameObject);
                }
                else
                {
                    isDead = true;
                    disableAll();
                }

                return;
            }
        }
    }

    public void addScore()
    {
        HighScoreTracker.highScoreTracker.addScore(scoreValue);
    }

    public void createScorePrompt()
    {
        GameObject prompt = Instantiate(scorePrompt, transform.position, Quaternion.identity);
        prompt.GetComponentInChildren<Text>().text = scoreValue.ToString();
    }

    public void disableAll()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public bool checkDeath()
    {
        return isDead;
    }

    public void killKey()
    {
        currentHP = 0;
        TakeDamage(0);
    }
}
