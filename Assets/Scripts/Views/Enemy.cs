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
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootEndPoint;

    [SerializeField] float moveStep;
    [SerializeField] float moveDownStep;
    
    public List<Enemy> neighbours = new List<Enemy>();
    public SpriteRenderer enemySpriteRenderer;
    
    public delegate void OnEnemyKilled(Enemy enemy);
    public event OnEnemyKilled onEnemyKilled;
    
    EnemyData data;
    
    public void SetData(EnemyConfig config)
    {
        data = new EnemyData();
        data.lives = config.lives;
        data.bulletSpeed = config.bulletSpeed;
        
        SetRandomColor(config);
    }

    void SetRandomColor(EnemyConfig config)
    {
        int randomColorIndex = Random.Range(0, config.numberOfColors);
        
        data.colorIndex = randomColorIndex;
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
        newBullet.GetComponent<Bullet>().Init(data.bulletSpeed,Vector3.down);
        
        // TODO: Play SFX
    }
    
    public void OnNeighbourKilled(Enemy enemyNeighbour)
    {
        neighbours.Remove(enemyNeighbour);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        data.lives -= 1;
        
        if (data.lives <= 0)
            Die(isFirstDeath: true);
    }

    void Die(bool isFirstDeath)
    {
        foreach (Enemy neighbour in neighbours)
        {
            neighbour.OnNeighbourKilled(this);

            if(data.colorIndex == neighbour.data.colorIndex &&
               isFirstDeath)
                neighbour.Die(isFirstDeath: false);
        }

        onEnemyKilled?.Invoke(this);
        Despawn();
    }

    public void Despawn()
    {
        PoolManager.Instance.Despawn(this.gameObject);
    }
}
