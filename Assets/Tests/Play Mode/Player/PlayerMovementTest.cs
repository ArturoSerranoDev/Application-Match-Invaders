// ----------------------------------------------------------------------------
// PlayerMovementTest.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Test - Check that Move function of player works as expected
// ----------------------------------------------------------------------------

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerMovementTest
    {
        MainCharacter player;
    
        [SetUp]
        public void Setup()
        {
            GameObject playerGO = new GameObject();
            player = playerGO.AddComponent<MainCharacter>();
            
            player.SetData(ScriptableObject.CreateInstance<PlayerConfig>());
        }

        [UnityTest]
        public IEnumerator TestLeftInputMovesCharacterPasses()
        {
            Vector3 shipInitialPos = player.transform.position;
    
            player.Move(Vector3.left);
            yield return new WaitForSeconds(0.1f);
        
            Assert.Less(player.transform.position.x,shipInitialPos.x);
        }

        [UnityTest]
        public IEnumerator TestRightInputMovesCharacterPasses()
        {
            Vector3 shipInitialPos = player.transform.position;
    
            player.Move(Vector3.right);
            yield return new WaitForSeconds(0.1f);
    
            Assert.Greater(player.transform.position.x,shipInitialPos.x);
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(player.gameObject);
        }
    }
}

