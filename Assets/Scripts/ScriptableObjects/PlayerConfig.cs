// ----------------------------------------------------------------------------
// EnemyConfig.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Holds config relative to Player
// ----------------------------------------------------------------------------
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerConfig", fileName = "PlayerConfig", order = 0)]
public class PlayerConfig : ScriptableObject
{
    public int lives;
    public float speed;
    public float bulletSpeed;
}
