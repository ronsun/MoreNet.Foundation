﻿using FluentAssertions;
using NUnit.Framework;

namespace MoreNet.Foundation.Extensions.Tests
{
    [TestFixture()]
    public class StringExtensionsTests
    {
        [Test()]
        [TestCase(@" !#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~中", @"\u0020\u0021\u0023\u0024\u0025\u0026\u0027\u0028\u0029\u002A\u002B\u002C\u002D\u002E\u002F\u0030\u0031\u0032\u0033\u0034\u0035\u0036\u0037\u0038\u0039\u003A\u003B\u003C\u003D\u003E\u003F\u0040\u0041\u0042\u0043\u0044\u0045\u0046\u0047\u0048\u0049\u004A\u004B\u004C\u004D\u004E\u004F\u0050\u0051\u0052\u0053\u0054\u0055\u0056\u0057\u0058\u0059\u005A\u005B\u005C\u005D\u005E\u005F\u0060\u0061\u0062\u0063\u0064\u0065\u0066\u0067\u0068\u0069\u006A\u006B\u006C\u006D\u006E\u006F\u0070\u0071\u0072\u0073\u0074\u0075\u0076\u0077\u0078\u0079\u007A\u007B\u007C\u007D\u007E\u4E2D")]
        // special case, escape double quote
        [TestCase("\u0022", @"\u0022")]
        [TestCase("", "")]
        public void ToUnicodeTest_NoParameter_ReturnsExpected(string stubString, string expected)
        {
            // arrange

            // act
            var actual = stubString.ToUnicode();

            // assert
            actual.Should().Be(expected);
        }

        [Test()]
        [TestCase("中", "x4", @"\u4e2d")]
        [TestCase("中", "X4", @"\u4E2D")]
        [TestCase("", "X4", "")]
        public void ToUnicodeTest_WithFormat_ReturnsExpected(string stubString, string stubFormat, string expected)
        {
            // arrange

            // act
            var actual = stubString.ToUnicode(stubFormat);

            // assert
            actual.Should().Be(expected);
        }

        [Test()]
        // prefix
        [TestCase("中", "x4", @"\u", default, @"\u4e2d")]
        [TestCase("中", "x4", "u+", default, "u+4e2d")]
        // format
        [TestCase("中", "x4", @"\u", default, @"\u4e2d")]
        [TestCase("中", "X4", @"\u", default, @"\u4E2D")]
        // ignore printable ascii
        [TestCase(@" !#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~中", "X4", @"\u", false, @"\u0020\u0021\u0023\u0024\u0025\u0026\u0027\u0028\u0029\u002A\u002B\u002C\u002D\u002E\u002F\u0030\u0031\u0032\u0033\u0034\u0035\u0036\u0037\u0038\u0039\u003A\u003B\u003C\u003D\u003E\u003F\u0040\u0041\u0042\u0043\u0044\u0045\u0046\u0047\u0048\u0049\u004A\u004B\u004C\u004D\u004E\u004F\u0050\u0051\u0052\u0053\u0054\u0055\u0056\u0057\u0058\u0059\u005A\u005B\u005C\u005D\u005E\u005F\u0060\u0061\u0062\u0063\u0064\u0065\u0066\u0067\u0068\u0069\u006A\u006B\u006C\u006D\u006E\u006F\u0070\u0071\u0072\u0073\u0074\u0075\u0076\u0077\u0078\u0079\u007A\u007B\u007C\u007D\u007E\u4E2D")]
        [TestCase("\u0022", "X4", @"\u", false, @"\u0022")]
        [TestCase(@" !#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~中", "X4", @"\u", true, @" !#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~\u4E2D")]
        [TestCase("\u0022", "X4", @"\u", true, @"""")]
        // empty
        [TestCase("", "X4", @"\u", true, "")]
        public void ToUnicodeTest_InputAllParameters_ReturnsExpected(
            string stubString,
            string stubFormat,
            string stubPrefix,
            bool stubConvertAll,
            string expected)
        {
            // arrange

            // act
            var actual = stubString.ToUnicode(stubFormat, stubPrefix, stubConvertAll);

            // assert
            actual.Should().Be(expected);
        }
    }
}