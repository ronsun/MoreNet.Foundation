using FluentAssertions;
using MoreNet.Foundation.Conventions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MoreNet.Foundation.Extensions.Tests
{
    [TestFixture()]
    public partial class QueryableExtensionsTests
    {
        [Test()]
        public void WhereIfTest_ShouldAppend_ExpectedQueryResult()
        {
            // arrange
            var stubQueryable = new List<string> { "a", "b" }.AsQueryable();
            Expression<Func<string, bool>> stubPredicate = r => r == "b";
            var expected = "b";

            // act
            var actual = stubQueryable.WhereIf(stubPredicate, true).First();

            // assert
            actual.Should().Be(expected);
        }

        [Test()]
        public void WhereIfTest_ShouldNotAppend_ExpectedQueryResult()
        {
            // arrange
            var stubQueryable = new List<string> { "a", "b" }.AsQueryable();
            Expression<Func<string, bool>> stubPredicate = r => r == "b";
            var expected = "a";

            // act
            var actual = stubQueryable.WhereIf(stubPredicate, false).First();

            // assert
            actual.Should().Be(expected);
        }

        [Test()]
        [TestCaseSource(nameof(PaginateTestCaseSource))]
        public void PaginateTest_ShouldGotExpectedListInOrder(
            IQueryable<string> stubQueryable,
            int stubPageNumber,
            int stubPageSize,
            List<string> expected)
        {
            // arrange
            var stubIPageable = Substitute.For<IPageable>();
            stubIPageable.PageNumber.Returns(stubPageNumber);
            stubIPageable.PageSize.Returns(stubPageSize);

            // act
            var actual = stubQueryable.Paginate(stubIPageable).ToList();

            // assert
            actual.Should().BeEquivalentTo(expected, option => option.WithStrictOrdering());
        }

        public static IEnumerable PaginateTestCaseSource()
        {
            IQueryable<string> stubQueryable = new List<string> { "a", "b", "c", "d" }.AsQueryable();
            // in range
            yield return new TestCaseData(stubQueryable, 1, 1, GenerateExpected("a"));
            yield return new TestCaseData(stubQueryable, 2, 1, GenerateExpected("b"));
            yield return new TestCaseData(stubQueryable, 1, 2, GenerateExpected("a", "b"));
            yield return new TestCaseData(stubQueryable, 2, 2, GenerateExpected("c", "d"));

            // take more than rest
            yield return new TestCaseData(stubQueryable, 1, 5, GenerateExpected("a", "b", "c", "d"));
            yield return new TestCaseData(stubQueryable, 2, 3, GenerateExpected("d"));

            // over page size
            yield return new TestCaseData(stubQueryable, 2, int.MaxValue, GenerateExpected());

            List<string> GenerateExpected(params string[] s)
            {
                return s.ToList();
            }
        }
    }
}