using System;

namespace MoreNet.Foundation.Extensions
{
    /// <summary>
    /// Extension methods for tuples.
    /// </summary>
    public static class TupleExtensions
    {
        /// <summary>
        /// Determines whether two ranges overlap (closed-open, [from, to)).
        /// </summary>
        /// <typeparam name="T">The type of the range values. Must implement <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="range">The first range (from, to).</param>
        /// <param name="other">The second range (from, to).</param>
        /// <returns><c>true</c> if the ranges overlap; otherwise, <c>false</c>.</returns>
        public static bool IsOverlapping<T>(this (T From, T To) range, (T From, T To) other)
            where T : IComparable<T>
        {
            if (range.From.CompareTo(range.To) >= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(range));
            }

            if (other.From.CompareTo(other.To) >= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(other));
            }

            var maxFrom = range.From.CompareTo(other.From) >= 0 ? range.From : other.From;
            var minTo = range.To.CompareTo(other.To) <= 0 ? range.To : other.To;

            return maxFrom.CompareTo(minTo) < 0;
        }
    }
}
