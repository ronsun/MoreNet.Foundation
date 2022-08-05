using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace MoreNet.Foundation.Extensions
{
    /// <summary>
    /// Extension methods for all generic types.
    /// </summary>
    public static partial class GenericExtensions
    {
        /// <summary>
        /// Assert argument should not null.
        /// </summary>
        /// <typeparam name="T">Type of argument.</typeparam>
        /// <param name="arg">Argument.</param>
        /// <param name="argName">Argument name.</param>
        /// <remarks>
        /// Should not be null for reference type.
        /// Should not be empty for <see cref="IEnumerable"/>.
        /// </remarks>
        public static void ShouldNotNull<T>(this T arg, string argName)
            where T : class
        {
            if (arg == null)
            {
                throw new ArgumentNullException(argName);
            }
        }

        /// <summary>
        /// Assert argument should not empty.
        /// </summary>
        /// <typeparam name="T">Type of argument.</typeparam>
        /// <param name="arg">Argument.</param>
        /// <param name="argName">Argument name.</param>
        /// <remarks>
        /// Should not be null for reference type.
        /// Should not be empty for <see cref="IEnumerable"/>.
        /// </remarks>
        public static void ShouldNotEmpty<T>(this T arg, string argName)
            where T : class
        {
            if (arg == null)
            {
                throw new ArgumentNullException(argName);
            }

            var argumentEmptyException = new ArgumentException("Value should not be empty", argName);
            if (arg is string s && s.Length == 0)
            {
                throw argumentEmptyException;
            }

            if (arg is IEnumerable enumerableTarget)
            {
                bool any = false;
                foreach (var item in enumerableTarget)
                {
                    any = true;
                    break;
                }

                if (!any)
                {
                    throw argumentEmptyException;
                }
            }
        }

        /// <summary>
        /// Assert argument should be defined enum.
        /// Throw exception if argement not defined in current enum type.
        /// </summary>
        /// <typeparam name="TEnum">Type of enum.</typeparam>
        /// <param name="arg">Argument.</param>
        /// <param name="argName">Argument name.</param>
        public static void ShouldBeDefined<TEnum>(this TEnum arg, string argName)
            where TEnum : Enum
        {
            if (Enum.IsDefined(arg.GetType(), arg) == false)
            {
                throw new ArgumentOutOfRangeException(argName);
            }
        }
    }
}