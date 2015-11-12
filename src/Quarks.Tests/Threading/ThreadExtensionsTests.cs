using System;
using System.Threading;
using Codestellation.Quarks.Threading;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Threading
{
    [TestFixture]
    public class ThreadExtensionsTests
    {
        [Test]
        public void Should_not_wait_if_join_itself()
        {
            var current = Thread.CurrentThread;

            var result = current.SafeJoin(TimeSpan.FromSeconds(10));

            Assert.That(result, Is.True);
        }
    }
}