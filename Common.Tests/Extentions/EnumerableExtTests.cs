using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codestellation.Common.Extentions;
using NUnit.Framework;

namespace Codestellation.Common.Tests.Extentions
{
    [TestFixture]
    public class EnumerableExtTests
    {
        [Test]
        public void Should_return_original_if_original_is_not_null()
        {
            object[] self = {123, "Hello", DateTime.Now};
            Assert.That(self.EmptyIfNull(), Is.EqualTo(self));
        }

        [Test]
        public void Should_return_empty_if_original_is_null()
        {
            object[] self = null;
            Assert.That(self.EmptyIfNull(), Is.EqualTo(Enumerable.Empty<object>()));
        }
    }
}
