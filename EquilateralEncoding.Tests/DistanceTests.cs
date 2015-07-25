using System;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

namespace EquilateralEncoding.Tests
{
    [TestFixture]
    public class EETests
    {
        //1-4 All pass
        //5-43 Pico pass
        //44-1220 Nano pass
        //1221-5000+ SixSigma pass
        private const int categories = 4;
        [Test]
        public void TestAllEqualDistance()
        {
            var results = GetAllDistancesList();
            var expected = new List<double>();
            foreach(var value in results)
            {
                expected.Add((double)results[0]);
            }
            CollectionAssert.AreEqual(expected, results);
        }

        [Test]
        public void TestAllPicoEqualDistance()
        {
            TestPercentage(GetAllDistancesList(), 0.000000000001);
        }

        [Test]
        public void TestAllNanoEqualDistance()
        {
            TestPercentage(GetAllDistancesList(), 0.000000001);
        }

        [Test]
        public void TestAllSixSigmaEqualDistance()
        {
            TestPercentage(GetAllDistancesList(), 0.00033999999);
        }

        private ArrayList GetAllDistancesList()
        {
            var eq = new Equilateral(categories);
            var input = eq.Result;
            var n = input.Length;
            var results = new ArrayList();
            for (int x = 0; x < n - 1; x++)
            {
                for (int z = x + 1; z < n; z++)
                {
                    double sum = 0;
                    for (int y = 0; y < n - 1; y++)
                    {
                        sum += Math.Pow(input[z][y] - input[x][y], 2);
                    }
                    results.Add(Math.Sqrt(sum));
                }
            }
            return results;
        }

        private void TestPercentage(ArrayList results, double per)
        {
            double result = 0;
            foreach (double value in results)
            {
                result += value;
            }
            result = result / results.Count;
            for (int x = 0; x < results.Count; x++)
            {
                var actual = (double)results[x];
                Assert.That(actual, Is.EqualTo(result).Within(per).Percent);
            }
        }
    }
}
