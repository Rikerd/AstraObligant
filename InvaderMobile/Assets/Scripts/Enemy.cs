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

    public GameObject particle;
    public GameObject audioSource;

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

                
                if (GetComponent<DropSystem>() != null)
                {
                    GetComponent<DropSystem>().Drop();
                }

                createScorePrompt();
                spawnAudio();
                spawnParticle();

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
        HighScoreTracker.highScoreTracker.addScore(scoreValue * GameManager.gm.getMultiplier());
    }

    public void createScorePrompt()
    {
        GameObject prompt = Instantiate(scorePrompt, transform.position, Quaternion.identity);
        prompt.GetComponentInChildren<Text>().text = (scoreValue * GameManager.gm.getMultiplier()).ToString();
    }

    public void spawnParticle()
    {
        if (particle != null)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }

    public void spawnAudio()
    {
        if (audioSource != null)
        {
            Instantiate(audioSource, transform.position, Quaternion.identity);
        }
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
