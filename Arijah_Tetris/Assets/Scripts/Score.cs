using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    public void AddScore(int linesCleared)
    {
        switch (linesCleared)
        {
            case 1: score += 100; break;
            case 2: score += 300; break;
            case 3: score += 500; break;
            case 4: score += 800; break;
            default: break;
        }
        UpdateScore();
        Debug.Log($"Score: {score}");
    }
    public void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    public int GetScore()
    {
        return score;
    }

}