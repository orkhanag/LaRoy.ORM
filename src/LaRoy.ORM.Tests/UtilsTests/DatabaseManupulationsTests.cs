using Dapper;
using LaRoy.ORM.Tests.DTO;
using LaRoy.ORM.Utils;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace LaRoy.ORM.Tests.UtilsTests
{
    public class DatabaseManupulationsTests : TestBase
    {

        private delegate string GenerateCreateTableQuery<T>(string tableName, IDbConnection connection, bool onlyKeyField = false);
        readonly GenerateCreateTableQuery<DailyCustomerPayments> generateCreateTableQueryDelegate = new GenerateCreateTableQuery<DailyCustomerPayments>(DataManupulations.GenerateCreateTableQuery<DailyCustomerPayments>);

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

        [Fact]
        public void CreateTemporaryTable_ShouldCreateTempTable_WhenSqlConnection()
        {
            //Arrange
            var tableName = "#TempTable";
            var connection = _sqlConnection;
            //Act
            connection.Open();
            connection.CreateTemporaryTable<DailyCustomerPayments>(tableName);
            var result = connection.Execute($@"INSERT INTO {tableName} (PinCode, InvoiceNumber, MobileNumber, RemainingDays, PayableAmount, DelayStatus, SendDate) 
                VALUES ('q1w2e3r', '123qwe', '0703626119', 1, 5, 0, GETDATE())");
            connection.Close();
            //Assert
            Assert.Equal(result, 1);
        }

        [Fact]
        public void CreateTemporaryTable_ShouldCreateTempTable_WhenNpgSqlConnection()
        {
            //Arrange
            var tableName = "TempTable";
            var connection = _npgSqlConnection;
            //Act
            connection.Open();
            connection.CreateTemporaryTable<DailyCustomerPayments>(tableName);
            var result = connection.Execute($@"INSERT INTO {tableName} (PinCode, InvoiceNumber, MobileNumber, RemainingDays, PayableAmount, DelayStatus, SendDate) 
                VALUES ('q1w2e3r', '123qwe', '0703626119', 1, 5, false, NOW())");
            connection.Close();
            //Assert
            Assert.Equal(result, 1);
        }

        [Fact]
        public void CreateTemporaryTable_ShouldCreateTempTable_WhenMySqlConnection()
        {
            //Arrange
            var tableName = "TempTable";
            var connection = _mySqlConnection;
            //Act
            connection.Open();
            connection.CreateTemporaryTable<DailyCustomerPayments>(tableName);
            var result = connection.Execute($@"INSERT INTO {tableName} (PinCode, InvoiceNumber, MobileNumber, RemainingDays, PayableAmount, DelayStatus, SendDate) 
                VALUES ('q1w2e3r', '123qwe', '0703626119', 1, 5, 0, NOW())");
            connection.Close();
            //Assert
            Assert.Equal(result, 1);
        }
    }
}
