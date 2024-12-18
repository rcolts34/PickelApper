using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Elements")]
    public GameObject gameOverPanel; 
    public Text gameOverScoreText;  // Final score
    public Text gameOverHighScoreText; // High score
    public Text gameOverText;
    public GameObject victoryPanel;
    public Text victoryScoreText;  // Final score
    public Text victoryHighScoreText; // High score
    public Text victoryMessage;
    public GameObject levelStartPanel;
    public Text startLevel;
    public Text startTime;

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

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }

        if (levelStartPanel != null)
        {
            levelStartPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Press 'G' to test activation
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.G)) // Press 'G' to test activation
        {
            gameOverPanel.SetActive(false);
            Time.timeScale = 1f;
        }

    }
    public void ShowGameOver(int score, int highScore)
    {
 
       gameOverPanel.SetActive(true);
        GameObject.FindWithTag("Enemy");
        Destroy(this.gameObject);
        //Time.timeScale = 0f;

        // Update the score UI
        gameOverScoreText.text = "Your Score: " + score;
        gameOverHighScoreText.text = "High Score: " + highScore;
        gameOverText.text = "GAME OVER";

    }

    public void ShowVictory(int score, int highScore)
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
        GameObject.FindWithTag("Enemy");
        Destroy(this.gameObject);

        // Update the score UI
        victoryMessage.text = "YOU WIN!";
        victoryScoreText.text = "Your Score: " + score;
        victoryHighScoreText.text = "High Score: " + highScore;
    }

    public void ShowStart(int currentLevel, float levelDuration)
    {
        if (levelStartPanel != null)
        {
            levelStartPanel.SetActive(true);

            if (startLevel != null)
            {
                startLevel.text = $"Level {currentLevel}";
            }

            if (startTime != null)
            {
                startTime.text = $"Duration: {levelDuration} seconds";
            }
            
        }
        StartCoroutine(HideLevelStartPanel());

    }

    private IEnumerator HideLevelStartPanel()
    {
        yield return new WaitForSeconds(3f);
        if(levelStartPanel != null)
        { 
            levelStartPanel.SetActive(false);
        }
    }

    public void RestartGame()
    {
        // Reset Time Scale
        Time.timeScale = 1f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
