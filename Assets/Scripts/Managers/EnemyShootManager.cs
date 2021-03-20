// ----------------------------------------------------------------------------
// EnemyShootManager.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Handles shooting of enemies
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootManager : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public float minTimeToShoot;
    public float maxTimeToShoot;
    public int enemyMaxBullets;

    Dictionary<int, List<Enemy>> enemiesPerColumn = new Dictionary<int, List<Enemy>>();

    int enemiesPerRow;
    bool isShootingEnabled;

    void Awake()
    {
        LevelManager.onGameStart += EnableShooting;
        LevelManager.onGameWon += DisableShooting;
        LevelManager.onGameLost += DisableShooting;
    }

    public void Init(LevelConfig levelConfig, List<Enemy> enemyList)
    {
        enemiesPerRow = levelConfig.enemiesPerRow;
        
        foreach (Enemy enemy in enemyList)
        {
            int horizontalIndex = enemy.GetVectorIndex().y;

            if(!enemiesPerColumn.ContainsKey(horizontalIndex))
                enemiesPerColumn.Add(horizontalIndex, new List<Enemy>()); 

            enemiesPerColumn[enemy.GetVectorIndex().y].Add(enemy);
            
            enemy.onEnemyKilled += OnEnemyKilled;
        }
    }

    void OnEnemyKilled(Enemy enemy)
    {
        enemiesPerColumn[enemy.GetVectorIndex().y].Remove(enemy);
    }
    
    IEnumerator StartShootingColumnCoroutine(int index)
    {
        while (isShootingEnabled)
        {
            float randomWait = Random.Range(minTimeToShoot, maxTimeToShoot);

            if (PoolManager.Instance.GetActiveMembersCount(enemyBulletPrefab) <= enemyMaxBullets - 1)
            {
                Enemy closestEnemy = null;
                closestEnemy = GetClosesEnemy(index, closestEnemy);

                if(closestEnemy != null)
                    closestEnemy.Shoot();
            }
            
            yield return new WaitForSeconds(randomWait);
        }
    }

    Enemy GetClosesEnemy(int index, Enemy closestEnemy)
    {
        int lowestIndex = 100;

        foreach (Enemy enemy in enemiesPerColumn[index])
        {
            if (enemy.GetVectorIndex().x < lowestIndex)
            {
                closestEnemy = enemy;
                lowestIndex = enemy.GetVectorIndex().x;
            }
        }

        return closestEnemy;
    }

    void EnableShooting()
    {
        isShootingEnabled = true;
        
        for (int i = 0; i < enemiesPerRow; i++)
        {
            StartCoroutine(StartShootingColumnCoroutine(index: i));
        }
    }

    void DisableShooting()
    {
        isShootingEnabled = false;
        
        StopAllCoroutines();
    }
}
