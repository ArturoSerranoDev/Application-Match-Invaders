// ----------------------------------------------------------------------------
// EnemyMoveManager.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Handles movement of enemies
// ----------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveManager : MonoBehaviour
{
    [SerializeField] float horizontalBoundary;
    [SerializeField] float verticalBoundary;
    List<Enemy> enemies = new List<Enemy>();

    Vector3 movementDir = Vector3.right;
    Coroutine movementCoroutine;
    bool isMovementEnabled;
    
    float speedStep;
    float moveSpeed;
    
    public delegate void OnEnemyReachedLimit();
    public event OnEnemyReachedLimit onEnemyReachedLimit;
    
    void OnEnable()
    {
        LevelManager.onGameStart += EnableMovement;
        LevelManager.onGameWon += DisableMovement;
        LevelManager.onGameLost += DisableMovement;
    }

    private void OnDisable()
    {
        LevelManager.onGameStart -= EnableMovement;
        LevelManager.onGameWon -= DisableMovement;
        LevelManager.onGameLost -= DisableMovement;
    }

    public void Init(EnemyConfig enemyConfig, List<Enemy> enemyList)
    {
        moveSpeed = enemyConfig.minSpeed;
        speedStep = (enemyConfig.maxSpeed - enemyConfig.minSpeed) / enemyList.Count;

        foreach (Enemy enemy in enemyList)
        {
            enemy.onEnemyKilled += OnEnemyKilled;
        }

        enemies = enemyList;
    }

    void OnEnemyKilled(Enemy enemy)
    {
        moveSpeed += speedStep;
        enemies.Remove(enemy);
    }

    void EnableMovement()
    {
        isMovementEnabled = true;

        Debug.Log("EnemyMoveManager: Starting Movement of Enemies");

        SetupMovement();
    }

    void SetupMovement()
    {
        movementCoroutine = StartCoroutine(MovementCoroutine());
    }

    void DisableMovement()
    {
        isMovementEnabled = false;
        StopCoroutine(movementCoroutine);
    }
    
    IEnumerator MovementCoroutine()
    {
        while (isMovementEnabled)
        {
            bool hasToChangeEnemyDir = false;
            for (var i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];
                enemy.Move(movementDir);

                if (IsEnemyOverHorizontalLimit(enemy.transform))
                    hasToChangeEnemyDir = true;

                yield return new WaitForSeconds(1 / moveSpeed);
            }

            if (hasToChangeEnemyDir)
            {
                for (var i = 0; i < enemies.Count; i++)
                {
                    Enemy enemy = enemies[i];
                    enemy.MoveDown();

                    // Lose condition
                    if (IsEnemyOverVerticalLimit(enemy.transform))
                    {
                        onEnemyReachedLimit?.Invoke();
                    }

                    yield return new WaitForSeconds(1 / moveSpeed);
                }


                movementDir = -movementDir;
            }
        }
    }

    bool IsEnemyOverHorizontalLimit(Transform enemyTransform)
    {
        return Mathf.Abs(enemyTransform.position.x) > horizontalBoundary;
    }
    bool IsEnemyOverVerticalLimit(Transform enemyTransform)
    {
        return enemyTransform.position.y < verticalBoundary;
    }
    
    public void Reset()
    {
        enemies.Clear();
    }
}