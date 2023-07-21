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
            Assert.Equal(1, result);
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
