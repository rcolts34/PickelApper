using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score Instance;
    private int score = 0;
    private int highScore = 0;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text highScoreText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;

        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore); // Save the new high score
            PlayerPrefs.Save(); // Ensure the value is written to disk
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void ResetHighScore()
    {
        highScore = 0;
        PlayerPrefs.SetInt("HighScore", highScore); // Reset in PlayerPrefs
        UpdateUI(); // Refresh the UI
    }

}
