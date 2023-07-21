using Dapper;
using LaRoy.ORM.BulkOperations;
using LaRoy.ORM.Tests.DTO;
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
        private readonly string _truncateQuery = "TRUNCATE TABLE DailyCustomerPayments";

        [Theory]
        [InlineData(1)]
        [InlineData(69)]
        [InlineData(420)]
        public void BulkUpdate_WithSqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount)!;
            _sqlConnection.BulkInsert(data);

            foreach (var item in data)
            {
                item.MobileNumber = "0703626119";
                item.DelayStatus = false;
            }

            //Act
            _sqlConnection.BulkUpdate(data);
            var checkCount = (int)_sqlConnection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments WHERE MobileNumber = '0703626119' AND DelayStatus = 0");
            _sqlConnection.Execute(_truncateQuery);

            //Assert
            Assert.Equal(data.Count(), checkCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(69)]
        [InlineData(420)]
        public void BulkUpdate_WithNpgSqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount)!;
            _npgSqlConnection.BulkInsert(data);

            foreach (var item in data)
            {
                item.MobileNumber = "0703626119";
                item.DelayStatus = false;
            }

            //Act
            _npgSqlConnection.BulkUpdate(data);
            var checkCount = (long)_npgSqlConnection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments WHERE MobileNumber = '0703626119' AND DelayStatus = false");
            _npgSqlConnection.Execute(_truncateQuery);

            //Assert
            Assert.Equal(data.Count(), checkCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(69)]
        [InlineData(420)]
        public void BulkUpdate_WithMySqlConnection_ShouldPerformCorrectOperation_AndReturnInsertedRowCount(int dataCount)
        {
            //Arrange
            var data = GenerateTestData(dataCount)!;
            _mySqlConnection.BulkInsert(data);

            foreach (var item in data)
            {
                item.MobileNumber = "0703626119";
                item.DelayStatus = false;
            }

            //Act
            _mySqlConnection.BulkUpdate(data);
            var checkCount = (long)_mySqlConnection.ExecuteScalar("SELECT COUNT(*) FROM DailyCustomerPayments WHERE MobileNumber = '0703626119' AND DelayStatus = 0");
            _mySqlConnection.Execute(_truncateQuery);

            //Assert
            Assert.Equal(data.Count(), checkCount);
        }
    }
}
