// ----------------------------------------------------------------------------
// ChapterConfig.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Holds config relative to Chapter, that holds itself several levels
// ----------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create ChapterConfig", fileName = "ChapterConfig", order = 0)]
public class ChapterConfig : ScriptableObject
{
    public List<LevelConfig> levels;
    public PlayerConfig playerConfig;

    public int GetNumberOfLevels()
    {
        return levels.Count;
    }
}
