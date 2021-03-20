using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveManager : MonoBehaviour
{
    public float boundaryLimit;
    List<Enemy> enemies = new List<Enemy>();

    Vector3 movementDir = Vector3.right;
    float speed = 10;
    public bool isMovementEnabled;
    
    void Awake()
    {
        LevelManager.onGameStart += EnableMovement;
        LevelManager.onGameWon += DisableMovement;
        LevelManager.onGameLost += DisableMovement;
    }

    void EnableMovement()
    {
        isMovementEnabled = true;

        //StartCoroutine(MovementCoroutine());
    }
    
    void DisableMovement()
    {
        isMovementEnabled = false;
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

                    yield return new WaitForSeconds(1/speed);
            }
            
            if (hasToChangeEnemyDir)
            {
                yield return new WaitForSeconds(1/speed);
                foreach (Enemy enemy in enemies)
                {
                    enemy.MoveDown();
                    movementDir = -movementDir;
                }

            }
        }
      
        
    }

    bool IsEnemyOverLimit(Transform enemyTransform)
    {
        return Mathf.Abs(enemyTransform.position.x) > boundaryLimit;
    }
}
