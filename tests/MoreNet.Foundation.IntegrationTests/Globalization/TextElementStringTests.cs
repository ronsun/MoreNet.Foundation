using FluentAssertions;
using NUnit.Framework;
using System.Collections;
using System.Linq;

namespace MoreNet.Foundation.Globalization.Tests
{
    [TestFixture()]
    public class TextElementStringTests
    {
        [Test()]
        [TestCaseSource(nameof(ReverseTestCaseSource_ReturnsLikeFromString))]
        public void ReverseTest_ReturnsLikeFromString(string stubTargetString, string expected)
        {
            // arrange
            var target = new TextElementString(stubTargetString);

            // act
            string actual = target.Reverse();

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable ReverseTestCaseSource_ReturnsLikeFromString()
        {
            // regular, empty
            yield return new TestCaseData("", new string("".Reverse().ToArray()));
            yield return new TestCaseData(null, new string(new string((char[])null).Reverse().ToArray()));

            // regular, empty
            yield return new TestCaseData("ab", new string("ab".Reverse().ToArray()));

            // mixed alphabets and grapheme in any normalization
            yield return new TestCaseData(
                "a \u00E9 \u0069\u0301 \u00F3" /* a é(NormalizationForm.FormC) í(NormalizationForm.FormD/FormKD) ó(NormalizationForm.FormKC) */,
                "\u00F3 \u0069\u0301 \u00E9 a");
        }

        [Test()]
        [TestCaseSource(nameof(ReplaceTestCaseSource_ReturnsLikeFromString))]
        public void ReplaceTest_ReturnsLikeFromString(string stubTargetString, string stubOldValue, string stubNewValue, string expected)
        {
            // arrange
            var target = new TextElementString(stubTargetString);

            // act
            string actual = target.Replace(stubOldValue, stubNewValue);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable ReplaceTestCaseSource_ReturnsLikeFromString()
        {
            // regular, replace to empty
            yield return new TestCaseData("a", "a", "", "a".Replace("a", ""));
            yield return new TestCaseData("a", "a", null, "a".Replace("a", null));

            // regular, include replacement from shorter to longer and from longer to shorter
            yield return new TestCaseData("aeio aeio aeio", "ei", "123", "aeio aeio aeio".Replace("ei", "123"));
            yield return new TestCaseData("aeio aeio aeio", "ei", "1", "aeio aeio aeio".Replace("ei", "1"));

            // regular, replace from head
            yield return new TestCaseData("aiaiai", "aiai", "12", "aiaiai".Replace("aiai", "12"));
            yield return new TestCaseData("aiaiai", "aai", "1", "aiaiai".Replace("aai", "1"));

            // regular, replace many times with partial match 
            yield return new TestCaseData("aiaiaa", "ai", "12", "aiaiaa".Replace("ai", "12"));
            yield return new TestCaseData("aiaaai", "ai", "12", "aiaaai".Replace("ai", "12"));
            yield return new TestCaseData("aaaiai", "ai", "12", "aaaiai".Replace("ai", "12"));

            // grapheme, should not replaced
            yield return new TestCaseData("e\u0301"/* é */, "e", "", "e\u0301");
            yield return new TestCaseData("e\u0301"/* é */, "\u0301", "", "\u0065\u0301");

            // mixed alphabets and grapheme in any normalization
            yield return new TestCaseData(
                "-a e\u0301 i\u0301 o\u0301 u\u0301-" /* -a é í ó ú- */,
                "a \u00E9 \u0069\u0301 \u00F3" /* a é(NormalizationForm.FormC) í(NormalizationForm.FormD/FormKD) ó(NormalizationForm.FormKC) */,
                "A e i o",
                "-A e i o u\u0301-");
        }

        [Test()]
        [TestCaseSource(nameof(RemoveTestCaseSource_InputStartIndex))]
        public void RemoveTest_InputStartIndex(string stubTargetString, int stubStartIndex, string expected)
        {
            // arrange
            var target = new TextElementString(stubTargetString);

            // act
            string actual = target.Remove(stubStartIndex);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable RemoveTestCaseSource_InputStartIndex()
        {
            // string
            yield return new TestCaseData("a", 0, "a".Remove(0));
            yield return new TestCaseData("ab", 0, "ab".Remove(0));
            yield return new TestCaseData("ab", 1, "ab".Remove(1));
            // mixed alphabets and grapheme in any normalization
            yield return new TestCaseData(
                "a \u00E9 \u0069\u0301 \u00F3 -" /* a é(NormalizationForm.FormC) í(NormalizationForm.FormD/FormKD) ó(NormalizationForm.FormKC) - */,
                8,
                "a \u00E9 \u0069\u0301 \u00F3 ");
        }

        [Test()]
        [TestCaseSource(nameof(RemoveTestCaseSource_InputStartIndexAndCount))]
        public void RemoveTest_InputStartIndexAndCount(string stubTargetString, int stubStartIndex, int stubCount, string expected)
        {
            // arrange
            var target = new TextElementString(stubTargetString);

            // act
            string actual = target.Remove(stubStartIndex, stubCount);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable RemoveTestCaseSource_InputStartIndexAndCount()
        {
            // string, regular
            yield return new TestCaseData("a", 0, 0, "a".Remove(0, 0));
            yield return new TestCaseData("ab", 0, 1, "ab".Remove(0, 1));
            yield return new TestCaseData("ab", 1, 1, "ab".Remove(1, 1));
            yield return new TestCaseData("ab", 1, 0, "ab".Remove(1, 0));

            // mixed alphabets and grapheme in any normalization
            yield return new TestCaseData(
                "a \u00E9 \u0069\u0301 \u00F3 -" /* a é(NormalizationForm.FormC) í(NormalizationForm.FormD/FormKD) ó(NormalizationForm.FormKC) - */,
                8,
                1,
                "a \u00E9 \u0069\u0301 \u00F3 ");
        }
    }
}