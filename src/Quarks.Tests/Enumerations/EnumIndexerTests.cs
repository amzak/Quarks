using System;
using System.Linq;
using Codestellation.Quarks.Enumerations;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Enumerations
{
    public enum Mixed : short
    {
        Yes = -3,
        No = 1
    }

    public enum Negative : short
    {
        Yes = -3,
        No = -5
    }

    public enum Positive : byte
    {
        Yes = 1,
        No = 8
    }

    [TestFixture(typeof(Mixed))]
    [TestFixture(typeof(Negative))]
    [TestFixture(typeof(Positive))]
    public class EnumIndexerTests<TEnum>
    {
        private readonly TEnum _minValue;
        private readonly TEnum _maxValue;

        public EnumIndexerTests()
        {
            _minValue = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Min();
            _maxValue = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Max();
        }

        [Test]
        public void Throws_if_key_is_no_enum()
        {
            Assert.Throws<TypeInitializationException>(() => new EnumIndexer<DateTime, Boolean>());
        }

        [Test]
        public void Can_set_and_get_value_at_index()
        {
            var indexer = new EnumIndexer<TEnum, DateTime>();

            indexer[_minValue] = DateTime.MinValue;
            indexer[_maxValue] = DateTime.MaxValue;

            var min = indexer[_minValue];
            var max = indexer[_maxValue];

            Assert.That(max, Is.EqualTo(DateTime.MaxValue));
            Assert.That(min, Is.EqualTo(DateTime.MinValue));
        }
    }
}