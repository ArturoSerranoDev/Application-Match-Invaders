using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject bunkerPrefab;
    
    [Header("Positions")]
    public Vector3 playerStartingPos;
    public Vector3 enemyStartingPos;
    public Vector3 bunkerStartingPos;
    
    public void BuildLevel(LevelConfig levelConfig)
    {
        // Get/Instantiate Player at starting Pos
        
        // Load enemies in their pos using pool
        
        // Load Bunkers with pool at their pos
    }
}
