using FluentAssertions;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace MoreNet.Foundation.Extensions.Tests
{
    [TestFixture()]
    public partial class EnumerableExtensionsTests
    {
#if !NET6_0_OR_GREATER
        [Test()]
        [TestCaseSource(nameof(ChunkTestCaseSource))]
        public void ChunkTest(IEnumerable<int> stubIEnumerable, int stubSize, IEnumerable<int[]> expected)
        {
            // arrange

            // act
            var actual = stubIEnumerable.Chunk(stubSize);

            // assert
            actual.Should().BeEquivalentTo(expected, option => option.WithStrictOrdering());
        }

        public static IEnumerable ChunkTestCaseSource()
        {
            List<int> stubIEnumerable = null;

            // Empty source
            stubIEnumerable = new List<int>();
            yield return new TestCaseData(stubIEnumerable, 1, new List<int[]>());

            // Size more than size of source
            stubIEnumerable = new List<int> { 1 };
            yield return new TestCaseData(stubIEnumerable, 2, new List<int[]> { new int[] { 1 } });

            stubIEnumerable = new List<int> { 1, 2, 3 };
            yield return new TestCaseData(stubIEnumerable, 2, new List<int[]> { new int[] { 1, 2 }, new int[] { 3 } });

            // Normal case
            stubIEnumerable = new List<int> { 1, 2, 3, 4 };
            yield return new TestCaseData(stubIEnumerable, 2, new List<int[]> { new int[] { 1, 2 }, new int[] { 3, 4 } });
        }
#endif
    }
}