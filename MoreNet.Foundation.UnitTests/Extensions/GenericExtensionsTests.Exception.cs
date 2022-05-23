using FluentAssertions;
using MoreNet.Foundation.Extensions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;

namespace MoreNet.Foundation.Assertion.Tests
{
    [TestFixture()]
    public partial class GenericExtensionsTests
    {
        [Test()]
        public void ShouldNotNullTest_InputNull_ThrowExpectedException()
        {
            // arrange
            object stubValue = null;
            var stubArgumentName = nameof(stubValue);

            // act
            Action action = () => stubValue.ShouldNotEmpty(stubArgumentName);

            // assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(stubArgumentName);
        }

        [Test()]
        public void ShouldNotEmptyTest_InputNull_ThrowExpectedException()
        {
            // arrange
            string stubValue = null;
            var stubArgumentName = nameof(stubValue);

            // act
            Action action = () => stubValue.ShouldNotEmpty(stubArgumentName);

            // assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(stubArgumentName);
        }

        [Test()]
        [TestCaseSource(nameof(ShouldNotEmptyTest_InputEmpty_ThrowExpectedException_TestCases))]
        public void ShouldNotEmptyTest_InputEmpty_ThrowExpectedException<T>(T stubValue)
            where T : class
        {
            // arrange
            var stubArgumentName = nameof(stubValue);

            // act
            Action action = () => stubValue.ShouldNotEmpty(stubArgumentName);

            // assert
            action.Should().Throw<ArgumentException>().WithParameterName(stubArgumentName);
        }

        public static IEnumerable ShouldNotEmptyTest_InputEmpty_ThrowExpectedException_TestCases()
        {
            yield return new TestCaseData(string.Empty);

            IEnumerable mockedEnumerable = Substitute.For<IEnumerable>();
            yield return new TestCaseData(mockedEnumerable);
        }
    }
}