// ----------------------------------------------------------------------------
// EnemyMovementTest.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Test - Check enemy movement
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EnemyMovementTest
    {
        Enemy enemy;
    
        [SetUp]
        public void Setup()
        {
            GameObject enemyGO = MonoBehaviour.Instantiate(Resources.Load("EnemyTest") as GameObject);
            enemy = enemyGO.GetComponent<Enemy>();
            
            GameObject sfxPlayerGO = MonoBehaviour.Instantiate(Resources.Load("SFXManagerTest") as GameObject, 
                Vector3.zero,Quaternion.identity);
        }
        
        [UnityTest]
        public IEnumerator TestEnemyMovesPasses()
        {
            float enemyInitialXPos = enemy.transform.position.x;
    
            enemy.Move(Vector3.right);
            yield return new WaitForSeconds(0.1f);
    
            Assert.AreNotEqual(enemyInitialXPos,enemy.transform.position.x);
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(enemy.gameObject);
        }
    }
}
