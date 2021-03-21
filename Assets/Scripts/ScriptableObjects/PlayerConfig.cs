// ----------------------------------------------------------------------------
// PlayerConfig.cs
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
    [Range(1,3)]
    public int lives;
    public int maxBullets = 10;
    public float speed = 10;
    public float bulletSpeed = 10;
    public float bulletCooldown;
}
