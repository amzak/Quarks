using System;
using Codestellation.Common.Timing;
using NUnit.Framework;

namespace Codestellation.Common.Tests.Timing
{
    [TestFixture]
    public class TimeUtilTests
    {
        [TestCase(156)]
        public void Should_truncate_millisecodes()
        {
            var original = new DateTimeOffset(2012, 12, 2, 23, 56, 44, 156, TimeSpan.FromHours(5));

            var truncated = TimeUtil.TruncateMilliseconds(original);

            var expected = new DateTimeOffset(2012, 12, 2, 23, 56, 44, TimeSpan.FromHours(5));

            Assert.That(truncated, Is.EqualTo(expected));
        }
    }
}