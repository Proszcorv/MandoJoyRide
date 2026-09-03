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

        int currentPlanet = PlayerPrefs.GetInt("CurrentPlanet", 0);
        string highScoreKey = "HighScore_" + currentPlanet;

        int highScore = PlayerPrefs.GetInt(highScoreKey, 0);

        if (finalScore > highScore)
        {
            highScore = finalScore;
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save();
        }

        finalScoreText.text = "Final score: " + finalScore;
        highScoreText.text = "High score: " + highScore;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}