// ----------------------------------------------------------------------------
// EnemyShootTest.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Test - Check enemy shoot
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EnemyShootTest
    {
        Enemy enemy;
        GameObject bullet;
    
        [SetUp]
        public void Setup()
        {
            GameObject enemyGO = GameObject.Instantiate(Resources.Load("EnemyTest") as GameObject);
            enemy = enemyGO.GetComponent<Enemy>();
            enemy.enemySpriteRenderer = enemyGO.GetComponent<SpriteRenderer>();
            enemy.SetData(ScriptableObject.CreateInstance<EnemyConfig>(), Vector2Int.zero);
        }
        
        [UnityTest]
        public IEnumerator TestEnemyShootPasses()
        {
            enemy.Shoot();
            yield return new WaitForSeconds(0.1f);
            
            Assert.NotZero(PoolManager.Instance.GetMembersInPool(enemy.bulletPrefab));
        }
        
        [UnityTest]
        public IEnumerator TestEnemyShootDownwardsPasses()
        {
            enemy.Shoot();
            yield return new WaitForSeconds(0.05f);

            Assert.NotZero(PoolManager.Instance.GetMembersInPool(enemy.bulletPrefab));
            
            bullet = PoolManager.Instance.GetObjectPool(enemy.bulletPrefab).pooledObjects[0];
            
            float bulletVerticalPos = bullet.transform.position.y;
            
            yield return new WaitForSeconds(0.05f);
            Assert.Less(bullet.transform.position.y,bulletVerticalPos);
        }
        
        [TearDown]
        public void Teardown()
        {
            Object.Destroy(enemy.gameObject);
        }
    }
}