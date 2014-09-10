using Codestellation.Quarks.Extentions;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Extentions
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