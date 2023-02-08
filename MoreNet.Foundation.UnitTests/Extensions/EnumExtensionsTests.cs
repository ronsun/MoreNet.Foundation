using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MoreNet.Foundation.Extensions.Tests
{
    [TestFixture()]
    public partial class EnumExtensionsTests
    {
        [Flags]
        public enum FakeFlagEnum
        {
            None = 0,
            A = 1,
            B = 1 << 1,
            D = 1 << 3,
            // Defined | Not Defined
            B4 = B | (1 << 2),
            // Defined | Defined
            BD = B | D,
            // Last
            Z = 1 << 63,
        }

        [Test()]
        [TestCaseSource(nameof(ExtractFlagsTestCaseSource_ReturnsExpectedInOrder))]
        public void ExtractFlagsTest_ReturnsExpectedInOrder(FakeFlagEnum target, IEnumerable<FakeFlagEnum> expected)
        {
            // arrange

            // act
            var actual = target.ExtractFlags();

            // assert
            actual.Should().BeEquivalentTo(expected, option => option.WithStrictOrdering());
        }

        public static IEnumerable ExtractFlagsTestCaseSource_ReturnsExpectedInOrder()
        {
            FakeFlagEnum target = default;
            IEnumerable<FakeFlagEnum> expected = default;

            // Value 0 excluded
            target = FakeFlagEnum.None;
            expected = new List<FakeFlagEnum> { };
            yield return new TestCaseData(target, expected);

            // In order
            target = (FakeFlagEnum.B | FakeFlagEnum.A);
            expected = new List<FakeFlagEnum> { FakeFlagEnum.A, FakeFlagEnum.B };
            yield return new TestCaseData(target, expected);

            // Defined flags
            target = (FakeFlagEnum.A | FakeFlagEnum.BD);
            expected = new List<FakeFlagEnum> { FakeFlagEnum.A, FakeFlagEnum.B, FakeFlagEnum.D };
            yield return new TestCaseData(target, expected);
        }
    }
}
