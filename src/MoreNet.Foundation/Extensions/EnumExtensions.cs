using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace MoreNet.Foundation.Extensions
{
    /// <summary>
    /// Extensions for <see cref="Enum"/> and <see cref="Enum"/>.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Extracts flag values from the flag Enum <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="source">The flag enum.</param>
        /// <returns>Extracted flag enum.</returns>
        public static IEnumerable<T> ExtractFlags<T>(this T source)
            where T : Enum
        {
            if (!Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
            {
                throw new ArgumentException($"Argument '{source}' should be a flag enum.", nameof(source));
            }

            ulong sourceValue = ToUInt64(source);
            ulong flagValue = 1;
            Type underlyingType = Enum.GetUnderlyingType(typeof(T));
            while (sourceValue >= flagValue)
            {
                bool hasFlag = (sourceValue & flagValue) == flagValue;
                if (hasFlag)
                {
                    T flag = (T)Convert.ChangeType(flagValue, underlyingType);

                    // Has flag but not defined, ex: (Foo.A | 2) has flag 2 but the value 2 is not an item defined in Foo.
                    bool isDefined = Enum.IsDefined(typeof(T), flag);
                    if (!isDefined)
                    {
                        throw new InvalidEnumArgumentException($"The value of argument '{nameof(source)}' ({flagValue}) is invalid for Enum type '{typeof(T)}'.");
                    }

                    yield return flag;
                }

                flagValue <<= 1;
            }

            // Took from https://referencesource.microsoft.com/#mscorlib/system/enum.cs,209
            ulong ToUInt64(T value)
            {
                var typeCode = Type.GetTypeCode(Enum.GetUnderlyingType(typeof(T)));
                switch (typeCode)
                {
                    case TypeCode.SByte:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                        return (ulong)Convert.ToInt64(value, CultureInfo.InvariantCulture);

                    case TypeCode.Byte:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                        return Convert.ToUInt64(value, CultureInfo.InvariantCulture);

                    default:
                        throw new InvalidOperationException("UnknownEnumType");
                }
            }
        }
    }
}