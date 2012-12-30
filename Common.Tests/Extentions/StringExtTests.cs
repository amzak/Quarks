using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codestellation.Common.Extentions;
using NUnit.Framework;

namespace Codestellation.Common.Tests.Extentions
{
    [TestFixture]
    public class StringExtTests
    {
        [Test]
        public void Should_format_string_with()
        {
            const string template = "Hello, {0}!";
            Assert.That(template.FormatWith("World"), Is.EqualTo("Hello, World!"));
        }
    }
}