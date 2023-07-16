using Dapper;
using LaRoy.ORM.BulkOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LaRoy.ORM.Tests.BulkOperationsTests
{
    public class BulkUpdateTests : TestBase
    {

        [Theory]
        [InlineData(1)]
        [InlineData(69)]
        [InlineData(420)]
        public void BulkUpdate_WithSqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount);
            var connection = _sqlConnection;
            connection.BulkInsert(data);
            foreach (var item in data)
            {
                item.MobileNumber = "0703626119";
                item.DelayStatus = false;
            }
            //Act
            connection.BulkUpdate(data);
            var checkCount = connection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments WHERE MobileNumber = '0703626119' AND DelayStatus = 0");
            connection.Execute("TRUNCATE TABLE DailyCustomerPayments");
            //Assert
            Assert.Equal(data.Count(), (int)checkCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(69)]
        [InlineData(420)]
        public void BulkUpdate_WithNpgSqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount);
            var connection = _npgSqlConnection;
            connection.BulkInsert(data);
            foreach (var item in data)
            {
                item.MobileNumber = "0703626119";
                item.DelayStatus = false;
            }
            //Act
            connection.BulkUpdate(data);
            var checkCount = connection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments WHERE MobileNumber = '0703626119' AND DelayStatus = false");
            connection.Execute("TRUNCATE TABLE DailyCustomerPayments");
            //Assert
            Assert.Equal(data.Count(), (Int64)checkCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(69)]
        [InlineData(420)]
        public void BulkUpdate_WithMySqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount);
            var connection = _mySqlConnection;
            connection.BulkInsert(data);
            foreach (var item in data)
            {
                item.MobileNumber = "0703626119";
                item.DelayStatus = false;
            }
            //Act
            connection.BulkUpdate(data);
            var checkCount = connection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments WHERE MobileNumber = '0703626119' AND DelayStatus = 0");
            connection.Execute("TRUNCATE TABLE DailyCustomerPayments");
            //Assert
            Assert.Equal(data.Count(), (Int64)checkCount);
        }
    }
}
