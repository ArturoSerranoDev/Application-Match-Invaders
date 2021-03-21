// ----------------------------------------------------------------------------
// ScoreCalculationTest.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Test - Check score calculation
// ----------------------------------------------------------------------------
using NUnit.Framework;

namespace Tests
{
    public class ScoreCalculationTest
    {
        [Test]
        public void ScoreCalculationOneKillPasses()
        {
            
            Assert.AreEqual(10,ScoreCalculator.GetScorePerKill(1));
        }
        
        [Test]
        public void ScoreCalculationTwoKillsPasses()
        {
            Assert.AreEqual(40,ScoreCalculator.GetScorePerKill(2));
        }
        
        [Test]
        public void ScoreCalculationThreeKillsPasses()
        {
            Assert.AreEqual(90,ScoreCalculator.GetScorePerKill(3));
        }
        
        [Test]
        public void ScoreCalculationFourKillsPasses()
        {
            Assert.AreEqual(200,ScoreCalculator.GetScorePerKill(4));
        }
        
        [Test]
        public void ScoreCalculationFiveKillsPasses()
        {
            Assert.AreEqual(400,ScoreCalculator.GetScorePerKill(5));
        }
        
    }
}
