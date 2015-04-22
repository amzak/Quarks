using Codestellation.Quarks.Testing;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    [TestFixture]
    public class SomeBooleanTests : SomeTests
    {
        [Test]
        public void Should_generate_different_values()
        {
            Should_generate_different_values(Some.Boolean);
        }

        [Test, Explicit]
        public void Should_generate_uniform_distribution()
        {
            Should_generate_uniform_distribution(Some.Boolean);
        }
    }
}