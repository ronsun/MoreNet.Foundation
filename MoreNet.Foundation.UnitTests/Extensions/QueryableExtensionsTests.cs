using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MoreNet.Foundation.Extensions.Tests
{
    [TestFixture()]
    public class QueryableExtensionsTests
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
    }
}