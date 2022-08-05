using System.Linq;

namespace MoreNet.Foundation.Extensions
{
    /// <summary>
    /// Extension methods for all generic types.
    /// </summary>
    public static partial class GenericExtensions
    {
        /// <summary>
        /// Indicate is the <paramref name="item"/> in the <paramref name="pool"/>.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="item">The item.</param>
        /// <param name="pool">The pool.</param>
        /// <returns>Is <paramref name="item"/> in the <paramref name="pool"/>.</returns>
        public static bool In<T>(this T item, params T[] pool)
        {
            if (pool.Any())
            {
                return pool.Contains(item);
            }

            return false;
        }
    }
}