﻿using System.Collections.Generic;
using System.Linq;
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
            var nodeCount1 = 0;

            //Act            
            var tree1 = NegaMaxAi.BuildGameTree(_numbers, 1);
            tree1.Traverse(node => ++nodeCount1);

            //Assert
            //+1 Cause of Tree Root
            Assert.That(nodeCount1, Is.EqualTo(_numbers.Length + 1));
        }

        [Test]
        public void TestBuildTreeDepth2_Test()
        {
            //Arrange
            var nodeCount = 0;

            //Act            
            var tree = NegaMaxAi.BuildGameTree(_numbers, 2);
            tree.Traverse(node => ++nodeCount);

            //Assert
            //+1 Cause of Tree Root
            var layer2 = _numbers.Length * (_numbers.Length - 1);
            var layer1 = _numbers.Length;

            Assert.That(nodeCount, Is.EqualTo(layer2 + layer1 + 1));
        }

        [Test]
        public void TestBuildTreeDepth3_Test()
        {
            //Arrange
            var nodeCount = 0;

            //Act            
            var tree = NegaMaxAi.BuildGameTree(_numbers, 3);
            tree.Traverse(node => ++nodeCount);

            //Assert
            //+1 Cause of Tree Root
            var layer1 = _numbers.Length;
            var layer2 = layer1 * (_numbers.Length - 1);
            var layer3 = layer2 * (_numbers.Length - 2);

            Assert.That(nodeCount, Is.EqualTo(layer3 + layer2 + layer1 + 1));
        }

        [Test]
        public void IEnumerable_Test()
        {
            IEnumerable<int> testNumbers = new[] {1, 2, 3, 4, 4, 4, 4};
            var arr = testNumbers.ToArray();
            var arr2 = testNumbers.ToArray<int>();
            var arr3 = (int[])testNumbers;


            Assert.That(testNumbers.Count(), Is.EqualTo(arr.Length));
            Assert.That(testNumbers.Count(), Is.EqualTo(arr2.Length));
            Assert.That(testNumbers.Count(), Is.EqualTo(arr3.Length));
        }
    }
}
