using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("UI Beállítások")]
    public TextMeshProUGUI scoreText;

    private float score;

    void Start()
    {
        score = 0f;
    }

    void Update()
    {
        score += 10f * Time.deltaTime;
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }
}