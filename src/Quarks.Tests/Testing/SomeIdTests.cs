using Codestellation.Quarks.Testing;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    [TestFixture]
    public class SomeIdTests
    {
        [Test]
        public void Should_generate_id_sequence()
        {
            Assert.That(Some.Id(), Is.EqualTo(1));
            Assert.That(Some.Id(), Is.EqualTo(2));
        }
    }
}