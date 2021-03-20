// ----------------------------------------------------------------------------
// EnemyConfig.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Holds config relative to Enemies
// ----------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create EnemyConfig", fileName = "EnemyConfig", order = 0)]

public class EnemyConfig : ScriptableObject
{
    public float minSpeed;
    public float maxSpeed;
    public int lives;
    public int bulletSpeed;
    public int numberOfColors;
    public List<Color> availableColors;
}
