// ----------------------------------------------------------------------------
// Enemy.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Controls the view and events fired related to the enemy
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : Ship
{
    public Transform shootEndPoint; 
    public GameObject bulletPrefab;

    [SerializeField] float moveStep = 0.15f;
    [SerializeField] float moveDownStep= 0.5f;
    
    public List<Enemy> neighbours = new List<Enemy>();
    public SpriteRenderer enemySpriteRenderer;
    
    public delegate void OnEnemyKilled(Enemy enemy);
    public event OnEnemyKilled onEnemyKilled;
    
    EnemyData enemyData;
    
    public void SetData(EnemyConfig config, Vector2Int index)
    {
        enemyData = new EnemyData();
        enemyData.lives = config.lives;
        enemyData.bulletSpeed = config.bulletSpeed;
        enemyData.indexPos = index;
        
        SetRandomColor(config);
    }

    void SetRandomColor(EnemyConfig config)
    {
        int randomColorIndex = Random.Range(0, config.numberOfColors);
        
        enemyData.colorIndex = randomColorIndex;
        enemySpriteRenderer.color = config.availableColors[randomColorIndex];
    }

    public void Move(Vector3 direction)
    {
        transform.position += direction * moveStep;
    }

    public void MoveDown()
    {
        transform.position += Vector3.down * moveDownStep;
    }

    public override void Shoot()
    {
        GameObject newBullet = PoolManager.Instance.Spawn(bulletPrefab, shootEndPoint.position, Quaternion.identity);
        
        // Rotate bullet downwards
        newBullet.GetComponent<Bullet>().Init(enemyData.bulletSpeed,Vector3.right * 180f);
        
        // TODO: Play SFX
    }
    
    public void OnNeighbourKilled(Enemy enemyNeighbour)
    {
        neighbours.Remove(enemyNeighbour);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerBullet"))
            return;
        
        enemyData.lives -= 1;
        
        if (enemyData.lives <= 0)
            Die(isFirstDeath: true);
    }

    void Die(bool isFirstDeath)
    {
        int deaths = 1;
        foreach (Enemy neighbour in neighbours)
        {
            neighbour.OnNeighbourKilled(this);

            if (enemyData.colorIndex == neighbour.enemyData.colorIndex &&
                isFirstDeath)
            {
                neighbour.Die(isFirstDeath: false);
                deaths++;
            }
        }

        if (isFirstDeath)
            LevelManager.AddScore(deaths);
        
        onEnemyKilled?.Invoke(this);
        Despawn();
    }

    public void Despawn()
    {
        PoolManager.Instance.Despawn(this.gameObject);
    }

    public Vector2Int GetVectorIndex()
    {
        return enemyData.indexPos;
    }
}
