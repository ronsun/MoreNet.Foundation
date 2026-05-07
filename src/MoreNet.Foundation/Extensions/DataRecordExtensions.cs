using System;
using System.Collections.Generic;
using System.Data;

namespace MoreNet.Foundation.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IDataRecord"/>.
    /// </summary>
    /// <remarks>
    /// Each data type has its own dedicated method instead of using a generic approach.
    /// A generic method would be 15-40% slower due to runtime type checking, boxing/unboxing overhead, and reduced JIT optimization.
    /// See benchmark: <c>tools\MoreNet.Foundation.Benchmark\Extensions\DataRecordExtensionsBenchmark.cs</c>
    /// </remarks>
    public static class DataRecordExtensions
    {
        /// <summary>
        /// Gets the value of the specified column as a <see cref="bool"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a <see cref="bool"/>.</returns>
        public static bool GetBoolean(this IDataRecord record, string name)
        {
            return record.GetBoolean(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable <see cref="bool"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a nullable <see cref="bool"/>.</returns>
        public static bool? GetNullableBoolean(this IDataRecord record, string name)
        {
            int ordinal = record.GetOrdinal(name);
            return record.IsDBNull(ordinal) ? null : record.GetBoolean(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a <see cref="char"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a <see cref="char"/>.</returns>
        public static char GetChar(this IDataRecord record, string name)
        {
            return record.GetChar(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable <see cref="char"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a nullable <see cref="char"/>.</returns>
        public static char? GetNullableChar(this IDataRecord record, string name)
        {
            int ordinal = record.GetOrdinal(name);
            return record.IsDBNull(ordinal) ? null : record.GetChar(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a <see cref="DateTime"/>.</returns>
        public static DateTime GetDateTime(this IDataRecord record, string name)
        {
            return record.GetDateTime(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a nullable <see cref="DateTime"/>.</returns>
        public static DateTime? GetNullableDateTime(this IDataRecord record, string name)
        {
            int ordinal = record.GetOrdinal(name);
            return record.IsDBNull(ordinal) ? null : record.GetDateTime(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a <see cref="decimal"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a <see cref="decimal"/>.</returns>
        public static decimal GetDecimal(this IDataRecord record, string name)
        {
            return record.GetDecimal(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable <see cref="decimal"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a nullable <see cref="decimal"/>.</returns>
        public static decimal? GetNullableDecimal(this IDataRecord record, string name)
        {
            int ordinal = record.GetOrdinal(name);
            return record.IsDBNull(ordinal) ? null : record.GetDecimal(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a <see cref="double"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a <see cref="double"/>.</returns>
        public static double GetDouble(this IDataRecord record, string name)
        {
            return record.GetDouble(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable <see cref="double"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a nullable <see cref="double"/>.</returns>
        public static double? GetNullableDouble(this IDataRecord record, string name)
        {
            int ordinal = record.GetOrdinal(name);
            return record.IsDBNull(ordinal) ? null : record.GetDouble(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a <see cref="float"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a <see cref="float"/>.</returns>
        public static float GetFloat(this IDataRecord record, string name)
        {
            return record.GetFloat(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable <see cref="float"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a nullable <see cref="float"/>.</returns>
        public static float? GetNullableFloat(this IDataRecord record, string name)
        {
            int ordinal = record.GetOrdinal(name);
            return record.IsDBNull(ordinal) ? null : record.GetFloat(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a <see cref="Guid"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a <see cref="Guid"/>.</returns>
        public static Guid GetGuid(this IDataRecord record, string name)
        {
            return record.GetGuid(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable <see cref="Guid"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a nullable <see cref="Guid"/>.</returns>
        public static Guid? GetNullableGuid(this IDataRecord record, string name)
        {
            int ordinal = record.GetOrdinal(name);
            return record.IsDBNull(ordinal) ? null : record.GetGuid(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a <see cref="short"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a <see cref="short"/>.</returns>
        public static short GetInt16(this IDataRecord record, string name)
        {
            return record.GetInt16(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable <see cref="short"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a nullable <see cref="short"/>.</returns>
        public static short? GetNullableInt16(this IDataRecord record, string name)
        {
            int ordinal = record.GetOrdinal(name);
            return record.IsDBNull(ordinal) ? null : record.GetInt16(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a <see cref="int"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a <see cref="int"/>.</returns>
        public static int GetInt32(this IDataRecord record, string name)
        {
            return record.GetInt32(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable <see cref="int"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a nullable <see cref="int"/>.</returns>
        public static int? GetNullableInt32(this IDataRecord record, string name)
        {
            int ordinal = record.GetOrdinal(name);
            return record.IsDBNull(ordinal) ? null : record.GetInt32(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a <see cref="long"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a <see cref="long"/>.</returns>
        public static long GetInt64(this IDataRecord record, string name)
        {
            return record.GetInt64(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable <see cref="long"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a nullable <see cref="long"/>.</returns>
        public static long? GetNullableInt64(this IDataRecord record, string name)
        {
            int ordinal = record.GetOrdinal(name);
            return record.IsDBNull(ordinal) ? null : record.GetInt64(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a <see cref="string"/> object.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as a <see cref="string"/>.</returns>
        public static string GetString(this IDataRecord record, string name)
        {
            return record.GetString(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the value of the specified column as an <see cref="object"/>.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns>The value of the specified column as an <see cref="object"/>.</returns>
        public static object GetValue(this IDataRecord record, string name)
        {
            return record.GetValue(record.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the name for the field to find.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <param name="ordinal">The zero-based column ordinal.</param>
        /// <returns>The name of the field or an empty string if there is no value to return.</returns>
        public static string GetName(this IDataRecord record, int ordinal)
        {
            return record.GetName(ordinal);
        }

        /// <summary>
        /// Gets all the attribute fields in the collection.
        /// </summary>
        /// <param name="record">The data record.</param>
        /// <returns>An array of field names.</returns>
        public static IEnumerable<string> GetNames(this IDataRecord record)
        {
            if (record == null)
            {
                yield break;
            }

            for (int i = 0; i < record.FieldCount; i++)
            {
                yield return record.GetName(i);
            }
        }
    }
}
