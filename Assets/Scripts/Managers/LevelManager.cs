using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField] UIGameManager uiGameManager;
    [SerializeField] LevelBuilder levelBuilder;
    [SerializeField] EnemyMoveManager enemyMoveManager;
    [SerializeField] EnemyShootManager enemyShootManager;
    
    [SerializeField] ChapterConfig chapterConfig;

    
    public delegate void OnGameStart();
    public static event OnGameStart onGameStart;
    public delegate void OnGamePaused(bool isPaused);
    public static event OnGamePaused onGamePaused;
    public delegate void OnGameWon();
    public static event OnGameWon onGameWon;
    public delegate void OnGameLost();
    public static event OnGameLost onGameLost;
    public delegate void OnHighScoreReached(int score);
    public static event OnHighScoreReached onHighScoreReached;
    
    public static int Score { get; private set; }
    public static int HighScore { get; private set; }
    public static int CurrentLevel { get; private set; }

    bool isPaused;
    int enemiesCount;
    void Start()
    {
        // TODO: Save/load check
        HighScore = SaveLoadController.Instance.GetLatestHighScore();
        
        LoadLevel();
    }

    public void LoadLevel()
    {
        LevelConfig levelConfig = chapterConfig.levels[CurrentLevel];

        List<Enemy> newEnemyList = levelBuilder.BuildLevel(levelConfig);
        enemiesCount = newEnemyList.Count;

        // Init Controllers
        enemyMoveManager.Init(levelConfig.enemyConfig, newEnemyList);
        enemyShootManager.Init(levelConfig, newEnemyList);
        uiGameManager.Init(levelConfig.playerConfig.lives);
        
        // Callbacks
        levelBuilder.Player.onPlayerHit += OnPlayerHit;
        levelBuilder.Player.onPlayerHit += uiGameManager.OnPlayerHit;
        enemyMoveManager.onEnemyReachedLimit += Lose;
        
        foreach (Enemy enemy in newEnemyList)
        {
            enemy.onEnemyKilled += OnEnemyKilled;
        }
        
        StartCoroutine(StartLevelCoroutine());
    }

    private IEnumerator StartLevelCoroutine()
    {
        // Animation showing player, then bunker, then enemies in rows
        yield return new WaitForEndOfFrame();
        
        onGameStart?.Invoke();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0f : 1f;
        onGamePaused?.Invoke(isPaused);
        
        if (isPaused)
            SaveHighScore();
    }

    public void GoToMainMenu()
    {
        GameManager.Instance.GoToHomeMenu();
    }
    
    public void GoToNextLevel()
    {
        CurrentLevel += 1;
        LoadLevel();

        uiGameManager.OnGameStart();
    }

    void OnPlayerHit(int lives)
    {
        // Lose Condition
        if(lives <= 0)
            Lose();
    }
    
    void OnEnemyKilled(Enemy enemy)
    {
        enemiesCount -= 1;
        
        // Win Condition
        if (enemiesCount <= 0)
            Win();
        
        uiGameManager.UpdateScore(Score);
    }
    
    public void Win()
    {
        Debug.Log("LevelManager: You Won!!");
        onGameWon?.Invoke();

        SaveHighScore();
    }

    public void Lose()
    {
        Debug.Log("LevelManager: You Lost!!");
        onGameLost?.Invoke();
        
        SaveHighScore();
    }
    
    public void Reset()
    {
        SaveHighScore();
        
        Score = 0;
        // reset logic
        levelBuilder.Reset();
        LoadLevel();
    }
    void SaveHighScore()
    {
        if (Score > HighScore)
        {
            SaveLoadController.Instance.SaveHighScore(Score);
        }
    }

    public void OnApplicationPause(bool pauseStatus)
    {
        SaveHighScore();
    }
    
    public void OnApplicationQuit()
    {
        SaveHighScore();
    }

    public static void AddScore(int deaths)
    {
        Score += ScoreCalculator.GetScorePerKill(deaths);
        
        Debug.Log("LevelManager: Score is " + Score.ToString() + " after last kill granted "+ ScoreCalculator.GetScorePerKill(deaths) + " Points!");
        
        if (Score > HighScore)
        {
            onHighScoreReached?.Invoke(Score);
        }
    }

   
}
