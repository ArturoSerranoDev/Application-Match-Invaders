// ----------------------------------------------------------------------------
// BunkerTest.cs
//
// Author: Arturo Serrano
// Date: 21/02/21
//
// Brief: Test - Bunker related methods
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BunkerTest
    {
        Bunker bunker;
    
        [SetUp]
        public void Setup()
        {
            GameObject bunkerGO = PoolManager.Instance.Spawn(Resources.Load("BunkerTest") as GameObject, 
                                                             Vector3.zero,Quaternion.identity);
            bunker = bunkerGO.GetComponent<Bunker>();
            bunker.Init();
        }
        
        [UnityTest]
        public IEnumerator TestBunkerLosesHealthPasses()
        {
            GameObject bullet = GameObject.Instantiate(Resources.Load("EnemyBulletTest") as GameObject);
            yield return new WaitForSeconds(0.2f);
            
            Assert.AreNotEqual(bunker.GetLives(), 5);
        }
        
        [UnityTest]
        public IEnumerator TestBunkerDiesAt5HitsPasses()
        {
            bunker.Init();
            
            for (int i = 0; i < 8; i++)
            {
                GameObject bullet = GameObject.Instantiate(Resources.Load("EnemyBulletTest") as GameObject);
                yield return new WaitForSeconds(0.1f);
            }
            
            Assert.False(bunker.gameObject.activeInHierarchy);
        }
        
        [TearDown]
        public void Teardown()
        {
            bunker.Despawn();
        }
    }
}