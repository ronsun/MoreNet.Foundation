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
    }
}