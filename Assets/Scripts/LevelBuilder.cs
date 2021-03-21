﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    
    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject bunkerPrefab;
    public GameObject bulletPrefab;
    
    [Header("Positions")]
    public Vector3 playerStartingPos;
    public Vector3 enemyStartingPos;
    public Vector3 enemyEndPos;
    public Vector3 bunkerStartingPos;

    public MainCharacter Player { get; private set; }
    
    List<Enemy> enemyList = new List<Enemy>();
    List<Bunker> bunkerList = new List<Bunker>();
    Enemy[,] enemies;
    const int numberOfBunkers = 4;
    
    public List<Enemy> BuildLevel(LevelConfig levelConfig)
    {
        // Player
        GameObject player = PoolManager.Instance.Spawn(playerPrefab, playerStartingPos, Quaternion.identity);
        MainCharacter playerManager = player.GetComponent<MainCharacter>();
        
        if(levelConfig.playerConfig != null)
            playerManager.SetData(levelConfig.playerConfig);

        Player = playerManager;

        // Enemies
        enemies = new Enemy[levelConfig.enemiesPerColumn, levelConfig.enemiesPerRow];
        
        float horizontalStep = (enemyEndPos.x - enemyStartingPos.x) / (levelConfig.enemiesPerRow - 1);
        float verticalStep = (enemyEndPos.y - enemyStartingPos.y) / (levelConfig.enemiesPerColumn - 1);
        Debug.Log("Horizontal Step: " + horizontalStep + ", Vertical Step: " + verticalStep);
        
        for (int i = 0; i < levelConfig.enemiesPerColumn; i++)
        {
            for (int j = 0; j < levelConfig.enemiesPerRow; j++)
            {
                Vector3 enemyPos = enemyStartingPos + new Vector3(horizontalStep * j, verticalStep * i);
                
                GameObject newEnemyGO = PoolManager.Instance.Spawn(enemyPrefab, enemyPos, Quaternion.identity);

                Enemy newEnemy = newEnemyGO.GetComponent<Enemy>();
                
                if(levelConfig.enemyConfig != null)
                    newEnemy.SetData(levelConfig.enemyConfig, new Vector2Int(i,j));

                // Add to array for easier handling of neighbours
                enemies[i,j] = newEnemy;
                enemyList.Add(newEnemy);
            }
        }

        horizontalStep = bunkerStartingPos.x * 2 / (numberOfBunkers - 1);
        // Bunkers
        for (int i = 0; i < numberOfBunkers; i++)
        {
            Vector3 bunkerPos = bunkerStartingPos - new Vector3(horizontalStep * i, 0);
            
            GameObject newBunkerGO = PoolManager.Instance.Spawn(bunkerPrefab, bunkerPos, Quaternion.identity);
            Bunker newBunker = newBunkerGO.GetComponent<Bunker>();
            newBunker.Init();
            bunkerList.Add(newBunker);
        }
        
        // Set neighbours of enemies
        for (int i = 0; i < levelConfig.enemiesPerColumn; i++)
        {
            for (int j = 0; j < levelConfig.enemiesPerRow; j++)
            {
                if (i > 0)
                    enemies[i,j].neighbours.Add(enemies[i - 1,j]);
                if (i < levelConfig.enemiesPerColumn - 1)
                    enemies[i,j].neighbours.Add(enemies[i + 1,j]);
                if (j > 0)
                    enemies[i,j].neighbours.Add(enemies[i,j - 1]);
                if (j < levelConfig.enemiesPerRow - 1)
                    enemies[i,j].neighbours.Add(enemies[i,j + 1]);
            }
        }

        return enemyList;
    }

    public void Reset()
    {
        foreach (Enemy enemy in enemyList)
        {
            enemy.Despawn();
        }
        
        foreach (Bunker bunker in bunkerList)
        {
            bunker.Despawn();
        }
        
        Player?.Despawn();
    }
}
