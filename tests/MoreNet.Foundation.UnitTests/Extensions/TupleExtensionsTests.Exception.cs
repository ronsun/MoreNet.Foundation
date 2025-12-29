using FluentAssertions;
using NUnit.Framework;
using System;

namespace MoreNet.Foundation.Extensions.Tests
{
    [TestFixture()]
    public partial class TupleExtensionsTests
    {
        [Test()]
        // from > to
        [TestCase(2, 1, 0, 1)]
        [TestCase(0, 1, 2, 1)]
        // from == to
        [TestCase(1, 1, 0, 2)]
        [TestCase(0, 2, 1, 1)]
        public void IsOverlappingTest_InvalidRanges_ThrowExpectedException(int rangeFrom, int rangeTo, int otherFrom, int otherTo)
        {
            // arrange
            var stubRange = (rangeFrom, rangeTo);
            var stubOther = (otherFrom, otherTo);

            // act
            Action action = () => stubRange.IsOverlapping(stubOther);

            // assert
            action.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
    }
}
