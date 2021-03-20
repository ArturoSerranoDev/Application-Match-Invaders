using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<Enemy> neighbours = new List<Enemy>();
    public SpriteRenderer EnemySpriteRenderer;
    
    public delegate void OnEnemyKilled(Enemy enemy);
    public event OnEnemyKilled onEnemyKilled;
    
    EnemyData data;
    
    public void SetData(EnemyConfig config)
    {
        data = new EnemyData();
        data.lives = config.lives;

        SetRandomColor(config);
    }

    void SetRandomColor(EnemyConfig config)
    {
        int randomColorIndex = Random.Range(0, config.numberOfColors);
        
        data.colorIndex = randomColorIndex;
        EnemySpriteRenderer.color = config.availableColors[randomColorIndex];
    }

    public void Move()
    {
        
    }

    public void Shoot()
    {
        
    }
    
    public void Despawn()
    {
        PoolManager.Instance.Despawn(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Despawn();
    }
}
