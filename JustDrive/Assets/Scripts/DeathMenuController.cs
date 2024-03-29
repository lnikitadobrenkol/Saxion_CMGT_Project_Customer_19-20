﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DeathMenuController : MonoBehaviour
{
    public Image backGroundImg;

    private bool isShowned = false;

    private float transition = 0.0f;
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isShowned)
        {
            return;
        }

        transition += Time.deltaTime;
        backGroundImg.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
    }

    public void ToggleEndMenu()
    {
        gameObject.SetActive(true);
        isShowned = true;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
