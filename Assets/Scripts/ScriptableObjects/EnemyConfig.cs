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
    // Init for testing
    public float minSpeed = 100;
    public float maxSpeed = 200;
    public int lives = 1;
    
    public int bulletSpeed = 10;
    public int numberOfColors = 1;
    public List<Color> availableColors = new List<Color>() {Color.black};
}
