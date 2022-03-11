using System;
using System.Collections.Generic;
using Codestellation.Quarks.Collections;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Collections
{
    [TestFixture]
    public class ReadOnlyCollectionExtensionsTests
    {
        [TestCase(1, "[[1],[2],[3],[4],[5],[6],[7],[8],[9],[10]]")]
        [TestCase(2, "[[1,2],[3,4],[5,6],[7,8],[9,10]]")]
        [TestCase(3, "[[1,2,3],[4,5,6],[7,8,9],[10]]")]
        [TestCase(8, "[[1,2,3,4,5,6,7,8],[9,10]]")]
        [TestCase(10, "[[1,2,3,4,5,6,7,8,9,10]]")]
        [TestCase(12, "[[1,2,3,4,5,6,7,8,9,10]]")]
        [TestCase(10, "[[1,2,3,4,5,6,7,8,9,10]]")]
        public void Should_split_collection_into_chunks(int chunkSize, string expected)
        {
            IReadOnlyCollection<int> list = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var chunks = list.AsChunks(chunkSize);
            var actual = JsonConvert.SerializeObject(chunks);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Should_throw_on_zero_chunk_size()
        {
            Assert.Throws<ArgumentException>(() => new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.AsChunks(0));
        }
    }
}