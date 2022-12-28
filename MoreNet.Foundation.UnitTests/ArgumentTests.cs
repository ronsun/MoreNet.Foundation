using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MoreNet.Foundation.Tests
{
    [TestFixture()]
    public partial class ArgumentTests
    {
        private enum FakeEnum
        {
            A = 1,
        }

        [Test()]
        [TestCaseSource(nameof(ShouldNotNullTest_InputEmpty_Pass_TestCases))]
        public void ShouldNotNullTest_InputEmpty_Pass<T>(T stubValue)
            where T : class
        {
            // arrange
            var stubArgumentName = nameof(stubValue);

            // act
            Argument.ShouldNotNull(stubValue, stubArgumentName);

            // assert
            Assert.Pass();
        }

        public static IEnumerable ShouldNotNullTest_InputEmpty_Pass_TestCases()
        {
            yield return new TestCaseData("");
            yield return new TestCaseData(new List<object>());
        }

        [Test()]
        [TestCaseSource(nameof(ShouldNotEmptyTest_InputValid_Pass_TestCases))]
        public void ShouldNotEmptyTest_InputValid_Pass<T>(T stubValue)
            where T : class
        {
            // arrange
            var stubName = nameof(stubValue);

            // act
            Argument.ShouldNotEmpty(stubValue, stubName);

            // assert
            Assert.Pass();
        }

        public static IEnumerable ShouldNotEmptyTest_InputValid_Pass_TestCases()
        {
            yield return new TestCaseData(" ");
            yield return new TestCaseData("a");

            var list = new List<object> { new object() };
            yield return new TestCaseData(list);
        }

        [Test()]
        public void ShouldBeDefinedTest_InputValid_Pass()
        {
            // arrange
            var stubValue = FakeEnum.A;
            var stubArgumentName = nameof(stubValue);

            // act
            Argument.ShouldBeDefined(stubValue, stubArgumentName);

            // assert
            Assert.Pass();
        }

        [Test()]
        // both min and max included
        [TestCase(0, 0, 2)]
        [TestCase(1, 0, 2)]
        [TestCase(2, 0, 2)]
        // min == max
        [TestCase(0, 0, 0)]
        public void ShouldInRangeTest_InputValid_Pass(
            int stubValue,
            int stubMin,
            int stubMax)
        {
            // arrange
            var stubArgumentName = nameof(stubValue);

            // act
            Argument.ShouldInRange<int>(stubValue, stubMin, stubMax, stubArgumentName);

            // assert
            Assert.Pass();
        }

        public void ShouldInRangeTest_InputComparable_Pass()
        {
            // arrange
            var mockedValue = Substitute.For<IComparable>();
            var mockedMin = Substitute.For<IComparable>();
            var mockedMax = Substitute.For<IComparable>();
            mockedValue.CompareTo(mockedMin).Returns(1);
            mockedValue.CompareTo(mockedMax).Returns(-1);

            var stubArgumentName = nameof(mockedValue);

            // act
            Argument.ShouldInRange(mockedValue, mockedMin, mockedMax, stubArgumentName);

            // assert
            Assert.Pass();
        }
    }
}