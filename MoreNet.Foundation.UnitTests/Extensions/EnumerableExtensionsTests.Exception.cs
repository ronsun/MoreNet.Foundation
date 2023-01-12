using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MoreNet.Foundation.Extensions.Tests
{
    [TestFixture()]
    public partial class EnumerableExtensionsTests
    {
#if !NET6_0_OR_GREATER
        [Test()]
        [TestCase(0)]
        [TestCase(-1)]
        public void ChunkTest_InputIncorrectSize_ThrowExpectedException(int stubSize)
        {
            // arrange
            IEnumerable<int> stubIEnumerable = Substitute.For<IEnumerable<int>>();

            // act
            Action action = () => stubIEnumerable.Chunk(stubSize);

            // assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test()]
        public void ChunkTest_InputNullSource_ThrowExpectedException()
        {
            // arrange
            int stubSize = 1;
            IEnumerable<int> stubIEnumerable = null;

            // act
            Action action = () => stubIEnumerable.Chunk(stubSize);

            // assert
            action.Should().Throw<ArgumentNullException>();
        }
#endif
    }
}