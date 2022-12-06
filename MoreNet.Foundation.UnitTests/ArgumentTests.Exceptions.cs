using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;

namespace MoreNet.Foundation.Tests
{
    [TestFixture()]
    public partial class ArgumentTests
    {
        [Test()]
        [TestCaseSource(nameof(ShouldNotEmptyTest_ThrowExpectedException_TestCases))]
        public void ShouldNotEmptyTest_ThrowExpectedException(object stubValue)
        {
            // arrange
            var stubName = nameof(stubValue);

            // act
            Action action = () => Argument.ShouldNotEmpty(stubValue, stubName);

            // assert
            action.Should().Throw<ArgumentException>().WithParameterName(stubName);
        }

        public static IEnumerable ShouldNotEmptyTest_ThrowExpectedException_TestCases()
        {
            // string (for IEnumerable<T>)
            string stubString = null;
            yield return new TestCaseData(stubString);
            stubString = string.Empty;
            yield return new TestCaseData(stubString);

            // IEnumerable
            IEnumerable stubEnumerable = null;
            yield return new TestCaseData(stubEnumerable);
            stubEnumerable = new object[] { };

            // Nullabe
            int? stubNullable = null;
            yield return new TestCaseData(stubNullable);
        }

        [Test()]
        public void ShouldBeDefinedTest_InputInvalid_ThrowExpectedException()
        {
            // arrange
            var stubValue = (FakeEnum)(-1);
            var stubArgumentName = nameof(stubValue);

            // act
            Action action = () => stubValue.ShouldBeDefined(stubArgumentName);

            // assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(stubArgumentName);
        }
    }
}