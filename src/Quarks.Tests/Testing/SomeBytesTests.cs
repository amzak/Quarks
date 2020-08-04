using Codestellation.Quarks.Testing;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    [TestFixture]
    public class SomeBytesTests
    {
        [Test]
        public void Should_generate_bytes_array()
        {
            byte[] data = Some.Bytes(20);

            Assert.That(data.Length, Is.EqualTo(20));
        }
    }
}