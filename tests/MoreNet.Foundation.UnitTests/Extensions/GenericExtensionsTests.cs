using FluentAssertions;
using NUnit.Framework;

namespace MoreNet.Foundation.Extensions.Tests
{
    [TestFixture()]
    public class GenericExtensionsTests
    {
        [Test()]
        public void InTest_In()
        {
            // arrange
            string stubItem = "a";

            // act
            var actual = stubItem.In("a", "b", "c");

            // assert
            actual.Should().BeTrue();
        }

        [Test()]
        public void InTest_NotIn()
        {
            // arrange
            string stubItem = "a";

            // act
            var actual = stubItem.In("b", "c", "d");

            // assert
            actual.Should().BeFalse();
        }
    }
}
