// ----------------------------------------------------------------------------
// PlayerShootingTest.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Test - Check player shoot
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerShootingTest
    {
        MainCharacter player;
        GameObject bullet;
    
        [SetUp]
        public void Setup()
        {
            GameObject playerGO = GameObject.Instantiate(Resources.Load("PlayerTest") as GameObject);
            player = playerGO.GetComponent<MainCharacter>();
            player.SetData(ScriptableObject.CreateInstance<PlayerConfig>());
        }
        
        [UnityTest]
        public IEnumerator TestPlayerShootPasses()
        {
            player.Shoot();
            yield return new WaitForSeconds(0.2f);
            
            Assert.NotZero(PoolManager.Instance.GetMembersInPool(player.bulletPrefab));
        }
        
        [TearDown]
        public void Teardown()
        {
            Object.Destroy(player.gameObject);
        }
    }
}