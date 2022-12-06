using System;
using System.Collections;

namespace MoreNet.Foundation
{
    /// <summary>
    /// Assert argument.
    /// </summary>
    public static class Argument
    {
        /// <summary>
        /// Assert argument should not null.
        /// </summary>
        /// <typeparam name="T">Type of argument.</typeparam>
        /// <param name="value">Argument value.</param>
        /// <param name="name">Parameter name.</param>
        /// <remarks>
        /// Should not be null for reference type.
        /// Should not be null for <see cref="Nullable{T}"/>
        /// Should not be empty for <see cref="IEnumerable"/>.
        /// </remarks>
        public static void ShouldNotNull<T>(this T value, string name)
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Assert argument should not empty.
        /// </summary>
        /// <typeparam name="T">Type of argument.</typeparam>
        /// <param name="value">Argument value.</param>
        /// <param name="name">Parameter name.</param>
        /// <remarks>
        /// Should not be empty for reference type.
        /// Should not be empty for <see cref="Nullable{T}"/>
        /// Should not be empty for <see cref="IEnumerable"/>.
        /// </remarks>
        public static void ShouldNotEmpty<T>(T value, string name)
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }

            if (value is IEnumerable enumerableTarget)
            {
                bool any = false;
                foreach (var item in enumerableTarget)
                {
                    any = true;
                    break;
                }

                if (!any)
                {
                    throw new ArgumentException("Value should not be empty", name);
                }
            }
        }

        /// <summary>
        /// Assert argument should be defined enum.
        /// Throw exception if argement not defined in current enum type.
        /// </summary>
        /// <typeparam name="TEnum">Type of enum.</typeparam>
        /// <param name="value">Argument value.</param>
        /// <param name="name">Parameter name.</param>
        public static void ShouldBeDefined<TEnum>(this TEnum value, string name)
            where TEnum : Enum
        {
            if (Enum.IsDefined(value.GetType(), value) == false)
            {
                throw new ArgumentOutOfRangeException(name);
            }
        }
    }
}
