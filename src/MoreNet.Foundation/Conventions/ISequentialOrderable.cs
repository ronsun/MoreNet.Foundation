using System;
using System.Collections.Generic;

namespace MoreNet.Foundation.Conventions
{
    /// <summary>
    /// Convention about sequentially order.
    /// </summary>
    /// <typeparam name="T">
    /// To indicate types for order, typically use <see cref="Enum"/> or constant to manage types.
    /// Use collection of <typeparamref name="T"/> (ex: <see cref="List{T}"/> where T is <see cref="Enum"/>)
    /// is also appropriate if need to sort a collection of items by multiple properties.
    /// </typeparam>
    public interface ISequentialOrderable<T>
    {
#pragma warning disable CA2227
        /// <summary>
        /// Gets or sets types for sequential order.
        /// </summary>
        IList<T> SequentialOrderBy { get; set; }
#pragma warning restore CA2227
    }
}