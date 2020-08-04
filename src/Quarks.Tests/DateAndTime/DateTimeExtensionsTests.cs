using System;
using Codestellation.Quarks.DateAndTime;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.DateAndTime
{
    public class DateTimeExtensionsTests
    {
        [Test]
        public void Should_discard_milliseconds()
        {
            var dateTime = new DateTime(2014, 12, 1, 12, 23, 45, 28);

            var result = dateTime.DiscardMilliseconds();

            var expected = new DateTime(2014, 12, 1, 12, 23, 45);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Should_discard_microseconds()
        {
            // given
            var dateTime = new DateTime(636639775118166766, DateTimeKind.Utc);

            // when
            var actual = dateTime.DiscardMicroseconds();

            // than
            var expected = new DateTime(2018, 06, 07, 14, 11, 51, 816, DateTimeKind.Utc);
            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(actual.Kind, Is.EqualTo(expected.Kind));
        }
    }
}