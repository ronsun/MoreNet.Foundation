using FluentAssertions;
using NUnit.Framework;

namespace MoreNet.Foundation.Extensions.Tests
{
    [TestFixture()]
    public partial class TupleExtensionsTests
    {
        [Test()]
        [TestCase(1, 5, 4, 7, true)]
        [TestCase(4, 7, 1, 5, true)]
        [TestCase(1, 5, 5, 10, false)]
        [TestCase(5, 10, 1, 5, false)]
        [TestCase(1, 5, 6, 10, false)]
        [TestCase(6, 10, 1, 5, false)]
        public void IsOverlappingTest(int rangeFrom, int rangeTo, int otherFrom, int otherTo, bool expected)
        {
            // arrange

            // act
            var actual = (rangeFrom, rangeTo).IsOverlapping((otherFrom, otherTo));

            // assert
            actual.Should().Be(expected);
        }
    }
}
