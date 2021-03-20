using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    const int numberOfBunkers = 4;
    
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

    Enemy[,] enemies;
    

    public void BuildLevel(LevelConfig levelConfig)
    {
        // Player
        GameObject player = PoolManager.Instance.Spawn(playerPrefab, playerStartingPos, Quaternion.identity);
        
        MainCharacter playerManager = player.GetComponent<MainCharacter>();
        playerManager.SetData(levelConfig.playerConfig);

        // Enemies
        enemies = new Enemy[levelConfig.enemiesPerColumn, levelConfig.enemiesPerRow];
        
        for (int i = 0; i < levelConfig.enemiesPerColumn; i++)
        {
            for (int j = 0; j < levelConfig.enemiesPerRow; j++)
            {
                float horizontalStep = (enemyEndPos.x - enemyStartingPos.x) / (levelConfig.enemiesPerRow - 1);
                float verticalStep = (enemyEndPos.y - enemyStartingPos.y) / (levelConfig.enemiesPerColumn - 1);
                Debug.Log("Horizontal Step: " + horizontalStep + ", Vertical Step: " + verticalStep);
                
                Vector3 enemyPos = enemyStartingPos + new Vector3(horizontalStep * j, verticalStep * i);
                
                GameObject newEnemyGO = PoolManager.Instance.Spawn(enemyPrefab, enemyPos, Quaternion.identity);

                Enemy newEnemy = newEnemyGO.GetComponent<Enemy>();
                newEnemy.SetData(levelConfig.enemyConfig);

                // Add to array for easier handling of neighbours
                enemies[i,j] = newEnemy;
            }
        }

        // Bunkers
        for (int i = 0; i < numberOfBunkers; i++)
        {
            float horizontalStep = bunkerStartingPos.x * 2 / (numberOfBunkers - 1);
            
            Vector3 bunkerPos = bunkerStartingPos - new Vector3(horizontalStep * i, 0);
            
            GameObject newBunker = PoolManager.Instance.Spawn(bunkerPrefab, bunkerPos, Quaternion.identity);
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
    }
}
