using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    [Header("Game Panel")] 
    [SerializeField] Text playScoreText;
    [SerializeField] GameObject newHighScoreTextGO;
    [SerializeField] List<GameObject> livesImage;
    
    [Header("Pause Panel")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] Text pauseHighScoreText;
    [SerializeField] Text pauseScoreText;
    
    [Header("Win/Lose Panel")]
    [SerializeField] GameObject winLosePanel;
    [SerializeField] GameObject goToNextLevelButtonGO;
    [SerializeField] GameObject restartLevelButtonGO;
    [SerializeField] Text endHighScoreText;
    [SerializeField] Text endScoreText;
    [SerializeField] Text endTitleText;
    
    public void Init(int playerLives)
    {
        foreach (GameObject livesImage in livesImage)
        {
            livesImage.SetActive(false);
        }

        for (int i = 0; i < playerLives; i++)
        {
            livesImage[i].SetActive(true);
        }

        playScoreText.text = string.Empty;
        LevelManager.onGameWon += OnGameWon;
        LevelManager.onGameLost += OnGameLost;
        LevelManager.onGamePaused += PauseGame;
        LevelManager.onHighScoreReached += OnHighScoreReached;
    }
    
    void PauseGame(bool isPaused)
    {
        if (winLosePanel.activeInHierarchy)
            return;
        
        pausePanel.SetActive(isPaused);
        pauseHighScoreText.text = "High Score: " + LevelManager.HighScore;
        pauseScoreText.text = "Your Score: " + LevelManager.Score;
    }
    
    void OnGameWon()
    {
        ShowEndLevelScreen(isVictory: true);
    }
    
    void OnGameLost()
    {
        ShowEndLevelScreen(isVictory: false);
    }

    public void OnPlayerHit(int lives)
    {
        livesImage[lives].SetActive(false);
    }
    
    void OnHighScoreReached(int highScore)
    {
        StartCoroutine(PlayHighScoreAnimation());
    }

    IEnumerator PlayHighScoreAnimation()
    {
        yield return new WaitForEndOfFrame();
    }
    

    public void ShowEndLevelScreen(bool isVictory)
    {
        winLosePanel.SetActive(true);
        
        goToNextLevelButtonGO.SetActive(isVictory);
        restartLevelButtonGO.SetActive(!isVictory);
        
        endHighScoreText.text = "High Score: " + LevelManager.HighScore;
        endScoreText.text = "Your Score: " + LevelManager.Score;
        endTitleText.text = isVictory ? "VICTORY" : "DEFEAT";
    }

    public void UpdateScore(int score)
    {
        playScoreText.text = score.ToString();
    }
    public void ShowEndChapterScreen()
    {
        winLosePanel.SetActive(true);
    }

    public void OnGameStart()
    {
        pausePanel.SetActive(false);
        winLosePanel.SetActive(false);
        newHighScoreTextGO.SetActive(false);
    }
}
