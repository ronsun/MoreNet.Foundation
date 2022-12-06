using NUnit.Framework;
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
            stubValue.ShouldNotNull(stubArgumentName);

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
            stubValue.ShouldBeDefined(stubArgumentName);

            // assert
            Assert.Pass();
        }
    }
}