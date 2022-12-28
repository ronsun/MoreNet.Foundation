using FluentAssertions;
using NUnit.Framework;
using System;

namespace MoreNet.Foundation.Globalization.Tests
{
    [TestFixture()]
    public partial class TextElementStringTests
    {
        [Test()]
        public void ContainsTest_ContainsNull_ThrowExpectedException()
        {
            // arrange
            var target = new TextElementString("");

            // act
            Action actual = () => target.Contains(null);

            // assert
            actual.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test()]
        public void ReplaceTest_ReplaceFromNull_ThrowExpectedException()
        {
            // arrange
            string stubOldValue = null;
            var target = new TextElementString("");

            // act
            Action actual = () => target.Replace(stubOldValue, string.Empty);

            // assert
            actual.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test()]
        public void ReplaceTest_ReplaceFromEmpty_ThrowExpectedException()
        {
            // arrange
            string stubOldValue = string.Empty;
            var target = new TextElementString("");

            // act
            Action actual = () => target.Replace(stubOldValue, string.Empty);

            // assert
            actual.Should().ThrowExactly<ArgumentException>();
        }

        [Test()]
        [TestCase(1)]
        [TestCase(-1)]
        public void RemoveTest_RemoveFromOutOfRange_ThrowExpectedException(int stubStartIndex)
        {
            // arrange
            var target = new TextElementString("");

            // act
            Action actual = () => target.Remove(stubStartIndex);

            // assert
            actual.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [Test()]
        [TestCase(1)]
        [TestCase(-1)]
        public void RemoveTest_RemoveFromOutOfRange_WithCount_ThrowExpectedException(int stubStartIndex)
        {
            // arrange
            int stubCount = 1;
            var target = new TextElementString("");

            // act
            Action actual = () => target.Remove(stubStartIndex, stubCount);

            // assert
            actual.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
    }
}