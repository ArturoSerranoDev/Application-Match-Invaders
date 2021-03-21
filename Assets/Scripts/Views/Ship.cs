// ----------------------------------------------------------------------------
// Ship.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Parent class that implements method used by different ships
// ----------------------------------------------------------------------------
using UnityEngine;

public class Ship : MonoBehaviour
{
    public virtual void Shoot(){}
    public virtual void Move(Vector3 direction) {}
    
    protected virtual void OnTriggerEnter2D(Collider2D collision){}
}
