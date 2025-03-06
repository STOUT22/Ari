using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Assign in Inspector
    public TextMeshProUGUI finalScoreText;
    private bool isGameOver = false;

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("Game Over!");
        gameOverPanel.SetActive(true); // Show Game Over UI

        int finalScore = FindObjectOfType<Score>().GetScore();
        finalScoreText.text = "Final Score: " + finalScore;

        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
