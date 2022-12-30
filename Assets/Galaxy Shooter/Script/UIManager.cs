using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImagenDisplay;
    public GameObject titleScreen;
    public Text scoreText;
    public int score = 0;

    public void UpdateLives(int currentLives)
    {
        livesImagenDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void HideScreenTitle()
    {
        titleScreen.SetActive(false);
    }

    public void ShowScreenTitle()
    {
        titleScreen.SetActive(true);
        scoreText.text = "Score: ";
    }
}
