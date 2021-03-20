using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    public int lives = 5;

    public void Init()
    {
        lives = 5;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            lives -= 1;
        
            if(lives <= 0)
                Despawn();
        }
    }

    void Despawn()
    {
        PoolManager.Instance.Despawn(this.gameObject);
    }
}
