using MoreNet.Foundation.Extensions;
using System.Linq;

namespace MoreNet.Foundation.Conventions
{
    /// <summary>
    /// Convention about pagination.
    /// Use it with <see cref="QueryableExtensions.Paginate{T}(IQueryable{T}, IPageable)"/>.
    /// </summary>
    public interface IPageable
    {
        /// <summary>
        /// Gets or sets page number (1-based).
        /// </summary>
        int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets page size.
        /// </summary>
        int PageSize { get; set; }
    }
}