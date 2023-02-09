using System;
using System.Linq;
using System.Linq.Expressions;
using MoreNet.Foundation.Conventions;

namespace MoreNet.Foundation.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IQueryable"/> and <see cref="IQueryable{T}"/>.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Filters a sequence of values based on a predicate if should be.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}"/> to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="shouldAppend">To indicate should append <paramref name="predicate"/> to <paramref name="source"/>.</param>
        /// <returns>
        /// If <paramref name="shouldAppend"/>, it is an <see cref="IQueryable{T}"/> that contains elements from the input sequence that satisfy the condition specified by predicate,
        /// otherwise, returns <paramref name="source"/>.
        /// </returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool shouldAppend)
        {
            if (shouldAppend)
            {
                return source.Where(predicate);
            }

            return source;
        }

        /// <summary>
        /// Pagination.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}"/> to filter.</param>
        /// <param name="pageable"><see cref="IPageable"/>.</param>
        /// <returns><see cref="IQueryable{T}"/> with pagination.</returns>
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, IPageable pageable)
        {
            Argument.ShouldNotNull(pageable, nameof(pageable));
            Argument.ShouldInRange(pageable.PageNumber, 0, int.MaxValue, nameof(pageable.PageNumber));
            Argument.ShouldInRange(pageable.PageSize, 1, int.MaxValue, nameof(pageable.PageSize));

            checked
            {
                int skipCount = (pageable.PageNumber - 1) * pageable.PageSize;
                return source.Skip(skipCount).Take(pageable.PageSize);
            }
        }
    }
}