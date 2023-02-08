using FluentAssertions;
using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Linq;

namespace MoreNet.Foundation.Extensions.Tests
{
    [TestFixture()]
    public partial class EnumExtensionsTests
    {
        public enum FakeEnum
        {
            A = 1,
        }

        [Test()]
        public void ExtractFlagsTest_WithUndefinedValue_ThrowExpectedException()
        {
            // arrange
            FakeFlagEnum target = (FakeFlagEnum.A | FakeFlagEnum.B4);

            // act
            Action action = () => target.ExtractFlags().ToList();

            // assert
            action.Should().Throw<InvalidEnumArgumentException>();
        }

        [Test()]
        public void ExtractFlagsTest_WithoutFlagsAttribute_ThrowExpectedException()
        {
            // arrange
            FakeEnum target = FakeEnum.A;

            // act
            Action action = () => target.ExtractFlags().ToList();

            // assert
            action.Should().Throw<ArgumentException>();
        }

        [Test()]
        public void ExtractFlagsTest_NotDefinedItemInFlag_ThrowExpectedException()
        {
            // arrange
            FakeFlagEnum target = FakeFlagEnum.B4;

            // act
            Action action = () => target.ExtractFlags().ToList();

            // assert
            action.Should().ThrowExactly<InvalidEnumArgumentException>();
        }
    }
}
