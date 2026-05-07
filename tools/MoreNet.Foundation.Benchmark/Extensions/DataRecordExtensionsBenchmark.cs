using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Data;

namespace MoreNet.Foundation.Benchmark.Extensions
{
    [SimpleJob(RuntimeMoniker.Net462, baseline: true)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [MemoryDiagnoser]
    public class DataRecordExtensionsBenchmark : IBenchmark
    {
        private IDataRecord _record;

        [GlobalSetup]
        public void Setup()
        {
            _record = new FakeDataRecord();
        }

        /// <summary>
        /// Baseline: Single Int32 read (most common scenario)
        /// </summary>
        [Benchmark(Baseline = true)]
        public void Baseline()
        {
            for (int i = 0; i < 100; i++)
            {
                _record.GetInt32("IntValue");
                _record.GetString("StringValue");
                _record.GetBoolean("BoolValue");
                _record.GetDateTime("DateValue");
                _record.GetDecimal("DecimalValue");
            }
        }

        /// <summary>
        /// Candidate: Generic Int32 read
        /// </summary>
        [Benchmark]
        public void Candidate()
        {
            for (int i = 0; i < 100; i++)
            {
                _record.GetValue<int>("IntValue");
                _record.GetValue<string>("StringValue");
                _record.GetValue<bool>("BoolValue");
                _record.GetValue<DateTime>("DateValue");
                _record.GetValue<decimal>("DecimalValue");
            }
        }
    }

    internal class FakeDataRecord : IDataRecord
    {
        private readonly Dictionary<string, int> _ordinalMap = new Dictionary<string, int>
        {
            { "IntValue", 0 },
            { "StringValue", 1 },
            { "BoolValue", 2 },
            { "DateValue", 3 },
            { "DecimalValue", 4 },
            { "NullableIntValue", 5 }
        };

        private readonly object[] _values = new object[]
        {
            // IntValue
            42,
            // StringValue
            "Test",
            // BoolValue
            true,
            // DateValue
            new DateTime(2024, 1, 1),
            // DecimalValue
            123.45m,
            // NullableIntValue
            DBNull.Value
        };

        public int GetOrdinal(string name) => _ordinalMap[name];
        public bool IsDBNull(int i) => _values[i] is DBNull;
        public int GetInt32(int i) => (int)_values[i];
        public string GetString(int i) => (string)_values[i];
        public bool GetBoolean(int i) => (bool)_values[i];
        public DateTime GetDateTime(int i) => (DateTime)_values[i];
        public decimal GetDecimal(int i) => (decimal)_values[i];

        // Unused members
        public int FieldCount => _values.Length;
        public object this[int i] => throw new NotImplementedException();
        public object this[string name] => throw new NotImplementedException();
        public byte GetByte(int i) => throw new NotImplementedException();
        public char GetChar(int i) => throw new NotImplementedException();
        public double GetDouble(int i) => throw new NotImplementedException();
        public float GetFloat(int i) => throw new NotImplementedException();
        public Guid GetGuid(int i) => throw new NotImplementedException();
        public short GetInt16(int i) => throw new NotImplementedException();
        public long GetInt64(int i) => throw new NotImplementedException();
        public object GetValue(int i) => throw new NotImplementedException();
        public string GetName(int i) => throw new NotImplementedException();
        public string GetDataTypeName(int i) => throw new NotImplementedException();
        public Type GetFieldType(int i) => throw new NotImplementedException();
        public int GetValues(object[] values) => throw new NotImplementedException();
        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => throw new NotImplementedException();
        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => throw new NotImplementedException();
        public IDataReader GetData(int i) => throw new NotImplementedException();
    }

    internal static class DataRecordMethods
    {
        internal static int GetInt32(this IDataRecord record, string name)
        {
            return record.GetInt32(record.GetOrdinal(name));
        }

        internal static string GetString(this IDataRecord record, string name)
        {
            return record.GetString(record.GetOrdinal(name));
        }

        internal static bool GetBoolean(this IDataRecord record, string name)
        {
            return record.GetBoolean(record.GetOrdinal(name));
        }

        internal static DateTime GetDateTime(this IDataRecord record, string name)
        {
            return record.GetDateTime(record.GetOrdinal(name));
        }

        internal static decimal GetDecimal(this IDataRecord record, string name)
        {
            return record.GetDecimal(record.GetOrdinal(name));
        }

        internal static T GetValue<T>(this IDataRecord record, string name)
        {
            int ordinal = record.GetOrdinal(name);
            Type type = typeof(T);

            if (type == typeof(int))
            {
                return (T)(object)record.GetInt32(ordinal);
            }

            if (type == typeof(string))
            {
                return (T)(object)record.GetString(ordinal);
            }

            if (type == typeof(bool))
            {
                return (T)(object)record.GetBoolean(ordinal);
            }

            if (type == typeof(DateTime))
            {
                return (T)(object)record.GetDateTime(ordinal);
            }

            if (type == typeof(decimal))
            {
                return (T)(object)record.GetDecimal(ordinal);
            }

            throw new NotSupportedException($"Type {type.Name} not supported");
        }
    }
}
