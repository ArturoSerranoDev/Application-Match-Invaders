using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveStep;
    [SerializeField] float moveDownStep;
    
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

    public void Move(Vector3 direction)
    {
        transform.position += direction * moveStep;
    }

    public void MoveDown()
    {
        transform.position += Vector3.down * moveDownStep;
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
