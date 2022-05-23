using NSubstitute;
using NUnit.Framework;
using System.Collections;
using MoreNet.Foundation.Extensions;

namespace MoreNet.Foundation.Assertion.Tests
{
    [TestFixture()]
    public partial class GenericExtensionsTests
    {
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

            IEnumerable mockedEnumerable = Substitute.For<IEnumerable>();
            yield return new TestCaseData(mockedEnumerable);
        }

        [Test()]
        [TestCaseSource(nameof(ShouldNotEmptyTest_InputValid_Pass_TestCases))]
        public void ShouldNotEmptyTest_InputValid_Pass<T>(T stubValue)
            where T : class
        {
            // arrange
            var stubArgumentName = nameof(stubValue);

            // act
            stubValue.ShouldNotEmpty(stubArgumentName);

            // assert
            Assert.Pass();
        }

        public static IEnumerable ShouldNotEmptyTest_InputValid_Pass_TestCases()
        {
            yield return new TestCaseData(" ");
            yield return new TestCaseData("a");

            int i = 0;
            IEnumerator mockedEnumerator = Substitute.For<IEnumerator>();
            mockedEnumerator.MoveNext().Returns(i++ < 1);
            mockedEnumerator.Current.Returns(new object());
            IEnumerable mockedEnumerable = Substitute.For<IEnumerable>();
            mockedEnumerable.GetEnumerator().Returns(mockedEnumerator);
            yield return new TestCaseData(mockedEnumerable);
        }
    }
}