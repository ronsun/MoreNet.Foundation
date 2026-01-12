using FluentAssertions;
using NUnit.Framework;
using System;

namespace MoreNet.Foundation.Extensions.Tests
{
    [TestFixture()]
    public partial class StringExtensionsTests
    {
        [Test()]
        // startIndex < 0
        [TestCase(-1, 5, null, '*')]
        public void MaskTest_InvalidStartIndex_ThrowExpectedException(int stubStartIndex, int stubLength, int? stubMaskLength, char stubMaskChar)
        {
            // arrange
            var stubString = "0123456789";

            // act
            Action action = () => stubString.Mask(stubStartIndex, stubLength, stubMaskLength, stubMaskChar);

            // assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test()]
        // maskLength < 0
        [TestCase(0, 5, -1, '*')]
        public void MaskTest_InvalidMaskLength_ThrowExpectedException(int stubStartIndex, int stubLength, int stubMaskLength, char stubMaskChar)
        {
            // arrange
            var stubString = "0123456789";

            // act
            Action action = () => stubString.Mask(stubStartIndex, stubLength, stubMaskLength, stubMaskChar);

            // assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}

