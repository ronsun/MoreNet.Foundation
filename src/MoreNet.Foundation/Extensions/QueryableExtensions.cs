using MoreNet.Foundation.Conventions;
using System;
using System.Linq;
using System.Linq.Expressions;

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
            Argument.ShouldInRange(pageable.PageNumber, 1, int.MaxValue, nameof(pageable.PageNumber));
            Argument.ShouldInRange(pageable.PageSize, 1, int.MaxValue, nameof(pageable.PageSize));

            checked
            {
                int skipCount = (pageable.PageNumber - 1) * pageable.PageSize;
                return source.Skip(skipCount).Take(pageable.PageSize);
            }
        }

        /// <summary>
        /// Applies an order condition and continues with sequential ordering when the query is already ordered.
        /// </summary>
        /// <typeparam name="TOrder">The type used to indicate the requested order.</typeparam>
        /// <typeparam name="TEntity">The type of the elements of query.</typeparam>
        /// <typeparam name="TProperty">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="query">An <see cref="IQueryable{T}"/> to order.</param>
        /// <param name="orderBy">The requested order value for the current ordering step.</param>
        /// <param name="keySelector">A function to extract the key used for ordering.</param>
        /// <param name="isDescending">To indicate whether descending order should be applied for the current ordering step.</param>
        /// <returns>
        /// An <see cref="IQueryable{T}"/> ordered by <paramref name="keySelector"/>.
        /// If <paramref name="query"/> is already ordered, continues with <see cref="Queryable.ThenBy{TSource, TKey}(IOrderedQueryable{TSource}, Expression{Func{TSource, TKey}})"/>
        /// or <see cref="Queryable.ThenByDescending{TSource, TKey}(IOrderedQueryable{TSource}, Expression{Func{TSource, TKey}})"/>;
        /// otherwise starts with <see cref="Queryable.OrderBy{TSource, TKey}(IQueryable{TSource}, Expression{Func{TSource, TKey}})"/>
        /// or <see cref="Queryable.OrderByDescending{TSource, TKey}(IQueryable{TSource}, Expression{Func{TSource, TKey}})"/>.
        /// </returns>
        /// <remarks>
        /// This method is intended to support both single order conditions such as <see cref="IOrderable{T}"/>
        /// and sequential order conditions such as <see cref="ISequentialOrderable{T}"/>.
        /// </remarks>
        public static IQueryable<TEntity> SequentialOrderBy<TOrder, TEntity, TProperty>(
            this IQueryable<TEntity> query,
            TOrder orderBy,
            Expression<Func<TEntity, TProperty>> keySelector,
            bool isDescending)
        {
            var isOrdered = TryGetOrdered<TEntity>(query, out var orderedQuery);

            return isOrdered switch
            {
                true when isDescending => orderedQuery.ThenByDescending(keySelector),
                true => orderedQuery.ThenBy(keySelector),
                false when isDescending => query.OrderByDescending(keySelector),
                _ => query.OrderBy(keySelector)
            };
        }

        private static bool TryGetOrdered<TEntity>(IQueryable query, out IOrderedQueryable<TEntity> orderedQuery)
        {
            var ordered = query.Expression is MethodCallExpression methodCall
                && (methodCall.Method.Name == nameof(Queryable.OrderBy)
                    || methodCall.Method.Name == nameof(Queryable.OrderByDescending)
                    || methodCall.Method.Name == nameof(Queryable.ThenBy)
                    || methodCall.Method.Name == nameof(Queryable.ThenByDescending));
            if (ordered)
            {
                orderedQuery = (IOrderedQueryable<TEntity>)query;
            }
            else
            {
                orderedQuery = null;
            }

            return ordered;
        }
    }
}
