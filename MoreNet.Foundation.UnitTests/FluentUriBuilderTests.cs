using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;

namespace MoreNet.Foundation.Tests
{
    [TestFixture()]
    public partial class FluentUriBuilderTests
    {
        public enum FakeEnum
        {
            A = 1,
            B = 2,
        }

        [Test()]
        public void CreateTest_GetUriSameWithCreatedByUriBuilder()
        {
            // arrange
            var expected = new UriBuilder().Uri;

            // act
            var actual = FluentUriBuilder.Create().Uri;

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test()]
        public void CreateTest_InputUri_GetUriSameWithCreatedByUriBuilder()
        {
            // arrange
            var stubUri = new Uri("http://a.b.c");
            var expected = new UriBuilder(stubUri).Uri;

            // act
            var actual = FluentUriBuilder.Create(stubUri).Uri;

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test()]
        public void SetSchemeTest_GetUriSameWithCreatedByUriBuilder()
        {
            // arrange
            var stubScheme = "https";
            var target = FluentUriBuilder.Create();
            var expected = new UriBuilder() { Scheme = stubScheme }.Uri;

            // act
            var actual = target.SetScheme(stubScheme).Uri;

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test()]
        public void SetQueryTest_GetUriSameWithCreatedByUriBuilder()
        {
            // arrange
            var stubQuery = "q=1";
            var target = FluentUriBuilder.Create();
            var expected = new UriBuilder() { Query = stubQuery }.Uri;

            // act
            var actual = target.SetQuery(stubQuery).Uri;

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test()]
        [TestCaseSource(nameof(AddQueryTest_StringScenario_GetUriSameWithCreatedByUriBuilder_TestCases))]
        public void AddQueryTest_StringScenario_GetUriSameWithCreatedByUriBuilder(string stubKey, string[] stubValues)
        {
            // arrange
            var target = FluentUriBuilder.Create();
            var expectedQuery = string.Join("&", stubValues.Where(r => !string.IsNullOrEmpty(r)).Select(r => $"{stubKey}={r}"));
            var expected = new UriBuilder() { Query = expectedQuery }.Uri;

            // act
            var actual = target.AddQuery(stubKey, stubValues).Uri;

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable AddQueryTest_StringScenario_GetUriSameWithCreatedByUriBuilder_TestCases()
        {
            // single value
            yield return new TestCaseData("k", new string[] { "v1" });

            // multiple values
            yield return new TestCaseData("k", new string[] { "v1", "v2" });

            // null value in values
            yield return new TestCaseData("k", new string[] { "" });
            yield return new TestCaseData("k", new string[] { null });
            yield return new TestCaseData("k", new string[] { "v", "" });
            yield return new TestCaseData("k", new string[] { "v", null });
        }

        [Test()]
        [TestCaseSource(nameof(AddQueryTest_EnumAsIntScenario_GetUriSameWithCreatedByUriBuilder_TestCases))]
        public void AddQueryTest_EnumAsIntScenario_GetUriSameWithCreatedByUriBuilder(string stubKey, FakeEnum[] stubValues)
        {
            // arrange
            var target = FluentUriBuilder.Create();
            var expectedQuery = string.Join("&", stubValues.Select(r => $"{stubKey}={(int)r}"));
            var expected = new UriBuilder() { Query = expectedQuery }.Uri;

            // act
            var actual = target.AddQuery<FakeEnum, int>(stubKey, stubValues).Uri;

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable AddQueryTest_EnumAsIntScenario_GetUriSameWithCreatedByUriBuilder_TestCases()
        {
            // single value
            yield return new TestCaseData("k", new FakeEnum[] { FakeEnum.A });

            // multiple values
            yield return new TestCaseData("k", new FakeEnum[] { FakeEnum.A, FakeEnum.B });
        }
    }
}