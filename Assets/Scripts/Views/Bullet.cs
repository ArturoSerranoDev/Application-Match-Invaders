// ----------------------------------------------------------------------------
// Bullet.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Controls bullet behaviour. Moves it forward at a constant rate
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed;

    public void Init(float speed, Vector3 rot)
    {
        this.speed = speed;
        this.transform.eulerAngles = rot;
    }
    
    void Update()
    {
        this.transform.position += transform.up * speed * Time.deltaTime;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        Despawn();
    }

    void Despawn()
    {
        PoolManager.Instance.Despawn(this.gameObject);
    }
}
