using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private int score = 0;

    public void ResetScore()
    {
        scoreText.text = "Score: 0";
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}

    

