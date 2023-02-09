using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;

namespace MoreNet.Foundation.Tests
{
    [TestFixture()]
    public partial class FluentUriBuilderTests
    {
        [Test()]
        [TestCaseSource(nameof(AddQueryTest_StringScenario_ThrowException_TestCases))]
        public void AddQueryTest_StringScenario_ThrowException(string stubKey, string[] stubValues)
        {
            // arrange
            var target = FluentUriBuilder.Create();

            // act
            Action act = () => target.AddQuery(stubKey, stubValues);

            // assert
            act.Should().Throw<Exception>();
        }

        public static IEnumerable AddQueryTest_StringScenario_ThrowException_TestCases()
        {
            // null
            yield return new TestCaseData("k", null);

            // empty
            yield return new TestCaseData("k", Array.Empty<string>());
        }

        [Test()]
        [TestCaseSource(nameof(AddQueryTest_EnumAsIntScenario_ThrowException_TestCases))]
        public void AddQueryTest_EnumAsIntScenario_ThrowException(string stubKey, FakeEnum[] stubValues)
        {
            // arrange
            var target = FluentUriBuilder.Create();

            // act
            Action act = () => target.AddQuery<FakeEnum, int>(stubKey, stubValues);

            // assert
            act.Should().Throw<Exception>();
        }

        public static IEnumerable AddQueryTest_EnumAsIntScenario_ThrowException_TestCases()
        {
            // null
            yield return new TestCaseData("k", null);

            // empty
            yield return new TestCaseData("k", Array.Empty<FakeEnum>());
        }
    }
}