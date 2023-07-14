using Dapper;
using LaRoy.ORM.BulkOperations;
using LaRoy.ORM.Tests.DTO;
using LaRoy.ORM.Utils;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LaRoy.ORM.Tests.UtilsTests
{
    public class DatabaseManupulationsTests : TestBase
    {
        [Fact]
        public void GenerateCreateTableQuery_ShouldGenerateQueryWithAllColumns_WhenConnectionIsSql()
        {
            //Arrange
            var tableName = "#TempTable";
            var connection = new SqlConnection();
            var expectedQuery = $@"CREATE  TABLE #TempTable (PinCode nvarchar(MAX), InvoiceNumber nvarchar(MAX), MobileNumber nvarchar(MAX), RemainingDays int, PayableAmount int, DelayStatus bit, SendDate datetime)";
            //Act
            var actualQuery = DatabaseManupulations.GenerateCreateTableQuery<DailyCustomerPayments>(tableName, connection);
            //Assert
            Assert.Equal(expectedQuery, actualQuery);
        }

        [Fact]
        public void GenerateCreateTableQuery_ShouldGenerateQueryWithOnlyKeyColumn_WhenConnectionIsSql()
        {
            //Arrange
            var tableName = "#TempTable";
            var connection = new SqlConnection();
            var expectedQuery = $@"CREATE  TABLE #TempTable (PinCode nvarchar(MAX))";
            //Act
            var actualQuery = DatabaseManupulations.GenerateCreateTableQuery<DailyCustomerPayments>(tableName, connection, onlyKeyField: true);
            //Assert
            Assert.Equal(expectedQuery, actualQuery);
        }

        [Fact]
        public void GenerateCreateTableQuery_ShouldGenerateQueryWithAllColumns_WhenConnectionIsNpgSql()
        {
            //Arrange
            var tableName = "TempTable";
            var connection = new NpgsqlConnection();
            var expectedQuery = $@"CREATE TEMP TABLE TempTable (PinCode text, InvoiceNumber text, MobileNumber text, RemainingDays integer, PayableAmount integer, DelayStatus boolean, SendDate date)";
            //Act
            var actualQuery = DatabaseManupulations.GenerateCreateTableQuery<DailyCustomerPayments>(tableName, connection);
            //Assert
            Assert.Equal(expectedQuery, actualQuery);
        }

        [Fact]
        public void GenerateCreateTableQuery_ShouldGenerateQueryWithOnlyKeyColumn_WhenConnectionIsNpgSql()
        {
            //Arrange
            var tableName = "TempTable";
            var connection = new NpgsqlConnection();
            var expectedQuery = $@"CREATE TEMP TABLE TempTable (PinCode text)";
            //Act
            var actualQuery = DatabaseManupulations.GenerateCreateTableQuery<DailyCustomerPayments>(tableName, connection, onlyKeyField: true);
            //Assert
            Assert.Equal(expectedQuery, actualQuery);
        }

        [Fact]
        public void GenerateCreateTableQuery_ShouldGenerateQueryWithAllColumns_WhenConnectionIsMySql()
        {
            //Arrange
            var tableName = "TempTable";
            var connection = new MySqlConnection();
            var expectedQuery = $@"CREATE TEMPORARY TABLE TempTable (PinCode text, InvoiceNumber text, MobileNumber text, RemainingDays int, PayableAmount int, DelayStatus bit, SendDate date)";
            //Act
            var actualQuery = DatabaseManupulations.GenerateCreateTableQuery<DailyCustomerPayments>(tableName, connection);
            //Assert
            Assert.Equal(expectedQuery, actualQuery);
        }

        [Fact]
        public void GenerateCreateTableQuery_ShouldGenerateQueryWithOnlyKeyColumn_WhenConnectionIsMySql()
        {
            //Arrange
            var tableName = "TempTable";
            var connection = new MySqlConnection();
            var expectedQuery = $@"CREATE TEMPORARY TABLE TempTable (PinCode text)";
            //Act
            var actualQuery = DatabaseManupulations.GenerateCreateTableQuery<DailyCustomerPayments>(tableName, connection, onlyKeyField: true);
            //Assert
            Assert.Equal(expectedQuery, actualQuery);
        }

        [Fact]
        public void CreateTemporaryTable_ShouldCreateTempTable()
        {
            //Arrange
            var tableName = "#TempTable";
            var connection = sqlConnection;
            //Act
            connection.Open();
            connection.CreateTemporaryTable<DailyCustomerPayments>(tableName);
            var result = connection.Execute($@"INSERT INTO {tableName} (PinCode, InvoiceNumber, MobileNumber, RemainingDays, PayableAmount, DelayStatus, SendDate) 
                VALUES ('q1w2e3r', '123qwe', '0703626119', 1, 5, 0, GETDATE())");
            connection.Close();
            //Assert
            Assert.Equal(result, 1);
        }
    }
}
