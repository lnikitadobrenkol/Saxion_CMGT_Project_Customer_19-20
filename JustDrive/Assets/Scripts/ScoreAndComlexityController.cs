using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndComlexityController : MonoBehaviour
{
    public Text scoreText;
    //public DeathMenuController deathMenu;

    private float score = 0.0f;

    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 10;
    private int scoreToNextLevel = 10;

    private bool isDead = false;

    void Update()
    {
        if (isDead)
        {
            return;
        }

        if (score >= scoreToNextLevel)
        {
            LevelUp();
        }
        score += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)score).ToString();
    }

    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
        {
            return;
        }

        scoreToNextLevel *= 2;
        difficultyLevel++;

        GetComponent<PlayerController>().SetSpeed(difficultyLevel);
    }

    public void OnDeath()
    {
        isDead = true;
        //deathMenu.ToggleEndMenu(score);
    }
}
