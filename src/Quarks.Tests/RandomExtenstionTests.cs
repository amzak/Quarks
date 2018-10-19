using System;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests
{
    [TestFixture]
    public class RandomExtenstionTests
    {
        [Test]
        public void RestoreStateTest()
        {
            const int n = 50;

            var original = new Random(123);
            for (var i = 0; i < 50; i++)
            {
                original.Next();
            }

            byte[] state = original.GetState();

            var expected = new int[n];
            for (var i = 0; i < expected.Length; i++)
            {
                expected[i] = original.Next();
            }

            var restored = new Random();
            restored.RestoreState(state);

            var actual = new int[n];
            for (var i = 0; i < actual.Length; i++)
            {
                actual[i] = restored.Next();
            }                        

            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }
}