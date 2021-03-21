using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    const int closeToHighScore = 30;

    [Header("Managers")]
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
    public delegate void OnCloseToHighScoreReached();
    public static event OnCloseToHighScoreReached onCloseToHighScoreReached;
    
    public static int Score { get; private set; }
    public static int HighScore { get; private set; }
    static int CurrentLevel { get; set; }
    static bool isHighScoreReached;

    bool isPaused;
    int enemiesCount;
    
    void Start()
    {
        HighScore = SaveLoadController.Instance.GetLatestHighScore();
        Score = 0;
        isHighScoreReached = false;
        
        Reset();
        LoadLevel();
    }

    void LoadLevel()
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

    IEnumerator StartLevelCoroutine()
    {
        yield return StartCoroutine(levelBuilder.StartLevelAnimation());
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
        SaveHighScore();

        Time.timeScale = 1f;
        Score = 0;
        CurrentLevel = 0;
        
        GameManager.Instance.GoToHomeMenu();
    }
    
    public void GoToNextLevel()
    {
        Reset();
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
    
    void Win()
    {
        Debug.Log("LevelManager: You Won!!");
        Debug.Log(chapterConfig.GetNumberOfLevels());
        onGameWon?.Invoke();
        CurrentLevel += 1;

        // End Game
        if (CurrentLevel >= chapterConfig.GetNumberOfLevels())
        {
            uiGameManager.ShowEndChapterScreen();
            SFXPlayer.Instance.PlaySFX(SFXPlayer.Instance.endGameTheme,GetComponent<AudioSource>(),0.5f,1);
        }
        else
            SFXPlayer.Instance.PlaySFX(SFXPlayer.Instance.victoryTheme,GetComponent<AudioSource>(),0.5f,1);

        
        SaveHighScore();
        
    }

    void Lose()
    {
        Debug.Log("LevelManager: You Lost!!");
        onGameLost?.Invoke();
        
        SaveHighScore();
        
        SFXPlayer.Instance.PlaySFX(SFXPlayer.Instance.defeatTheme,GetComponent<AudioSource>(),0.5f,1);
    }

    public void ResetButtonPressed()
    {
        Score = 0;
        isHighScoreReached = false;
        
        Reset();
        LoadLevel();
    }
    public void Reset()
    {
        SaveHighScore();
        
        levelBuilder.Reset();
        enemyMoveManager.Reset();
        enemyShootManager.Reset();
        PoolManager.Instance.Reset();
        
        uiGameManager.OnGameStart();
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

        if (isHighScoreReached)
        {
            HighScore = Score;
            return;
        }
        
        if (Score > HighScore)
        {
            onHighScoreReached?.Invoke(Score);
            isHighScoreReached = true;
        }
        else if (Score + closeToHighScore > HighScore)
        {
            onCloseToHighScoreReached?.Invoke();
        }
    }

   
}
