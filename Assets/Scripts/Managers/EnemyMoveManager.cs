// ----------------------------------------------------------------------------
// EnemyMoveManager.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Handles movement of enemies
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveManager : MonoBehaviour
{
    [SerializeField] float boundaryLimit;
    public List<Enemy> enemies = new List<Enemy>();

    Vector3 movementDir = Vector3.right;
    Coroutine movementCoroutine;
    bool isMovementEnabled;
    
    float speedStep;
    float moveSpeed;
    
    void Awake()
    {
        LevelManager.onGameStart += EnableMovement;
        LevelManager.onGameWon += DisableMovement;
        LevelManager.onGameLost += DisableMovement;
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
            foreach (Enemy enemy in enemies)
            {
                enemy.Move(movementDir);

                if (IsEnemyOverLimit(enemy.transform))
                    hasToChangeEnemyDir = true;

                yield return new WaitForSeconds(1 / moveSpeed);
            }

            if (hasToChangeEnemyDir)
            {
                foreach (Enemy enemy in enemies)
                {
                    enemy.MoveDown();
                    yield return new WaitForSeconds(1 / moveSpeed);
                }
                
                movementDir = -movementDir;
            }
        }
    }

    bool IsEnemyOverLimit(Transform enemyTransform)
    {
        return Mathf.Abs(enemyTransform.position.x) > boundaryLimit;
    }
}