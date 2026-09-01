using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [Header("Alap UI Beállítások")]
    public TextMeshProUGUI scoreText;

    [Header("Game Over UI Beállítások")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject newPlanetButton;

    private float score;
    private bool isGameOver = false;

    void Start()
    {
        score = 0f;
        Time.timeScale = 1f;
    }

    void Update()
    {
        score += 10f * Time.deltaTime;
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }

    public void TriggerGameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        int finalScore = Mathf.FloorToInt(score);

        gameOverPanel.SetActive(true);

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (finalScore > highScore)
        {
            highScore = finalScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        finalScoreText.text = "Final score: " + finalScore;
        highScoreText.text = "High score: " + highScore;

        if (highScore >= 1000)
        {
            newPlanetButton.SetActive(true);
        }
        else
        {
            newPlanetButton.SetActive(false);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}