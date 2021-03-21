// ----------------------------------------------------------------------------
// LevelBuilderTest.cs
//
// Author: Arturo Serrano
// Date: 21/02/21
//
// Brief: Test - Level Creation
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LevelBuilderTest
    {
        LevelBuilder levelBuilder;
        List<Enemy> enemies;
    
        [SetUp]
        public void Setup()
        {
            GameObject levelBuilderGO = MonoBehaviour.Instantiate(Resources.Load("LevelBuilderTest") as GameObject);
            levelBuilder = levelBuilderGO.GetComponent<LevelBuilder>();
            enemies = levelBuilder.BuildLevel(ScriptableObject.CreateInstance<LevelConfig>());
            levelBuilder.Player.SetData(ScriptableObject.CreateInstance<PlayerConfig>());
        }
        
        [UnityTest]
        public IEnumerator TestEnemiesAreBeingSpawnedPasses()
        {
            yield return new WaitForSeconds(0.1f);
            Assert.NotNull(enemies);
        }
        
        [UnityTest]
        public IEnumerator TestPlayerIsBeingSpawnedPasses()
        {
            yield return new WaitForSeconds(0.1f);
            Assert.NotNull(levelBuilder.Player);
        }
        
        [TearDown]
        public void Teardown()
        {
            Object.Destroy(levelBuilder.gameObject);
        }
    }
}