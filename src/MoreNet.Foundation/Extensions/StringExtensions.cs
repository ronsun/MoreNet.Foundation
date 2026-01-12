using System;
using System.Text;

namespace MoreNet.Foundation.Extensions
{
    /// <summary>
    /// Extensions for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        private const string DefaultUnicodeFormat = "X4";

        private const string DefaultUnicodePrefix = @"\u";

        /// <summary>
        /// Convert string to unicode in default prefix and format, ex: "\u012A".
        /// </summary>
        /// <inheritdoc cref="ToUnicode(string, string, string, bool)"/>
        public static string ToUnicode(this string value)
        {
            return ToUnicode(value, DefaultUnicodeFormat, DefaultUnicodePrefix, false);
        }

        /// <summary>
        /// Convert string to unicode in default prefix <see cref="DefaultUnicodePrefix"/>.
        /// </summary>
        /// <inheritdoc cref="ToUnicode(string, string, string, bool)"/>
        public static string ToUnicode(this string value, string format)
        {
            return ToUnicode(value, format, DefaultUnicodePrefix, false);
        }

        /// <summary>
        /// Convert string to unicode.
        /// </summary>
        /// <param name="value">The extended <see cref="string"/>.</param>
        /// <param name="format">Format, ex: <see cref="DefaultUnicodeFormat"/>.</param>
        /// <param name="prefix">Prefix, ex: <see cref="DefaultUnicodePrefix"/>.</param>
        /// <param name="ignorePrintableAscii">If true, ignore printable ASCII from 32 (included) to 126 (included), otherwise, convert all.</param>
        /// <returns>Unicode.</returns>
        public static string ToUnicode(this string value, string format, string prefix, bool ignorePrintableAscii)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                // ASCII range
                if (ignorePrintableAscii && c >= 32 && c <= 126)
                {
                    sb.Append(c);
                }
                else
                {
                    sb.Append(prefix);
                    sb.Append(((int)c).ToString(format, default));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Mask a portion of the string with the default mask character '*'.
        /// </summary>
        /// <param name="value">The extended <see cref="string"/>.</param>
        /// <param name="startIndex">The zero-based starting character position of the mask.</param>
        /// <param name="length">The number of characters to mask.</param>
        /// <returns>A new string with the specified portion masked.</returns>
        public static string Mask(this string value, int startIndex, int length)
        {
            return Mask(value, startIndex, length, null);
        }

        /// <summary>
        /// Mask a portion of the string with the default mask character '*'.
        /// </summary>
        /// <param name="value">The extended <see cref="string"/>.</param>
        /// <param name="startIndex">The zero-based starting character position of the mask.</param>
        /// <param name="length">The number of characters to mask.</param>
        /// <param name="maskLength">The length of the mask. If null, uses the same length as the masked portion.</param>
        /// <returns>A new string with the specified portion masked.</returns>
        public static string Mask(this string value, int startIndex, int length, int? maskLength)
        {
            return Mask(value, startIndex, length, maskLength, '*');
        }

        /// <summary>
        /// Mask a portion of the string with the specified mask character.
        /// </summary>
        /// <param name="value">The extended <see cref="string"/>.</param>
        /// <param name="startIndex">The zero-based starting character position of the mask.</param>
        /// <param name="length">The number of characters to mask. If exceeds the boundary, will use the actual available length.</param>
        /// <param name="maskLength">The length of the mask. If null, uses the actual masked length. Must be greater than 0 if specified.</param>
        /// <param name="maskChar">The character to use for masking.</param>
        /// <returns>A new string with the specified portion masked.</returns>
        public static string Mask(this string value, int startIndex, int length, int? maskLength, char maskChar)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            Argument.ShouldInRange(startIndex, 0, int.MaxValue, nameof(startIndex));
            Argument.ShouldInRange(length, 0, int.MaxValue, nameof(length));

            // Calculate actual masked length (clamped to boundary)
            int actualMaskedLength = Math.Min(length, Math.Max(value.Length - startIndex, 0));
            if (actualMaskedLength == 0)
            {
                return value;
            }

            // Determine mask character count
            int actualMaskLength = maskLength ?? actualMaskedLength;
            if (actualMaskLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maskLength));
            }

            // Calculate indices and result length
            int endIndex = startIndex + actualMaskedLength;
            int resultLength = startIndex + actualMaskLength + (value.Length - endIndex);

            // Build masked string
#if NET6_0_OR_GREATER
            return string.Create(
                resultLength,
                (value, startIndex, endIndex, maskChar, actualMaskLength),
                (span, state) =>
                {
                    var (src, start, end, mask, maskLen) = state;

                    src.AsSpan(0, start).CopyTo(span);
                    span.Slice(start, maskLen).Fill(mask);
                    src.AsSpan(end).CopyTo(span.Slice(start + maskLen));
                });
#else
            var sb = new StringBuilder(resultLength);
            sb.Append(value, 0, startIndex);
            sb.Append(maskChar, actualMaskLength);
            sb.Append(value, endIndex, value.Length - endIndex);
            return sb.ToString();
#endif
        }
    }
}