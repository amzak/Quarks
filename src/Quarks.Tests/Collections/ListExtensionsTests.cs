using System;
using System.Collections.Generic;
using Codestellation.Quarks.Collections;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Collections
{
    [TestFixture]
    public class ListExtensionsTests
    {
        [TestCase(1, "[[1],[2],[3],[4],[5],[6],[7],[8],[9],[10]]")]
        [TestCase(2, "[[1,2],[3,4],[5,6],[7,8],[9,10]]")]
        [TestCase(3, "[[1,2,3],[4,5,6],[7,8,9],[10]]")]
        [TestCase(8, "[[1,2,3,4,5,6,7,8],[9,10]]")]
        [TestCase(10, "[[1,2,3,4,5,6,7,8,9,10]]")]
        [TestCase(12, "[[1,2,3,4,5,6,7,8,9,10]]")]
        public void Test(int batchSize, string expected)
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            ListExtensions.BatchEnumerable<int> batchEnumerable = list.Partition(batchSize);

            var actual = JsonConvert.SerializeObject(batchEnumerable);
            Console.WriteLine(actual);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}