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

        [Test()]
        [TestCaseSource(nameof(SequentialOrderByTestCaseSource_ReturnsExpectedInOrder))]
        public void SequentialOrderByTest_ReturnsExpectedInOrder(
            Func<IQueryable<Version>, IQueryable<Version>> arrangeQuery,
            IEnumerable<Version> expected)
        {
            // arrange
            var stubQueryable = GenerateVersions().AsQueryable();

            // act
            var actual = arrangeQuery.Invoke(stubQueryable).ToList();

            // assert
            actual.Should().BeEquivalentTo(expected, option => option.WithStrictOrdering());
        }

        [Test()]
        public void SequentialOrderByTest_WithIOrderable_ReturnsExpectedInOrder()
        {
            // arrange
            var stubQueryable = GenerateVersions().AsQueryable();
            var stubOrderable = Substitute.For<IOrderable<VersionOrderBy>>();
            stubOrderable.OrderBy.Returns(VersionOrderBy.MajorDescending);

            // act
            IQueryable<Version> actualQuery = stubQueryable;
            switch (stubOrderable.OrderBy)
            {
                case VersionOrderBy.Major:
                case VersionOrderBy.MajorDescending:
                    actualQuery = actualQuery.SequentialOrderBy(
                        stubOrderable.OrderBy,
                        item => item.Major,
                        stubOrderable.OrderBy == VersionOrderBy.MajorDescending);
                    break;
                case VersionOrderBy.Minor:
                case VersionOrderBy.MinorDescending:
                    actualQuery = actualQuery.SequentialOrderBy(
                        stubOrderable.OrderBy,
                        item => item.Minor,
                        stubOrderable.OrderBy == VersionOrderBy.MinorDescending);
                    break;
                case VersionOrderBy.Build:
                case VersionOrderBy.BuildDescending:
                    actualQuery = actualQuery.SequentialOrderBy(
                        stubOrderable.OrderBy,
                        item => item.Build,
                        stubOrderable.OrderBy == VersionOrderBy.BuildDescending);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var actual = actualQuery.ToList();

            // assert
            actual.Should().BeEquivalentTo(
                new List<Version>
                {
                    new Version(2, 2, 2, 0),
                    new Version(2, 1, 1, 0),
                    new Version(2, 2, 1, 0),
                    new Version(2, 1, 2, 0),
                    new Version(1, 1, 1, 0),
                    new Version(1, 2, 2, 0),
                    new Version(1, 1, 2, 0),
                    new Version(1, 2, 1, 0),
                },
                option => option.WithStrictOrdering());
        }

        [Test()]
        public void SequentialOrderByTest_WithISequentialOrderable_ReturnsExpectedInOrder()
        {
            // arrange
            var stubQueryable = GenerateVersions().AsQueryable();
            var stubSequentialOrderable = Substitute.For<ISequentialOrderable<VersionOrderBy>>();
            stubSequentialOrderable.SequentialOrderBy.Returns(
                new List<VersionOrderBy>
                {
                    VersionOrderBy.MajorDescending,
                    VersionOrderBy.MinorDescending,
                    VersionOrderBy.BuildDescending,
                });

            // act
            IQueryable<Version> actualQuery = stubQueryable;
            foreach (var orderBy in stubSequentialOrderable.SequentialOrderBy)
            {
                switch (orderBy)
                {
                    case VersionOrderBy.Major:
                    case VersionOrderBy.MajorDescending:
                        actualQuery = actualQuery.SequentialOrderBy(
                            orderBy,
                            item => item.Major,
                            orderBy == VersionOrderBy.MajorDescending);
                        break;
                    case VersionOrderBy.Minor:
                    case VersionOrderBy.MinorDescending:
                        actualQuery = actualQuery.SequentialOrderBy(
                            orderBy,
                            item => item.Minor,
                            orderBy == VersionOrderBy.MinorDescending);
                        break;
                    case VersionOrderBy.Build:
                    case VersionOrderBy.BuildDescending:
                        actualQuery = actualQuery.SequentialOrderBy(
                            orderBy,
                            item => item.Build,
                            orderBy == VersionOrderBy.BuildDescending);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var actual = actualQuery.ToList();

            // assert
            actual.Should().BeEquivalentTo(
                new List<Version>
                {
                    new Version(2, 2, 2, 0),
                    new Version(2, 2, 1, 0),
                    new Version(2, 1, 2, 0),
                    new Version(2, 1, 1, 0),
                    new Version(1, 2, 2, 0),
                    new Version(1, 2, 1, 0),
                    new Version(1, 1, 2, 0),
                    new Version(1, 1, 1, 0),
                },
                option => option.WithStrictOrdering());
        }

        public static IEnumerable SequentialOrderByTestCaseSource_ReturnsExpectedInOrder()
        {
            // Major ASC, Minor ASC, Build ASC
            yield return new TestCaseData(
                new Func<IQueryable<Version>, IQueryable<Version>>(
                    query => query
                        .SequentialOrderBy(VersionOrderBy.Major, item => item.Major, false)
                        .SequentialOrderBy(VersionOrderBy.Minor, item => item.Minor, false)
                        .SequentialOrderBy(VersionOrderBy.Build, item => item.Build, false)),
                new List<Version>
                {
                    new Version(1, 1, 1, 0),
                    new Version(1, 1, 2, 0),
                    new Version(1, 2, 1, 0),
                    new Version(1, 2, 2, 0),
                    new Version(2, 1, 1, 0),
                    new Version(2, 1, 2, 0),
                    new Version(2, 2, 1, 0),
                    new Version(2, 2, 2, 0),
                });

            // Major ASC, Minor ASC, Build DESC
            yield return new TestCaseData(
                new Func<IQueryable<Version>, IQueryable<Version>>(
                    query => query
                        .SequentialOrderBy(VersionOrderBy.Major, item => item.Major, false)
                        .SequentialOrderBy(VersionOrderBy.Minor, item => item.Minor, false)
                        .SequentialOrderBy(VersionOrderBy.BuildDescending, item => item.Build, true)),
                new List<Version>
                {
                    new Version(1, 1, 2, 0),
                    new Version(1, 1, 1, 0),
                    new Version(1, 2, 2, 0),
                    new Version(1, 2, 1, 0),
                    new Version(2, 1, 2, 0),
                    new Version(2, 1, 1, 0),
                    new Version(2, 2, 2, 0),
                    new Version(2, 2, 1, 0),
                });

            // Major ASC, Minor DESC, Build ASC
            yield return new TestCaseData(
                new Func<IQueryable<Version>, IQueryable<Version>>(
                    query => query
                        .SequentialOrderBy(VersionOrderBy.Major, item => item.Major, false)
                        .SequentialOrderBy(VersionOrderBy.MinorDescending, item => item.Minor, true)
                        .SequentialOrderBy(VersionOrderBy.Build, item => item.Build, false)),
                new List<Version>
                {
                    new Version(1, 2, 1, 0),
                    new Version(1, 2, 2, 0),
                    new Version(1, 1, 1, 0),
                    new Version(1, 1, 2, 0),
                    new Version(2, 2, 1, 0),
                    new Version(2, 2, 2, 0),
                    new Version(2, 1, 1, 0),
                    new Version(2, 1, 2, 0),
                });

            // Major ASC, Minor DESC, Build DESC
            yield return new TestCaseData(
                new Func<IQueryable<Version>, IQueryable<Version>>(
                    query => query
                        .SequentialOrderBy(VersionOrderBy.Major, item => item.Major, false)
                        .SequentialOrderBy(VersionOrderBy.MinorDescending, item => item.Minor, true)
                        .SequentialOrderBy(VersionOrderBy.BuildDescending, item => item.Build, true)),
                new List<Version>
                {
                    new Version(1, 2, 2, 0),
                    new Version(1, 2, 1, 0),
                    new Version(1, 1, 2, 0),
                    new Version(1, 1, 1, 0),
                    new Version(2, 2, 2, 0),
                    new Version(2, 2, 1, 0),
                    new Version(2, 1, 2, 0),
                    new Version(2, 1, 1, 0),
                });

            // Major DESC, Minor ASC, Build ASC
            yield return new TestCaseData(
                new Func<IQueryable<Version>, IQueryable<Version>>(
                    query => query
                        .SequentialOrderBy(VersionOrderBy.MajorDescending, item => item.Major, true)
                        .SequentialOrderBy(VersionOrderBy.Minor, item => item.Minor, false)
                        .SequentialOrderBy(VersionOrderBy.Build, item => item.Build, false)),
                new List<Version>
                {
                    new Version(2, 1, 1, 0),
                    new Version(2, 1, 2, 0),
                    new Version(2, 2, 1, 0),
                    new Version(2, 2, 2, 0),
                    new Version(1, 1, 1, 0),
                    new Version(1, 1, 2, 0),
                    new Version(1, 2, 1, 0),
                    new Version(1, 2, 2, 0),
                });

            // Major DESC, Minor ASC, Build DESC
            yield return new TestCaseData(
                new Func<IQueryable<Version>, IQueryable<Version>>(
                    query => query
                        .SequentialOrderBy(VersionOrderBy.MajorDescending, item => item.Major, true)
                        .SequentialOrderBy(VersionOrderBy.Minor, item => item.Minor, false)
                        .SequentialOrderBy(VersionOrderBy.BuildDescending, item => item.Build, true)),
                new List<Version>
                {
                    new Version(2, 1, 2, 0),
                    new Version(2, 1, 1, 0),
                    new Version(2, 2, 2, 0),
                    new Version(2, 2, 1, 0),
                    new Version(1, 1, 2, 0),
                    new Version(1, 1, 1, 0),
                    new Version(1, 2, 2, 0),
                    new Version(1, 2, 1, 0),
                });

            // Major DESC, Minor DESC, Build ASC
            yield return new TestCaseData(
                new Func<IQueryable<Version>, IQueryable<Version>>(
                    query => query
                        .SequentialOrderBy(VersionOrderBy.MajorDescending, item => item.Major, true)
                        .SequentialOrderBy(VersionOrderBy.MinorDescending, item => item.Minor, true)
                        .SequentialOrderBy(VersionOrderBy.Build, item => item.Build, false)),
                new List<Version>
                {
                    new Version(2, 2, 1, 0),
                    new Version(2, 2, 2, 0),
                    new Version(2, 1, 1, 0),
                    new Version(2, 1, 2, 0),
                    new Version(1, 2, 1, 0),
                    new Version(1, 2, 2, 0),
                    new Version(1, 1, 1, 0),
                    new Version(1, 1, 2, 0),
                });

            // Major DESC, Minor DESC, Build DESC
            yield return new TestCaseData(
                new Func<IQueryable<Version>, IQueryable<Version>>(
                    query => query
                        .SequentialOrderBy(VersionOrderBy.MajorDescending, item => item.Major, true)
                        .SequentialOrderBy(VersionOrderBy.MinorDescending, item => item.Minor, true)
                        .SequentialOrderBy(VersionOrderBy.BuildDescending, item => item.Build, true)),
                new List<Version>
                {
                    new Version(2, 2, 2, 0),
                    new Version(2, 2, 1, 0),
                    new Version(2, 1, 2, 0),
                    new Version(2, 1, 1, 0),
                    new Version(1, 2, 2, 0),
                    new Version(1, 2, 1, 0),
                    new Version(1, 1, 2, 0),
                    new Version(1, 1, 1, 0),
                });
        }

        private static List<Version> GenerateVersions()
        {
            return new List<Version>
            {
                new Version(2, 2, 2, 0),
                new Version(1, 1, 1, 0),
                new Version(2, 1, 1, 0),
                new Version(1, 2, 2, 0),
                new Version(1, 1, 2, 0),
                new Version(2, 2, 1, 0),
                new Version(1, 2, 1, 0),
                new Version(2, 1, 2, 0),
            };
        }

        public enum VersionOrderBy
        {
            Major = 1,
            MajorDescending = 2,
            Minor = 4,
            MinorDescending = 8,
            Build = 16,
            BuildDescending = 32,
        }
    }
}
