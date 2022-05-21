using MoreNet.Foundation;
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

        [Test()]
        public void AddQueryTest_AddMupltiple_GetUriSameWithCreatedByUriBuilder()
        {
            // arrange
            var stubKey1 = "k1";
            var stubValue1 = "v1";
            var stubKey2 = "k2";
            var stubValue2 = "v2";
            var target = FluentUriBuilder.Create();
            var expectedQuery = $"{stubKey1}={stubValue1}&{stubKey2}={stubValue2}";
            var expected = new UriBuilder() { Query = expectedQuery }.Uri;

            // act
            var actual = target
                .AddQuery(stubKey1, stubValue1)
                .AddQuery(stubKey2, stubValue2)
                .Uri;

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test()]
        public void SetPortTest_GetUriSameWithCreatedByUriBuilder()
        {
            // arrange
            int stubPort = 8000;
            var target = FluentUriBuilder.Create();
            var expected = new UriBuilder() { Port = stubPort }.Uri;

            // act
            var actual = target.SetPort(stubPort).Uri;

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test()]
        public void SetPathTest_GetUriSameWithCreatedByUriBuilder()
        {
            // arrange
            string stubPath = "/p/a/t/h";
            var target = FluentUriBuilder.Create();
            var expected = new UriBuilder() { Path = stubPath }.Uri;

            // act
            var actual = target.SetPath(stubPath).Uri;

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test()]
        public void SetUserTest_UserNameOnly_GetUriSameWithCreatedByUriBuilder()
        {
            // arrange
            string stubUserName = "user";
            var target = FluentUriBuilder.Create();
            var expected = new UriBuilder() { UserName = stubUserName }.Uri;

            // act
            var actual = target.SetUser(stubUserName).Uri;

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test()]
        public void SetUserTest_UserNameAndPassword_GetUriSameWithCreatedByUriBuilder()
        {
            // arrange
            string stubUserName = "user";
            string stubPassword = "password";
            var target = FluentUriBuilder.Create();
            var expected = new UriBuilder() { UserName = stubUserName, Password = stubPassword }.Uri;

            // act
            var actual = target.SetUser(stubUserName, stubPassword).Uri;

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test()]
        public void SetFragment_GetUriSameWithCreatedByUriBuilder()
        {
            // arrange
            string stubFragment = "fragment";
            var target = FluentUriBuilder.Create();
            var expected = new UriBuilder() { Fragment = stubFragment }.Uri;

            // act
            var actual = target.SetFragment(stubFragment).Uri;

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test()]
        public void SetHost_GetUriSameWithCreatedByUriBuilder()
        {
            // arrange
            string stubHost = "host";
            var target = FluentUriBuilder.Create();
            var expected = new UriBuilder() { Host = stubHost }.Uri;

            // act
            var actual = target.SetHost(stubHost).Uri;

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test()]
        public void ToStringTest_GetUriSameWithCreatedByUriBuilder()
        {
            // arrange
            var target = FluentUriBuilder.Create();
            var expected = new UriBuilder().ToString();

            // act
            var actual = target.ToString();

            // assert
            actual.Should().BeEquivalentTo(expected);
        }


        [Test()]
        public void ToStringTest_AddQueries_GetUriSameWithCreatedByUriBuilder()
        {
            // arrange
            var stubKey1 = "k1";
            var stubValue1 = "v1";
            var stubKey2 = "k2";
            var stubValue2 = "v2";
            var target = FluentUriBuilder.Create();
            var expectedQuery = $"{stubKey1}={stubValue1}&{stubKey2}={stubValue2}";
            var expected = new UriBuilder() { Query = expectedQuery }.ToString();

            // act
            var actual = target
                .AddQuery(stubKey1, stubValue1)
                .AddQuery(stubKey2, stubValue2)
                .ToString();

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

    }
}