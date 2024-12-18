using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance; // Singleton for global access

    [Header("Level Settings")]
    public int currentLevel = 1;         // Current level number
    public int maxLevels = 3;
    public float levelDuration = 30f;   // Time to survive for each level
    public float enemySpawnRate = 2f;   // Spawn rate in seconds
    public int enemiesPerSpawn = 1;     // Enemies spawned at once

    private float levelTimer = 0f;      // Timer for level duration
    private bool isGameOver = false;

    [Header("UI Elements")]
    public UnityEngine.UI.Text levelText;
    public UnityEngine.UI.Text timerText;

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
    }

    public void Start()
    {
        StartLevel();
        GameManager.Instance.ShowStart(currentLevel, levelDuration);
        Debug.Log($"Starting Level {currentLevel}: Duration {levelDuration}s, Spawn Rate {enemySpawnRate}s, Enemies Per Spawn {enemiesPerSpawn}");


    }

    public void Update()
    {
        if (isGameOver) return;
        {
            
        }
        levelTimer -= Time.deltaTime;

        // Check if the current level is complete
        if (levelTimer <= 0)
        {
            NextLevel();
        }
    }
    public void StartLevel()
    {
        // Adjust spawn rate with a logarithmic curve
        float baseSpawnRate = 5f;
        float minSpawnRate = 0.5f;
        float newSpawnRate = Mathf.Max(baseSpawnRate / Mathf.Log(currentLevel + 1, 2), minSpawnRate);
        Main.Instance.SetSpawnRate(newSpawnRate);

        // Adjust enemy count with square root growth
        int baseEnemyCount = 1;
        int newEnemyCount = baseEnemyCount + Mathf.FloorToInt(Mathf.Sqrt(currentLevel));
        Main.Instance.UpdateEnemyCount(newEnemyCount);

        // Adjust level duration
        float baseDuration = 30f;
        float durationIncrease = 5f;
        levelDuration = 30f + (5f * currentLevel);

        Debug.Log($"Starting Level {currentLevel}: Duration {levelDuration}s, Spawn Rate {newSpawnRate}s, Enemies Per Spawn {newEnemyCount}");



        levelTimer = levelDuration;
        Main.Instance.SetSpawnRate(newSpawnRate); // Update spawn rate in Main
        Main.Instance.UpdateEnemyCount(newEnemyCount);
        GameManager.Instance.ShowStart(currentLevel, levelDuration);
        Debug.Log($"Starting Level {currentLevel}: Duration {levelDuration}s, Spawn Rate {enemySpawnRate}s, Enemies Per Spawn {enemiesPerSpawn}");
        
    }

    private void NextLevel()
    {
        if (currentLevel >= maxLevels)
        {
            EndGame();
            return;
        }


        currentLevel++;
        float baseSpawnRate = 6f;
        float minSpawnRate = 0.5f;
        enemySpawnRate = Mathf.Max(baseSpawnRate / Mathf.Log(currentLevel + 1, 2), minSpawnRate);
        enemiesPerSpawn = 1 + Mathf.FloorToInt(Mathf.Sqrt(currentLevel));
        levelDuration = 30f + (5f * currentLevel);
        StartLevel();                // Restart the timer for the next level

        Main.Instance.SetSpawnRate(enemySpawnRate);
        Main.Instance.UpdateEnemyCount(enemiesPerSpawn); // Adjust enemies per spawn
        Debug.Log($"Level {currentLevel} started! Spawn Rate: {enemySpawnRate}, Enemies per Spawn: {enemiesPerSpawn}");
    }

    private void EndGame()
    {
        if (isGameOver) return;
        isGameOver = true;
        int score = Score.Instance.GetScore();
        int highScore = Score.Instance.GetHighScore();

        Main.Instance.spawnEnemies = false;
        GameManager.Instance.ShowVictory(score, highScore);
        Debug.Log("You survived all levels! Game Over.");

    }



}
