// ----------------------------------------------------------------------------
// Bunker.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Controls Bunker behaviour.
// ----------------------------------------------------------------------------
using UnityEngine;

public class Bunker : MonoBehaviour
{
    [SerializeField] int lives = 5;

    public void Init()
    {
        lives = 5;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            lives -= 1;

            if (lives <= 0)
            {
                SFXPlayer.Instance.PlaySFX(SFXPlayer.Instance.enemyDestroyed,SFXPlayer.Instance.musicSource,0.5f,Random.Range(0.1f,0.2f));
                Despawn();
            }
        }
    }

    public void Despawn()
    {
        PoolManager.Instance.Despawn(this.gameObject);
    }
    
    public int GetLives()
    {
        return lives;
    }
}
