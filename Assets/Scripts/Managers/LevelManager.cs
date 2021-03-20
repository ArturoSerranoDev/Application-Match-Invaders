using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    
    public delegate void OnGameWon();
    public static event OnGameWon onGameWon;
    public delegate void OnGameLost();
    public static event OnGameLost onGameLost;
    
    public int Score { get; private set; }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void IncrementScore()
    {
        Score++;
    }

    public void Reset()
    {
        Score = 0;
        // reset logic
    }
}
