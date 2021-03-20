// ----------------------------------------------------------------------------
// LevelConfig.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Holds config relative to Levels
// ----------------------------------------------------------------------------
using UnityEngine;

[CreateAssetMenu(menuName = "Create LevelConfig", fileName = "LevelConfig", order = 0)]
public class LevelConfig : ScriptableObject
{
    public EnemyConfig enemyConfig;
    public PlayerConfig playerConfig;

    public int enemiesPerRow;
    public int enemiesPerColumn;
}
