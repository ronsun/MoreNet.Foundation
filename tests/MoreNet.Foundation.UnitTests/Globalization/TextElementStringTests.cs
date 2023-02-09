using MoreNet.Foundation.Globalization;
using FluentAssertions;
using NUnit.Framework;
using System.Collections;

namespace MoreNet.Foundation.Globalization.Tests
{
    [TestFixture()]
    public partial class TextElementStringTests
    {
        [Test()]
        // regular, full match in sequence
        [TestCase("a", "a", true)]
        [TestCase("ab", "ab", true)]
        [TestCase("ab", "ba", false)]
        [TestCase("abc", "ac", false)]
        // regualr, contains
        [TestCase("aeio", "ae", true)]
        [TestCase("aeio", "ei", true)]
        [TestCase("aeio", "io", true)]
        // regualr, contains with partial match
        [TestCase("aaaoaaai", "aai", true)]
        [TestCase("aaaoaaai", "aau", false)]
        // grapheme, should not contains
        [TestCase("e\u0301"/* é */, "e", false)]
        [TestCase("e\u0301"/* é */, "\u0301", false)]
        // mixed alphabets and grapheme in any normalization, should contains
        [TestCase(
            "-a e\u0301 i\u0301 o\u0301 u\u0301-" /* -a é í ó ú- */,
            "a \u00E9 \u0069\u0301 \u00F3" /* a é(NormalizationForm.FormC) í(NormalizationForm.FormD/FormKD) ó(NormalizationForm.FormKC) */,
            true)]
        public void ContainsTest_NormalCases(string stubTargetString, string stubSubstring, bool expected)
        {
            // arrange
            var target = new TextElementString(stubTargetString);

            // act
            var actual = target.Contains(stubSubstring);

            // assert
            actual.Should().Be(expected);
        }

        [Test()]
        [TestCaseSource(nameof(ContainsTestCaseSource_EmptyEdgeCases))]
        public void ContainsTest_EmptyEdgeCases(string stubTargetString, string stubSubstring, bool expected)
        {
            // arrange
            var target = new TextElementString(stubTargetString);

            // act
            var actual = target.Contains(stubSubstring);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable ContainsTestCaseSource_EmptyEdgeCases()
        {
            yield return new TestCaseData("", "", "".Contains(""));
            yield return new TestCaseData(null, "", new string((char[])null).Contains(""));
            yield return new TestCaseData("a", "", "a".Contains(""));
            yield return new TestCaseData("a", "", "a".Contains(""));
        }

        [Test()]
        // regualr
        [TestCase("")]
        [TestCase(null)]
        [TestCase("aeio")]
        // grapheme with normalization
        [TestCase("a\u0301e\u0301i\u0301o\u0301" /* áéíó */)]
        // mixed alphabets and grapheme in any normalization
        [TestCase(
            "a \u00E9 \u0069\u0301 \u00F3" /* a é(NormalizationForm.FormC) í(NormalizationForm.FormD/FormKD) ó(NormalizationForm.FormKC) */
            )]
        public void ToStringTest(string stubString)
        {
            // arrange
            var target = new TextElementString(stubString);
            var expected = $"{stubString}";

            // act
            var actual = target.ToString();

            // assert
            actual.Should().Be(expected);
        }

        [Test()]
        // regualr
        [TestCase("")]
        [TestCase(null)]
        [TestCase("aeio")]
        // grapheme with normalization
        [TestCase("a\u0301e\u0301i\u0301o\u0301" /* áéíó */)]
        // mixed alphabets and grapheme in any normalization
        [TestCase("a e\u0301 i\u0301 o\u0301 u\u0301" /* a é í ó ú */)]
        public void ImplictlyConversionTest_ToString(string stubString)
        {
            // arrange
            var target = new TextElementString(stubString);
            var expected = $"{stubString}";

            // act
            string actual = target;

            // assert
            actual.Should().Be(expected);
        }
    }
}