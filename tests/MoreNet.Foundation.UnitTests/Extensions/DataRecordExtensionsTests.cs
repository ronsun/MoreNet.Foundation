using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Data;

namespace MoreNet.Foundation.Extensions.Tests
{
    [TestFixture()]
    public class DataRecordExtensionsTests
    {

        [Test()]
        [TestCaseSource(nameof(GetBooleanTestCaseSource))]
        public void GetBooleanTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            bool expected)
        {
            // act
            var actual = stubRecord.GetBoolean(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetBooleanTestCaseSource()
        {
            IDataRecord stubRecord;

            // true case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("IsActive").Returns(0);
            stubRecord.GetBoolean(0).Returns(true);
            yield return new TestCaseData(stubRecord, "IsActive", true);

            // false case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("IsDeleted").Returns(0);
            stubRecord.GetBoolean(0).Returns(false);
            yield return new TestCaseData(stubRecord, "IsDeleted", false);
        }

        [Test()]
        [TestCaseSource(nameof(GetNullableBooleanTestCaseSource))]
        public void GetNullableBooleanTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            bool? expected)
        {
            // act
            var actual = stubRecord.GetNullableBoolean(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetNullableBooleanTestCaseSource()
        {
            IDataRecord stubRecord;

            // null case - DBNull value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("IsActive").Returns(0);
            stubRecord.IsDBNull(0).Returns(true);
            yield return new TestCaseData(stubRecord, "IsActive", null);

            // not null case - true value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("IsActive").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetBoolean(0).Returns(true);
            yield return new TestCaseData(stubRecord, "IsActive", true);

            // not null case - false value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("IsDeleted").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetBoolean(0).Returns(false);
            yield return new TestCaseData(stubRecord, "IsDeleted", false);
        }

        [Test()]
        [TestCaseSource(nameof(GetCharTestCaseSource))]
        public void GetCharTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            char expected)
        {
            // act
            var actual = stubRecord.GetChar(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetCharTestCaseSource()
        {
            IDataRecord stubRecord;

            // 'A' case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("Letter").Returns(0);
            stubRecord.GetChar(0).Returns('A');
            yield return new TestCaseData(stubRecord, "Letter", 'A');

            // 'Z' case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("LastLetter").Returns(0);
            stubRecord.GetChar(0).Returns('Z');
            yield return new TestCaseData(stubRecord, "LastLetter", 'Z');
        }

        [Test()]
        [TestCaseSource(nameof(GetNullableCharTestCaseSource))]
        public void GetNullableCharTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            char? expected)
        {
            // act
            var actual = stubRecord.GetNullableChar(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetNullableCharTestCaseSource()
        {
            IDataRecord stubRecord;

            // null case - DBNull value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("Letter").Returns(0);
            stubRecord.IsDBNull(0).Returns(true);
            yield return new TestCaseData(stubRecord, "Letter", null);

            // not null case - 'A'
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("Letter").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetChar(0).Returns('A');
            yield return new TestCaseData(stubRecord, "Letter", 'A');

            // not null case - 'Z'
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("LastLetter").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetChar(0).Returns('Z');
            yield return new TestCaseData(stubRecord, "LastLetter", 'Z');
        }

        [Test()]
        [TestCaseSource(nameof(GetDateTimeTestCaseSource))]
        public void GetDateTimeTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            DateTime expected)
        {
            // act
            var actual = stubRecord.GetDateTime(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetDateTimeTestCaseSource()
        {
            IDataRecord stubRecord;

            // min value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MinDate").Returns(0);
            stubRecord.GetDateTime(0).Returns(DateTime.MinValue);
            yield return new TestCaseData(stubRecord, "MinDate", DateTime.MinValue);

            // max value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MaxDate").Returns(0);
            stubRecord.GetDateTime(0).Returns(DateTime.MaxValue);
            yield return new TestCaseData(stubRecord, "MaxDate", DateTime.MaxValue);
        }

        [Test()]
        [TestCaseSource(nameof(GetNullableDateTimeTestCaseSource))]
        public void GetNullableDateTimeTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            DateTime? expected)
        {
            // act
            var actual = stubRecord.GetNullableDateTime(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetNullableDateTimeTestCaseSource()
        {
            IDataRecord stubRecord;

            // null case - DBNull value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("CreatedDate").Returns(0);
            stubRecord.IsDBNull(0).Returns(true);
            yield return new TestCaseData(stubRecord, "CreatedDate", null);

            // not null case - min value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MinDate").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetDateTime(0).Returns(DateTime.MinValue);
            yield return new TestCaseData(stubRecord, "MinDate", DateTime.MinValue);

            // not null case - max value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MaxDate").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetDateTime(0).Returns(DateTime.MaxValue);
            yield return new TestCaseData(stubRecord, "MaxDate", DateTime.MaxValue);
        }

        [Test()]
        [TestCaseSource(nameof(GetDecimalTestCaseSource))]
        public void GetDecimalTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            decimal expected)
        {
            // act
            var actual = stubRecord.GetDecimal(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetDecimalTestCaseSource()
        {
            IDataRecord stubRecord;

            // min value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MinValue").Returns(0);
            stubRecord.GetDecimal(0).Returns(decimal.MinValue);
            yield return new TestCaseData(stubRecord, "MinValue", decimal.MinValue);

            // max value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MaxValue").Returns(0);
            stubRecord.GetDecimal(0).Returns(decimal.MaxValue);
            yield return new TestCaseData(stubRecord, "MaxValue", decimal.MaxValue);
        }

        [Test()]
        [TestCaseSource(nameof(GetNullableDecimalTestCaseSource))]
        public void GetNullableDecimalTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            decimal? expected)
        {
            // act
            var actual = stubRecord.GetNullableDecimal(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetNullableDecimalTestCaseSource()
        {
            IDataRecord stubRecord;

            // null case - DBNull value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("Price").Returns(0);
            stubRecord.IsDBNull(0).Returns(true);
            yield return new TestCaseData(stubRecord, "Price", null);

            // not null case - min value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MinValue").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetDecimal(0).Returns(decimal.MinValue);
            yield return new TestCaseData(stubRecord, "MinValue", decimal.MinValue);

            // not null case - max value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MaxValue").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetDecimal(0).Returns(decimal.MaxValue);
            yield return new TestCaseData(stubRecord, "MaxValue", decimal.MaxValue);
        }

        [Test()]
        [TestCaseSource(nameof(GetDoubleTestCaseSource))]
        public void GetDoubleTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            double expected)
        {
            // act
            var actual = stubRecord.GetDouble(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetDoubleTestCaseSource()
        {
            IDataRecord stubRecord;

            // min value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MinValue").Returns(0);
            stubRecord.GetDouble(0).Returns(double.MinValue);
            yield return new TestCaseData(stubRecord, "MinValue", double.MinValue);

            // max value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MaxValue").Returns(0);
            stubRecord.GetDouble(0).Returns(double.MaxValue);
            yield return new TestCaseData(stubRecord, "MaxValue", double.MaxValue);
        }

        [Test()]
        [TestCaseSource(nameof(GetNullableDoubleTestCaseSource))]
        public void GetNullableDoubleTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            double? expected)
        {
            // act
            var actual = stubRecord.GetNullableDouble(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetNullableDoubleTestCaseSource()
        {
            IDataRecord stubRecord;

            // null case - DBNull value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("Value").Returns(0);
            stubRecord.IsDBNull(0).Returns(true);
            yield return new TestCaseData(stubRecord, "Value", null);

            // not null case - min value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MinValue").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetDouble(0).Returns(double.MinValue);
            yield return new TestCaseData(stubRecord, "MinValue", double.MinValue);

            // not null case - max value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MaxValue").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetDouble(0).Returns(double.MaxValue);
            yield return new TestCaseData(stubRecord, "MaxValue", double.MaxValue);
        }

        [Test()]
        [TestCaseSource(nameof(GetFloatTestCaseSource))]
        public void GetFloatTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            float expected)
        {
            // act
            var actual = stubRecord.GetFloat(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetFloatTestCaseSource()
        {
            IDataRecord stubRecord;

            // min value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MinValue").Returns(0);
            stubRecord.GetFloat(0).Returns(float.MinValue);
            yield return new TestCaseData(stubRecord, "MinValue", float.MinValue);

            // max value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MaxValue").Returns(0);
            stubRecord.GetFloat(0).Returns(float.MaxValue);
            yield return new TestCaseData(stubRecord, "MaxValue", float.MaxValue);
        }

        [Test()]
        [TestCaseSource(nameof(GetNullableFloatTestCaseSource))]
        public void GetNullableFloatTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            float? expected)
        {
            // act
            var actual = stubRecord.GetNullableFloat(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetNullableFloatTestCaseSource()
        {
            IDataRecord stubRecord;

            // null case - DBNull value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("Value").Returns(0);
            stubRecord.IsDBNull(0).Returns(true);
            yield return new TestCaseData(stubRecord, "Value", null);

            // not null case - min value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MinValue").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetFloat(0).Returns(float.MinValue);
            yield return new TestCaseData(stubRecord, "MinValue", float.MinValue);

            // not null case - max value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MaxValue").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetFloat(0).Returns(float.MaxValue);
            yield return new TestCaseData(stubRecord, "MaxValue", float.MaxValue);
        }

        [Test()]
        [TestCaseSource(nameof(GetGuidTestCaseSource))]
        public void GetGuidTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            Guid expected)
        {
            // act
            var actual = stubRecord.GetGuid(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetGuidTestCaseSource()
        {
            IDataRecord stubRecord;

            // empty guid case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("EmptyGuid").Returns(0);
            stubRecord.GetGuid(0).Returns(Guid.Empty);
            yield return new TestCaseData(stubRecord, "EmptyGuid", Guid.Empty);

            // non-empty guid case
            var testGuid = Guid.NewGuid();
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("Id").Returns(0);
            stubRecord.GetGuid(0).Returns(testGuid);
            yield return new TestCaseData(stubRecord, "Id", testGuid);
        }

        [Test()]
        [TestCaseSource(nameof(GetNullableGuidTestCaseSource))]
        public void GetNullableGuidTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            Guid? expected)
        {
            // act
            var actual = stubRecord.GetNullableGuid(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetNullableGuidTestCaseSource()
        {
            IDataRecord stubRecord;

            // null case - DBNull value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("Id").Returns(0);
            stubRecord.IsDBNull(0).Returns(true);
            yield return new TestCaseData(stubRecord, "Id", null);

            // not null case - empty guid
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("EmptyGuid").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetGuid(0).Returns(Guid.Empty);
            yield return new TestCaseData(stubRecord, "EmptyGuid", Guid.Empty);

            // not null case - non-empty guid
            var testGuid = Guid.NewGuid();
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("Id").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetGuid(0).Returns(testGuid);
            yield return new TestCaseData(stubRecord, "Id", testGuid);
        }

        [Test()]
        [TestCaseSource(nameof(GetInt16TestCaseSource))]
        public void GetInt16Test_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            short expected)
        {
            // act
            var actual = stubRecord.GetInt16(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetInt16TestCaseSource()
        {
            IDataRecord stubRecord;

            // min value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MinValue").Returns(0);
            stubRecord.GetInt16(0).Returns(short.MinValue);
            yield return new TestCaseData(stubRecord, "MinValue", short.MinValue);

            // max value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MaxValue").Returns(0);
            stubRecord.GetInt16(0).Returns(short.MaxValue);
            yield return new TestCaseData(stubRecord, "MaxValue", short.MaxValue);
        }

        [Test()]
        [TestCaseSource(nameof(GetNullableInt16TestCaseSource))]
        public void GetNullableInt16Test_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            short? expected)
        {
            // act
            var actual = stubRecord.GetNullableInt16(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetNullableInt16TestCaseSource()
        {
            IDataRecord stubRecord;

            // null case - DBNull value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("Value").Returns(0);
            stubRecord.IsDBNull(0).Returns(true);
            yield return new TestCaseData(stubRecord, "Value", null);

            // not null case - min value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MinValue").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetInt16(0).Returns(short.MinValue);
            yield return new TestCaseData(stubRecord, "MinValue", short.MinValue);

            // not null case - max value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MaxValue").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetInt16(0).Returns(short.MaxValue);
            yield return new TestCaseData(stubRecord, "MaxValue", short.MaxValue);
        }

        [Test()]
        [TestCaseSource(nameof(GetInt32TestCaseSource))]
        public void GetInt32Test_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            int expected)
        {
            // act
            var actual = stubRecord.GetInt32(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetInt32TestCaseSource()
        {
            IDataRecord stubRecord;

            // min value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MinValue").Returns(0);
            stubRecord.GetInt32(0).Returns(int.MinValue);
            yield return new TestCaseData(stubRecord, "MinValue", int.MinValue);

            // max value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MaxValue").Returns(0);
            stubRecord.GetInt32(0).Returns(int.MaxValue);
            yield return new TestCaseData(stubRecord, "MaxValue", int.MaxValue);
        }

        [Test()]
        [TestCaseSource(nameof(GetNullableInt32TestCaseSource))]
        public void GetNullableInt32Test_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            int? expected)
        {
            // act
            var actual = stubRecord.GetNullableInt32(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetNullableInt32TestCaseSource()
        {
            IDataRecord stubRecord;

            // null case - DBNull value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("Value").Returns(0);
            stubRecord.IsDBNull(0).Returns(true);
            yield return new TestCaseData(stubRecord, "Value", null);

            // not null case - min value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MinValue").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetInt32(0).Returns(int.MinValue);
            yield return new TestCaseData(stubRecord, "MinValue", int.MinValue);

            // not null case - max value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MaxValue").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetInt32(0).Returns(int.MaxValue);
            yield return new TestCaseData(stubRecord, "MaxValue", int.MaxValue);
        }

        [Test()]
        [TestCaseSource(nameof(GetInt64TestCaseSource))]
        public void GetInt64Test_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            long expected)
        {
            // act
            var actual = stubRecord.GetInt64(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetInt64TestCaseSource()
        {
            IDataRecord stubRecord;

            // min value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MinValue").Returns(0);
            stubRecord.GetInt64(0).Returns(long.MinValue);
            yield return new TestCaseData(stubRecord, "MinValue", long.MinValue);

            // max value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MaxValue").Returns(0);
            stubRecord.GetInt64(0).Returns(long.MaxValue);
            yield return new TestCaseData(stubRecord, "MaxValue", long.MaxValue);
        }

        [Test()]
        [TestCaseSource(nameof(GetNullableInt64TestCaseSource))]
        public void GetNullableInt64Test_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            long? expected)
        {
            // act
            var actual = stubRecord.GetNullableInt64(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetNullableInt64TestCaseSource()
        {
            IDataRecord stubRecord;

            // null case - DBNull value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("Value").Returns(0);
            stubRecord.IsDBNull(0).Returns(true);
            yield return new TestCaseData(stubRecord, "Value", null);

            // not null case - min value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MinValue").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetInt64(0).Returns(long.MinValue);
            yield return new TestCaseData(stubRecord, "MinValue", long.MinValue);

            // not null case - max value
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("MaxValue").Returns(0);
            stubRecord.IsDBNull(0).Returns(false);
            stubRecord.GetInt64(0).Returns(long.MaxValue);
            yield return new TestCaseData(stubRecord, "MaxValue", long.MaxValue);
        }

        [Test()]
        [TestCaseSource(nameof(GetStringTestCaseSource))]
        public void GetStringTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            string expected)
        {
            // act
            var actual = stubRecord.GetString(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetStringTestCaseSource()
        {
            IDataRecord stubRecord;

            // empty string case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("EmptyString").Returns(0);
            stubRecord.GetString(0).Returns(string.Empty);
            yield return new TestCaseData(stubRecord, "EmptyString", string.Empty);

            // non-empty string case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("Name").Returns(0);
            stubRecord.GetString(0).Returns("Test String");
            yield return new TestCaseData(stubRecord, "Name", "Test String");
        }

        [Test()]
        [TestCaseSource(nameof(GetValueTestCaseSource))]
        public void GetValueTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            string columnName,
            object expected)
        {
            // act
            var actual = stubRecord.GetValue(columnName);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetValueTestCaseSource()
        {
            IDataRecord stubRecord;

            // integer value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("IntValue").Returns(0);
            stubRecord.GetValue(0).Returns(42);
            yield return new TestCaseData(stubRecord, "IntValue", 42);

            // string value case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetOrdinal("StringValue").Returns(0);
            stubRecord.GetValue(0).Returns("Test");
            yield return new TestCaseData(stubRecord, "StringValue", "Test");
        }

        [Test()]
        [TestCaseSource(nameof(GetNameTestCaseSource))]
        public void GetNameTest_ReturnsExpectedValue(
            IDataRecord stubRecord,
            int ordinal,
            string expected)
        {
            // act
            var actual = stubRecord.GetName(ordinal);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable GetNameTestCaseSource()
        {
            IDataRecord stubRecord;

            // first column case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetName(0).Returns("Id");
            yield return new TestCaseData(stubRecord, 0, "Id");

            // second column case
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.GetName(1).Returns("Name");
            yield return new TestCaseData(stubRecord, 1, "Name");
        }

        [Test()]
        [TestCaseSource(nameof(GetNamesTestCaseSource))]
        public void GetNamesTest_ReturnsExpectedFieldNames(
            IDataRecord stubRecord,
            string[] expected)
        {
            // act
            var actual = stubRecord.GetNames();

            // assert
            actual.Should().BeEquivalentTo(expected, option => option.WithStrictOrdering());
        }

        public static IEnumerable GetNamesTestCaseSource()
        {
            IDataRecord stubRecord;

            // null case - null record
            stubRecord = null;
            yield return new TestCaseData(stubRecord, new string[0]);

            // empty case - empty record with no fields
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.FieldCount.Returns(0);
            yield return new TestCaseData(stubRecord, new string[0]);

            // not null case - record with multiple fields
            stubRecord = Substitute.For<IDataRecord>();
            stubRecord.FieldCount.Returns(3);
            stubRecord.GetName(0).Returns("Id");
            stubRecord.GetName(1).Returns("Name");
            stubRecord.GetName(2).Returns("Email");
            yield return new TestCaseData(stubRecord, new[] { "Id", "Name", "Email" });
        }
    }
}
