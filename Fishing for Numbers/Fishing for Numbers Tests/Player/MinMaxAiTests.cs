using System;
using Fishing_for_Numbers;
using Fishing_for_Numbers.Player;
using NUnit.Framework;

namespace Fishing_for_Numbers_Tests.Player
{
    [TestFixture]
    public class MinMaxAiTests
    {
        private int[] _numbers;


        [SetUp]
        public void SetUp()
        {
            _numbers = new[] {1, 33, 67, 23, 21, 92, 73, 59, 42, 14};
        }

        [Test]
        public void TestBuildTreeDepth1_Test()
        {
            //Arrange
            var player = new MinMaxAi(1);
            var nodeCount1 = 0;

            //Act            
            var tree1 = player.BuildGameTree(_numbers, 1);
            tree1.Traverse(node => ++nodeCount1);

            //Assert
            //+1 Cause of Tree Root
            Assert.That(nodeCount1, Is.EqualTo(_numbers.Length + 1));
        }

        [Test]
        public void TestBuildTreeDepth2_Test()
        {
            //Arrange
            var player = new MinMaxAi(2);
            var nodeCount = 0;

            //Act            
            var tree = player.BuildGameTree(_numbers, 2);
            tree.Traverse(node => ++nodeCount);

            //Assert
            //+1 Cause of Tree Root
            Assert.That(nodeCount, Is.EqualTo(_numbers.Length + _numbers.Length * (_numbers.Length - 1) + 1));
        }

        [Test]
        public void TestBuildTreeDepth3_Test()
        {
            //Arrange
            var player = new MinMaxAi(3);
            var nodeCount = 0;

            //Act            
            var tree = player.BuildGameTree(_numbers, 3);
            tree.Traverse(node => ++nodeCount);

            //Assert
            //+1 Cause of Tree Root

            //TODO Assert
        }
    }
}
