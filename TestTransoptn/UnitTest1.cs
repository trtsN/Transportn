using NUnit.Framework;
using Transportn;
using System.Collections.Generic;

namespace TestTransoptn
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void GetCostPositive()
        {

            Dictionary<int, int> resultTest = new Dictionary<int, int>()
            {
                [0] = 1,
                [1] = 2,
                [2] = 0
            };

            int n = 3;
            int[,] priceTest = new int[n, n];

            priceTest[0, 0] = int.MaxValue;
            priceTest[1, 1] = int.MaxValue;
            priceTest[2, 2] = int.MaxValue;

            priceTest[0, 1] = 1;
            priceTest[0, 2] = 2;

            priceTest[1, 0] = 1;
            priceTest[1, 2] = 3;

            priceTest[2, 0] = 2;
            priceTest[2, 1] = 3;

            int test = 6;
            int result = Program.GetCost(0, resultTest, priceTest);
            //resultTest[0] = 1;
            int t = result;

            bool f = (test == result);

            Assert.IsTrue(f);
        }

        [Test]
        public void GetCostNegative()
        {

            Dictionary<int, int> resultTest = new Dictionary<int, int>()
            {
                [0] = 1,
                [1] = 2,
                [2] = 0
            };

            int n = 3;
            int[,] priceTest = new int[n, n];

            priceTest[0, 0] = int.MaxValue;
            priceTest[1, 1] = int.MaxValue;
            priceTest[2, 2] = int.MaxValue;

            priceTest[0, 1] = 1;
            priceTest[0, 2] = 2;

            priceTest[1, 0] = 1;
            priceTest[1, 2] = 3;

            priceTest[2, 0] = 2;
            priceTest[2, 1] = 3;

            int test = 61;
            int result = Program.GetCost(0, resultTest, priceTest);
            //resultTest[0] = 1;
            int t = result;
            Assert.AreNotEqual(test, result);
        }
    }

    [TestFixture]
    public class Tests1 : Zeros
    {
        [Test]
        public void BestZeroesPositiveTest()
        {
            int n = 3;
            int[,] priceTest = new int[n, n];

            priceTest[0, 0] = int.MaxValue;
            priceTest[1, 1] = int.MaxValue;
            priceTest[2, 2] = int.MaxValue;

            priceTest[0, 1] = 1;
            priceTest[0, 2] = 2;

            priceTest[1, 0] = 1;
            priceTest[1, 2] = 3;

            priceTest[2, 0] = 2;
            priceTest[2, 1] = 3;

            int i = 5;
            int j = sum_place(priceTest, 0, 1);

            Assert.AreEqual(i, j);
        }

        [Test]
        public void BestZeroesNegativeTest()
        {
            int n = 3;
            int[,] priceTest = new int[n, n];

            priceTest[0, 0] = int.MaxValue;
            priceTest[1, 1] = int.MaxValue;
            priceTest[2, 2] = int.MaxValue;

            priceTest[0, 1] = 1;
            priceTest[0, 2] = 2;

            priceTest[1, 0] = 1;
            priceTest[1, 2] = 3;

            priceTest[2, 0] = 2;
            priceTest[2, 1] = 3;

            int i = 1;
            int j = sum_place(priceTest, 0, 1);

            Assert.AreNotEqual(i, j);
        }
    }
}