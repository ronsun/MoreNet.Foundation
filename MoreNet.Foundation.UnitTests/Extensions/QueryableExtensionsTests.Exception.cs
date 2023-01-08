using FluentAssertions;
using MoreNet.Foundation.Conventions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;

namespace MoreNet.Foundation.Extensions.Tests
{
    [TestFixture()]
    public partial class QueryableExtensionsTests
    {
        [Test()]
        public void PaginateTest_Overflow_ThrowExpectedException()
        {
            // arrange
            var mockedQueryable = Substitute.For<IQueryable<string>>();
            var stubIPageable = Substitute.For<IPageable>();
            stubIPageable.PageNumber.Returns(int.MaxValue);
            stubIPageable.PageSize.Returns(3);

            // act
            Action action = () => mockedQueryable.Paginate(stubIPageable);

            // assert
            action.Should().Throw<OverflowException>();
        }

        [Test()]
        public void PaginateTest_PageNumberOutOfRange_ThrowExpectedException()
        {
            // arrange
            var mockedQueryable = Substitute.For<IQueryable<string>>();
            var stubIPageable = Substitute.For<IPageable>();
            stubIPageable.PageNumber.Returns(-1);
            stubIPageable.PageSize.Returns(1);

            // act
            Action action = () => mockedQueryable.Paginate(stubIPageable);

            // assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test()]
        public void PaginateTest_PageSizeOutOfRange_ThrowExpectedException()
        {
            // arrange
            var mockedQueryable = Substitute.For<IQueryable<string>>();
            var stubIPageable = Substitute.For<IPageable>();
            stubIPageable.PageNumber.Returns(1);
            stubIPageable.PageSize.Returns(0);

            // act
            Action action = () => mockedQueryable.Paginate(stubIPageable);

            // assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}