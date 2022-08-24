using FluentAssertions;
using NUnit.Framework;
using System;

namespace MoreNet.Foundation.Globalization.Tests
{
    [TestFixture()]
    public partial class TextElementStringTests
    {
        [Test()]
        public void ContainsTest_ContainsNull_ThrowExpectedException()
        {
            // arrange
            var target = new TextElementString("");

            // act
            Action actual = () => target.Contains(null);

            // assert
            actual.Should().ThrowExactly<ArgumentNullException>();
        }

    }
}