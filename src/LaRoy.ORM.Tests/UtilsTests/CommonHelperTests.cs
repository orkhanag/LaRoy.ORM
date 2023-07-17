using LaRoy.ORM.Tests.DTO;
using LaRoy.ORM.Utils;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace LaRoy.ORM.Tests.UtilsTests
{
    public class CommonHelperTests : TestBase
    {
        private delegate string GenerateCreateTableQuery<T>(string tableName, IDbConnection connection, bool onlyKeyField = false);
        readonly GenerateCreateTableQuery<DailyCustomerPayments> generateCreateTableQueryDelegate = new GenerateCreateTableQuery<DailyCustomerPayments>(CommonHelper.GenerateCreateTableQuery<DailyCustomerPayments>);

        [Fact]
        public void GenerateCreateTableQuery_ShouldGenerateQueryWithAllColumns_WhenSqlConnection()
        {
            //Arrange
            var tableName = "#TempTable";
            var connection = new SqlConnection();
            var expectedQuery = $@"CREATE  TABLE #TempTable (PinCode nvarchar(MAX), InvoiceNumber nvarchar(MAX), MobileNumber nvarchar(MAX), RemainingDays int, PayableAmount int, DelayStatus bit, SendDate datetime)";
            //Act
            var actualQuery = generateCreateTableQueryDelegate(tableName, connection);
            //Assert
            Assert.Equal(expectedQuery, actualQuery);
        }

        [Fact]
        public void GenerateCreateTableQuery_ShouldGenerateQueryWithOnlyKeyColumn_WhenSqlConnection()
        {
            //Arrange
            var tableName = "#TempTable";
            var connection = new SqlConnection();
            var expectedQuery = $@"CREATE  TABLE #TempTable (PinCode nvarchar(MAX))";
            //Act
            var actualQuery = generateCreateTableQueryDelegate(tableName, connection, onlyKeyField: true);
            //Assert
            Assert.Equal(expectedQuery, actualQuery);
        }

        [Fact]
        public void GenerateCreateTableQuery_ShouldGenerateQueryWithAllColumns_WhenNpgSqlConnection()
        {
            //Arrange
            var tableName = "TempTable";
            var connection = new NpgsqlConnection();
            var expectedQuery = $@"CREATE TEMP TABLE TempTable (PinCode text, InvoiceNumber text, MobileNumber text, RemainingDays integer, PayableAmount integer, DelayStatus boolean, SendDate date)";
            //Act
            var actualQuery = generateCreateTableQueryDelegate(tableName, connection);
            //Assert
            Assert.Equal(expectedQuery, actualQuery);
        }

        [Fact]
        public void GenerateCreateTableQuery_ShouldGenerateQueryWithOnlyKeyColumn_WhenNpgSqlConnection()
        {
            //Arrange
            var tableName = "TempTable";
            var connection = new NpgsqlConnection();
            var expectedQuery = $@"CREATE TEMP TABLE TempTable (PinCode text)";
            //Act
            var actualQuery = generateCreateTableQueryDelegate(tableName, connection, onlyKeyField: true);
            //Assert
            Assert.Equal(expectedQuery, actualQuery);
        }

        [Fact]
        public void GenerateCreateTableQuery_ShouldGenerateQueryWithAllColumns_WhenMySqlConnection()
        {
            //Arrange
            var tableName = "TempTable";
            var connection = new MySqlConnection();
            var expectedQuery = $@"CREATE TEMPORARY TABLE TempTable (PinCode text, InvoiceNumber text, MobileNumber text, RemainingDays int, PayableAmount int, DelayStatus bit, SendDate date)";
            //Act
            var actualQuery = generateCreateTableQueryDelegate(tableName, connection);
            //Assert
            Assert.Equal(expectedQuery, actualQuery);
        }

        [Fact]
        public void GenerateCreateTableQuery_ShouldGenerateQueryWithOnlyKeyColumn_WhenMySqlConnection()
        {
            //Arrange
            var tableName = "TempTable";
            var connection = new MySqlConnection();
            var expectedQuery = $@"CREATE TEMPORARY TABLE TempTable (PinCode text)";
            //Act
            var actualQuery = generateCreateTableQueryDelegate(tableName, connection, onlyKeyField: true);
            //Assert
            Assert.Equal(expectedQuery, actualQuery);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(69)]
        [InlineData(420)]
        [InlineData(4269)]
        public void ToDataTable_ShouldConvertDataToDataTable(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount);
            //Act
            DataTable dataTable = data.ToDataTable();
            //Assert
            var columnNames = dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
            var dataType = data.First().GetType();

            Assert.Equal(dataType.Name, dataTable.TableName);
            Assert.Equal(data.Count(), dataTable.Rows.Count);
            Assert.Equal(dataType.GetProperties().Length, dataTable.Columns.Count);

            foreach (var item in dataType.GetProperties())
                Assert.Contains(item.Name, columnNames);
        }

        [Fact]
        public void GetKeyField_ShouldGetFiledWithKeyAttribute()
        {
            //Arrange
            var item = new DailyCustomerPayments();
            //Act
            var keyField = CommonHelper.GetKeyField<DailyCustomerPayments>();
            //
            Assert.Equal("PinCode", keyField.Name);
            Assert.Equal(typeof(string), keyField.PropertyType);
        }

        [Fact]
        public void GetKeyField_ShouldThrowNotSupportedException_WhenTypeHasNoKeyField()
        {
            // Arrange
            var instance = new TypeWithoutKeyField();

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => CommonHelper.GetKeyField<TypeWithoutKeyField>());
        }
    }
}
