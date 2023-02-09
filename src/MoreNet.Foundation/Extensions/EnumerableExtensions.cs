using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MoreNet.Foundation.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable"/> and <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
#if !NET6_0_OR_GREATER

        /// <summary>
        /// Split the elements of a sequence into chunks of size at most <paramref name="size"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> whose elements to chunk.</param>
        /// <param name="size">Maximum size of each chunk.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> that contains the elements the input sequence split into chunks of size <paramref name="size"/>.
        /// </returns>
        public static IEnumerable<TSource[]> Chunk<TSource>(this IEnumerable<TSource> source, int size)
        {
            Argument.ShouldNotNull(source, nameof(source));
            Argument.ShouldInRange(size, 1, int.MaxValue, nameof(size));

            return source
                .Select((v, i) => new { v, groupIndex = i / size })
                .GroupBy(x => x.groupIndex)
                .Select(g => g.Select(x => x.v).ToArray());
        }

#endif
    }
}