using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField] ChapterConfig chapterConfig;

    [SerializeField] LevelBuilder levelBuilder;
    
    public delegate void OnGameStart();
    public static event OnGameStart onGameStart;
    public delegate void OnGameWon();
    public static event OnGameWon onGameWon;
    public delegate void OnGameLost();
    public static event OnGameLost onGameLost;
    
    public int Score { get; private set; }
    public int CurrentLevel { get; private set; }


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
        
        StartCoroutine(StartLevelCoroutine());
    }

    private IEnumerator StartLevelCoroutine()
    {
        // Animation showing player, then bunker, then enemies in rows
        yield return new WaitForEndOfFrame();
    }

    public void Win()
    {
        onGameWon?.Invoke();
    }

    public void Lose()
    {
        onGameLost?.Invoke();
    }


    public void IncrementScore()
    {
        Score++;
        
        // TODO: Score calculation
    }

    public void Reset()
    {
        Score = 0;
        // reset logic
    }
}
