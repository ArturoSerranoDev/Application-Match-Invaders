using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField] ChapterConfig chapterConfig;

    [SerializeField] LevelBuilder levelBuilder;
    [SerializeField] EnemyMoveManager enemyMoveManager;
    [SerializeField] EnemyShootManager enemyShootManager;
    
    public delegate void OnGameStart();
    public static event OnGameStart onGameStart;
    public delegate void OnGameWon();
    public static event OnGameWon onGameWon;
    public delegate void OnGameLost();
    public static event OnGameLost onGameLost;
    
    public static int Score { get; private set; }
    public static int CurrentLevel { get; private set; }

    int enemiesCount;
    void Start()
    {
        // TODO: Save/load check
        
        LoadLevel();
    }

    public void LoadFromMenu()
    {
        // Get Last level Saved
    }

    public void LoadLevel()
    {
        LevelConfig levelConfig = chapterConfig.levels[CurrentLevel];

        List<Enemy> newEnemyList = levelBuilder.BuildLevel(levelConfig);
        enemiesCount = newEnemyList.Count;

        // Init Controllers
        enemyMoveManager.Init(levelConfig.enemyConfig, newEnemyList);
        enemyShootManager.Init(levelConfig, newEnemyList);
        
        // Callbacks
        levelBuilder.Player.onPlayerHit += OnPlayerHit;
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
    }
    
    public void Win()
    {
        Debug.Log("LevelManager: You Won!!");
        onGameWon?.Invoke();
    }

    public void Lose()
    {
        Debug.Log("LevelManager: You Lost!!");
        onGameLost?.Invoke();
    }
    
    public void Reset()
    {
        Score = 0;
        // reset logic
    }

    public static void AddScore(int deaths)
    {
        Score += ScoreCalculator.GetScorePerKill(deaths);
        
        Debug.Log("LevelManager: Score is " + Score.ToString() + " after last kill granted "+ ScoreCalculator.GetScorePerKill(deaths));
    }
}
