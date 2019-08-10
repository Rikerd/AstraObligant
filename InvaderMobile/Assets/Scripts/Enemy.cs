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

    protected int currentHP;

    public virtual void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            addScore();

            GetComponent<DropSystem>().Drop();
            createScorePrompt();
            Destroy(gameObject);

            return;
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
}
