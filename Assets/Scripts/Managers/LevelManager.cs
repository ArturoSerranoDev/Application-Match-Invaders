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
        // Get Level config from chapter
        LevelConfig levelConfig = chapterConfig.levels[CurrentLevel];

        levelBuilder.BuildLevel(levelConfig);

        levelBuilder.Player.onPlayerHit += OnPlayerHit;
        
        enemyMoveManager.Init(levelConfig.enemyConfig, levelBuilder.enemyList);
        enemyShootManager.Init(levelConfig, levelBuilder.enemyList);
        
        StartCoroutine(StartLevelCoroutine());
    }

    private IEnumerator StartLevelCoroutine()
    {
        // Animation showing player, then bunker, then enemies in rows
        yield return new WaitForEndOfFrame();
        
        onGameStart?.Invoke();
        
    }

    public void OnPlayerHit(int lives)
    {
        if(lives <= 0)
            Lose();
    }

    public void Win()
    {
        onGameWon?.Invoke();
    }

    public void Lose()
    {
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
